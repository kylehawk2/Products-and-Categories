using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products_and_Catagories.Models;

namespace Products_and_Catagories.Controllers
{
    public class HomeController : Controller
    {
        private ProductCatagoryContext dbContext;
        public HomeController (ProductCatagoryContext context)
        {
            dbContext = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<Product> AllProducts = dbContext.Products.ToList();
            ViewBag.allproducts = AllProducts;
            return View();
        }
        [HttpGet]
        [Route("/Catagory")]
        public IActionResult Catagory()
        {
            List<Catagory> AllCatagories = dbContext.Catagories.ToList();
            ViewBag.allcatagories = AllCatagories;
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
