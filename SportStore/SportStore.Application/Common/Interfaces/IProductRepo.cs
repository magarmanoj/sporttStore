using Microsoft.EntityFrameworkCore.Migrations.Operations;
using SportStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.Common.Interfaces
{
    public interface IProductRepo
    {
        void Toevoegen(Products products); 
        void UpdateProduct(Products products);
        Products FindByCategoryId(int categoryId);
        Products FindById(int id);
        Products FindByIdWithCategories(int id);
        void DeleteProduct(int id);
    }
}
