using Repositories.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository.Interface
{
    public interface IDirectionRepository : IGenericRepository<Direction>
    {
        Task<bool> AddRange(List<Direction> directions);
        Task<bool> RemoveRange(string recipeId);
        IEnumerable<Direction> GetDirectionOfRecipe(string recipeId);
    }
}
