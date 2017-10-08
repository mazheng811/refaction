using Moq;
using RefactorMeNetCore.Exceptions;
using RefactorMeNetCore.Models;
using RefactorMeNetCore.Repositories;
using RefactorMeNetCore.Services;
using System.Collections.Generic;
using Xunit;

namespace RefactorMeNetCoreTests.ServiceTests
{
    public class ProductServiceTests
    {
        protected Mock<IOptionService> OptionServiceMock;
        protected Mock<IProductRepository> ProductRepositoryMock;
        protected ProductService ServiceTest;

        public ProductServiceTests()
        {
            OptionServiceMock = new Mock<IOptionService>();
            ProductRepositoryMock = new Mock<IProductRepository>();
            ServiceTest = new ProductService(OptionServiceMock.Object, ProductRepositoryMock.Object);
        }

        [Fact]
        public async void CreateAsync_ShouldReturnProduct()
        {
            // Arrange
            var productExpected = new Product() { Name = "product" };

            ProductRepositoryMock.Setup(x => x.CreateAsync(productExpected)).ReturnsAsync(productExpected);

            // Act
            var result = await ServiceTest.CreateAsync(productExpected);

            // Assert            
            Assert.Equal(productExpected, result);
        }

        [Fact]
        public async void CreatOptionByProductIdAsync_ShouldReturnOption()
        {
            // Arrange
            var resultExpected = new ProductOption() { Name = "option" };
            var product = new Product() { Name = "product" };

            OptionServiceMock.Setup(x => x.CreateOptionAsync(resultExpected)).ReturnsAsync(resultExpected);
            ProductRepositoryMock.Setup(x => x.IsProductExistedByIdAsync(product.Id)).ReturnsAsync(true);

            // Act
            var result = await ServiceTest.CreatOptionByProductIdAsync(product.Id, resultExpected);

            // Assert            
            Assert.Equal(resultExpected, result);
        }

        [Fact]
        public async void CreatOptionByProductIdAsync_ShouldThrowException()
        {
            // Arrange
            var resultExpected = new ProductOption() { Name = "option" };
            var product = new Product() { Name = "product" };

            OptionServiceMock.Setup(x => x.CreateOptionAsync(resultExpected)).ReturnsAsync(resultExpected);
            ProductRepositoryMock.Setup(x => x.IsProductExistedByIdAsync(product.Id)).ReturnsAsync(false);

            // Assert                        
            await Assert.ThrowsAsync<ProductNotFoundException>(() => ServiceTest.CreatOptionByProductIdAsync(product.Id, resultExpected));
        }

        [Fact]
        public async void DeleteAsync_ShouldReturnProduct()
        {
            // Arrange
            var product = new Product() { Name = "product" };
            
            ProductRepositoryMock.Setup(x => x.DeleteAsync(product.Id)).ReturnsAsync(product);

            // Act
            var result = await ServiceTest.DeleteAsync(product.Id);

            // Assert            
            Assert.Equal(product, result);
        }

        [Fact]
        public async void DeleteOptionByProductIdAndOptionIdAsync_ShouldReturnOption()
        {
            // Arrange
            var product = new Product() { Name = "product" };
            var resultExpected = new ProductOption() { Name = "option" };

            OptionServiceMock.Setup(x => x.DeleteOptionByProductIdAndOptionIdAsync(product.Id, resultExpected.Id)).ReturnsAsync(resultExpected);
            ProductRepositoryMock.Setup(x => x.IsProductExistedByIdAsync(product.Id)).ReturnsAsync(true);

            // Act
            var result = await ServiceTest.DeleteOptionByProductIdAndOptionIdAsync(product.Id, resultExpected.Id);

            // Assert            
            Assert.Equal(resultExpected, result);
        }

        [Fact]
        public async void DeleteOptionByProductIdAndOptionIdAsync_ShouldThrowException()
        {
            // Arrange
            var product = new Product() { Name = "product" };
            var resultExpected = new ProductOption() { Name = "option" };

            OptionServiceMock.Setup(x => x.DeleteOptionByProductIdAndOptionIdAsync(product.Id, resultExpected.Id)).ReturnsAsync(resultExpected);
            ProductRepositoryMock.Setup(x => x.IsProductExistedByIdAsync(product.Id)).ReturnsAsync(false);

            // Assert            
            await Assert.ThrowsAsync<ProductNotFoundException>(() => ServiceTest.DeleteOptionByProductIdAndOptionIdAsync(product.Id, resultExpected.Id));
        }

