using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(int page=1)
        {

            HomePageViewModel homePageViewModel = new HomePageViewModel()
            {
                Product = await _db.Product.Include(m => m.Category).ToListAsync(),
                Category = await _db.Category.ToListAsync()
            };

            homePageViewModel.Pager = new Pager(homePageViewModel.Product.Count(), page);

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {

                List<ShoppingCart> lstShoppingCart = await _db.ShoppingCart.Where(u => u.CustomerId == claim.Value).ToListAsync();

                int count = 0;

                foreach (var cartItem in lstShoppingCart)
                {
                    count += cartItem.Quantity;
                }

                HttpContext.Session.SetInt32("CartCount", count);
            }

            ViewData["Action"] = "Index";
            
            if (page == 0)
            {
                return RedirectToAction("Index", "OrderHistory");
            }

            return View(homePageViewModel);
        }

        public async Task<IActionResult> Category(string id, int page = 1)
        {

            HomePageViewModel homePageViewModel = new HomePageViewModel()
            {
                Product = await _db.Product.Where(j => j.Category.Name == id).Include(m => m.Category).ToListAsync(),
                Category = await _db.Category.ToListAsync()
            };

            homePageViewModel.Pager = new Pager(homePageViewModel.Product.Count(), page);

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {

                List<ShoppingCart> lstShoppingCart = await _db.ShoppingCart.Where(u => u.CustomerId == claim.Value).ToListAsync();

                int count = 0;

                foreach (var cartItem in lstShoppingCart)
                {
                    count += cartItem.Quantity;
                }

                HttpContext.Session.SetInt32("CartCount", count);
            }

            ViewData["Action"] = "Category";
            ViewData["id"] = id;

            return View("Index", homePageViewModel);
        }

        public async Task<IActionResult> SearchResult(string SearchString, int page)
        {
            if (SearchString == null)
            {
                return RedirectToAction("Index");
            }

            HomePageViewModel homePageViewModel = new HomePageViewModel()
            {
                Product = await _db.Product.Where(j => j.Name.Contains(SearchString) || j.Description.Contains(SearchString)).Include(m => m.Category).ToListAsync(),
                Category = await _db.Category.ToListAsync()
            };

            homePageViewModel.Pager = new Pager(homePageViewModel.Product.Count(), page);

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {

                List<ShoppingCart> lstShoppingCart = await _db.ShoppingCart.Where(u => u.CustomerId == claim.Value).ToListAsync();

                int count = 0;

                foreach (var cartItem in lstShoppingCart)
                {
                    count += cartItem.Quantity;
                }

                HttpContext.Session.SetInt32("CartCount", count);
            }

            ViewData["Action"] = "CurrentSearch";
            ViewData["id"] = SearchString;
            ViewData["SearchResult"] = homePageViewModel.Product.Count() + " product(s) related to \"" + SearchString + "\"";

            return View("Index", homePageViewModel);
        }

        public IActionResult CurrentSearch(string id, int page)
        {
            string currentSearch = id;
            int currentPage = page;
            return RedirectToAction("SearchResult", new { SearchString = currentSearch, page = currentPage });
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(int id, int page, string currentaction, string actiondata)
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

            int currentPage = page;

            switch (currentaction)
            {
                case "Index":
                    return RedirectToAction("Index", new { page = currentPage });
                case "CurrentSearch":
                    return RedirectToAction("SearchResult", new { SearchString = actiondata, page = currentPage });
                case "Category":
                    return RedirectToAction("Category", new { id = actiondata, page = currentPage });
                default:
                    return RedirectToAction("Index", new { page = currentPage });
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
