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
    public class AgeController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IAgeService _ageService;
        private readonly IMapper _mapper;

        public AgeController(IMapper mapper, IAgeService ageService, IConfiguration configuration)
        {
            _mapper = mapper;
            _ageService = ageService;
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
                var list = _ageService.GetAllAge();
                var age = new List<Age>();
                foreach (var item in list)
                {
                    age.Add(_mapper.Map<Age>(item));
                }
                return Ok(new
                {
                    Status = 1,
                    Data = age
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
        public async Task<IActionResult> GetAge(string id)
        {
            try
            {
                var age = await _ageService.GetAge(id);
                return Ok(new
                {
                    Status = 1,
                    Data = age
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
        public async Task<IActionResult> AddNewAge(AgeVM model)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role == CommonValues.ADMIN || role == CommonValues.STAFF)
                {
                    var age = _mapper.Map<Age>(model);
                    var check = await _ageService.AddAge(age);
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
                return StatusCode(400, new
                {
                    Status = "Error",
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAge(AgeUpdateVM model)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role == CommonValues.ADMIN || role == CommonValues.STAFF)
                {
                    var age = _mapper.Map<Age>(model);
                    var check = await _ageService.UpdateAge(age);
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
                return StatusCode(400, new
                {
                    Status = "Error",
                    ErrorMessage = ex.Message
                });
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
                    var check = await _ageService.DeleteAge(id);
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
                return StatusCode(400, new
                {
                    Status = "Error",
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
