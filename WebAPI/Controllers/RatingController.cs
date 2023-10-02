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
    public class RatingController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IRatingService ratingService;
        private readonly IMapper mapper;

        public RatingController(IRatingService ratingService, IMapper mapper)
        {
            this.ratingService = ratingService;
            this.mapper = mapper;
        }

        private string GenerateJwtToken(string id)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, id),
            new Claim(ClaimTypes.Role, "Customer")
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
                var list = ratingService.GetAllRatingOfOneCus();
                var rating = new List<Rating>();
                foreach (var item in list)
                {
                    rating.Add(mapper.Map<Rating>(item));
                }
                return Ok(new
                {
                    Status = 1,
                    Data = rating
                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRatingOfCus(string recipeId)
        {
            try
            {
                var cusId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var rating = await ratingService.GetRating(cusId, recipeId);
                return Ok(new
                {
                    Status = 1,
                    Data = rating
                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRating(RatingVM model)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == CommonValues.CUSTOMER)
                    {
                        var rating = mapper.Map<Rating>(model);
                        var check = await ratingService.AddOrUpdate(rating);
                        return check ? Ok(new
                        {
                            Message = "Success!!!"
                        }) : Ok(new
                        {
                            Message = "Fail"
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
                return StatusCode(400, new
                {
                    Status = "Error",
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRating(string recipeId)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                var cusId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == CommonValues.CUSTOMER)
                    {
                        var check = await ratingService.Delete(cusId, recipeId);
                        return check ? Ok(new
                        {
                            Message = "Delete Success!!!"
                        }) : Ok(new
                        {
                            Message = "Delete Fail"
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
                return StatusCode(400, new
                {
                    Status = "Error",
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetAllRatingOnRecipe(string recipeId)
        {
            try
            {
                return Ok(ratingService.GetAllRatingOfRecipe(recipeId));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AvgRatingOnRecipe(string recipeId)
        {
            try
            {
                return Ok(ratingService.AvgRatingOfRecipe(recipeId));
            }
            catch(Exception ex)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
