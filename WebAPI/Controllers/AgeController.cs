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

        private string GenerateJwtToken(string id,string role)
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
                if(role == CommonValues.ADMIN || role == CommonValues.STAFF) 
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
                else
                {
                    return Unauthorized();
                }
                
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAge(string id)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role == CommonValues.ADMIN || role == CommonValues.STAFF)
                {
                    var age = _ageService.GetAge(id);
                    return Ok(new
                    {
                        Status = 1,
                        Data = age
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
        public async Task<IActionResult> AddNewAge(AgeVM model)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role == CommonValues.ADMIN || role == CommonValues.STAFF)
                {
                    if (string.IsNullOrEmpty(model.AgeName))
                    {
                        return StatusCode(400, new
                        {
                            Message = "AgeName cannot empty!!!"
                        });
                    }
                    else
                    {
                        var age = _mapper.Map<Age>(model);
                        var check = _ageService.AddAge(age);
                        return await check ? Ok(new
                        {
                            Message = "Add Success!!!"
                        }) : Ok(new
                        {
                            Message = "Add Fail!!!"
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
        public async Task<IActionResult> UpdateAge(AgeVM model)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role == CommonValues.ADMIN || role == CommonValues.STAFF)
                {
                    if (string.IsNullOrEmpty(model.AgeName))
                    {
                        return StatusCode(400, new
                        {
                            Message = "AgeName cannot empty!!!"
                        });
                    }
                    else
                    {
                        var age = _mapper.Map<Age>(model);
                        var check = _ageService.UpdateAge(age);
                        return await check ? Ok(new
                        {
                            Message = "Update Success!!!"
                        }) : Ok(new
                        {
                            Message = "Update Fail!!!"
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
        public async Task<IActionResult> DeleteAge(string id)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role == CommonValues.ADMIN || role == CommonValues.STAFF)
                {
                    var check = _ageService.DeleteAge(id);
                    return await check ? Ok(new
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
