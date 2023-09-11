using Repositories.EntityModels;
using Repositories.Repository.Interface;
using Repositories.Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IRecipeService
    {
        Task<Recipe> Get(string id);
        IEnumerable<Recipe> GetAll();
        Task<bool> Add(Recipe recipe, List<IngredientOfRecipe> ingredientOfRecipes);
        Task<bool> Update(Recipe recipe, List<IngredientOfRecipe> ingredientOfRecipes);
        Task<bool> Delete(string id);
    }
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository recipeRepository;
        private readonly IAgeRepository ageRepository;
        private readonly IIngredientOfRecipeRepository ingredientOfRecipeRepository;
        public RecipeService(IRecipeRepository recipeRepository, IAgeRepository ageRepository, IIngredientOfRecipeRepository ingredientOfRecipeRepository)
        {
            this.recipeRepository = recipeRepository;
            this.ageRepository = ageRepository;
            this.ingredientOfRecipeRepository = ingredientOfRecipeRepository;
        }
        public async Task<bool> Add(Recipe recipe, List<IngredientOfRecipe> ingredientOfRecipes)
        {
            var recipeId = "";
            try
            {
                recipe.RecipeId = AutoGenId.AutoGenerateId();
                recipeId = recipe.RecipeId;
                recipe.CreateTime = DateTime.Now;
                recipe.UpdateTime = DateTime.Now;
                //check meal
                if (ageRepository.Get(recipe.AgeId) == null)
                {
                    throw new Exception("Not Found Age");
                }
                recipe.IsDelete = false;
                recipe.IngredientOfRecipes = new List<IngredientOfRecipe>();
                var checkAdd = await recipeRepository.Add(recipe);
                if (checkAdd)
                {
                    foreach (var ingredientOfRecipe in ingredientOfRecipes)
                    {
                        ingredientOfRecipe.RecipeId = recipe.RecipeId;
                        //check ingredient
                    }
                }
                return checkAdd && await ingredientOfRecipeRepository.AddRange(ingredientOfRecipes);
            }
            catch (Exception ex)
            {
                await ingredientOfRecipeRepository.RemoveRange(recipeId);
                await recipeRepository.Delete(recipeId);
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> Delete(string id)
        {
            try
            {
                var recipe = recipeRepository.Get(id).Result;
                if (recipe == null)
                {
                    throw new Exception("Not Found Recipe");
                }
                else
                {
                    recipe.IsDelete = true;
                    return recipeRepository.Update(id, recipe);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Recipe> Get(string id)
        {
            try
            {
                return recipeRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Recipe> GetAll()
        {
            try
            {
                return recipeRepository.GetAll(x => x.IsDelete == false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(Recipe recipe, List<IngredientOfRecipe> ingredientOfRecipes)
        {
            var recipeBK = new Recipe();
            var iOrBK = new List<IngredientOfRecipe>();
            try
            {
                var check = recipeRepository.Get(recipe.RecipeId).Result;
                if (check == null)
                {
                    throw new Exception("Not Found Recipe");
                }
                else
                {
                    recipeBK = check;
                    iOrBK = ingredientOfRecipeRepository.GetAll(x => x.RecipeId == check.RecipeId).ToList();
                }
                check.UpdateTime = DateTime.Now;
                check.StaffUpdate = recipe.StaffUpdate;
                check.RecipeName = recipe.RecipeName;
                check.RecipeImage = recipe.RecipeImage;
                check.Protein = recipe.Protein;
                check.Carbohydrate = recipe.Carbohydrate;
                check.Fat = recipe.Fat;
                check.Calories = recipe.Calories;
                //check meal
                if (ageRepository.Get(recipe.AgeId) == null)
                {
                    throw new Exception("Not Found Age");
                }
                else
                {
                    check.AgeId = recipe.AgeId;
                }
                check.ForPremium = recipe.ForPremium;
                check.IsDelete = false;
                check.IngredientOfRecipes = new List<IngredientOfRecipe>();
                var checkAdd = await recipeRepository.Update(check.RecipeId, check);
                if (checkAdd)
                {
                    await ingredientOfRecipeRepository.RemoveRange(check.RecipeId);
                    foreach (var ingredientOfRecipe in ingredientOfRecipes)
                    {
                        ingredientOfRecipe.RecipeId = recipe.RecipeId;
                        //check ingredient
                    }
                }
                return checkAdd && await ingredientOfRecipeRepository.AddRange(ingredientOfRecipes);
            }
            catch (Exception ex)
            {
                await recipeRepository.Update(recipeBK.RecipeId, recipeBK);
                await ingredientOfRecipeRepository.RemoveRange(recipeBK.RecipeId);
                await ingredientOfRecipeRepository.AddRange(iOrBK);
                throw new Exception(ex.Message);
            }
        }
    }
}