        [Fact]
        public async void GetAllOptionsByProductIdAsync_ShouldReturnOptions()
        {
            // Arrange
            var product = new Product() { Name = "product" };
            var resultExpected = new List<ProductOption>() { new ProductOption() { Name = "option1"}, new ProductOption() { Name = "option2"} };

            OptionServiceMock.Setup(x => x.GetAllOptionByProductIdAsync(product.Id)).ReturnsAsync(resultExpected);
            ProductRepositoryMock.Setup(x => x.IsProductExistedByIdAsync(product.Id)).ReturnsAsync(true);

            // Act
            var result = await ServiceTest.GetAllOptionsByProductIdAsync(product.Id);

            // Assert            
            Assert.Equal(resultExpected, result);
        }

        [Fact]
        public async void GetAllOptionsByProductIdAsync_ShouldThrowException()
        {
            // Arrange
            var product = new Product() { Name = "product" };
            var resultExpected = new List<ProductOption>() { new ProductOption() { Name = "option1" }, new ProductOption() { Name = "option2" } };

            OptionServiceMock.Setup(x => x.GetAllOptionByProductIdAsync(product.Id)).ReturnsAsync(resultExpected);
            ProductRepositoryMock.Setup(x => x.IsProductExistedByIdAsync(product.Id)).ReturnsAsync(false);

            // Assert            
            await Assert.ThrowsAsync<ProductNotFoundException>(() => ServiceTest.GetAllOptionsByProductIdAsync(product.Id));            
        }

        [Fact]
        public async void GetAllProductsAsync_ShouldReturnProducts()
        {
            // Arrange            
            var resultExpected = new List<Product>() { new Product() { Name = "product1" }, new Product() { Name = "product2" } };
            
            ProductRepositoryMock.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(resultExpected);

            // Act
            var result = await ServiceTest.GetAllProductsAsync();

            // Assert            
            Assert.Equal(resultExpected, result);
        }

        [Fact]
        public async void GetOptionByProductIdAndOptionIdAsync_ShouldReturnOption()
        {
            // Arrange
            var product = new Product() { Name = "product" };
            var resultExpected = new ProductOption() { Name = "option1" };

            OptionServiceMock.Setup(x => x.GetOptionByProductIdAndOptionIdAsync(product.Id, resultExpected.Id)).ReturnsAsync(resultExpected);
            OptionServiceMock.Setup(x => x.IsOptionExsitedById(resultExpected.Id)).ReturnsAsync(true);
            ProductRepositoryMock.Setup(x => x.IsProductExistedByIdAsync(product.Id)).ReturnsAsync(true);

            // Act
            var result = await ServiceTest.GetOptionByProductIdAndOptionIdAsync(product.Id, resultExpected.Id);

            // Assert            
            Assert.Equal(resultExpected, result);
        }

        [Fact]
        public async void GetOptionByProductIdAndOptionIdAsync_ShouldThrow_ProductNotFoundException()
        {
            // Arrange
            var product = new Product() { Name = "product" };
            var resultExpected = new ProductOption() { Name = "option1" };

            OptionServiceMock.Setup(x => x.GetOptionByProductIdAndOptionIdAsync(product.Id, resultExpected.Id)).ReturnsAsync(resultExpected);
            OptionServiceMock.Setup(x => x.IsOptionExsitedById(resultExpected.Id)).ReturnsAsync(false);
            ProductRepositoryMock.Setup(x => x.IsProductExistedByIdAsync(product.Id)).ReturnsAsync(false);
            
            // Assert            
            await Assert.ThrowsAsync<ProductNotFoundException>(() => ServiceTest.GetOptionByProductIdAndOptionIdAsync(product.Id, resultExpected.Id));            
        }

        [Fact]
        public async void GetOptionByProductIdAndOptionIdAsync_ShouldThrow_OptionNotFoundException()
        {
            // Arrange
            var product = new Product() { Name = "product" };
            var resultExpected = new ProductOption() { Name = "option1" };

            OptionServiceMock.Setup(x => x.GetOptionByProductIdAndOptionIdAsync(product.Id, resultExpected.Id)).ReturnsAsync(resultExpected);
            OptionServiceMock.Setup(x => x.IsOptionExsitedById(resultExpected.Id)).ReturnsAsync(false);
            ProductRepositoryMock.Setup(x => x.IsProductExistedByIdAsync(product.Id)).ReturnsAsync(true);

            // Assert            
            await Assert.ThrowsAsync<OptionNotFoundException>(() => ServiceTest.GetOptionByProductIdAndOptionIdAsync(product.Id, resultExpected.Id));
        }

        [Fact]
        public async void GetProductByIdAsync_ShouldReturnProduct()
        {
            // Arrange            
            var product = new Product() { Name = "product" };

            ProductRepositoryMock.Setup(x => x.GetProductByIdAsync(product.Id)).ReturnsAsync(product);

            // Act
            var result = await ServiceTest.GetProductByIdAsync(product.Id);

            // Assert            
            Assert.Equal(product, result);
        }

