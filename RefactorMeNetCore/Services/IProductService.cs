using RefactorMeNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorMeNetCore.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsByNameAsync(string name);
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Guid id, Product product);
        Task<Product> DeleteAsync(Guid productId);
        Task<List<ProductOption>> GetAllOptionsByProductIdAsync(Guid id);
        Task<ProductOption> GetOptionByProductIdAndOptionIdAsync(Guid id, Guid optionId);
        Task<ProductOption> CreatOptionByProductIdAsync(Guid id, ProductOption option);
        Task<ProductOption> UpdateOptionByProductIdAndOptionIdAsync(Guid id, Guid optionId, ProductOption option);
        Task<ProductOption> DeleteOptionByProductIdAndOptionIdAsync(Guid id, Guid optionId);
    }
}
