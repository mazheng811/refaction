using Microsoft.EntityFrameworkCore;
using RefactorMeNetCore.Models;
using RefactorMeNetCore.Repositories;
using System;
using System.Collections.Generic;
using Xunit;

namespace RefactorMeNetCoreTests.RepositoryTests
{
    public class ProductRepositoryTests
    {
        //use the in Memory to test the repository

        [Fact]
        public async void CreateAsync_ShouldAdd()
        {
            // Arrange
            //create in memory options
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "CreateAsync_ShouldAdd")
                .Options;

            var product = new Product() { Name = "product name", Price = 100 };

            // Act
            using (var context = new DataBaseContext(options))
            {
                var productRepository = new ProductRepository(context);

                var result = await productRepository.CreateAsync(product);

                // Assert        
                Assert.Equal(product, result);
            }

            using (var context = new DataBaseContext(options))
            {
                Assert.Equal(1, await context.Product.CountAsync());
            }
        }

        [Fact]
        public async void DeleteAsync_ShouldDelete()
        {
            // Arrange            
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "DeleteAsync_ShouldDelete")
                .Options;

            var product = new Product() { Id = Guid.NewGuid(), Name = "product name", Price = 100 };

            using (var context = new DataBaseContext(options))
            {
                await context.Product.AddAsync(product);

                await context.SaveChangesAsync();

                Assert.Equal(1, await context.Product.CountAsync());
            }

            // Act
            using (var context = new DataBaseContext(options))
            {
                var productRepository = new ProductRepository(context);

                var result = await productRepository.DeleteAsync(product.Id);
            }

            // Assert     
            using (var context = new DataBaseContext(options))
            {
                Assert.Equal(0, await context.Product.CountAsync());
            }
        }

        [Fact]
        public async void GetAllProductsAsync_ShouldGetAllProducts()
        {
            // Arrange            
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "GetAllProductsAsync_ShouldGetAllProducts")
                .Options;

            var products = new List<Product>() { new Product() { Id = Guid.NewGuid(), Name = "product name", Price = 100 }, new Product() { Id = Guid.NewGuid() } };

            using (var context = new DataBaseContext(options))
            {
                await context.Product.AddRangeAsync(products);

                await context.SaveChangesAsync();

                Assert.Equal(2, await context.Product.CountAsync());
            }

            // Act
            using (var context = new DataBaseContext(options))
            {
                var productRepository = new ProductRepository(context);

                var result = await productRepository.GetAllProductsAsync();

                Assert.True(result.Count == products.Count);
            }            
        }

        [Fact]
        public async void GetProductByIdAsync_ShouldGetProduct()
        {
            // Arrange            
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "GetProductByIdAsync_ShouldGetProduct")
                .Options;

            var product = new Product() { Id = Guid.NewGuid(), Name = "product name", Price = 100 };

            using (var context = new DataBaseContext(options))
            {
                await context.Product.AddAsync(product);

                await context.SaveChangesAsync();

                Assert.Equal(1, await context.Product.CountAsync());
            }

            // Act
            using (var context = new DataBaseContext(options))
            {
                var productRepository = new ProductRepository(context);

                var result = await productRepository.GetProductByIdAsync(product.Id);

                Assert.Equal(result.Id, product.Id);
            }            
        }

        [Fact]
        public async void GetProductsByNameAsync_ShouldGetProducts()
        {
            // Arrange            
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "GetProductsByNameAsync_ShouldGetProducts")
                .Options;

            var products = new List<Product>() { new Product() { Id = Guid.NewGuid(), Name = "product name", Price = 100 }, new Product() { Id = Guid.NewGuid(), Name = "name" } };

            using (var context = new DataBaseContext(options))
            {
                await context.Product.AddRangeAsync(products);

                await context.SaveChangesAsync();

                Assert.Equal(2, await context.Product.CountAsync());
            }

            // Act
            using (var context = new DataBaseContext(options))
            {
                var productRepository = new ProductRepository(context);

                var result = await productRepository.GetProductsByNameAsync("product");

                Assert.Equal(1, result.Count);
                Assert.Equal("product name", result[0].Name);
            }
        }

        [Fact]
        public async void IsProductExistedByIdAsync_ShouldReturnBool()
        {
            // Arrange            
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "IsProductExistedByIdAsync_ShouldReturnBool")
                .Options;

            var products = new List<Product>() { new Product() { Id = Guid.NewGuid(), Name = "product name", Price = 100 }, new Product() { Id = Guid.NewGuid(), Name = "name" } };

            using (var context = new DataBaseContext(options))
            {
                await context.Product.AddRangeAsync(products);

                await context.SaveChangesAsync();

                Assert.Equal(2, await context.Product.CountAsync());
            }

            // Act
            using (var context = new DataBaseContext(options))
            {
                var productRepository = new ProductRepository(context);

                var result = await productRepository.IsProductExistedByIdAsync(Guid.NewGuid());

                Assert.Equal(false, result);                
            }

            // Act
            using (var context = new DataBaseContext(options))
            {
                var productRepository = new ProductRepository(context);

                var result = await productRepository.IsProductExistedByIdAsync(products[0].Id);

                Assert.Equal(true, result);
            }
        }

        [Fact]
        public async void UpdateAsync_ShouldUpdate()
        {
            // Arrange            
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "UpdateAsync_ShouldUpdate")
                .Options;

            var product = new Product() { Id = Guid.NewGuid(), Name = "product", DeliveryPrice = 10.2m, Price = 100, Description = "Description" };

            var products = new List<Product>() { product, new Product() { Id = Guid.NewGuid(), Name = "name" } };

            using (var context = new DataBaseContext(options))
            {
                await context.Product.AddRangeAsync(products);

                await context.SaveChangesAsync();

                Assert.Equal(2, await context.Product.CountAsync());
            }

            product.Name = "updated name";
            product.DeliveryPrice = 20;
            product.Price = 200;
            product.Description = "updated des";

            // Act
            using (var context = new DataBaseContext(options))
            {
                var productRepository = new ProductRepository(context);

                var result = await productRepository.UpdateAsync(product);

                Assert.Equal(result, product);                
            }

            using (var context = new DataBaseContext(options))
            {
                var updatedResult = await context.Product.FirstAsync(p => p.Id == product.Id);

                Assert.Equal(product.Name, updatedResult.Name);
                Assert.Equal(product.DeliveryPrice, updatedResult.DeliveryPrice);
                Assert.Equal(product.Price, updatedResult.Price);
                Assert.Equal(product.Description, updatedResult.Description);
            }
        }
    }
}
