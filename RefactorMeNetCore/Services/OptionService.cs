using RefactorMeNetCore.Models;
using RefactorMeNetCore.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefactorMeNetCore.Services
{
    public class OptionService : IOptionService
    {
        private readonly IOptionRepository _optionRepository;

        public OptionService(IOptionRepository optionRepository)
        {
            _optionRepository = optionRepository;
        }

        public Task<ProductOption> CreateOptionAsync(ProductOption option)
        {
            return _optionRepository.CreatOptionAsync(option);
        }

        public async Task<ProductOption> DeleteOptionByProductIdAndOptionIdAsync(Guid id, Guid optionId)
        {
            return await _optionRepository.DeleteOptionByProductIdAndOptionIdAsync(id, optionId);
        }

        public async Task<List<ProductOption>> GetAllOptionByProductIdAsync(Guid id)
        {
            return await _optionRepository.GetAllOptionByProductIdAsync(id);
        }

        public async Task<ProductOption> GetOptionByProductIdAndOptionIdAsync(Guid id, Guid optionId)
        {
            return await _optionRepository.GetOptionByProductIdAndOptionIdAsync(id, optionId);
        }

        public async Task<bool> IsOptionExsitedById(Guid optionId)
        {
            return await _optionRepository.IsOptionExistedById(optionId);
        }

        public async Task<ProductOption> UpdateOptionAsync(ProductOption option)
        {
            return await _optionRepository.UpdateOptionAsync(option);
        }
    }
}
