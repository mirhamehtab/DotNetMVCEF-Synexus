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
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        // 2. POST: api/api
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

        // 3. GET: api/api/products?page=1&pageSize=10&search=&sortBy=name&sortDir=asc&categoryId=
        [HttpGet("products")]
        public async Task<IActionResult> GetProductsPaged(
            int page = 1,
            int pageSize = 10,
            string? search = null,
            string? sortBy = "Name",
            string? sortDir = "asc",
            int? categoryId = null)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(p => p.Name.Contains(search));

            query = (sortBy?.ToLower(), sortDir?.ToLower()) switch
            {
                ("price", "desc") => query.OrderByDescending(p => p.Price),
                ("price", _) => query.OrderBy(p => p.Price),
                ("name", "desc") => query.OrderByDescending(p => p.Name),
                _ => query.OrderBy(p => p.Name)
            };

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new PagedResult<Product>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            });
        }
    }
}