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
    public interface IRatingService
    {
        IEnumerable<Rating> GetAllRatingOfOneCus();
        Task<Rating> GetRating(string cusId, string recipeId);
        Task<bool> AddOrUpdate(Rating rating);
        Task<bool> Delete(string cusId, string recipeId);
        int GetAllRatingOfRecipe(string recipeId);
        int AvgRatingOfRecipe(string recipeId);
    }

    public class RatingService : IRatingService
    {
        private readonly IRatingRepository repository;
        private readonly ICustomerRepository cusRepository;
        private readonly IRecipeRepository recipeRepository;
        public RatingService(IRatingRepository repository, ICustomerRepository cusRepository, IRecipeRepository recipeRepository)
        {
            this.repository = repository;
            this.cusRepository = cusRepository;
            this.recipeRepository = recipeRepository;
        }
        public IEnumerable<Rating> GetAllRatingOfOneCus()
        {
            try
            {
                var ratings = repository.GetAll(x => x.IsDelete == false);
                foreach (var rating in ratings)
                {
                    rating.Customer = cusRepository.Get(rating.CustomerId).Result;
                    rating.Recipe = recipeRepository.Get(rating.RecipeId).Result;
                }
                return ratings;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Rating> GetRating(string cusId,string recipeId)
        {
            try
            {
                var rating = repository.GetAll(x => x.CustomerId == cusId && x.RecipeId == recipeId).FirstOrDefault();
                if(rating != null)
                {                   
                    rating.Customer = await cusRepository.Get(cusId);
                    rating.Recipe = await recipeRepository.Get(recipeId);
                    return rating;
                }
                else
                {
                    throw new Exception("Not Found Rating Of Customer");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> AddOrUpdate(Rating rating)
        {
            try
            {
                var ratingOfCus = repository.GetAll(x => x.CustomerId == rating.CustomerId && x.RecipeId == rating.RecipeId).FirstOrDefault();
                if (ratingOfCus != null)
                {
                    ratingOfCus.Rate = rating.Rate;
                    ratingOfCus.Comment = rating.Comment;
                    ratingOfCus.RatingImage = rating.RatingImage;
                    return await repository.Update(rating.RatingId, ratingOfCus);
                }
                else
                {
                    rating.RatingId = AutoGenId.AutoGenerateId();
                    rating.Date = DateTime.Now;
                    rating.IsDelete = false;
                    return await repository.Add(rating);
                }              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(string cusId, string recipeId)
        {
            try
            {
                var rating = repository.GetAll(x => x.CustomerId == cusId && x.RatingId == recipeId).FirstOrDefault();
                if(rating != null) 
                {
                    return await repository.DeleteWithCondition(rating);
                }
                else
                {
                    throw new Exception("Not Found Rating Of Customer");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetAllRatingOfRecipe(string recipeId)
        {
            try
            {
                var totalRating = repository.GetAll(x => x.RecipeId == recipeId);
                int count = 0;
                foreach (var rating in totalRating)
                {
                    count++;
                }               
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int AvgRatingOfRecipe(string recipeId)
        {
            try
            {
                var totalRatingOfRecipe = GetAllRatingOfRecipe(recipeId);
                var totalRating = repository.GetAll(x => x.IsDelete == false);
                var listRtingofRecipeId = new List<Rating>();
                double count = 0;
                foreach (var rating in totalRating)
                {
                    if (rating.RecipeId == recipeId)
                    {
                        listRtingofRecipeId.Add(rating);
                    }
                }
                foreach (var rating in listRtingofRecipeId)
                {
                    count +=(double) rating.Rate;
                }
                var avgRating = count / totalRatingOfRecipe;
                var remainder = avgRating - Math.Floor(avgRating);
                if (remainder < 0.5)
                {
                    avgRating = avgRating - remainder;
                }
                else if (remainder >= 0.5)
                {
                    avgRating = (avgRating - remainder) + 1;
                }
                return (int)avgRating;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
