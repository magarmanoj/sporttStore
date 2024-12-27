using Microsoft.AspNetCore.Mvc;
using SportStore.Domain.Entities;
using SportStore.Infrastructure.Data;

namespace SportStore.WebUI.Controllers
{
    public class CatalogController : Controller
    {
        private AppDbContext _appDbContext;
        public CatalogController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Catalog()
        {
            List<Products> products = _appDbContext.Products.ToList();

            return View("Catalog", products);
        }
    }
}
