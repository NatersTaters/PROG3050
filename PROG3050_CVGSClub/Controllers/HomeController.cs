using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROG3050_CVGSClub.Models;

namespace PROG3050_CVGSClub.Controllers
{
	public class HomeController : Controller
	{
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
				return View();
			}
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
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
