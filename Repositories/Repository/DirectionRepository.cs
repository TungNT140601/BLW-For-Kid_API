using Microsoft.EntityFrameworkCore;
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
    public class DirectionRepository : GenericRepository<Direction>, IDirectionRepository
    {
        public DirectionRepository(BLW_FOR_KIDContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AddRange(List<Direction> directions)
        {
            try
            {
                await dbSet.AddRangeAsync(directions);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Direction> GetDirectionOfRecipe(string recipeId)
        {
            try
            {
                return dbSet.Where(x => x.RecipeId == recipeId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> RemoveRange(string recipeId)
        {
            try
            {
                dbSet.RemoveRange(dbSet.Where(x => x.RecipeId == recipeId).ToList());
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
