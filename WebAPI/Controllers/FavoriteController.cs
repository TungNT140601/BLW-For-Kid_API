using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.EntityModels;
using Services;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IFavoriteService _favoriteService;
        private readonly IMapper _mapper;

        public FavoriteController(IFavoriteService favoriteService, IMapper mapper)
        {
            _favoriteService = favoriteService;
            _mapper = mapper;
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
        public async Task<IActionResult> GetAllRecipeFavorite(string cusId)
        {
            try
            {
                var list = _favoriteService.GetAllRecipeFavoriteOfOneCus(cusId);
                var favorite = new List<Favorite>();
                foreach (var item in list)
                {
                    favorite.Add(_mapper.Map<Favorite>(item));
                }
                return Ok(new
                {
                    Status = 1,
                    Data = favorite
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipeFavorite(FavoriteVM model)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if(role == CommonValues.CUSTOMER)
                    {
                        var favorite = _mapper.Map<Favorite>(model);
                        var check = await _favoriteService.Add(favorite);
                        return check ? Ok(new
                        {
                            Message = "Add Success!!!"
                        }) : Ok(new
                        {
                            Message = "Add Fail"
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFavorite(string cusId, string recipeId)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == CommonValues.CUSTOMER)
                    {
                        return await _favoriteService.Delete(cusId, recipeId) ? Ok(new
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
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
