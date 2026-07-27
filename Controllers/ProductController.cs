using DotNetMVCEF.Models;
using DotNetMVCEF.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DotNetMVCEF.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext applicationDbContext) //dependency injection
        {
            _context = applicationDbContext;
        }
        // GET: ProductController
        /*  public ActionResult Index() //to view added products
          {
              var products = _context.Products.ToList(); //view in form of list
              return View(products);
          } */


        // GET: ProductController
        public ActionResult Index()
        {
            return View(); // ab data JS se API se aata hai, controller se nahi
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create() //to create a form
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddProduct addProduct)
        {
            try
            {
                Product product = new Product()
                {
                    Name = addProduct.Name,
                    Description = addProduct.Description,
                    Price = addProduct.Price
                };
                _context.Products.Add(product); //product will be saved in Product table
                _context.SaveChanges(); // will change changes in database
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id); //linq query
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            try
            {
                var dbProduct = _context.Products.SingleOrDefault(p => p.Id == product.Id); //linq query
                dbProduct.Name = product.Name;
                dbProduct.Description = product.Description;
                dbProduct.Price = product.Price;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Product product)
        {
            try
            {
                var dbProduct = _context.Products.SingleOrDefault(p => p.Id == product.Id); //linq query
                _context.Products.Remove(dbProduct);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
