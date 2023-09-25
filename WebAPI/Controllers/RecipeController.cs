using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.EntityModels;
using Services;
using System.Security.Claims;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRecipeService recipeService;
        private readonly ICustomerService customerService;
        private readonly IIngredientService ingredientService;
        public RecipeController(IRecipeService recipeService, ICustomerService customerService, IMapper mapper, IIngredientService ingredientService)
        {
            this.recipeService = recipeService;
            this.customerService = customerService;
            this.mapper = mapper;
            this.ingredientService = ingredientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipe(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    throw new Exception("Id Null");
                }
                var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

                var isPremium = false;
                if (role != null)
                {
                    if (role == CommonValues.ADMIN || role == CommonValues.STAFF)
                    {
                        isPremium = true;
                    }
                    else
                    {
                        if (role == CommonValues.CUSTOMER)
                        {
                            var cusId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                            var cus = await customerService.CheckPremium(cusId);
                            isPremium = cus.IsPremium.Value;
                        }
                    }
                }

                var recipe = await recipeService.Get(id);

                if (recipe == null)
                {
                    throw new Exception("Not Found Recipe");
                }
                else
                {
                    var recipeVM = await ChangeToVMDetail(recipe);
                    if (isPremium)
                    {
                        return StatusCode(200, new
                        {
                            Status = "Success",
                            IsPremium = true,
                            Data = recipeVM
                        });
                    }
                    else if (!isPremium && !recipe.ForPremium.Value)
                    {
                        return StatusCode(200, new
                        {
                            Status = "Success",
                            IsPremium = false,
                            Data = recipeVM
                        });
                    }
                    else
                    {
                        throw new Exception("Not Found Recipe");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    Message = ex.Message,
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecipe()
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                var isPremium = false;
                if (role != null)
                {
                    if (role == CommonValues.ADMIN || role == CommonValues.STAFF)
                    {
                        isPremium = true;
                    }
                    else
                    {
                        if (role == CommonValues.CUSTOMER)
                        {
                            var cusId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                            var cus = await customerService.CheckPremium(cusId);
                            isPremium = cus.IsPremium.Value;
                        }
                    }
                }
                var recipes = recipeService.GetAll(isPremium).ToList();
                var recipeVMs = ChangeToVMList(recipes);
                return StatusCode(200, new
                {
                    Status = "Success",
                    Data = recipeVMs
                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    Message = ex.Message,
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRecipe(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    throw new Exception("Id Null");
                }

                var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                if (role == null)
                {
                    return StatusCode(401, new
                    {
                        Status = "Unauthorize",
                        Message = "Not Login"
                    });
                }

                if (!(role == CommonValues.ADMIN || role == CommonValues.STAFF))
                {
                    throw new Exception("Role Denied");
                }

                var check = await recipeService.Delete(id);

                return check ? StatusCode(200, new
                {
                    Status = "Success",
                    Message = "Delete Success"
                }) : StatusCode(200, new
                {
                    Status = "Fail",
                    Message = "Delete Fail"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    Message = ex.Message,
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipe(RecipeAddUpdateVM recipeVM)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                if (role == null)
                {
                    return StatusCode(401, new
                    {
                        Status = "Unauthorize",
                        Message = "Not Login"
                    });
                }

                if (!(role == CommonValues.ADMIN || role == CommonValues.STAFF))
                {
                    throw new Exception("Role Denied");
                }

                var staffId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var recipe = ValidateRecipe(recipeVM);

                var check = await recipeService.Add(recipe, recipe.IngredientOfRecipes.ToList(), recipe.Directions.ToList(), staffId);


                return check ? StatusCode(200, new
                {
                    Status = "Success",
                    Message = "Add Success"
                }) : StatusCode(200, new
                {
                    Status = "Fail",
                    Message = "Add Fail"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    Message = ex.Message,
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRecipe(string id, RecipeAddUpdateVM recipeVM)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                if (role == null)
                {
                    return StatusCode(401, new
                    {
                        Status = "Unauthorize",
                        Message = "Not Login"
                    });
                }

                if (!(role == CommonValues.ADMIN || role == CommonValues.STAFF))
                {
                    throw new Exception("Role Denied");
                }
                if (id == null || id != recipeVM.RecipeId)
                {
                    throw new Exception("Id Invalid");
                }

                var staffId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var recipe = ValidateRecipe(recipeVM);

                var check = await recipeService.Update(recipe, recipe.IngredientOfRecipes.ToList(), recipe.Directions.ToList(), staffId);


                return check ? StatusCode(200, new
                {
                    Status = "Success",
                    Message = "Update Success"
                }) : StatusCode(200, new
                {
                    Status = "Fail",
                    Message = "Update Fail"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    Message = ex.Message,
                });
            }
        }

        private Recipe ValidateRecipe(RecipeAddUpdateVM recipeVM)
        {
            var errMsg = "";

            #region Name
            if (string.IsNullOrWhiteSpace(recipeVM.RecipeName))
            {
                errMsg += ";Recipe Name Empty";
            }
            #endregion
            #region Meal
            if (string.IsNullOrWhiteSpace(recipeVM.MealId))
            {
                errMsg += ";Select Meal";
            }
            #endregion
            #region Image
            var image = "";
            if (recipeVM.RecipeImage != null)
            {
                foreach (var item in recipeVM.RecipeImage)
                {
                    image += item + ";";
                }
            }
            else
            {
                errMsg += ";Select Recipe's Image";
            }
            #endregion
            #region AgeId
            if (string.IsNullOrWhiteSpace(recipeVM.AgeId))
            {
                errMsg += ";Select Age";
            }
            #endregion
            #region ForPremium
            if (recipeVM.ForPremium == null)
            {
                recipeVM.ForPremium = false;
            }
            #endregion
            #region DirectionVMs
            var directions = new List<Direction>();
            if (recipeVM.DirectionVMs.Any())
            {

                var checkValidNum = false;
                var checkEmptyDesc = false;

                foreach (var directionVM in recipeVM.DirectionVMs)
                {
                    //Num
                    if (directionVM.DirectionNum == null || directionVM.DirectionNum < 1)
                    {
                        checkValidNum = true;
                    }

                    //Desc
                    if (string.IsNullOrWhiteSpace(directionVM.DirectionDesc))
                    {
                        checkEmptyDesc = true;
                    }

                    //Image
                    var imageDirection = "";
                    if (directionVM.DirectionImage.Any())
                    {
                        foreach (var item in directionVM.DirectionImage)
                        {
                            imageDirection += item + ";";
                        }
                    }
                    directions.Add(new Direction
                    {
                        DirectionDesc = directionVM.DirectionDesc,
                        DirectionNum = directionVM.DirectionNum,
                        DirectionImage = imageDirection,
                    });
                }

                if (checkValidNum)
                {
                    errMsg += ";Invalid Recipe's Step Num";
                }
                if (checkEmptyDesc)
                {
                    errMsg += ";Recipe's Step Desc Empty";
                }
            }
            else
            {
                errMsg += ";Add at least 1 step for Recipe";
            }
            #endregion
            #region Ingredient
            var ingredientOfRecipe = new List<IngredientOfRecipe>();
            if (recipeVM.IngredientOfRecipeVMs.Any())
            {
                var checkValidQuantity = false;
                var duplicate = recipeVM.IngredientOfRecipeVMs.GroupBy(x => x.IngredientId).Any(x => x.Count() > 0);
                foreach (var item in recipeVM.IngredientOfRecipeVMs)
                {
                    if (item.Quantity == null || item.Quantity <= 0)
                    {
                        checkValidQuantity = true;
                    }
                    else
                    {
                        ingredientOfRecipe.Add(new IngredientOfRecipe
                        {
                            IngredientId = item.IngredientId,
                            Quantity = item.Quantity,
                        });
                    }
                }
                if (checkValidQuantity)
                {
                    errMsg += ";Invalid Quantiy Of Ingredient";
                }
                if (duplicate)
                {
                    errMsg += ";Duplicate Ingredient";
                }
            }
            else
            {
                errMsg += ";Select at least 1 Ingredient for Recipe";
            }
            #endregion

            return new Recipe
            {
                RecipeId = recipeVM.RecipeId,
                RecipeName = recipeVM.RecipeName,
                RecipeImage = image,
                AgeId = recipeVM.AgeId,
                MealId = recipeVM.MealId,
                ForPremium = recipeVM.ForPremium,
                Directions = directions,
                IngredientOfRecipes = ingredientOfRecipe,
            };
        }

        private async Task<RecipeVM> ChangeToVMDetail(Recipe recipe)
        {
            try
            {
                var recipeVM = mapper.Map<RecipeVM>(recipe);

                #region Direction
                var directionVMs = new List<DirectionVM>();
                if (recipe.Directions.Any())
                {
                    foreach (var direction in recipe.Directions)
                    {
                        directionVMs.Add(mapper.Map<DirectionVM>(direction));
                    }
                    directionVMs = directionVMs.OrderBy(x => x.DirectionNum).ToList();
                    recipeVM.DirectionVMs = directionVMs;
                }
                #endregion

                #region IngredientOfRecipe
                var ingredientOfRecipeVMs = new List<IngredientOfRecipeVM>();
                if (recipe.IngredientOfRecipes.Any())
                {
                    foreach (var ingredientOfRecipe in recipe.IngredientOfRecipes)
                    {
                        var ingredient = await ingredientService.Get(ingredientOfRecipe.IngredientId);
                        ingredientOfRecipeVMs.Add(new IngredientOfRecipeVM
                        {
                            IngredientId = ingredientOfRecipe.IngredientId,
                            IngredientImage = ingredient.IngredientImage,
                            IngredientName = ingredient.IngredientName,
                            Measure = ingredient.Measure,
                            Quantity = ingredientOfRecipe.Quantity,
                            RecipeId = ingredientOfRecipe.RecipeId
                        });
                    }
                    ingredientOfRecipeVMs = ingredientOfRecipeVMs.OrderBy(x => x.IngredientName).ToList();
                    recipeVM.IngredientOfRecipeVMs = ingredientOfRecipeVMs;
                }
                #endregion

                #region Rating
                var ratingVMs = new List<RatingVM>();
                recipe.Ratings = recipeService.GetRatings(recipe.RecipeId).ToList();
                if (recipe.Ratings.Any())
                {
                    foreach (var rating in recipe.Ratings)
                    {
                        ratingVMs.Add(new RatingVM
                        {
                            Avatar = rating.Customer.Avatar,
                            RecipeId = rating.RecipeId,
                            Comment = rating.Comment,
                            CustomerId = rating.CustomerId,
                            Date = rating.Date,
                            Fullname = rating.Customer.Fullname,
                            Rate = rating.Rate,
                            RatingImage = rating.RatingImage
                        });
                    }
                    recipeVM.RatingVMs = ratingVMs;
                }
                recipeVM.TotalRate = recipe.Ratings.Count();
                recipeVM.AveRate = recipeService.AveRate(recipe.RecipeId);
                #endregion

                #region TotalFavorite
                recipeVM.TotalFavorite = recipeService.CountFavorite(recipe.RecipeId);
                #endregion

                return recipeVM;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private IEnumerable<RecipeVM> ChangeToVMList(List<Recipe> recipes)
        {
            try
            {
                var recipeVMs = new List<RecipeVM>();
                foreach (var recipe in recipes)
                {
                    var recipeVM = mapper.Map<RecipeVM>(recipe);

                    #region Rating
                    recipeVM.TotalRate = recipeService.CountRating(recipe.RecipeId);
                    recipeVM.AveRate = recipeService.AveRate(recipe.RecipeId);
                    #endregion

                    #region TotalFavorite
                    recipeVM.TotalFavorite = recipeService.CountFavorite(recipe.RecipeId);
                    #endregion
                    recipeVMs.Add(recipeVM);
                }

                return recipeVMs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
