using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkAndGo.Data.Models
{
    public class ShoppingCart
    {
        private readonly AppDBContext _appDBContext;

        private ShoppingCart(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public string ShoppingCartId { get; set; }

        public List <ShoppingCartItem> ShoppingCartItems { get; set; }



        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<AppDBContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context)
            {
                ShoppingCartId = cartId
            };


        }


        public void AddtoCart(Drink drink, int amount)
        {     
            //check whether if there is a particular drink in that particular   cart
            var shoppingCartItem = _appDBContext.ShoppingCartItems.SingleOrDefault(s => s.Drink.DrinkID == drink.DrinkID && s.ShoppingCartId == ShoppingCartId);

            if(shoppingCartItem==null)  //No such drink in that cart
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Drink = drink,
                    Amount = 1
                };

                _appDBContext.ShoppingCartItems.Add(shoppingCartItem);
            }

            else // there is a drink in that type in the cart. so if you add another of same type, you just increment the amount.
            {
                shoppingCartItem.Amount++;
            }

            _appDBContext.SaveChanges();


        }


        public int RemoveFromCart(Drink drink)
        {
             //check whether if there is a particular drink in that particular   cart

            var shoppingCartItem =
                    _appDBContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Drink.DrinkID == drink.DrinkID && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _appDBContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _appDBContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                   (ShoppingCartItems =
                       _appDBContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Drink)
                           .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _appDBContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _appDBContext.ShoppingCartItems.RemoveRange(cartItems);

            _appDBContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _appDBContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Drink.Price * c.Amount).Sum();
            return total;
        }

    }
}
