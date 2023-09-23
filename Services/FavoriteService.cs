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
                var fav = repository.GetAll(x => x.CustomerId == cusId);
                var check = false;
                foreach(var item in fav)
                {
                    if(item.RecipeId == recipeId)
                    {
                        check =  await repository.Delete(fav);
                    }
                }
                return check;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
