using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SA52T03_SWStore.Data;
using SA52T03_SWStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace SA52T03_SWStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        public string cusID { get; set; }
        public CartController (ApplicationDbContext context)
        {
            _context = context;
        }
        // this is for view model for cart
        public IActionResult Index()
        {
            return View(AllCartItems());
        }
        // GET: Product Details and add to cart
        public IActionResult AddtoCart(int id)
        {
            cusID = getcusID();
            //check if any id exist in the shopping cart
            var cart = _context.ShoppingCart.SingleOrDefault(c => c.Id == id && c.CustomerID == cusID);

            //check if cart is null or not
            if (cart == null)
            {
                cart = new Cart
                {
                    qty = 1,
                    Id = id
                };
                //save the cart in the database
                _context.ShoppingCart.Add(cart);
            }
            else
            // item is in cart and the person want to buy more
            {
                cart.qty++;
            }
            // if add more save the changes again
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public void RemoveFromCart(int id)
        {
            cusID = getcusID();
            //retrieve the current shopping session
            var cartitem = _context.ShoppingCart.Single(c => c.Id == id && c.CustomerID == cusID);
            

            if (cartitem != null)
            {
                if (cartitem.qty >1)
                {
                    cartitem.qty--;
                }
                else
                {
                    _context.ShoppingCart.Remove(cartitem);
                }
            }
            _context.SaveChanges();
        }

        public string getcusID()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            cusID = claim.Value;
            return cusID;
        }

        public List<Cart> AllCartItems()
        {
            return _context.ShoppingCart.Where(c => c.CustomerID == cusID).ToList();
        }



                
        }
    }

