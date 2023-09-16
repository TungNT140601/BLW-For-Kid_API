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
    public class StaffAccountController : ControllerBase
    {
        private readonly IConfiguration configuration;
        protected readonly IStaffAccountService _staffAccountService;
        private readonly IMapper _mapper;

        public StaffAccountController(IStaffAccountService staffAccountService, IMapper mapper, IConfiguration configuration)
        {
            _staffAccountService = staffAccountService;
            _mapper = mapper;
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
        public async Task<IActionResult> GetStaff(string id)
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
                            Status = 1,
                            Message = "Success",
                            Data = _mapper.Map<StaffAccount>(_staffAccountService.GetStaffAccount(id))
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

        [HttpGet]
        public async Task<IActionResult> GetStaffAcounts()
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == CommonValues.ADMIN)
                    {
                        var list = _staffAccountService.GetAllStaffAccount();
                        var staffAccount = new List<StaffAccount>();
                        foreach (var item in list)
                        {
                            staffAccount.Add(_mapper.Map<StaffAccount>(item));
                        }
                        return Ok(new
                        {
                            Status = 1,
                            Message = "Success",
                            Data = staffAccount
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
            catch
            (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewStaff(StaffAccountAddVM model)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if(role == CommonValues.ADMIN)
                    {
                        if (string.IsNullOrEmpty(model.Email))
                        {
                            return StatusCode(400, new
                            {
                                Message = "Email cannot be empty!!!"
                            });
                        }
                        else if (string.IsNullOrEmpty(model.Username))
                        {
                            return StatusCode(400, new
                            {
                                Message = "Username cannot be empty!!!"
                            });
                        }
                        else if (string.IsNullOrEmpty(model.Password))
                        {
                            return StatusCode(400, new
                            {
                                Message = "Password cannot be empty!!!"
                            });
                        }
                        else if (string.IsNullOrEmpty(model.Fullname))
                        {
                            return StatusCode(400, new
                            {
                                Message = "Fullname cannot be empty!!!"
                            });
                        }
                        else
                        {
                            var staffAccount = _mapper.Map<StaffAccount>(model);
                            var check = _staffAccountService.Add(staffAccount);
                            return await check ? Ok(new
                            {
                                Status = 1,
                                Message = "Success"
                            }) : Ok(new
                            {
                                Status = 0,
                                Message = "Fail"
                            });
                        }
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

        [HttpPut]
        public async Task<IActionResult> UpdateStaff(string id, StaffAccountUpdateVM model)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == CommonValues.ADMIN || role == CommonValues.STAFF)
                    {
                        if (string.IsNullOrEmpty(model.Fullname))
                        {
                            return StatusCode(400, new
                            {
                                Message = "FullName cannot be empty!!!"
                            });
                        }
                        else
                        {
                            model.Id = id;
                            var staffAccount = _mapper.Map<StaffAccount>(model);
                            var check = _staffAccountService.Add(staffAccount);
                            return await check ? Ok(new
                            {
                                Status = 1,
                                Message = "Success"
                            }) : Ok(new
                            {
                                Status = 0,
                                Message = "Fail"
                            });
                        }
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

        [HttpDelete]
        public async Task<IActionResult> DeleteStaff(string id)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == CommonValues.ADMIN)
                    {
                        var acccountStaff = _staffAccountService.GetStaffAccount(id);
                        if (acccountStaff != null)
                        {
                            var check = _staffAccountService.Delete(id);
                            return await check ? Ok(new
                            {
                                Status = 1,
                                Message = "Success"
                            }) : Ok(new
                            {
                                Status = 0,
                                Message = "Fail"
                            });
                        }
                        else
                        {
                            return NotFound();
                        }
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

        [HttpPost]
        public async Task<IActionResult> ChangePWdStaffAccount(ChangePwdStaffAccountVM changePwd)
        {
            try
            {                
                if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "Staff")
                {
                    var cusId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    var check = _staffAccountService.ChangePwdStaff(cusId, changePwd.OldPassword, changePwd.NewPassword).Result;
                    return check ? Ok(new
                    {
                        Status = "Change Success"
                    }) : Ok(new
                    {
                        Status = "Change Fail"
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
            catch (AggregateException ae)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    ErrorMessage = ae.InnerExceptions[0].Message
                });
            }
        }

        [HttpPost]
        public IActionResult LoginAdminOrStaff(string username, string password)
        {
            try
            {
                var role = "";
                var account = _staffAccountService.CheckLogin(username).Result;
                if (account != null)
                {
                    if (account.Password != password)
                    {
                        return StatusCode(400, new
                        {
                            Message = "Password incorrect!!!"
                        });
                    }
                    else
                    {
                        if (account.Role == 0 || account.Role == 1)
                        {
                            if(account.Role == 0)
                            {
                                role = CommonValues.ADMIN;
                            }
                            else if(account.Role == 1)
                            {
                                role = CommonValues.STAFF;
                            }
                            return Ok(new
                            {
                                Message = "Login success!!!",
                                Token = GenerateJwtToken(account.StaffId, role)
                            });
                            
                        }
                        else
                        {
                            return StatusCode(400, new
                            {
                                Message = "You do not have permission!!!"
                            });
                        }
                    }
                }
                else
                {
                    return NotFound();
                }

            }
            catch (AggregateException ae)
            {
                return BadRequest();
            }
        }
    }
}
