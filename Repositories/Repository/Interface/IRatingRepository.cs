﻿using Repositories.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository.Interface
{
    public interface IRatingRepository : IGenericRepository<Rating>
    {
        public Task<bool> DeleteWithCondition(Rating rating);
    }
}
