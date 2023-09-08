using Repositories.DataAccess;
using Repositories.EntityModels;
using Repositories.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class PlanDetailRepository : GenericRepository<PlanDetail>, IPlanDetailRepository
    {
        public PlanDetailRepository(BLW_FOR_KIDContext dbContext) : base(dbContext)
        {
        }
        public async Task<bool> AddRange(List<PlanDetail> planDetails)
        {
            try
            {
                foreach (PlanDetail planDetail in planDetails)
                {
                    dbSet.Add(planDetail);
                }
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> RemoveRange(string planId)
        {
            try
            {
                var planDetails = dbSet.Where(x => x.PlanId == planId).ToList();
                foreach (PlanDetail planDetail in planDetails)
                {
                    dbSet.Remove(planDetail);
                }
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
