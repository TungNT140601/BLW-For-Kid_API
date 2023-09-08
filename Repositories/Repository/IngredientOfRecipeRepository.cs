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
    public class IngredientOfRecipeRepository : GenericRepository<IngredientOfRecipe>, IIngredientOfRecipeRepository
    {
        public IngredientOfRecipeRepository(BLW_FOR_KIDContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AddRange(List<IngredientOfRecipe> ingredientOfRecipes)
        {
            try
            {
                foreach (var ingredientOfRecipe in ingredientOfRecipes)
                {
                    dbSet.Add(ingredientOfRecipe);
                }
                await dbContext.SaveChangesAsync();
                return true;
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
                var ingredientOfRecipes = dbSet.Where(x => x.RecipeId == recipeId).ToList();
                foreach (var ingredientOfRecipe in ingredientOfRecipes)
                {
                    dbSet.Remove(ingredientOfRecipe);
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
