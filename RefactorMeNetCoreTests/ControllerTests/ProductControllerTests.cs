using Microsoft.AspNetCore.Mvc;
using Moq;
using RefactorMeNetCore.Controllers;
using RefactorMeNetCore.Models;
using RefactorMeNetCore.Services;
using System;
using System.Collections.Generic;
using Xunit;


namespace RefactorMeNetCoreTests.ControllerTests
{
    public class ProductControllerTests
    {
        protected ProductsController ProductController
        {
            get;
        }
        protected Mock<IProductService> ProductServiceMock
        {
            get;
        }

        public ProductControllerTests()
        {
            ProductServiceMock = new Mock<IProductService>();
            ProductController = new ProductsController(ProductServiceMock.Object);
        }

        [Fact]
        public async void GetProductsAsync_WithOutProductName_ShouldReturnAll()
        {
            // Arrange
            var products = new List<Product>()
            {
                    new Product{Id = Guid.NewGuid(), Name ="product1" },
                    new Product{Id = Guid.NewGuid(), Name ="product2" }
            };

            ProductServiceMock.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(products);

            // Act
            var result = await ProductController.GetProductsAsync(string.Empty);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            //convert the ok result from anonymous type to a dynamic so we can compare the results
            dynamic dResult = new DynamicObjectResultValue(okResult.Value);

            Assert.Equal(products, dResult.Items);
        }

