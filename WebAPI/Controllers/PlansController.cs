using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.EntityModels;
using Services;
using System.Security.Claims;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly IPlanService planService;
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;
        public PlansController(IPlanService planService, IMapper mapper)
        {
            this.planService = planService;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetPlanDetailOfDate(string id, int date)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == "Customer" || role == "Admin" || role == "Staff")
                    {
                        if (role == "Customer")
                        {
                            var cus = await customerService.CheckPremium(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
                            if (!cus.IsPremium.Value)
                            {
                                return StatusCode(404, new
                                {
                                    Status = "Access Denied",
                                    ErrorMessage = "You are not a Premium customer"
                                });
                            }
                        }

                        var plan = await planService.GetPlan(id);
                        if (plan != null)
                        {
                            List<PlanDetail> planDetails = new List<PlanDetail>();
                            List<PlanDetailVM> breakfast = new List<PlanDetailVM>();
                            List<PlanDetailVM> lunch = new List<PlanDetailVM>();
                            List<PlanDetailVM> dinner = new List<PlanDetailVM>();
                            List<PlanDetailVM> snack = new List<PlanDetailVM>();
                            planDetails = plan.PlanDetails.ToList();
                            foreach (var planDetail in planDetails)
                            {
                                if (planDetail.Date == date)
                                {
                                    var planDetailVM = (new PlanDetailVM
                                    {
                                        PlanDetailId = planDetail.PlanDetailId,
                                        RecipeVM = mapper.Map<RecipeVM>(planDetail.Recipe)
                                    });
                                    planDetailVM.RecipeVM.AgeName = planDetail.Recipe.Age.AgeName;
                                    planDetailVM.RecipeVM.MealName = planDetail.Recipe.Meal.MealName;
                                    if (planDetail.MealOfDate == 1)
                                    {
                                        breakfast.Add(planDetailVM);
                                    }
                                    if (planDetail.MealOfDate == 2)
                                    {
                                        lunch.Add(planDetailVM);
                                    }
                                    if (planDetail.MealOfDate == 3)
                                    {
                                        dinner.Add(planDetailVM);
                                    }
                                    if (planDetail.MealOfDate == 4)
                                    {
                                        snack.Add(planDetailVM);
                                    }
                                }
                            }
                            return Ok(new
                            {
                                BreakFast = breakfast,
                                Lunch = lunch,
                                Dinner = dinner,
                                Snack = snack,
                            });
                        }
                        else
                        {
                            return StatusCode(404, new
                            {
                                Status = "Not Found",
                                ErrorMessage = "Not Found plan"
                            });
                        }
                    }
                    else
                    {
                        return StatusCode(401, new
                        {
                            Status = "Error",
                            ErrorMessage = "Role Denied"
                        });
                    }
                }
                else
                {
                    return StatusCode(401, new
                    {
                        Status = "Not Login",
                        ErrorMessage = "You are not login"
                    });
                }
            }
            catch (AggregateException ae)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    ErrorMessage = ae.InnerExceptions[0].Message
                });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPlan()
        {
            try
            {
                var plans = await planService.GetPlans();
                List<PlanVM> planVMs = new List<PlanVM>();
                foreach (var plan in plans)
                {
                    planVMs.Add(new PlanVM
                    {
                        PlanId = plan.PlanId,
                        PlanName = plan.PlanName,
                        AgeName = plan.Age.AgeName
                    });
                }
                return Ok(new
                {
                    Status = "Success",
                    Data = planVMs
                });
            }
            catch (AggregateException ae)
            {
                return StatusCode(500, new
                {
                    Status = "Sever Error",
                    Message = ae.InnerExceptions[0].Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddPlan(PlanAddVM planAddVM)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == "Admin" || role == "Staff")
                    {
                        var cusId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                        var plan = new Plan
                        {
                            AgeId = planAddVM.AgeId,
                            PlanName = planAddVM.PlanName
                        };
                        var planDetails = new List<PlanDetail>();
                        if (planAddVM.PlanDetails != null)
                        {
                            foreach (var planDetail in planAddVM.PlanDetails)
                            {
                                planDetails.Add(new PlanDetail
                                {
                                    Date = planDetail.Date,
                                    MealOfDate = planDetail.MealOfDate,
                                    RecipeId = planDetail.RecipeId,
                                });
                            }
                        }
                        else
                        {
                            return StatusCode(400, new
                            {
                                Status = "Error",
                                ErrorMessage = "Plan Detail cannot be empty"
                            });
                        }
                        return await planService.Add(plan, planDetails) ? Ok(new
                        {
                            Status = "Add Success"
                        }) : Ok(new
                        {
                            Status = "Add Fail"
                        });
                    }
                    else
                    {
                        return StatusCode(400, new
                        {
                            Status = "Error",
                            ErrorMessage = "Role Denied"
                        });
                    }
                }
                else
                {
                    return StatusCode(401, new
                    {
                        Status = "Not Login",
                        ErrorMessage = "You are not login"
                    });
                }
            }
            catch (AggregateException ae)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    ErrorMessage = ae.InnerExceptions[0].Message
                });
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePlan(string planId, PlanAddVM planAddVM)
        {
            try
            {
                if (planId != planAddVM.PlanId)
                {
                    return StatusCode(404, new
                    {
                        Status = "Error",
                        ErrorMessage = "Plan Id Not Found"
                    });
                }
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == "Admin" || role == "Staff")
                    {
                        var cusId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                        var plan = new Plan
                        {
                            AgeId = planAddVM.AgeId,
                            PlanName = planAddVM.PlanName
                        };
                        var planDetails = new List<PlanDetail>();
                        if (planAddVM.PlanDetails != null)
                        {
                            foreach (var planDetail in planAddVM.PlanDetails)
                            {
                                planDetails.Add(new PlanDetail
                                {
                                    Date = planDetail.Date,
                                    MealOfDate = planDetail.MealOfDate,
                                    RecipeId = planDetail.RecipeId,
                                });
                            }
                        }
                        else
                        {
                            return StatusCode(400, new
                            {
                                Status = "Error",
                                ErrorMessage = "Plan Detail cannot be empty"
                            });
                        }
                        return await planService.Update(plan, planDetails) ? Ok(new
                        {
                            Status = "Update Success"
                        }) : Ok(new
                        {
                            Status = "Update Fail"
                        });
                    }
                    else
                    {
                        return StatusCode(400, new
                        {
                            Status = "Error",
                            ErrorMessage = "Role Denied"
                        });
                    }
                }
                else
                {
                    return StatusCode(401, new
                    {
                        Status = "Not Login",
                        ErrorMessage = "You are not login"
                    });
                }
            }
            catch (AggregateException ae)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    ErrorMessage = ae.InnerExceptions[0].Message
                });
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePlan(string planId)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == "Admin" || role == "Staff")
                    {
                        return await planService.Delete(planId) ? Ok(new
                        {
                            Status = "Delete Success"
                        }) : Ok(new
                        {
                            Status = "Delete Fail"
                        });
                    }
                    else
                    {
                        return StatusCode(400, new
                        {
                            Status = "Error",
                            ErrorMessage = "Role Denied"
                        });
                    }
                }
                else
                {
                    return StatusCode(401, new
                    {
                        Status = "Not Login",
                        ErrorMessage = "You are not login"
                    });
                }
            }
            catch (AggregateException ae)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    ErrorMessage = ae.InnerExceptions[0].Message
                });
            }
        }
    }
}
