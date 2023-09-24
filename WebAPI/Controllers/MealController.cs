using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.EntityModels;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IMealService _mealService;
        private readonly IMapper _mapper;

        public MealController(IMapper mapper, IMealService mealService, IConfiguration configuration)
        {
            _mapper = mapper;
            this._mealService = mealService;
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
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role == CommonValues.ADMIN || role == CommonValues.STAFF)
                {
                    var list = _mealService.GetAllMeal();
                    var meal = new List<Meal>();
                    foreach (var item in list)
                    {
                        meal.Add(_mapper.Map<Meal>(item));
                    }
                    return Ok(new
                    {
                        Status = 1,
                        Data = meal
                    });
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

        [HttpGet]
        public async Task<IActionResult> GetMeal(string id)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role == CommonValues.ADMIN || role == CommonValues.STAFF)
                {
                    var meal = await _mealService.GetMeal(id);
                    return Ok(new
                    {
                        Status = 1,
                        Data = meal
                    });
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
        public async Task<IActionResult> AddNewMeal(MealVM model)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role == CommonValues.ADMIN || role == CommonValues.STAFF)
                {
                    var meal = _mapper.Map<Meal>(model);
                    var check = await _mealService.AddMeal(meal);
                    return check ? Ok(new
                    {
                        Message = "Add Success!!!"
                    }) : Ok(new
                    {
                        Message = "Add Fail!!!"
                    });
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
        public async Task<IActionResult> UpdateAge(MealVM model)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role == CommonValues.ADMIN || role == CommonValues.STAFF)
                {
                    var meal = _mapper.Map<Meal>(model);
                    var check = await _mealService.UpdateMeal(meal);
                    return check ? Ok(new
                    {
                        Message = "Update Success!!!"
                    }) : Ok(new
                    {
                        Message = "Update Fail!!!"
                    });
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
        public async Task<IActionResult> DeleteAge(string id)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role == CommonValues.ADMIN || role == CommonValues.STAFF)
                {
                    var check = await _mealService.DeleteMeal(id);
                    return check ? Ok(new
                    {
                        Message = "Delete Success!!!"
                    }) : Ok(new
                    {
                        Message = "Delete Fail!!!"
                    });
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
    }
}
