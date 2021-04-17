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

        public async Task<IActionResult> Index(int page = 1)
        {

            HomePageViewModel homePageViewModel = new HomePageViewModel()
            {
                Product = await _db.Product.Include(m => m.Category).ToListAsync(),
                Category = await _db.Category.ToListAsync()
            };

            homePageViewModel.Pager = new Pager(homePageViewModel.Product.Count(), page);

            ViewData["Action"] = "Index";

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
                int count = shoppingCartCount(_db, claim.Value);
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
                Product = await _db.Product.Where(j => j.Name.Contains(SearchString) || j.Description.Contains(SearchString) || j.Category.Name.Contains(SearchString)).Include(m => m.Category).ToListAsync(),
                Category = await _db.Category.ToListAsync()
            };

            homePageViewModel.Pager = new Pager(homePageViewModel.Product.Count(), page);

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
        public IActionResult AddToCart(int id)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var productFromDb = _db.Product.Where(m => m.Id == id).FirstOrDefault();

            ShoppingCart shoppingCart = new ShoppingCart
            {
                CustomerId = claim.Value,
                Product = productFromDb,
                ProductId = productFromDb.Id,
                Quantity = 1
            };

            ShoppingCart cartFromDb = _db.ShoppingCart.Where(c => c.CustomerId == shoppingCart.CustomerId
                                               && c.ProductId == shoppingCart.ProductId).FirstOrDefault();

            int productCount = 0;
            if (cartFromDb == null)
            {
                _db.ShoppingCart.Add(shoppingCart);
                productCount = 1;
            }
            else
            {
                cartFromDb.Quantity++;
                productCount = cartFromDb.Quantity;
            }
            _db.SaveChanges();

            int count = shoppingCartCount(_db, claim.Value);
            HttpContext.Session.SetInt32("CartCount", count);
            string totalprice = TotalPrice(_db, claim.Value);


            return Json(new { count, productCount, totalprice });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public static int shoppingCartCount(ApplicationDbContext db, string CustomerId)
        {
            List<ShoppingCart> lstShoppingCart = db.ShoppingCart.Where(u => u.CustomerId == CustomerId).ToList();

            int count = 0;

            foreach (var cartItem in lstShoppingCart)
            {
                count += cartItem.Quantity;
            }
            return count;
        }

        public static string TotalPrice(ApplicationDbContext db, string CustomerId)
        {
            List<ShoppingCart> shoppingCartItems = db.ShoppingCart.Where(u => u.CustomerId == CustomerId).Include(e => e.Product).ToList();
            double total = 0;
            foreach (ShoppingCart shoppingCart in shoppingCartItems)
            {
                total += shoppingCart.Product.Price * shoppingCart.Quantity;
            }
            string totalprice = "$" + string.Format("{0:f}", total);
            return totalprice;
        }
    }
}
