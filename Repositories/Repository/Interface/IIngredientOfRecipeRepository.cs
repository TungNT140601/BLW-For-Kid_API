using Repositories.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository.Interface
{
    public interface IIngredientOfRecipeRepository : IGenericRepository<IngredientOfRecipe>
    {
        Task<bool> AddRange(List<IngredientOfRecipe> ingredientOfRecipes);
        Task<bool> RemoveRange(string recipeId);
    }
}
