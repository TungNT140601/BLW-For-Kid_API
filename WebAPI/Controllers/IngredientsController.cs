using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.EntityModels;
using Services;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IIngredientService ingredientService;
        private readonly IMapper mapper;

        public IngredientsController(IIngredientService ingredientService, IMapper mapper)
        {
            this.ingredientService = ingredientService;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        private string GenerateJwtToken(string id, string role)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, id),
            new Claim(ClaimTypes.Role, role)
        };
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var ingredients = ingredientService.GetAll();
                var ingredientVMs = new List<IngredientVM>();
                foreach (var ingredient in ingredients)
                {
                    ingredientVMs.Add(mapper.Map<IngredientVM>(ingredient));
                }
                return Ok(new
                {
                    Status = "Success",
                    Data = ingredientVMs
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
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == CommonValues.ADMIN || role == CommonValues.STAFF)
                    {
                        return Ok(new
                        {
                            Status = "Get ID Success",
                            Data = mapper.Map<IngredientVM>(await ingredientService.Get(id))
                        });
                    }
                    else
                    {
                        return StatusCode(400, new
                        {
                            Status = -1,
                            Message = "Role Denied"
                        });
                    }
                }
                else
                {
                    return Unauthorized();
                }
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
                var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == CommonValues.ADMIN || role == CommonValues.STAFF)
                    {
                        var ingredient = Validate(ingredientVM);
                        ingredient.StaffCreate = id;
                        var check = await ingredientService.Add(ingredient);
                        return check ? Ok(new
                        {
                            Status = 1,
                            Message = "Add Ingredient Success"
                        }) : Ok(new
                        {
                            Status = 0,
                            Message = "Add Ingredient Fail"
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            Status = 0,
                            Message = "Role Denied",
                            Data = new { }
                        });
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateIngredient(IngredientVM ings)
        {
            try
            {
                var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == CommonValues.ADMIN || role == CommonValues.STAFF)
                    {
                        var ingredient = Validate(ings);
                        ingredient.StaffUpdate = id;
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
                    }
                    else
                    {
                        return Ok(new
                        {
                            Status = 0,
                            Message = "Role Denied",
                            Data = new { }
                        });
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteIngredient(string id)
        {


            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == CommonValues.ADMIN || role == CommonValues.STAFF)
                    {
                        var ingredient = await ingredientService.Get(id);
                        if (ingredient == null)
                        {
                            throw new Exception("Not Found Ingredient");
                        }

                        ingredient.StaffDelete = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                        var check = ingredientService.Delete(ingredient);

                        return await check ? Ok(new
                        {
                            Status = 1,
                            Message = "Delete Ingredient Success"
                        }) : Ok(new
                        {
                            Status = 0,
                            Message = "Delete Ingredient Fail"
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            Status = 0,
                            Message = "Role Denied",
                            Data = new { }
                        });
                    }
                }
                else
                {
                    return Unauthorized();
                }
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
            //if (string.IsNullOrEmpty(ingredientVM.IngredientImage.Trim()))
            //{
            //    throw new Exception("Ingredient Image cannot be empty!!!");
            //}
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
            //if (ingredientVM.CreateTime == DateTime.Now)
            //{
            //    throw new Exception("Ingredient CreateTime cannot be a date now!!!");
            //}
            //if (string.IsNullOrEmpty(ingredientVM.StaffCreate.Trim()))
            //{
            //    throw new Exception("StaffCreate cannot be empty!!!");
            //}
            //if (ingredientVM.UpdateTime == DateTime.Now)
            //{
            //    throw new Exception("Ingredient UpdateTime cannot be a date now!!!");
            //}
            //if (string.IsNullOrEmpty(ingredientVM.StaffUpdate.Trim()))
            //{
            //    throw new Exception("StaffUpdate cannot be empty!!!");
            //}
            //if (ingredientVM.DeleteDate == DateTime.Now)
            //{
            //    throw new Exception("Ingredient DeleteDate cannot be a date now!!!");
            //}
            //if (string.IsNullOrEmpty(ingredientVM.StaffDelete.Trim()))
            //{
            //    throw new Exception("StaffDelete cannot be empty!!!");
            //}
            return mapper.Map<Ingredient>(ingredientVM);
        }
    }


}
