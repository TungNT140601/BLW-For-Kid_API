using Microsoft.VisualBasic;
using Repositories.EntityModels;
using Repositories.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IFavoriteService
    {
        IEnumerable<Favorite> GetAllRecipeFavoriteOfOneCus(string cusId);
        Task<bool> Add(Favorite favorite);
        Task<bool> Delete(string cusId, string recipeId);
        int TotalFavOnRecipe(string recipeId);
    }

    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository repository;
        private readonly ICustomerRepository cusRepository;
        private readonly IRecipeRepository recipeRepository;
        public FavoriteService(IFavoriteRepository repository, ICustomerRepository cusRepository, IRecipeRepository recipeRepository)
        {
            this.repository = repository;
            this.cusRepository = cusRepository;
            this.recipeRepository = recipeRepository;
        }

        public  IEnumerable<Favorite> GetAllRecipeFavoriteOfOneCus(string cusId)
        {
            try
            {
                var check =  repository.GetAll(x => x.CustomerId == cusId);
                foreach (var item in check)
                {
                    item.Customer = cusRepository.Get(item.CustomerId).Result;
                    item.Customer.Favorites = null;
                    item.Recipe = recipeRepository.Get(item.RecipeId).Result;
                    item.Recipe.Favorites = null;
                }
                return check;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> Add(Favorite favorite)
        {
            try
            {
                return await repository.Add(favorite);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> Delete(string cusId, string recipeId)
        {
            try
            {
                var fav = repository.GetAll(x => x.CustomerId == cusId && x.RecipeId == recipeId).FirstOrDefault();
                var check = false;
                if (fav != null)
                {
                    return await repository.DeleteFav(fav);
                }
                else
                {
                    throw new Exception("Not Found!!!");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int TotalFavOnRecipe(string recipeId)
        {
            try
            {
                var fav = repository.GetAll(x => x.RecipeId == recipeId);
                int count = 0;
                foreach (var item in fav)
                {
                    count++;
                }
                return count;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
