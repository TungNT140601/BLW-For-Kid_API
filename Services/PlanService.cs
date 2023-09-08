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
        IEnumerable<Plan> GetPlans();
        Task<bool> Add(Plan plan, List<PlanDetail> planDetails);
        Task<bool> Update(Plan plan, List<PlanDetail> planDetails);
        Task<bool> Delete(string id);
    }
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository planRepository;
        private readonly IPlanDetailRepository planDetailRepository;
        private readonly IAgeRepository ageRepository;
        private readonly IRecipeRepository recipeRepository;
        public PlanService(IPlanRepository planRepository, IPlanDetailRepository planDetailRepository, IAgeRepository ageRepository, IRecipeRepository recipeRepository)
        {
            this.planRepository = planRepository;
            this.planDetailRepository = planDetailRepository;
            this.ageRepository = ageRepository;
            this.recipeRepository = recipeRepository;
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
                var age = ageRepository.Get(plan.AgeId);
                if (age == null)
                {
                    throw new Exception("Age Not Found!!!");
                }
                plan.PlanId = AutoGenId.AutoGenerateId();
                planId = plan.PlanId;
                plan.PlanDetails = new List<PlanDetail>();
                var addPlan = await planRepository.Add(plan);
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
                }
                return await planDetailRepository.AddRange(planDetails) && addPlan;
            }
            catch (Exception ex)
            {
                await planRepository.Delete(planId);
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> Delete(string id)
        {
            try
            {
                var plan = planRepository.Get(id);
                if (plan == null)
                {
                    throw new Exception("Not Found Plan");
                }
                else
                {
                    plan.Result.IsDelete = true;
                    return planRepository.Update(id, plan.Result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Plan> GetPlan(string id)
        {
            try
            {
                var plan = planRepository.Get(id);
                if (plan == null)
                {
                    throw new Exception("Not Found Plan");
                }
                plan.Result.PlanDetails = planDetailRepository.GetAll(x => x.PlanId == id).ToList();
                return plan;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Plan> GetPlans()
        {
            try
            {
                return planRepository.GetAll(x => x.IsDelete == false);
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
                var check = planRepository.Get(plan.PlanId);
                if (check.Result == null)
                {
                    throw new Exception("Not Found Plan");
                }
                else
                {
                    planBk = check.Result;
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
                }
                return await planDetailRepository.AddRange(planDetails) && updateCheck;
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
