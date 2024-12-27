using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SportStore.Application.Common.Interfaces;
using SportStore.Domain.Entities;
using SportStore.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Infrastructure.Repositories
{
    public class ProductRepo : IProductRepo
    {

        private AppDbContext _appDbContext;
        public ProductRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Toevoegen(Products product)
        {
            _appDbContext.Products.Add(product);
            _appDbContext.SaveChanges();
        }

        public void UpdateProduct(Products product)
        {
            _appDbContext.Products.Update(product);
            _appDbContext.SaveChanges();
        }
        public Products FindByCategoryId(int categoryId)
        {
            return _appDbContext.Products.FirstOrDefault(p => p.CategoryId == categoryId);
        }

        public Products FindById(int id)
        {
            return _appDbContext.Products.FirstOrDefault(p => p.Id == id);
        }
        public Products FindByIdWithCategories(int id)
        {
            return _appDbContext.Products.Include(p => p.Categories).FirstOrDefault(p => p.Id == id);
        }

        public void DeleteProduct(int id)
        {
            Products product = _appDbContext.Products.Find(id);
            if (product != null)
            {
                _appDbContext.Products.Remove(product);
                _appDbContext.SaveChanges();
            }
        }

    }
}
