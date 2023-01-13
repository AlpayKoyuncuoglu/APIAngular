using APIANG.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIANG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //private readonly SocialContext _context;
        protected readonly AppDbContext _context;
        //private static readonly string[] Products =
        //{
        //    "samsung s6",
        //    "samsung s7",
        //    "samsung s8"
        //};

        private static List<Product> _products;

        public ProductsController(AppDbContext context)
        {
            _context = context;
            //_products = new List<Product>();
            //_products.Add(new Product() { ProductId = 1, Name = "Samsung s6", Price = 3000, IsActive = false });
            //_products.Add(new Product() { ProductId = 2, Name = "Samsung s7", Price = 4000, IsActive = false });
            //_products.Add(new Product() { ProductId = 3, Name = "Samsung s8", Price = 5000, IsActive = false });
            //_products.Add(new Product() { ProductId = 4, Name = "Samsung s9", Price = 6000, IsActive = false });
            //_products.Add(new Product() { ProductId = 5, Name = "Samsung s10", Price = 7000, IsActive = false });
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var produtcs = await _context.Products.ToListAsync();
            return Ok(produtcs);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        //public string GetProduct(int id)
        {
            //return Products[id];
            var p = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            if (p == null)
            {
                return NotFound();
            }
            return Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product entity)
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = entity.ProductId }, entity);
        }

        //[HttpPost]
        //public IActionResult CreateProduct(Product p)
        //{
        //    _products.Add(p);
        //    foreach (var item in _products)
        //    {
        //        System.Console.WriteLine(item.Name);
        //    }
        //    return Ok(p);
        //}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product entity)
        {
            if (id != entity.ProductId)
            {
                return BadRequest();
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = entity.Name;
            product.Price = entity.Price;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product==null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
