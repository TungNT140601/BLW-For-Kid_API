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
    }

    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository repository;
        public FavoriteService(IFavoriteRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Favorite> GetAllRecipeFavoriteOfOneCus(string cusId)
        {
            try
            {
                return repository.GetAll(x => x.CustomerId == cusId);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Task<bool> Add(Favorite favorite)
        {
            try
            {
                return repository.Add(favorite);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<bool> Delete(string cusId, string recipeId)
        {
            try
            {
                var fav = repository.GetAll(x => x.CustomerId == cusId && x.RecipeId == recipeId);
                return repository.Delete(fav);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
