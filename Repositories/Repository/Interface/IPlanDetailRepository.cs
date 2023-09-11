using Repositories.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository.Interface
{
    public interface IPlanDetailRepository : IGenericRepository<PlanDetail>
    {
        Task<bool> AddRange(List<PlanDetail> planDetails);
        Task<bool> RemoveRange(string planId);
    }
}
