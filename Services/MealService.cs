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
    public interface IMealService
    {
        IEnumerable<Meal> GetAllMeal();
        Task<Meal> GetMeal(string id);
        Task<bool> AddMeal(Meal meal);
        Task<bool> UpdateMeal(Meal meal);
        Task<bool> DeleteMeal(Meal meal);
    }

    public class MealService : IMealService
    {
        private readonly IMealRepository repository;
        public MealService(IMealRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> AddMeal(Meal meal)
        {
            try
            {
                if (string.IsNullOrEmpty(meal.MealName))
                {
                    throw new Exception("MealName cannot be empty!!!");
                }
                else
                {
                    meal.MealId = AutoGenId.AutoGenerateId();
                    meal.CreateTime = DateTime.Now;
                    meal.IsDelete = false;
                    return await repository.Add(meal);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteMeal(Meal meal)
        {
            try
            {
                var check = await repository.Get(meal.MealId);
                if (check != null)
                {
                    check.DeleteDate = DateTime.Now;
                    check.IsDelete = true;
                    return await repository.Update(meal.MealId, check);
                }
                else
                {
                    throw new Exception("Not Found Meal");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Meal> GetAllMeal()
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

        public async Task<Meal> GetMeal(string id)
        {
            try
            {
                var check = await repository.Get(id);
                if (check != null)
                {
                    return check;
                }
                else
                {
                    throw new Exception("Not Found Meal");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateMeal(Meal meal)
        {
            try
            {
                var check = await repository.Get(meal.MealId);
                if (check != null)
                {
                    if (string.IsNullOrEmpty(meal.MealName))
                    {
                        throw new Exception("MealName cannot be empty!!!");
                    }
                    else
                    {
                        check.MealName = meal.MealName;
                        check.StaffUpdate = meal.StaffUpdate;
                        check.UpdateTime = DateTime.Now;
                        return await repository.Update(meal.MealId, check);
                    }
                }
                else
                {
                    throw new Exception("Not Found Meal");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
