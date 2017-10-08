using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RefactorMeNetCore.Models;

namespace RefactorMeNetCore.Services
{
    public interface IOptionService
    {
        Task<List<ProductOption>> GetAllOptionByProductIdAsync(Guid id);
        Task<ProductOption> GetOptionByProductIdAndOptionIdAsync(Guid id, Guid optionId);
        Task<ProductOption> CreateOptionAsync(ProductOption option);
        Task<bool> IsOptionExsitedById(Guid optionId);
        Task<ProductOption> UpdateOptionAsync(ProductOption option);
        Task<ProductOption> DeleteOptionByProductIdAndOptionIdAsync(Guid id, Guid optionId);
    }
}
