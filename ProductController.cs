using DotNetMVCEF.Models;
using DotNetMVCEF.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DotNetMVCEF.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductController(ApplicationDbContext applicationDbContext, IWebHostEnvironment env)
        {
            _context = applicationDbContext;
            _env = env;
        }


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
        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddProduct addProduct)
        {
            try
            {
                string? imagePath = null;

                if (addProduct.ImageFile != null && addProduct.ImageFile.Length > 0)
                {
                    const long maxSizeBytes = 2 * 1024 * 1024;
                    if (addProduct.ImageFile.Length > maxSizeBytes)
                    {
                        ModelState.AddModelError("ImageFile", "Image must be 2MB or smaller.");
                        return View(addProduct);
                    }

                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                    var extension = Path.GetExtension(addProduct.ImageFile.FileName).ToLowerInvariant();

                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("ImageFile", "Only JPG, PNG, or WEBP images are allowed.");
                        return View(addProduct);
                    }

                    var fileName = $"{Guid.NewGuid()}{extension}";
                    var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");

                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await addProduct.ImageFile.CopyToAsync(stream);
                    }

                    imagePath = $"/uploads/{fileName}";
                }

                Product product = new Product()
                {
                    Name = addProduct.Name,
                    Description = addProduct.Description,
                    Price = addProduct.Price,
                    ImagePath = imagePath
                };
                _context.Products.Add(product);
                _context.SaveChanges();
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
