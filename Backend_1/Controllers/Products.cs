using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private static List<Products> products = new List<Products>
        {
                new Products
                {
                    Id = 1,
                    Name = "Kakor",
                    Description = "20St I En låda",
                    Price = 60,
                    
                },
                new Products
                {
                    Id = 2,
                    Name = "Cola",
                    Description = "1 Liter",
                    Price = 20,
                   
                }

         };
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Products>>> Get()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Products>>> Get(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return BadRequest("Product not found.");
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<List<Users>>> AddProduct(Products product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(await _context.Products.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Products>>> UpdateProduct(Products request)
        {
            var Product = await _context.Products.FindAsync(request.Id);
            if (Product == null)
                return BadRequest("Product not found.");

            Product.Name = request.Name;
            Product.Description = request.Description;
            Product.Price = request.Price;
            Product.InStock = request.InStock;


            await _context.SaveChangesAsync();

            return Ok(await _context.Products.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Products>>> Delete(int id)
        {
            var Product = await _context.Products.FindAsync(id);
            if (Product == null)
                return BadRequest("Product not found.");

            _context.Products.Remove(Product);
            await _context.SaveChangesAsync();
            return Ok(await _context.Products.ToListAsync());
        }
    }
    
}
