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
        IEnumerable<Recipe> GetAll(bool isPremium, string? search, List<string>? ageIds, List<string>? mealIds);
        Task<bool> Add(Recipe recipe, List<IngredientOfRecipe> ingredientOfRecipes, List<Direction> directions, string staffCreateId);
        Task<bool> Update(Recipe recipe, List<IngredientOfRecipe> ingredientOfRecipes, List<Direction> directions, string staffUpdateId);
        Task<bool> Delete(string id);
        int CountFavorite(string recipeId);
        int CountRating(string recipeId);
        double AveRate(string recipeId);
        IEnumerable<Rating> GetRatings(string recipeId);
    }
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository recipeRepository;
        private readonly IAgeRepository ageRepository;
        private readonly IIngredientOfRecipeRepository ingredientOfRecipeRepository;
        private readonly IDirectionRepository directionRepository;
        private readonly IMealRepository mealRepository;
        private readonly IIngredientRepository ingredientRepository;
        private readonly IRatingRepository ratingRepository;
        private readonly IFavoriteRepository favoriteRepository;
        private readonly ICustomerRepository customerRepository;
        public RecipeService(IRecipeRepository recipeRepository, IAgeRepository ageRepository
            , IIngredientOfRecipeRepository ingredientOfRecipeRepository, IDirectionRepository directionRepository
            , IMealRepository mealRepository, IIngredientRepository ingredientRepository
            , IFavoriteRepository favoriteRepository, IRatingRepository ratingRepository
            , ICustomerRepository customerRepository)
        {
            this.recipeRepository = recipeRepository;
            this.ageRepository = ageRepository;
            this.ingredientOfRecipeRepository = ingredientOfRecipeRepository;
            this.directionRepository = directionRepository;
            this.mealRepository = mealRepository;
            this.ingredientRepository = ingredientRepository;
            this.ratingRepository = ratingRepository;
            this.favoriteRepository = favoriteRepository;
            this.customerRepository = customerRepository;
        }
        public async Task<bool> Add(Recipe recipe, List<IngredientOfRecipe> ingredientOfRecipes, List<Direction> directions, string staffCreateId)
        {
            var recipeId = "";
            try
            {
                //Gen Id
                recipe.RecipeId = AutoGenId.AutoGenerateId();
                recipeId = recipe.RecipeId;

                //Add Staff Create
                recipe.StaffCreate = staffCreateId;
                recipe.CreateTime = DateTime.Now;

                //Add Staff Update
                recipe.StaffUpdate = staffCreateId;
                recipe.UpdateTime = DateTime.Now;

                //Check Meal Exist
                var meal = await mealRepository.Get(recipe.MealId);
                if (meal == null)
                {
                    throw new Exception("Not Found Meal");
                }

                //Check Age Exist
                var age = await ageRepository.Get(recipe.AgeId);
                if (age == null)
                {
                    throw new Exception("Not Found Age");
                }

                recipe.IsDelete = false;
                recipe.IngredientOfRecipes = new List<IngredientOfRecipe>();
                recipe.Directions = new List<Direction>();

                //Add Ingredient In Recipe
                foreach (var ingredientOfRecipe in ingredientOfRecipes)
                {
                    ingredientOfRecipe.RecipeId = recipe.RecipeId;
                    //Check Ingredient Exist
                    var ingredient = await ingredientRepository.Get(ingredientOfRecipe.IngredientId);
                    if (ingredient == null)
                    {
                        throw new Exception("Not Found Ingredient");
                    }
                }

                //checkAddIngredient = await ingredientOfRecipeRepository.AddRange(ingredientOfRecipes);
                recipe.IngredientOfRecipes = ingredientOfRecipes;

                //Add Direction Of Recipe
                foreach (var direction in directions)
                {
                    direction.RecipeId = recipe.RecipeId;
                    direction.DirectionId = AutoGenId.AutoGenerateId();
                }

                //checkAddDirection = await directionRepository.AddRange(directions);
                recipe.Directions = directions;

                //Flag
                var checkAddRecipe = await recipeRepository.Add(recipe);

                if (!checkAddRecipe)
                {
                    throw new Exception("Add Fail");
                }
                return true;
            }
            catch (Exception ex)
            {
                await ingredientOfRecipeRepository.RemoveRange(recipeId);
                await directionRepository.RemoveRange(recipeId);
                await recipeRepository.Delete(recipeId);
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                var recipe = await recipeRepository.Get(id);
                if (recipe == null)
                {
                    throw new Exception("Not Found Recipe");
                }
                else
                {
                    recipe.IsDelete = true;
                    return await recipeRepository.Update(id, recipe);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Recipe> Get(string id)
        {
            try
            {
                var recipe = await recipeRepository.Get(id);
                if (recipe != null)
                {
                    recipe.Directions = directionRepository.GetDirectionOfRecipe(recipe.RecipeId).ToList();
                    recipe.IngredientOfRecipes = ingredientOfRecipeRepository.GetAll(x => x.RecipeId == recipe.RecipeId).ToList();
                    recipe.Age = await ageRepository.Get(recipe.AgeId);
                    recipe.Meal = await mealRepository.Get(recipe.MealId);
                    recipe.Calories = 0;
                    recipe.Fat = 0;
                    recipe.Carbohydrate = 0;
                    recipe.Protein = 0;
                    foreach (var ingredientOfRecipe in recipe.IngredientOfRecipes)
                    {
                        var ingredient = await ingredientRepository.Get(ingredientOfRecipe.IngredientId);
                        recipe.Calories = ingredientOfRecipe.Quantity * ingredient.Calories;
                        recipe.Fat = ingredientOfRecipe.Quantity * ingredient.Fat;
                        recipe.Carbohydrate = ingredientOfRecipe.Quantity * ingredient.Carbohydrate;
                        recipe.Protein = ingredientOfRecipe.Quantity * ingredient.Protein;
                    }
                    recipe.Calories = (double)Math.Ceiling((decimal)recipe.Calories.Value);
                    recipe.Fat = (double)Math.Ceiling((decimal)recipe.Fat.Value);
                    recipe.Carbohydrate = (double)Math.Ceiling((decimal)recipe.Carbohydrate.Value);
                    recipe.Protein = (double)Math.Ceiling((decimal)recipe.Protein.Value);
                }
                return recipe;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Recipe> GetAll(bool isPremium, string? search, List<string>? ageIds, List<string>? mealIds)
        {
            try
            {
                if (ageIds != null)
                {
                    if (ageIds.Count() == 0)
                    {
                        ageIds = null;
                    }
                    else if (ageIds.Count() == 1 && string.IsNullOrEmpty(ageIds[0]))
                    {
                        ageIds = null;
                    }
                }

                if (mealIds != null)
                {
                    if (mealIds.Count() == 0)
                    {
                        mealIds = null;
                    }
                    else if (mealIds.Count() == 1 && string.IsNullOrEmpty(mealIds[0]))
                    {
                        mealIds = null;
                    }
                }

                var recipes = new List<Recipe>();
                if (isPremium)
                {
                    recipes = recipeRepository.GetAll(x => x.IsDelete == false
                    && (search == null || (search != null && x.RecipeName.Trim().ToLower().Contains(search.Trim().ToLower())))
                    && (ageIds == null || (ageIds != null && ageIds.Count() > 0 && ageIds.Contains(x.AgeId)))
                    && (mealIds == null || (mealIds != null && mealIds.Count() > 0 && mealIds.Contains(x.MealId)))
                    ).ToList();
                    foreach (var recipe in recipes)
                    {
                        recipe.Age = ageRepository.Get(recipe.AgeId).Result;
                        recipe.Meal = mealRepository.Get(recipe.MealId).Result;
                    }
                }
                else
                {
                    recipes = recipeRepository.GetAll(x => x.ForPremium == false && x.IsDelete == false
                    && (search == null || (search != null && x.RecipeName.Trim().ToLower().Contains(search.Trim().ToLower())))
                    && (ageIds == null || (ageIds != null && ageIds.Count() > 0 && ageIds.Contains(x.AgeId)))
                    && (mealIds == null || (mealIds != null && mealIds.Count() > 0 && mealIds.Contains(x.MealId)))
                    ).ToList();
                    foreach (var recipe in recipes)
                    {
                        recipe.Age = ageRepository.Get(recipe.AgeId).Result;
                        recipe.Meal = mealRepository.Get(recipe.MealId).Result;
                    }
                }
                return recipes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(Recipe recipe, List<IngredientOfRecipe> ingredientOfRecipes, List<Direction> directions, string staffUpdateId)
        {
            //Create bk
            var recipeBK = new Recipe();
            var iOrBK = new List<IngredientOfRecipe>();
            var bkDirection = new List<Direction>();
            try
            {
                var check = await recipeRepository.Get(recipe.RecipeId);
                if (check == null)
                {
                    throw new Exception("Not Found Recipe");
                }
                else
                {
                    recipeBK = check;
                    //add value to bk
                    iOrBK = ingredientOfRecipeRepository.GetAll(x => x.RecipeId == check.RecipeId).ToList();
                    bkDirection = directionRepository.GetDirectionOfRecipe(check.RecipeId).ToList();
                }
                //update updateTime vs staffUpdate
                check.UpdateTime = DateTime.Now;
                check.StaffUpdate = staffUpdateId;

                //Check Meal Exist
                var meal = await mealRepository.Get(recipe.MealId);
                if (meal == null)
                {
                    throw new Exception("Not Found Meal");
                }
                else
                {
                    check.MealId = recipe.MealId;
                }

                //Check Age Exist
                var age = await ageRepository.Get(recipe.AgeId);
                if (age == null)
                {
                    throw new Exception("Not Found Age");
                }
                else
                {
                    check.AgeId = recipe.AgeId;
                }

                //Update other info
                check.RecipeName = recipe.RecipeName;

                check.RecipeImage = recipe.RecipeImage;

                check.Protein = recipe.Protein;

                check.Carbohydrate = recipe.Carbohydrate;

                check.Fat = recipe.Fat;

                check.Calories = recipe.Calories;

                check.ForPremium = recipe.ForPremium;

                check.IsDelete = false;

                check.CookTime = recipe.CookTime;

                check.StandTime = recipe.StandTime;

                check.PrepareTime = recipe.PrepareTime;

                check.TotalTime = recipe.TotalTime;

                check.Servings = recipe.Servings;

                check.RecipeDesc = recipe.RecipeDesc;

                //Flag
                var checkUpdateRecipe = false;
                //var checkUpdateIngredient = false;
                //var checkUpdateDirection = false;

                //Remove Old Data in db to Add new to Update
                var checkIngredient = await ingredientOfRecipeRepository.RemoveRange(check.RecipeId);
                if (checkIngredient)
                {
                    foreach (var ingredientOfRecipe in ingredientOfRecipes)
                    {
                        ingredientOfRecipe.RecipeId = recipe.RecipeId;
                        //Check Ingredient Exist
                        var ingredient = await ingredientRepository.Get(ingredientOfRecipe.IngredientId);
                        if (ingredient == null)
                        {
                            throw new Exception("Not Found Ingredient");
                        }
                    }
                    //checkUpdateIngredient = await ingredientOfRecipeRepository.AddRange(ingredientOfRecipes);
                    check.IngredientOfRecipes = ingredientOfRecipes;
                }
                else
                {
                    throw new Exception("Error Ingredient when Remove OLD datas in db then Add NEW datas to Update");
                }

                //Remove Old Data in db to Add new to Update
                var checkDirection = await directionRepository.RemoveRange(check.RecipeId);
                if (checkDirection)
                {
                    foreach (var direction in directions)
                    {
                        direction.DirectionId = AutoGenId.AutoGenerateId();
                        direction.RecipeId = recipe.RecipeId;
                    }
                    //checkUpdateDirection = await directionRepository.AddRange(directions);
                    check.Directions = directions;
                }
                else
                {
                    throw new Exception("Error Direction when Remove OLD datas in db then Add NEW datas to Update");
                }

                //Update Ingredient In Recipe via Direcion Of Recipe

                checkUpdateRecipe = await recipeRepository.Update(check.RecipeId, check);


                //Check if something false, then throw exception
                //if (!(checkUpdateRecipe && checkUpdateIngredient && checkUpdateDirection))
                if (!(checkUpdateRecipe))
                {
                    throw new Exception("Update Fail");
                }

                return true;
            }
            catch (Exception ex)
            {
                //Update to before update
                await recipeRepository.Update(recipeBK.RecipeId, recipeBK);
                await ingredientOfRecipeRepository.RemoveRange(recipeBK.RecipeId);
                await ingredientOfRecipeRepository.AddRange(iOrBK);
                await directionRepository.RemoveRange(recipeBK.RecipeId);
                await directionRepository.AddRange(bkDirection);
                throw new Exception(ex.Message);
            }
        }
        public int CountFavorite(string recipeId)
        {
            try
            {
                return favoriteRepository.GetAll(x => x.RecipeId == recipeId).Count();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int CountRating(string recipeId)
        {
            try
            {
                return ratingRepository.GetAll(x => x.RecipeId == recipeId).Count();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public double AveRate(string recipeId)
        {

            try
            {
                var ratings = ratingRepository.GetAll(x => x.RecipeId == recipeId).ToList();
                var count = ratings.Count();
                if (count == 0)
                {
                    return 0;
                }
                var totalVote = ratings.Sum(x => x.Rate);
                var aveRate = (double)(totalVote / count);
                return aveRate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<Rating> GetRatings(string recipeId)
        {
            try
            {
                var ratings = ratingRepository.GetAll(x => x.RecipeId == recipeId).ToList();
                foreach (var item in ratings)
                {
                    item.Customer = customerRepository.Get(item.CustomerId).Result;
                }
                return ratings;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
