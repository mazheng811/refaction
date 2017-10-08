using Microsoft.EntityFrameworkCore;
using RefactorMeNetCore.Models;
using RefactorMeNetCore.Repositories;
using System;
using System.Collections.Generic;
using Xunit;

namespace RefactorMeNetCoreTests.RepositoryTests
{
    public class OptionRepositoryTests
    {
        private ProductOption firstOption;
        private ProductOption secondOption;

        private List<ProductOption> optionsList;

        public OptionRepositoryTests()
        {
            firstOption = new ProductOption() { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), Name = "First option name" };
            secondOption = new ProductOption() { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), Name = "Second option name" };

            optionsList = new List<ProductOption>() { firstOption, secondOption };
        }

        [Fact]
        public async void CreatOptionAsync_ShouldAdd()
        {
            // Arrange
            //create in memory options
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "CreatOptionAsync_ShouldAdd")
                .Options;
            
            // Act
            using (var context = new DataBaseContext(options))
            {
                var repository = new OptionRepository(context);

                var result = await repository.CreatOptionAsync(firstOption);

                // Assert        
                Assert.Equal(firstOption, result);
            }

            using (var context = new DataBaseContext(options))
            {
                Assert.Equal(1, await context.ProductOption.CountAsync());
            }
        }

        [Fact]
        public async void DeleteOptionByProductIdAndOptionIdAsync_ShouldDelete()
        {
            // Arrange
            //create in memory options
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "DeleteOptionByProductIdAndOptionIdAsync_ShouldDelete")
                .Options;

            using (var context = new DataBaseContext(options))
            {
                await context.AddRangeAsync(optionsList);

                await context.SaveChangesAsync();

                Assert.Equal(2, await context.ProductOption.CountAsync());
            }

            // Act
            using (var context = new DataBaseContext(options))
            {
                var repo = new OptionRepository(context);

                var result = await repo.DeleteOptionByProductIdAndOptionIdAsync(firstOption.ProductId, firstOption.Id);

                Assert.Equal(firstOption.Id, result.Id);
            }

            using (var context = new DataBaseContext(options))
            {
                Assert.Equal(1, await context.ProductOption.CountAsync());
            }
        }

        [Fact]
        public async void GetAllOptionByProductIdAsync_ShouldGetOptions()
        {
            // Arrange
            //create in memory options
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "GetAllOptionByProductIdAsync_ShouldGetOptions")
                .Options;

            using (var context = new DataBaseContext(options))
            {
                await context.AddRangeAsync(optionsList);

                await context.SaveChangesAsync();

                Assert.Equal(2, await context.ProductOption.CountAsync());
            }

            // Act
            using (var context = new DataBaseContext(options))
            {
                var repo = new OptionRepository(context);

                var result = await repo.GetAllOptionByProductIdAsync(firstOption.ProductId);

                Assert.Equal(1, result.Count);

                Assert.Equal(firstOption.Id, result[0].Id);
            }
        }

        [Fact]
        public async void GetOptionByProductIdAndOptionIdAsync_ShouldGetOption()
        {
            // Arrange
            //create in memory options
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "GetOptionByProductIdAndOptionIdAsync_ShouldGetOption")
                .Options;

            using (var context = new DataBaseContext(options))
            {
                await context.AddRangeAsync(optionsList);

                await context.SaveChangesAsync();

                Assert.Equal(2, await context.ProductOption.CountAsync());
            }

            // Act
            using (var context = new DataBaseContext(options))
            {
                var repo = new OptionRepository(context);

                var result = await repo.GetOptionByProductIdAndOptionIdAsync(firstOption.ProductId, firstOption.Id);

                Assert.Equal(firstOption.Id, result.Id);
            }
        }

        [Fact]
        public async void IsOptionExistedById_ShouldReturnBool()
        {
            // Arrange
            //create in memory options
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "IsOptionExistedById_ShouldReturnBool")
                .Options;

            using (var context = new DataBaseContext(options))
            {
                await context.AddRangeAsync(optionsList);

                await context.SaveChangesAsync();

                Assert.Equal(2, await context.ProductOption.CountAsync());
            }

            // Act
            using (var context = new DataBaseContext(options))
            {
                var repo = new OptionRepository(context);

                var result = await repo.IsOptionExistedById(firstOption.Id);

                Assert.Equal(true, result);
            }

            using (var context = new DataBaseContext(options))
            {
                var repo = new OptionRepository(context);

                var result = await repo.IsOptionExistedById(Guid.NewGuid());

                Assert.Equal(false, result);
            }
        }

        [Fact]
        public async void UpdateOptionAsync_ShouldUpdateOption()
        {
            // Arrange
            //create in memory options
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "UpdateOptionAsync_ShouldUpdateOption")
                .Options;

            using (var context = new DataBaseContext(options))
            {
                await context.AddRangeAsync(optionsList);

                await context.SaveChangesAsync();

                Assert.Equal(2, await context.ProductOption.CountAsync());
            }

            firstOption.Name = "updated name";
            firstOption.Description = "updated des";

            // Act
            using (var context = new DataBaseContext(options))
            {
                var repo = new OptionRepository(context);

                var result = await repo.UpdateOptionAsync(firstOption);

                Assert.Equal(firstOption.Id, result.Id);
            }

            using (var context = new DataBaseContext(options))
            {
                var updatedoption = await context.ProductOption.FirstAsync(o => o.Id == firstOption.Id);

                Assert.Equal(firstOption.Name, updatedoption.Name);
                Assert.Equal(firstOption.Description, updatedoption.Description);
            }
        }
    }
}
