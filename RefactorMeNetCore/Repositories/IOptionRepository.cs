using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RefactorMeNetCore.Models;

namespace RefactorMeNetCore.Repositories
{
    public interface IOptionRepository
    {
        Task<List<ProductOption>> GetAllOptionByProductIdAsync(Guid id);
        Task<ProductOption> GetOptionByProductIdAndOptionIdAsync(Guid id, Guid optionId);
        Task<ProductOption> CreatOptionAsync(ProductOption option);
        Task<bool> IsOptionExistedById(Guid optionId);
        Task<ProductOption> UpdateOptionAsync(ProductOption option);
        Task<ProductOption> DeleteOptionByProductIdAndOptionIdAsync(Guid id, Guid optionId);
    }
}
