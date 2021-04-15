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
            
            HttpContext.Session.SetInt32("CartCount", 0);

            return RedirectToAction("Index", "OrderHistory");
        }

        public async Task<IActionResult> Add(int id)
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);            

            
            ShoppingCart shoppingCartItem = await _context.ShoppingCart.Where(c => c.CustomerId == userId
                                                && c.ProductId == id).FirstOrDefaultAsync();

            shoppingCartItem.Quantity++;
            
            await _context.SaveChangesAsync();

            List<ShoppingCart> shoppingCartItems = await _context.ShoppingCart.Where(u => u.CustomerId == userId).ToListAsync();

            int count = 0;

            foreach (var cartItem in shoppingCartItems)
            {
                count += cartItem.Quantity;
            }

            HttpContext.Session.SetInt32("CartCount", count);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Deduce(int id)
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            ShoppingCart shoppingCartItem = _context.ShoppingCart.Where(j => j.CustomerId == userId && j.ProductId == id).FirstOrDefault();
            
            if (shoppingCartItem.Quantity == 1)
            {
                _context.Remove(shoppingCartItem);                

            }
            else
            {
                shoppingCartItem.Quantity--;
            }

            await _context.SaveChangesAsync();

            List<ShoppingCart> shoppingCartItems = await _context.ShoppingCart.Where(u => u.CustomerId == userId).ToListAsync();

            int count = 0;

            foreach (var cartItem in shoppingCartItems)
            {
                count += cartItem.Quantity;
            }

            HttpContext.Session.SetInt32("CartCount", count);

            return RedirectToAction("Index");
        }
    }
}
