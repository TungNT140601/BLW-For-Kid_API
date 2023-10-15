using Repositories.EntityModels;
using Repositories.Repository.Interface;
using Repositories.Ultilities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IIngredientService
    {
        Task<Ingredient> Get(string id);
        IEnumerable<Ingredient> GetAll();
        Task<bool> Add(Ingredient ingredient);
        Task<bool> Update(Ingredient ingredient);
        Task<bool> Delete(Ingredient ing);
    }
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository ingredientRepository;
        public IngredientService(IIngredientRepository ingredientRepository)
        {
            this.ingredientRepository = ingredientRepository;
        }

        public Task<bool> Add(Ingredient ingredient)
        {
            try
            {
                if (ingredientRepository.GetAll(x => x.IngredientName == ingredient.IngredientName && x.IsDelete == false).Any())
                {
                    throw new Exception("Name exist");
                }
                ingredient.IngredientId = AutoGenId.AutoGenerateId();
                ingredient.CreateTime = DateTime.Now;
                ingredient.UpdateTime = DateTime.Now;
                ingredient.DeleteDate = DateTime.Now;
                ingredient.IngredientImage = null;
                ingredient.IsDelete = true;
                return ingredientRepository.Add(ingredient);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Ingredient> Get(string id)
        {
            try
            {
                return ingredientRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Ingredient> GetAll()
        {
            try
            {
                return ingredientRepository.GetAll(x => x.IsDelete == true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(Ingredient ing)
        {
            try
            {
                var ingredient = await ingredientRepository.Get(ing.IngredientId);
                if (ingredient != null)
                {

                    return await ingredientRepository.Update(ing.IngredientId, ingredient);
                }
                else
                {
                    throw new Exception("Not Found Ingredient");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> Update(Ingredient item)
        {
            try
            {
                var ingredient = await ingredientRepository.Get(item.IngredientId);
                if (ingredientRepository.GetAll(x => x.IngredientId != item.IngredientId && x.IngredientName == item.IngredientName && x.IsDelete == false).Any())
                {
                    throw new Exception("Name exist");
                }
                ingredient.IngredientName = item.IngredientName;
                ingredient.Measure = item.Measure;
                ingredient.Protein = item.Protein;
                ingredient.Carbohydrate = item.Carbohydrate;
                ingredient.Fat = item.Fat;
                ingredient.Calories = item.Calories;

                return await ingredientRepository.Update(ingredient.IngredientId, ingredient);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
