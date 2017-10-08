using Moq;
using RefactorMeNetCore.Models;
using RefactorMeNetCore.Repositories;
using RefactorMeNetCore.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace RefactorMeNetCoreTests.ServiceTests
{
    public class OptionServiceTests
    {
        protected Mock<IOptionRepository> OptionRepositoryMock;
        protected OptionService ServiceTest;

        public OptionServiceTests()
        {
            OptionRepositoryMock = new Mock<IOptionRepository>();
            ServiceTest = new OptionService(OptionRepositoryMock.Object);
        }

        [Fact]
        public async void CreateOptionAsync_ShouldReturnOption()
        {
            // Arrange
            var expectedResult = new ProductOption() { Name = "option name" };

            OptionRepositoryMock.Setup(x => x.CreatOptionAsync(expectedResult)).ReturnsAsync(expectedResult);

            // Act
            var result = await ServiceTest.CreateOptionAsync(expectedResult);

            // Assert            
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void DeleteOptionByProductIdAndOptionIdAsync_ShouldReturnOption()
        {
            // Arrange
            var expectedResult = new ProductOption() { Name = "option name" };
            var productId = Guid.NewGuid();
            
            OptionRepositoryMock.Setup(x => x.DeleteOptionByProductIdAndOptionIdAsync(productId, expectedResult.Id)).ReturnsAsync(expectedResult);

            // Act
            var result = await ServiceTest.DeleteOptionByProductIdAndOptionIdAsync(productId, expectedResult.Id);

            // Assert            
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void GetAllOptionByProductIdAsync_ShouldReturnOptions()
        {
            // Arrange
            var resultExpected = new List<ProductOption>() { new ProductOption() { Name = "option1" }, new ProductOption() { Name = "option2" } };
            var productId = Guid.NewGuid();

            OptionRepositoryMock.Setup(x => x.GetAllOptionByProductIdAsync(productId)).ReturnsAsync(resultExpected);

            // Act
            var result = await ServiceTest.GetAllOptionByProductIdAsync(productId);

            // Assert            
            Assert.Equal(resultExpected, result);
        }

        [Fact]
        public async void GetOptionByProductIdAndOptionIdAsync_ShouldReturnOption()
        {
            // Arrange
            var expectedResult = new ProductOption() { Name = "option name" };
            var productId = Guid.NewGuid();

            OptionRepositoryMock.Setup(x => x.GetOptionByProductIdAndOptionIdAsync(productId, expectedResult.Id)).ReturnsAsync(expectedResult);

            // Act
            var result = await ServiceTest.GetOptionByProductIdAndOptionIdAsync(productId, expectedResult.Id);

            // Assert            
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void IsOptionExsitedById_ShouldReturnBool()
        {
            // Arrange
            var expectedResult = new ProductOption() { Name = "option name" };
            
            OptionRepositoryMock.Setup(x => x.IsOptionExistedById(expectedResult.Id)).ReturnsAsync(false);

            // Act
            var result = await ServiceTest.IsOptionExsitedById(expectedResult.Id);

            // Assert            
            Assert.Equal(false, result);
        }

        [Fact]
        public async void UpdateOptionAsync_ShouldReturnOption()
        {
            // Arrange
            var expectedResult = new ProductOption() { Name = "option name" };

            OptionRepositoryMock.Setup(x => x.UpdateOptionAsync(expectedResult)).ReturnsAsync(expectedResult);

            // Act
            var result = await ServiceTest.UpdateOptionAsync(expectedResult);

            // Assert            
            Assert.Equal(expectedResult, result);
        }

    }
}
