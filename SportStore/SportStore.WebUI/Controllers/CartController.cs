using Microsoft.AspNetCore.Mvc;
using SportStore.Application.Common.Interfaces;
using SportStore.Domain.Entities;
using System.Text.Json;

namespace SportStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepo _productRepository; 

        public CartController(IProductRepo productRepository)
        {
            _productRepository = productRepository;
        }
        public IActionResult AddToCart(int productId, int aantal)
        {
            string cartJson = HttpContext.Session.GetString("cart");
            ShoppingCart shoppingCart = cartJson != null ? JsonSerializer.Deserialize<ShoppingCart>(cartJson) : new ShoppingCart();

            // Retrieve product from repository
            Products product = _productRepository.FindById(productId);

            if (product != null)
            {
                shoppingCart.AddItem(productId, aantal);

                // Save updated cart back to session
                string updatedCartJson = JsonSerializer.Serialize(shoppingCart);
                HttpContext.Session.SetString("cart", updatedCartJson);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
