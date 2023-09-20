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
        Task<Rating> GetRating(string id);
        Task<bool> Add(Rating rating);
        Task<bool> Delete(string id);
        Task<bool> Update(Rating rating);
    }

    public class RatingService : IRatingService
    {
        private readonly IRatingRepository repository;
        public RatingService(IRatingRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Rating> GetAllRatingOfOneCus()
        {
            try
            {
                return repository.GetAll(x => x.IsDelete == false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Rating> GetRating(string id)
        {
            try
            {
                var rating = repository.Get(id); 
                if(rating != null)
                {
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
        public Task<bool> Add(Rating rating)
        {
            try
            {
                rating.RatingId = AutoGenId.AutoGenerateId();
                rating.Date = DateTime.Now;
                rating.IsDelete = false;
                return repository.Add(rating);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                var rating = await repository.Get(id);
                if(rating != null) 
                {
                    rating.IsDelete = true;
                    return await repository.Update(id, rating);
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

        public async Task<bool> Update(Rating rating)
        {
            try
            {
                var ratingOfCus = await repository.Get(rating.RatingId);
                if(ratingOfCus != null)
                {
                    ratingOfCus.Rate = rating.Rate;
                    ratingOfCus.Comment = rating.Comment;
                    ratingOfCus.RatingImage = rating.RatingImage;
                    return await repository.Update(rating.RatingId, ratingOfCus);
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
    }
}
