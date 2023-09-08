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
        Task<bool> Add(Recipe recipe);
        Task<bool> Update(Recipe recipe);
        Task<bool> Delete(string id);
    }
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository recipeRepository;
        public RecipeService(IRecipeRepository recipeRepository)
        {
            this.recipeRepository = recipeRepository;
        }
        public Task<bool> Add(Recipe recipe)
        {
            try
            {
                recipe.RecipeId = AutoGenId.AutoGenerateId();
                recipe.CreateTime = DateTime.Now;
                recipe.UpdateTime = DateTime.Now;

                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Recipe> Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recipe> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Recipe recipe)
        {
            throw new NotImplementedException();
        }
    }
}
