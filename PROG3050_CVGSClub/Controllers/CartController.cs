using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PROG3050_CVGSClub.Models;
using PROG3050_CVGSClub.Helpers;
using System.Linq;

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

        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            double taxRate = 0.13;

            if (cart != null)
            {
                ViewBag.cart = cart;
                ViewBag.total = _cartDependency.TotalSum(cart); // Can this be put into a testable function?
                ViewBag.tax = _cartDependency.TaxAmount(ViewBag.total, taxRate);
                ViewBag.final = _cartDependency.FinalCost(ViewBag.total, ViewBag.tax);
            }

            return View();
        }

        public IActionResult Buy(int id)
        {
            var games = _context.Games.FirstOrDefault(m => m.GameId == id);
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item 
                {
                    Game = games, 
                    Quantity = 1 
                });

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item
                    {
                        Game = games,
                        Quantity = 1
                    });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        private int isExist(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Game.GameId.Equals(id))
                    return i;
            }

            return -1;
        }

    }
}
