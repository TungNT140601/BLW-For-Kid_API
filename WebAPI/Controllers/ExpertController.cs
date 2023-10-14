using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.EntityModels;
using Services;
using System.Configuration;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExpertController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IExpertService expertService;
        private readonly IMapper mapper;

        public ExpertController(IExpertService expertService, IMapper mapper, IConfiguration configuration)
        {
            this.expertService = expertService;
            this.mapper = mapper;
            this.configuration = configuration;
        }

       

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(new
                {
                    Status = "Get List Success",
                    Data = expertService.GetAll()
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
                        Data = expertService.Get(id)
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
        public async Task<IActionResult> AddExpert(ExpertVM expertVM)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == CommonValues.ADMIN || role == CommonValues.STAFF)
                    {
                        var experts = mapper.Map<Expert>(expertVM);
                        var check = expertService.Add(experts);
                return await check ? Ok(new
                {
                    Status = 1,
                    Message = "Add Expert Success"
                }) : Ok(new
                {
                    Status = 0,
                    Message = "Add Expert Fail"
                });
            }
                else
                return Ok(new
                {
                    Status = 0,
                    Message = "Role Denied",
                    Data = new { }
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
        public async Task<IActionResult> UpdateExpert(ExpertUpdateVM expertVM)
        {
            

            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == CommonValues.ADMIN || role == CommonValues.STAFF)
                    {
                        var expert = mapper.Map<Expert>(expertVM);
                        var check = expertService.Update(expert);
                return await check ? Ok(new
                {
                    Status = 1,
                    Message = "Update Expert Success"
                }) : Ok(new
                {
                    Status = 0,
                    Message = "Update Expert Fail"
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
        public async Task<IActionResult> DeleteExpert(ExpertDeleteVM model)
        {
            

            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == CommonValues.ADMIN || role == CommonValues.STAFF)
                    {
                        var expertdel = mapper.Map<Expert>(model);
                        var check = expertService.Delete(expertdel);
                        return await check ? Ok(new
                        {
                            Status = 1,
                            Message = "Delete Expert Success"
                        }) : Ok(new
                        {
                            Status = 0,
                            Message = "Delete Expert Fail"
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
                    return BadRequest();
                }
                }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordExpert resetPassword)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == "Expert")
                    {
                        var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                        var check = expertService.ResetPassword(id,resetPassword.OldPassword, resetPassword.NewPassword).Result;
                        return check ? Ok(new
                        {
                            Status = "Reset Success"
                        }) : Ok(new
                        {
                            Status = "Reset Fail"
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
                    return BadRequest();
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

        //private Expert Validate(ExpertVM expertVM)
        //{
        //    if (string.IsNullOrEmpty(expertVM.Email.Trim()))
        //    {
        //        throw new Exception("Expert Email cannot be empty!!!");
        //    }
        //    if (expertVM.Gender < 0)
        //    {
        //        throw new Exception("Expert Gender cannot be a date now!!!");
        //    }
        //    if (string.IsNullOrEmpty(expertVM.Username.Trim()))
        //    {
        //        throw new Exception("Expert Username cannot be a negative number!!!");
        //    }
        //    if (string.IsNullOrEmpty(expertVM.Password.Trim()))
        //    {
        //        throw new Exception("Expert Password cannot be empty!!!");
        //    }

        //    if (string.IsNullOrEmpty(expertVM.Name.Trim()))
        //    {
        //        throw new Exception("Expert Name cannot be empty!!!");
        //    }
            
        //    return mapper.Map<Expert>(expertVM);
        //}

        private string GenerateJwtToken(string id)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, id),
            new Claim(ClaimTypes.Role, "Expert")
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

        

        [HttpPost]
        public async Task<IActionResult> LoginExpert(ExpLogin expLogin)
        {
            try
            {
                var exp = expertService.LoginExpert(expLogin.Username, expLogin.Password).Result;
                return Ok(new
                {
                    Status = "Login Success",
                    Data = new
                    {
                        Fullname = exp.Name,
                        Avatar = exp.Avatar,
                        Phonenum = exp.PhoneNum,
                    },
                    Token = GenerateJwtToken(exp.ExpertId)
                });
            }
            catch (AggregateException ae)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    Data = new { },
                    Token = "",
                    ErrorMessage = ae.InnerExceptions[0].Message
                });
            }
        }
    }
}
