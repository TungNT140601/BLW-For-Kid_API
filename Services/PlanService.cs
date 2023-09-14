using Repositories.EntityModels;
using Repositories.Repository;
using Repositories.Repository.Interface;
using Repositories.Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPlanService
    {
        Task<Plan> GetPlan(string id);
        Task<IEnumerable<Plan>> GetPlans();
        Task<bool> Add(Plan plan, List<PlanDetail> planDetails);
        Task<bool> Update(Plan plan, List<PlanDetail> planDetails);
        Task<bool> Delete(string id);
    }
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository planRepository;
        private readonly IPlanDetailRepository planDetailRepository;
        private readonly IAgeRepository ageRepository;
        private readonly IMealRepository mealRepository;
        private readonly IRecipeRepository recipeRepository;
        public PlanService(IPlanRepository planRepository, IPlanDetailRepository planDetailRepository
            , IAgeRepository ageRepository, IRecipeRepository recipeRepository, IMealRepository mealRepository)
        {
            this.planRepository = planRepository;
            this.planDetailRepository = planDetailRepository;
            this.ageRepository = ageRepository;
            this.recipeRepository = recipeRepository;
            this.mealRepository = mealRepository;
        }

        public async Task<bool> Add(Plan plan, List<PlanDetail> planDetails)
        {
            var planId = "";
            try
            {
                var check = planRepository.GetAll(x => x.PlanName == plan.PlanName && x.IsDelete == false).FirstOrDefault();
                if (check != null)
                {
                    throw new Exception("Plan Name Exist!!!");
                }
                var age = await ageRepository.Get(plan.AgeId);
                if (age == null)
                {
                    throw new Exception("Age Not Found!!!");
                }
                plan.PlanId = AutoGenId.AutoGenerateId();
                planId = plan.PlanId;
                plan.PlanDetails = new List<PlanDetail>();
                var addPlan = await planRepository.Add(plan);
                var addPlanDetail = false;
                if (addPlan)
                {
                    foreach (var planDetail in planDetails)
                    {
                        planDetail.PlanDetailId = AutoGenId.AutoGenerateId();
                        planDetail.PlanId = plan.PlanId;
                        planDetail.IsDelete = false;
                        if (recipeRepository.Get(planDetail.RecipeId) == null)
                        {
                            throw new Exception("Recipe Not Found!!!");
                        }
                    }
                    addPlanDetail = await planDetailRepository.AddRange(planDetails);
                }
                if (addPlanDetail == false || addPlan == false)
                {
                    throw new Exception("Add Fail!!!");
                }
                return true;
            }
            catch (Exception ex)
            {
                await planDetailRepository.RemoveRange(planId);
                await planRepository.Delete(planId);
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                var plan = await planRepository.Get(id);
                if (plan == null)
                {
                    throw new Exception("Not Found Plan");
                }
                else
                {
                    plan.IsDelete = true;
                    return await planRepository.Update(id, plan);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Plan> GetPlan(string id)
        {
            try
            {
                var plan = await planRepository.Get(id);
                if (plan == null || plan.IsDelete.Value == false)
                {
                    throw new Exception("Not Found Plan");
                }
                plan.PlanDetails = planDetailRepository.GetAll(x => x.PlanId == id).ToList();
                if (plan.PlanDetails != null)
                {
                    foreach (var planDetail in plan.PlanDetails)
                    {
                        planDetail.Recipe = await recipeRepository.Get(planDetail.RecipeId);
                        planDetail.Recipe.Age = await ageRepository.Get(planDetail.Recipe.AgeId);
                        planDetail.Recipe.Meal = await mealRepository.Get(planDetail.Recipe.MealId);
                    }
                }
                return plan;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Plan>> GetPlans()
        {
            try
            {
                var plans = planRepository.GetAll(x => x.IsDelete == false);
                if (plans != null)
                {
                    foreach (var plan in plans)
                    {
                        plan.Age = await ageRepository.Get(plan.AgeId);
                    }
                }
                return plans;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(Plan plan, List<PlanDetail> planDetails)
        {
            var planBk = new Plan();
            var planDetailBk = new List<PlanDetail>();
            try
            {
                var check = await planRepository.Get(plan.PlanId);
                if (check == null)
                {
                    throw new Exception("Not Found Plan");
                }
                else
                {
                    planBk = check;
                    planDetailBk = planDetailRepository.GetAll(x => x.PlanId == planBk.PlanId).ToList();
                }

                var checkName = planRepository.GetAll(x => x.PlanId != plan.PlanId && x.PlanName == plan.PlanName && x.IsDelete == false).FirstOrDefault();
                if (checkName != null)
                {
                    throw new Exception("Plan Name Exist!!!");
                }
                var age = ageRepository.Get(plan.AgeId);
                if (age == null)
                {
                    throw new Exception("Age Not Found!!!");
                }
                plan.PlanDetails = new List<PlanDetail>();
                var updateCheck = await planRepository.Update(plan.PlanId, plan);
                var updatePlanDetail = true;
                if (updateCheck)
                {
                    await planDetailRepository.RemoveRange(plan.PlanId);
                    foreach (var planDetail in planDetails)
                    {
                        planDetail.PlanDetailId = AutoGenId.AutoGenerateId();
                        planDetail.PlanId = plan.PlanId;
                        planDetail.IsDelete = false;
                        if (recipeRepository.Get(planDetail.RecipeId) == null)
                        {
                            throw new Exception("Recipe Not Found!!!");
                        }
                    }
                    updatePlanDetail = await planDetailRepository.AddRange(planDetails);
                }
                if (updatePlanDetail && updateCheck)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Update Fail");
                }

            }
            catch (Exception ex)
            {
                await planRepository.Update(planBk.PlanId, planBk);
                await planDetailRepository.RemoveRange(planBk.PlanId);
                await planDetailRepository.AddRange(planDetailBk);
                throw new Exception(ex.Message);
            }
        }
    }
}
