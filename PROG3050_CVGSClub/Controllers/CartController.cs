using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PROG3050_CVGSClub.Models;
using PROG3050_CVGSClub.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using PROG3050_CVGSClub.Helpers;

namespace PROG3050_CVGSClub.Controllers
{
    public class CartController : Controller
    {
        private readonly CvgsClubContext _context;
        private readonly CartDependency _cartDependency = new CartDependency();

        public CartController(CvgsClubContext context)
        {
            _context = context;
        }

        // GET: Cart
        [Authorize]
        public IActionResult Index()
        {
            string memberId = HttpContext.Session.GetString("userId");
            string url = "/Identity/Account/Login";
            if (memberId == null)
            {
                return LocalRedirect(url);
            }
            else
            {
                var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                double taxRate = 0.13;

                if (cart != null)
                {
                    ViewBag.cart = cart;
                    ViewBag.total = _cartDependency.TotalSum(cart);
                    ViewBag.tax = _cartDependency.TaxAmount(ViewBag.total, taxRate);
                    ViewBag.final = _cartDependency.FinalCost(ViewBag.total, ViewBag.tax);
                }

                return View();
            }
        }
        
        // Checkout/buy items in cart 
        public IActionResult Buy(int id)
        {
            var games = _context.Games.FirstOrDefault(m => m.GameId == id);
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

            if (cart == null)
            {
                cart = _cartDependency.AddToCart(games, null);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                cart = _cartDependency.BuyGame(id, cart, games);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            return RedirectToAction("Index");
        }

        //Remove game from cart
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            cart = _cartDependency.RemoveGame(id, cart);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }


        //Checkout method that adds all items in the cart to the GameLibrary Table
        public async Task<IActionResult> CheckOut()
        {
            var memberId = HttpContext.Session.GetString("userId");
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

            if (cart != null)
            {
                ViewBag.cart = cart;
                ViewBag.total = _cartDependency.TotalSum(cart);
                ViewBag.tax = _cartDependency.TaxAmount(ViewBag.total);
                ViewBag.final = _cartDependency.FinalCost(ViewBag.total, ViewBag.tax);

                // Do we really need to count the cart? There should only be one right?
                for (int i = 0; i < cart.Count; i++)
                {
                    GameLibrariesController gamesLibraryController = new GameLibrariesController(_context);
                    await gamesLibraryController.Create(_cartDependency.GameLibrary(cart, memberId, i));
                }
            }

            return View();
        }
    }
}
