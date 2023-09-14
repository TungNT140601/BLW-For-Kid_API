using Microsoft.EntityFrameworkCore;
using Repositories.DataAccess;
using Repositories.EntityModels;
using Repositories.Repository.Interface;
using Repositories.Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(BLW_FOR_KIDContext dBContext): base(dBContext) 
        {
        }

        
    }
}
