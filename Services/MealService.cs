using Repositories.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IMealService
    {
        IEnumerable<Meal> GetAllMeal();
        Task<Meal> GetMeal(string id);
        Task<bool> AddMeal(Meal age);
        Task<bool> UpdateMeal(Meal age);
        Task<bool> DeleteMeal(string id);
    }

    public class MealService : IAgeService
    {
        public Task<bool> AddAge(Age age)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAge(Age age)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAge(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Age> GetAge(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Age> GetAllAge()
        {
            throw new NotImplementedException();
        }
    }
}
