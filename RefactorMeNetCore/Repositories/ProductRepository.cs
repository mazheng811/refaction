using Microsoft.EntityFrameworkCore;
using RefactorMeNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorMeNetCore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataBaseContext _dataBaseContext;
        public ProductRepository(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            product.Id = Guid.NewGuid();

            await _dataBaseContext.Product.AddAsync(product);

            await _dataBaseContext.SaveChangesAsync();

            return product;
        }

        public async Task<Product> DeleteAsync(Guid productId)
        {
            var product = await GetProductByIdAsync(productId);

            _dataBaseContext.Entry(product).State = EntityState.Deleted;
            await _dataBaseContext.SaveChangesAsync();

            return product;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = await _dataBaseContext.Product.Where(p => true).ToListAsync();

            return products;
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _dataBaseContext.Product.FirstAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetProductsByNameAsync(string name)
        {
            var products = await _dataBaseContext.Product.Where(p => p.Name.ToLower().Contains(name)).ToListAsync();

            return products;
        }

        public async Task<bool> IsProductExistedByIdAsync(Guid id)
        {
            return await _dataBaseContext.Product.AnyAsync(p => p.Id == id);
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var oldProduct = await GetProductByIdAsync(product.Id);

            oldProduct.Name = product.Name;
            oldProduct.Price = product.Price;
            oldProduct.DeliveryPrice = product.DeliveryPrice;
            oldProduct.Description = product.Description;

            await _dataBaseContext.SaveChangesAsync();

            return product;
        }
    }
}
