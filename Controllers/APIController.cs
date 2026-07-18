// Ensure this namespace matches where your Product entity lives
using DotNetMVCEF.Models;
using DotNetMVCEF.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetMVCEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public APIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. GET: api/api
        // Retrieves all products from your database
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        // 2. POST: api/api
        // Adds a new product to your database
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product newProduct)
        {
            if (newProduct == null)
            {
                return BadRequest("Product data is null.");
            }

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Product added successfully!", data = newProduct });
        }
    }
    }