        [Fact]
        public async void GetProductsByNameAsync_ShouldReturnProducts()
        {
            // Arrange            
            var resultExpected = new List<Product>() { new Product() { Name = "product1" }, new Product() { Name = "product2" } };

            ProductRepositoryMock.Setup(x => x.GetProductsByNameAsync("name")).ReturnsAsync(resultExpected);

            // Act
            var result = await ServiceTest.GetProductsByNameAsync("name");

            // Assert            
            Assert.Equal(resultExpected, result);
        }

        [Fact]
        public async void UpdateAsync_ShouldReturnProduct()
        {
            // Arrange
            var product = new Product() { Name = "product" };

            ProductRepositoryMock.Setup(x => x.IsProductExistedByIdAsync(product.Id)).ReturnsAsync(true);
            ProductRepositoryMock.Setup(x => x.UpdateAsync(product)).ReturnsAsync(product);

            // Act
            var result = await ServiceTest.UpdateAsync(product.Id, product);

            // Assert            
            Assert.Equal(product, result);
        }

        [Fact]
        public async void UpdateAsync_ShouldThrowException()
        {
            // Arrange
            var product = new Product() { Name = "product" };

            ProductRepositoryMock.Setup(x => x.IsProductExistedByIdAsync(product.Id)).ReturnsAsync(false);
            ProductRepositoryMock.Setup(x => x.UpdateAsync(product)).ReturnsAsync(product);

            // Assert            
            await Assert.ThrowsAsync<ProductNotFoundException>(() => ServiceTest.UpdateAsync(product.Id, product));           
        }

        [Fact]
        public async void UpdateOptionByProductIdAndOptionIdAsync_ShouldReturnOption()
        {
            // Arrange
            var product = new Product() { Name = "product" };
            var resultExpected = new ProductOption() { Name = "option1" };

            OptionServiceMock.Setup(x => x.UpdateOptionAsync(resultExpected)).ReturnsAsync(resultExpected);
            OptionServiceMock.Setup(x => x.IsOptionExsitedById(resultExpected.Id)).ReturnsAsync(true);
            ProductRepositoryMock.Setup(x => x.IsProductExistedByIdAsync(product.Id)).ReturnsAsync(true);

            // Act
            var result = await ServiceTest.UpdateOptionByProductIdAndOptionIdAsync(product.Id, resultExpected.Id, resultExpected);

            // Assert            
            Assert.Equal(resultExpected, result);
        }

        [Fact]
        public async void UpdateOptionByProductIdAndOptionIdAsync_ShouldThrow_ProductNotFoundException()
        {
            // Arrange
            var product = new Product() { Name = "product" };
            var resultExpected = new ProductOption() { Name = "option1" };

            OptionServiceMock.Setup(x => x.UpdateOptionAsync(resultExpected)).ReturnsAsync(resultExpected);
            OptionServiceMock.Setup(x => x.IsOptionExsitedById(resultExpected.Id)).ReturnsAsync(false);
            ProductRepositoryMock.Setup(x => x.IsProductExistedByIdAsync(product.Id)).ReturnsAsync(false);

            // Assert            
            await Assert.ThrowsAsync<ProductNotFoundException>(() => ServiceTest.UpdateOptionByProductIdAndOptionIdAsync(product.Id, resultExpected.Id, resultExpected));
        }

        [Fact]
        public async void UpdateOptionByProductIdAndOptionIdAsync_ShouldThrow_OptionNotFoundException()
        {
            // Arrange
            var product = new Product() { Name = "product" };
            var resultExpected = new ProductOption() { Name = "option1" };

            OptionServiceMock.Setup(x => x.UpdateOptionAsync(resultExpected)).ReturnsAsync(resultExpected);
            OptionServiceMock.Setup(x => x.IsOptionExsitedById(resultExpected.Id)).ReturnsAsync(false);
            ProductRepositoryMock.Setup(x => x.IsProductExistedByIdAsync(product.Id)).ReturnsAsync(true);

            // Assert            
            await Assert.ThrowsAsync<OptionNotFoundException>(() => ServiceTest.UpdateOptionByProductIdAndOptionIdAsync(product.Id, resultExpected.Id, resultExpected));
        }

        [Fact]
        public async void IsProductExistedByIdAsync_ShouldReturnBool()
        {
            // Arrange
            var product = new Product() { Name = "product" };

            ProductRepositoryMock.Setup(x => x.IsProductExistedByIdAsync(product.Id)).ReturnsAsync(true);
            
            // Act
            var result = await ServiceTest.IsProductExistedByIdAsync(product.Id);

            // Assert            
            Assert.Equal(true, result);
        }
    }
}
