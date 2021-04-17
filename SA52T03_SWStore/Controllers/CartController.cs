using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SA52T03_SWStore.Data;
using SA52T03_SWStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SA52T03_SWStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<ShoppingCart> shoppingCartItems = await _context.ShoppingCart.Where(u => u.CustomerId == userId)
                .Include(e => e.Product).ToListAsync();

            return View(shoppingCartItems);
        }

        public async Task<IActionResult> CheckOut()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            Order order = new Order()
            {
                OrderDate = DateTime.Now,
                CustomerId = userId,
                OrderDetail = new List<OrderDetail>()
            };

            List<ShoppingCart> shoppingCartItems = await _context.ShoppingCart.Where(j => j.CustomerId == userId).ToListAsync();

            foreach (ShoppingCart shoppingCartItem in shoppingCartItems)
            {
                order.OrderDetail.Add(new OrderDetail { ProductId = shoppingCartItem.ProductId, Quantity = shoppingCartItem.Quantity });
                _context.ShoppingCart.Remove(shoppingCartItem);
            }

            _context.Add(order);

            await _context.SaveChangesAsync();

            foreach (OrderDetail orderDetail in order.OrderDetail)
            {
                for (int i = 0; i < orderDetail.Quantity; i++)
                {
                    string aCChain = AcChain(orderDetail, i);
                    ACode aCode = new ACode { OrderDetailId = orderDetail.Id, ACChain = aCChain, OrderDetail = orderDetail };
                    orderDetail.ACode.Add(aCode);
                    _context.Add(aCode);
                }
            }
            await _context.SaveChangesAsync();

            HttpContext.Session.SetInt32("CartCount", 0);

            return RedirectToAction("Index", "OrderHistory");
        }

        public IActionResult Deduce(int id)
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            ShoppingCart shoppingCartItem = _context.ShoppingCart.Where(j => j.CustomerId == userId && j.ProductId == id).FirstOrDefault();

            int productCount = 0;

            if (shoppingCartItem.Quantity == 1)
            {
                _context.Remove(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Quantity--;
                productCount = shoppingCartItem.Quantity;
            }

            _context.SaveChanges();

            int count = HomeController.shoppingCartCount(_context, userId);
            HttpContext.Session.SetInt32("CartCount", count);

            return Json(new { message = "Add Success", count, productCount });
        }

        public static string AcChain(OrderDetail od, int i)
        {
            double n = od.OrderId + od.ProductId + od.Quantity + i;
            double ac1 = od.OrderId / n;
            double ac2 = od.ProductId / n;
            double ac3 = od.Quantity / n;
            for (int j = 0; j < od.OrderId; j++)
            {
                ac1 = 3.99 * ac1 * (1 - ac1);
                ac3 = 3.99 * ac3 * (1 - ac3);
            }
            for (int j = 0; j < od.ProductId; j++)
            {
                ac1 = 3.98 * ac1 * (1 - ac1);
                ac2 = 3.98 * ac2 * (1 - ac2);
            }
            for (int j = 0; j < od.Quantity; j++)
            {
                ac2 = 3.97 * ac2 * (1 - ac2);
                ac3 = 3.97 * ac3 * (1 - ac3);
            }
            for (int j = 0; j < i; j++)
            {
                ac1 = 3.96 * ac1 * (1 - ac1);
                ac2 = 3.96 * ac2 * (1 - ac2);
                ac3 = 3.96 * ac3 * (1 - ac3);
            }
            string s = ac1.ToString("0.000000").Substring(3, 5) + "-" + ac2.ToString("0.000000").Substring(3, 5) + "-" + ac3.ToString("0.000000").Substring(3, 5);
            return s;
        }
    }
}
