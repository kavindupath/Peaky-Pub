using DrinkAndGo.Data.Interfaces;
using DrinkAndGo.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkAndGo.Data.Repositories
{
    public class OrderRepository:IOrderRepository
    {
        private readonly AppDBContext _appDbContext;
        private readonly ShoppingCart _shoppingCart;


        public OrderRepository(AppDBContext appDbContext, ShoppingCart shoppingCart)
        {
            _appDbContext = appDbContext;
            _shoppingCart = shoppingCart;
        }


        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            _appDbContext.Orders.Add(order);

            var shoppingCartItems = _shoppingCart.ShoppingCartItems; //Get all the items in the shopping cart

            foreach (var Item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()    // For each item in the cart, create a order details object and add the below details
                {
                    Amount = Item.Amount,
                    DrinkId = Item.Drink.DrinkID,
                    OrderId = order.OrderId,
                    Price = Item.Drink.Price
                };

                _appDbContext.OrderDetails.Add(orderDetail);  //Add each order detail object to the database
            }

            _appDbContext.SaveChanges();
        }
    }

}
