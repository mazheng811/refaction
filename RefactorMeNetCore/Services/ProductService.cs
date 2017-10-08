using RefactorMeNetCore.Exceptions;
using RefactorMeNetCore.Models;
using RefactorMeNetCore.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace RefactorMeNetCore.Services
{
    public class ProductService : IProductService
    {
        private readonly IOptionService _optionService;
        private readonly IProductRepository _productRepository;

        public ProductService(IOptionService optionService, IProductRepository productRepository)
        {
            _optionService = optionService;
            _productRepository = productRepository;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            return await _productRepository.CreateAsync(product);
        }

        public async Task<ProductOption> CreatOptionByProductIdAsync(Guid id, ProductOption option)
        {
            var isProductExsit = await _productRepository.IsProductExistedByIdAsync(id);

            if (!isProductExsit)
            {
                throw new ProductNotFoundException();
            }

            option.Id = Guid.NewGuid();
            option.ProductId = id;

            return await _optionService.CreateOptionAsync(option);
        }

        public async Task<Product> DeleteAsync(Guid productId)
        {
            return await _productRepository.DeleteAsync(productId);
        }

        public async Task<ProductOption> DeleteOptionByProductIdAndOptionIdAsync(Guid id, Guid optionId)
        {
            var isProductExsit = await _productRepository.IsProductExistedByIdAsync(id);

            if (!isProductExsit)
            {
                throw new ProductNotFoundException();
            }

            return await _optionService.DeleteOptionByProductIdAndOptionIdAsync(id, optionId);
        }

        public async Task<List<ProductOption>> GetAllOptionsByProductIdAsync(Guid id)
        {
            var isProductExsit = await _productRepository.IsProductExistedByIdAsync(id);

            if (!isProductExsit)
            {
                throw new ProductNotFoundException();
            }

            return await _optionService.GetAllOptionByProductIdAsync(id);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();

            return products;
        }

        public async Task<ProductOption> GetOptionByProductIdAndOptionIdAsync(Guid id, Guid optionId)
        {
            var isProductExsit = await _productRepository.IsProductExistedByIdAsync(id);

            if (!isProductExsit)
            {
                throw new ProductNotFoundException();
            }

            var isOptionExsited = await _optionService.IsOptionExsitedById(optionId);

            if (!isOptionExsited)
            {
                throw new OptionNotFoundException();
            }

            var option =  await _optionService.GetOptionByProductIdAndOptionIdAsync(id, optionId);

            return option;
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task<List<Product>> GetProductsByNameAsync(string name)
        {
            var products = await _productRepository.GetProductsByNameAsync(name);

            return products;
        }

        public async Task<Product> UpdateAsync(Guid id, Product product)
        {
            var isProductExsit = await _productRepository.IsProductExistedByIdAsync(id);

            if (!isProductExsit)
            {
                throw new ProductNotFoundException();
            }
            
            product.Id = id;

            return await _productRepository.UpdateAsync(product);
        }

        public async Task<ProductOption> UpdateOptionByProductIdAndOptionIdAsync(Guid id, Guid optionId, ProductOption option)
        {
            var isProductExsit = await _productRepository.IsProductExistedByIdAsync(id);

            if (!isProductExsit)
            {
                throw new ProductNotFoundException();
            }

            var isOptionExsited = await _optionService.IsOptionExsitedById(optionId);

            if (!isOptionExsited)
            {
                throw new OptionNotFoundException();
            }

            option.Id = optionId;
            option.ProductId = id;

            return await _optionService.UpdateOptionAsync(option);
        }

        public async Task<bool> IsProductExistedByIdAsync(Guid id)
        {
            return await _productRepository.IsProductExistedByIdAsync(id);
        }
    }
}
