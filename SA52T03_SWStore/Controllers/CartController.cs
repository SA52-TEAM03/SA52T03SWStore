using Microsoft.AspNetCore.Authorization;
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
            var applicationDbContext = _context.ShoppingCart.Where(j => j.ApplicationUser.Id == userId)
                .Include(e => e.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> CheckOut()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            Order order = new Order()
            {
                OrderDate = DateTime.Now,
                CustomerId = userId,
                OrderDetail = new List<OrderDetail>()
            };

            List<ShoppingCart> shoppingCarts = _context.ShoppingCart.Where(j => j.CustomerId == userId).ToList();

            foreach (ShoppingCart shoppingCart in shoppingCarts)
            {
                order.OrderDetail.Add(new OrderDetail { ProductId = shoppingCart.ProductId, Quantity = shoppingCart.Quantity });
                _context.ShoppingCart.Remove(shoppingCart);
            }

            _context.Add(order);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "OrderHistory");
        }

        public async Task<IActionResult> Add(int id)
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var productFromDb = await _context.Product.Where(m => m.Id == id).FirstOrDefaultAsync();

            ShoppingCart shoppingCart = new ShoppingCart
            {
                CustomerId = userId,
                Product = productFromDb,
                ProductId = productFromDb.Id,
                Quantity = 1
            };

            ShoppingCart cartFromDb = await _context.ShoppingCart.Where(c => c.CustomerId == shoppingCart.CustomerId
                                                && c.ProductId == shoppingCart.ProductId).FirstOrDefaultAsync();

            if (cartFromDb == null)
            {
                await _context.ShoppingCart.AddAsync(shoppingCart);

            }
            else
            {
                cartFromDb.Quantity++;
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Deduce(int id)
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            ShoppingCart cartFromDb = _context.ShoppingCart.Where(j => j.CustomerId == userId && j.ProductId == id).FirstOrDefault();
            if (cartFromDb.Quantity > 1)
            {
                cartFromDb.Quantity--;
            }
            else
            {
                _context.Remove(cartFromDb);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
