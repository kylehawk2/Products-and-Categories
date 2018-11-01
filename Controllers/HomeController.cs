using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products_and_Catagories.Models;
using Microsoft.AspNetCore.Http;

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
        [Route("/Product/{productId}")]
        public IActionResult ShowProduct(int productId)
        {
            Product this_product = dbContext.Products.Include(a => a.Associations).ThenInclude(c => c.Catagories).FirstOrDefault(p => p.ProductId == productId);
            if(this_product == null)
                return RedirectToAction("Index");
            
            IEnumerable<Catagory> UsedCatagories = this_product.Associations.Select(a => a.Catagories);
            // 
            IEnumerable<Catagory> UnusedCatagories = dbContext.Catagories
            // Including the Catagory Associations
                .Include(a => a.Associations)
                // Only include catagories whose associations have no productId that matches 'x' or productId for the view, wont be included in the result
                .Where(c => c.Associations.All(a => a.ProductId != productId)); 
            ViewBag.this_product = this_product;
            ViewBag.UsedCatagories = UsedCatagories;
            ViewBag.UnusedCatagories = UnusedCatagories;

            return View("ShowProduct");
        }
        [HttpPost]
        [Route("/")]
        public IActionResult CreateProduct(Product product)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(product);
                dbContext.SaveChanges();
                return RedirectToAction("CreateProduct");
            }
            return View("Index");
        }
        [HttpPost]
        [Route("/Products/{productId}")]
        
        public IActionResult UpdateProduct(int productId, int catagoryId)
        {
            Product this_product = dbContext.Products.Include(a => a.Associations).ThenInclude(p => p.Products).FirstOrDefault(c => c.ProductId == productId);
            Boolean tf = this_product.Associations.Any(a => a.CatagoryId == catagoryId);
            if(!tf)
            {
                Association connection = new Association()
                {
                    CatagoryId = catagoryId,
                    ProductId = productId
                };
                this_product.Associations.Add(connection);
                dbContext.SaveChanges();
                return RedirectToAction("ShowProduct", new {productId = productId});
            }
            return View("Index", new {productId = productId});
        }
        [HttpGet]
        [Route("/Catagory")]
        public IActionResult Catagory()
        {
            List<Catagory> AllCatagories = dbContext.Catagories.ToList();
            ViewBag.allcatagories = AllCatagories;
            return View();
        }
        [HttpGet]
        [Route("/Catagory/{catagoryId}")]
        public IActionResult ShowCatagory(int catagoryId)
        {
            Catagory this_catagory = dbContext.Catagories.Include(a => a.Associations).ThenInclude(c => c.Products).FirstOrDefault(p => p.CatagoryId == catagoryId);
            if(this_catagory == null)
                return RedirectToAction("Index");
            
            IEnumerable<Product> UsedProducts = this_catagory.Associations.Select(a => a.Products);
            IEnumerable<Product> UnusedProducts = dbContext.Products.Include(a => a.Associations).Where(b => b.Associations.All(c => c.CatagoryId != catagoryId));
            ViewBag.this_catagory = this_catagory;
            ViewBag.UsedProducts = UsedProducts;
            ViewBag.UnusedProducts = UnusedProducts;
            return View("ShowCatagory");
        }
        [HttpPost]
        [Route("/Catagory")]
        public IActionResult CreateCatagory(Catagory catagory)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(catagory);
                dbContext.SaveChanges();
                return RedirectToAction("CreateCatagory");
            }
            return View("Catagory");
        }
        [HttpPost]
        [Route("/Catagory/{catagoryId}")]
        public IActionResult UpdateCatagory(int catagoryId, int productId)
        {
            Catagory this_catagory = dbContext.Catagories.Include(a => a.Associations).FirstOrDefault(c => c.CatagoryId == catagoryId);
            Boolean tf = this_catagory.Associations.Any(a => a.ProductId == productId);
            if(!tf)
            {
                Association connection = new Association()
                {
                    CatagoryId = catagoryId,
                    ProductId = productId
                };
                this_catagory.Associations.Add(connection);
                dbContext.SaveChanges();
                return RedirectToAction("Catagory", new {catagoryId = catagoryId});
            }
            return View("Catagory", new {catagoryId = catagoryId});
        }
    }
}
