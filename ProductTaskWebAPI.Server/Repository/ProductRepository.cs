using Microsoft.EntityFrameworkCore;
using ProductTaskWebAPI.Server.Data;
using ProductTaskWebAPI.Server.Models;

namespace ProductTaskWebAPI.Server.Repository
{
    public class ProductRepository
    {
        private readonly AppDbContext _appDbcontext;

        public ProductRepository(AppDbContext appDbcontext)
        {
            _appDbcontext = appDbcontext;
        }
        public async Task<List<Product>> GetProducts()
        {
            return await _appDbcontext.Products.ToListAsync();
        }
        public async Task SaveProduct(Product product)
        {
            await _appDbcontext.Products.AddAsync(product);
            await _appDbcontext.SaveChangesAsync();
        }
        public async Task DeleteProduct(int id)
        {
            var product = await _appDbcontext.Products.FindAsync(id);
            if (product != null)
            {
                _appDbcontext.Products.Remove(product);
                await _appDbcontext.SaveChangesAsync();
            }
        }
        public async Task UpdateProduct(Product product)
        {
            var existingProduct = await _appDbcontext.Products.FindAsync(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                existingProduct.Amount = product.Amount;
                _appDbcontext.Products.Update(existingProduct);
                await _appDbcontext.SaveChangesAsync();

            }

        }
        public async Task<Product> GetProductById(int id)
        {
            return await _appDbcontext.Products.FindAsync(id);
        }
    }
}
