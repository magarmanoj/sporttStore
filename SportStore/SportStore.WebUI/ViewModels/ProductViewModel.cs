using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportStore.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SportStore.WebUI.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Naam is verplicht")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Beschrijving is verplicht")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Prijs is verplicht")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Categorie is verplicht")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ValidateNever]
        public string CategoryName { get; set; } 

        public IFormFile Image { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }

        [ValidateNever]
        public List<Categories> CategoriesList { get; set; }
    }
}
