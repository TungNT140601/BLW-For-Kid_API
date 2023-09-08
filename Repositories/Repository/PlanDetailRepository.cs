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
    }
}
