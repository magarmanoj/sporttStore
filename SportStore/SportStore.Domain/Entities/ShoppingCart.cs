using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Domain.Entities
{
    public class ShoppingCart
    {
        public List<CartLine> CartLines { get; set; }
        public ShoppingCart()
        {
            // Initialize CartLines to an empty list
            CartLines = new List<CartLine>();
        }

        public void AddItem(int productId, int quantity)
        {
            CartLine cartLine = CartLines.FirstOrDefault(cl => cl.ProductId == productId);

            if (cartLine != null)
            {
                cartLine.Quantity += quantity;
            }
            else
            {
                CartLines.Add(new CartLine { ProductId = productId, Quantity = quantity });
            }
        }
    }
}
