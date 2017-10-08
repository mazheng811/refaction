using Microsoft.EntityFrameworkCore;
using RefactorMeNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorMeNetCore.Repositories
{
    public class OptionRepository : IOptionRepository
    {
        private readonly DataBaseContext _dataBaseContext;

        public OptionRepository(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public async Task<ProductOption> CreatOptionAsync(ProductOption option)
        {
            await _dataBaseContext.ProductOption.AddAsync(option);

            await _dataBaseContext.SaveChangesAsync();

            return option;
        }

        public async Task<ProductOption> DeleteOptionByProductIdAndOptionIdAsync(Guid id, Guid optionId)
        {
            var option = await GetOptionByProductIdAndOptionIdAsync(id, optionId);

            _dataBaseContext.Entry(option).State = EntityState.Deleted;
            await _dataBaseContext.SaveChangesAsync();

            return option;
        }

        public async Task<List<ProductOption>> GetAllOptionByProductIdAsync(Guid id)
        {
            return await _dataBaseContext.ProductOption.Where(o => o.ProductId == id).ToListAsync();
        }

        public async Task<ProductOption> GetOptionByProductIdAndOptionIdAsync(Guid id, Guid optionId)
        {
            return await _dataBaseContext.ProductOption.FirstAsync(o => o.ProductId == id && o.Id == optionId);
        }

        public async Task<bool> IsOptionExistedById(Guid optionId)
        {
            return await _dataBaseContext.ProductOption.AnyAsync(o => o.Id == optionId);
        }

        public async Task<ProductOption> UpdateOptionAsync(ProductOption option)
        {
            var oldOption = await GetOptionByProductIdAndOptionIdAsync(option.ProductId, option.Id);

            oldOption.Name = option.Name;
            oldOption.Description = option.Description;

            await _dataBaseContext.SaveChangesAsync();

            return option;
        }
    }
}
