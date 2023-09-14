using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.EntityModels;
using Repositories.Repository.Interface;

using Services;
using System.ComponentModel.Design;
using System.Data;
using System.Security.Claims;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientService ingredientService;
        private readonly IMapper mapper;

        public IngredientsController(IIngredientService ingredientService, IMapper mapper)
        {
            this.ingredientService = ingredientService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(new
                {
                    Status = "Get List Success",
                    Data = ingredientService.GetAll()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                return Ok(new
                {
                    Status = "Get ID Success",
                    Data = ingredientService.Get(id)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddIngredient(IngredientVM ingredientVM)
        {
            try
            {
                //var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                //if (!string.IsNullOrEmpty(role))
                //{
                //if (role == "ADMIN" && role == "STAFF")
                //{
                var ingredient = Validate(ingredientVM);
                var check = ingredientService.Add(ingredient);
                return await check ? Ok(new
                {
                    Status = 1,
                    Message = "Add Ingredient Success"
                }) : Ok(new
                {
                    Status = 0,
                    Message = "Add Ingredient Fail"
                });
            //}
            //else
            // return   //Ok(new
                        //{
                        //    Status = 0,
                        //    Message = "Role Denied",
                        //    Data = new { }
                        //});
                    
                //else
                //{
                //    return Unauthorized();
                //}
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateIngredient(string id, IngredientVM ingredientVM)
        {
            if(id != ingredientVM.IngredientId)
            {
                return BadRequest();
            }

            try
            {
                //var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                //if (!string.IsNullOrEmpty(role))
                //{
                //    if (role == "ADMIN" && role == "STAFF")
                //    {
                        var ingredient = Validate(ingredientVM);
                        var check = ingredientService.Update(ingredient);
                        return await check ? Ok(new
                        {
                            Status = 1,
                            Message = "Update Ingredient Success"
                        }) : Ok(new
                        {
                            Status = 0,
                            Message = "Update Ingredient Fail"
                        });
                    //}
                //    else
                //    {
                //        return Ok(new
                //        {
                //            Status = 0,
                //            Message = "Role Denied",
                //            Data = new { }
                //        });
                //    }
                //}
                //else
                //{
                //    return Unauthorized();
                //}
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteIngredient(string id)
        {
            //if (ingredientService.Get(id) == null)
            //{
            //    return BadRequest();
            //}

            try
            {
                //var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                //if (!string.IsNullOrEmpty(role))
                //{
                    //if (role == "ADMIN" && role == "STAFF")
                    //{
                        var check = ingredientService.Delete(id);
                        return await check ? Ok(new
                        {
                            Status = 1,
                            Message = "Delete Ingredient Success"
                        }) : Ok(new
                        {
                            Status = 0,
                            Message = "Delete Ingredient Fail"
                        });
                    //}
                    //else
                    //{
                    //    return Ok(new
                    //    {
                    //        Status = 0,
                    //        Message = "Role Denied",
                    //        Data = new { }
                    //    });
                    //}
                //else
                //{
                //    return BadRequest();
                //}
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private Ingredient Validate(IngredientVM ingredientVM)
        {
            if (string.IsNullOrEmpty(ingredientVM.IngredientName.Trim()))
            {
                throw new Exception("Ingredient Name cannot be empty!!!");
            }
            if (string.IsNullOrEmpty(ingredientVM.IngredientImage.Trim()))
            {
                throw new Exception("Ingredient Image cannot be empty!!!");
            }
            if (string.IsNullOrEmpty(ingredientVM.Measure.Trim()))
            {
                throw new Exception("Measure cannot be empty!!!");
            }
            if (ingredientVM.Protein < 0)
            {
                throw new Exception("Ingredient Protein cannot be a negative number!!!");
            }
            if (ingredientVM.Carbohydrate < 0)
            {
                throw new Exception("Ingredient Protein cannot be a negative number!!!");
            }
            if (ingredientVM.Fat < 0)
            {
                throw new Exception("Ingredient Protein cannot be a negative number!!!");
            }
            ingredientVM.Calories = ingredientVM.Fat * 9 + ingredientVM.Carbohydrate * 4 + ingredientVM.Protein * 4;
            if(ingredientVM.CreateTime ==  DateTime.Now) 
            {
                throw new Exception("Ingredient CreateTime cannot be a date now!!!");
            }
            if (string.IsNullOrEmpty(ingredientVM.StaffCreate.Trim()))
            {
                throw new Exception("StaffCreate cannot be empty!!!");
            }
            if (ingredientVM.UpdateTime == DateTime.Now)
            {
                throw new Exception("Ingredient UpdateTime cannot be a date now!!!");
            }
            if (string.IsNullOrEmpty(ingredientVM.StaffUpdate.Trim()))
            {
                throw new Exception("StaffUpdate cannot be empty!!!");
            }
            if (ingredientVM.DeleteDate == DateTime.Now)
            {
                throw new Exception("Ingredient DeleteDate cannot be a date now!!!");
            }
            if (string.IsNullOrEmpty(ingredientVM.StaffDelete.Trim()))
            {
                throw new Exception("StaffDelete cannot be empty!!!");
            }
            return mapper.Map<Ingredient>(ingredientVM);
        }
    }
}
