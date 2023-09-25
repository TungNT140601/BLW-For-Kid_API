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
    public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(BLW_FOR_KIDContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> DeleteFav(Favorite favorite)
        {
            try
            {
                dbSet.Remove(favorite);
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
