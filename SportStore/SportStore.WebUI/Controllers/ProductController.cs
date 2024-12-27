using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SportStore.Application.Common.Interfaces;
using SportStore.Domain.Entities;
using SportStore.Infrastructure.Data;
using SportStore.WebUI.ViewModels;

namespace SportStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IFileUploadService _fileUploadService;
        private readonly IProductRepo _productRepository;
        private AppDbContext _appDbContext;

        public ProductController(AppDbContext dbContext, IFileUploadService fileUploadService, IProductRepo productRepository)
        {
            _appDbContext = dbContext;
            _fileUploadService = fileUploadService;
            _productRepository = productRepository;
        }
        public IActionResult Product()
        {
            List<Categories> categoriesList = _appDbContext.Categories.ToList();
            ProductViewModel viewModel = new ProductViewModel
            {
                CategoriesList = categoriesList
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProductError(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CategoriesList = _appDbContext.Categories.ToList();
                return View("Product", model);
            }

            if (model.Image != null && model.Image.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photos", fileName);

                _fileUploadService.UploadFile(model.Image.OpenReadStream(), filePath);

                model.ImageUrl = fileName;
            }
            Products product = new Products
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                CategoryId = model.CategoryId,
                ImageURL = model.ImageUrl
            };

            _productRepository.Toevoegen(product);
            _appDbContext.SaveChanges();

            return RedirectToAction("Catalog", "Catalog");

        }
        public IActionResult Details(int id)
        {
            Products product = _productRepository.FindByIdWithCategories(id);

            ProductViewModel viewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageURL,
                CategoryId = product.CategoryId,
                CategoryName = product.Categories.Name,
                CategoriesList = _appDbContext.Categories.ToList()
            };

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            Products product = _productRepository.FindById(id);
            ProductViewModel viewModel = new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                ImageUrl = product.ImageURL,
                CategoriesList = _appDbContext.Categories.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int categoryId, ProductViewModel model)
        {
            Products product = _productRepository.FindByCategoryId(categoryId);
            if (model.Image != null && model.Image.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photos", fileName);

                _fileUploadService.UploadFile(model.Image.OpenReadStream(), filePath);

                product.ImageURL = fileName;
            }

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;

            _productRepository.UpdateProduct(product);
            _appDbContext.SaveChanges();

            return RedirectToAction("Details", "Product", new { id = product.Id });
        }

        public IActionResult Delete(int id)
        {
           _productRepository.DeleteProduct(id);
            return RedirectToAction("Catalog", "Catalog");
        }
    }
}