        [Fact]
        public async void GetProductsAsync_WithProductName_ShouldReturnProductsByName()
        {
            // Arrange            
            var expectedResult = new List<Product>()
            {
                    new Product{Id = Guid.NewGuid(), Name ="product1" },
                    new Product{Id = Guid.NewGuid(), Name ="product111" }
            };

            ProductServiceMock.Setup(x => x.GetProductsByNameAsync("1")).ReturnsAsync(expectedResult);

            // Act
            var result = await ProductController.GetProductsAsync("1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            //convert the ok result from anonymous type to a dynamic so we can compare the results
            dynamic dResult = new DynamicObjectResultValue(okResult.Value);

            Assert.Equal(expectedResult, dResult.Items);
        }

        [Fact]
        public async void GetProductByIdAsync_ShouldReturnProductsById()
        {
            // Arrange
            var expectedProduct = new Product() { Id = Guid.NewGuid(), Name = "product" };

            ProductServiceMock.Setup(x => x.GetProductByIdAsync(expectedProduct.Id)).ReturnsAsync(expectedProduct);

            // Act
            var result = await ProductController.GetProductByIdAsync(expectedProduct.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(expectedProduct, okResult.Value);
        }

        [Fact]
        public async void CreatProductAsync_ShouldReturnOKResult()
        {
            // Arrange
            var expectedProduct = new Product() { Id = Guid.NewGuid(), Name = "product" };

            ProductServiceMock.Setup(x => x.CreateAsync(expectedProduct)).ReturnsAsync(expectedProduct);

            // Act
            var result = await ProductController.CreatProductAsync(expectedProduct);

            // Assert
            var okResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(expectedProduct, okResult.Value);
            Assert.Equal(nameof(ProductController.GetProductByIdAsync), okResult.ActionName);
        }

        [Fact]
        public async void CreatProductAsync_ShouldReturnBadRequestResult()
        {
            // Arrange
            var expectedProduct = new Product() { Id = Guid.NewGuid(), Name = "product" };

            ProductServiceMock.Setup(x => x.CreateAsync(expectedProduct)).ReturnsAsync(expectedProduct);

            ProductController.ModelState.AddModelError("key", "error");

            // Act
            var result = await ProductController.CreatProductAsync(expectedProduct);

            // Assert
            var badResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void UpdateProductAsync_ShouldReturnOk()
        {
            // Arrange
            var expectedProduct = new Product() { Id = Guid.NewGuid(), Name = "product" };

            ProductServiceMock.Setup(x => x.UpdateAsync(expectedProduct.Id, expectedProduct)).ReturnsAsync(expectedProduct);

            // Act
            var result = await ProductController.UpdateProductAsync(expectedProduct.Id, expectedProduct);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedProduct, okResult.Value);
        }

        [Fact]
        public async void UpdateProductAsync_ShouldReturnBadRequestResult()
        {
            // Arrange
            var expectedProduct = new Product() { Id = Guid.NewGuid(), Name = "product" };

            ProductServiceMock.Setup(x => x.UpdateAsync(expectedProduct.Id, expectedProduct)).ReturnsAsync(expectedProduct);

            ProductController.ModelState.AddModelError("key", "error");

            // Act
            var result = await ProductController.UpdateProductAsync(expectedProduct.Id, expectedProduct);

            // Assert
            var badResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void DeleteProductAndOptions_ShouldReturnOk()
        {
            // Arrange
            var expectedProduct = new Product() { Id = Guid.NewGuid(), Name = "product" };

            ProductServiceMock.Setup(x => x.DeleteAsync(expectedProduct.Id)).ReturnsAsync(expectedProduct);

            // Act
            var result = await ProductController.DeleteProductAndOptions(expectedProduct.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedProduct, okResult.Value);
        }

        [Fact]
        public async void GetAllOptionsByProductIdAsync_ShouldReturnOk()
        {
            // Arrange
            var expectedProduct = new Product() { Id = Guid.NewGuid(), Name = "product" };

            var expectedOptions = new List<ProductOption>() { new ProductOption() { Id = Guid.NewGuid(), Name = "Option1" }, new ProductOption() { Name = "Option2" } };

            ProductServiceMock.Setup(x => x.GetAllOptionsByProductIdAsync(expectedProduct.Id)).ReturnsAsync(expectedOptions);

            // Act
            var result = await ProductController.GetAllOptionsByProductIdAsync(expectedProduct.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            dynamic dResult = new DynamicObjectResultValue(okResult.Value);

            Assert.Equal(expectedOptions, dResult.Items);
        }

        [Fact]
        public async void GetOptionByProductIdAndOptionIdAsync_ShouldReturnOk()
        {
            // Arrange
            var expectedProduct = new Product() { Id = Guid.NewGuid(), Name = "product" };

            var expectedOption = new ProductOption { Name = "Option" };

            ProductServiceMock.Setup(x => x.GetOptionByProductIdAndOptionIdAsync(expectedProduct.Id, expectedOption.Id)).ReturnsAsync(expectedOption);

            // Act
            var result = await ProductController.GetOptionByProductIdAndOptionIdAsync(expectedProduct.Id, expectedOption.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedOption, okResult.Value);
        }

        [Fact]
        public async void CreatOptionByProductIdAsync_ShouldReturnOk()
        {
            // Arrange
            var expectedProduct = new Product() { Id = Guid.NewGuid(), Name = "product" };

            var expectedOption = new ProductOption { Name = "Option" };

            ProductServiceMock.Setup(x => x.CreatOptionByProductIdAsync(expectedProduct.Id, expectedOption)).ReturnsAsync(expectedOption);

            // Act
            var result = await ProductController.CreatOptionByProductIdAsync(expectedProduct.Id, expectedOption);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(expectedOption, createdAtActionResult.Value);
            Assert.Equal(nameof(ProductController.GetOptionByProductIdAndOptionIdAsync), createdAtActionResult.ActionName);
        }

        [Fact]
        public async void UpdateOptionByProductIdAndOptionIdAsync_ShouldReturnOk()
        {
            // Arrange
            var expectedProduct = new Product() { Id = Guid.NewGuid(), Name = "product" };

            var expectedOption = new ProductOption { Name = "Option" };

            ProductServiceMock.Setup(x => x.UpdateOptionByProductIdAndOptionIdAsync(expectedProduct.Id, expectedOption.Id, expectedOption)).ReturnsAsync(expectedOption);

            // Act
            var result = await ProductController.UpdateOptionByProductIdAndOptionIdAsync(expectedProduct.Id, expectedOption.Id, expectedOption);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedOption, okResult.Value);
        }

        [Fact]
        public async void DeleteOptionByProductIdAndOptionIdAsync_ShouldReturnOk()
        {
            // Arrange
            var expectedProduct = new Product() { Id = Guid.NewGuid(), Name = "product" };

            var expectedOption = new ProductOption { Name = "Option" };

            ProductServiceMock.Setup(x => x.DeleteOptionByProductIdAndOptionIdAsync(expectedProduct.Id, expectedOption.Id)).ReturnsAsync(expectedOption);

            // Act
            var result = await ProductController.DeleteOptionByProductIdAndOptionIdAsync(expectedProduct.Id, expectedOption.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedOption, okResult.Value);
        }        
    }
}
