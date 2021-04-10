using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SA52T03_SWStore.Data;
using SA52T03_SWStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SA52T03_SWStore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            HomePageViewModel homePageViewModel = new HomePageViewModel()
            {
                Product = await _db.Product.Include(m => m.Category).ToListAsync(),
                Category = await _db.Category.ToListAsync()
            };

            return View(homePageViewModel);
        }


        public async Task<IActionResult> AddToCart(int id)
        {

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var productFromDb = await _db.Product.Where(m => m.Id == id).FirstOrDefaultAsync();


            ShoppingCart shoppingCart = new ShoppingCart
            {
                CustomerId = claim.Value,
                Product = productFromDb,
                ProductId = productFromDb.Id,
                Quantity = 1
            };

            ShoppingCart cartFromDb = await _db.ShoppingCart.Where(c => c.CustomerId == shoppingCart.CustomerId
                                                && c.ProductId == shoppingCart.ProductId).FirstOrDefaultAsync();

            if (cartFromDb == null)
            {
                await _db.ShoppingCart.AddAsync(shoppingCart);

            }
            else
            {
                cartFromDb.Quantity++;
            }
            await _db.SaveChangesAsync();


            return RedirectToAction("Index");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
