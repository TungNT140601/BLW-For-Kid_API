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
    public class ExpertRepository : GenericRepository<Expert>, IExpertRepository
    {
        public ExpertRepository(BLW_FOR_KIDContext dBContext) : base(dBContext) {}
    }
}
