using Microsoft.AspNetCore.Mvc;
using ProductTaskWebAPI.Server.Models;
using ProductTaskWebAPI.Server.Repository;

namespace ProductTaskWebAPI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }
        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            await _productRepository.SaveProduct(product);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound(new { Message = "Product not found!" });
            }


            await _productRepository.DeleteProduct(id);
            return Ok(new { Message = "Product deleted succesfully." });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest(new { Message = "Product Id mismatch" });
            }

            var existingProduct = await _productRepository.GetProductById(product.Id);
            if (existingProduct == null)
            {
                return NotFound(new { Message = "Product not found!" });
            }
            await _productRepository.UpdateProduct(product);
            return Ok(new { Message = "Product updated succesfully." });
        }
    }
}
