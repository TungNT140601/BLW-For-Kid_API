﻿using AutoMapper;
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
                return Ok(new
                {
                    Status = 1,
                    Message = "Success",
                    Data = _mapper.Map<StaffAccount>(_staffAccountService.GetStaffAccount(id))
                }); 
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
                var list = _staffAccountService.GetAllStaffAccount();
                var staffAccount = new List<StaffAccount>();
                foreach (var item in list)
                {SS
                    staffAccount.Add(_mapper.Map<StaffAccount>(item));
                }
                return Ok(new
                {
                    Status = 1,
                    Message = "Success",
                    Data = staffAccount
                });
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
                if(string.IsNullOrEmpty(model.Email))
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
                else if(string.IsNullOrEmpty(model.Password))
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
                    if(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "Staff")
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
                    else
                    {
                        return StatusCode(400, new
                        {
                            Status = "Error",
                            Message = "Role denied"
                        });
                    }
                    
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
                var acccountStaff = _staffAccountService.GetStaffAccount(id);
                if(acccountStaff != null)
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
                            return Ok(new
                            {
                                Message = "Login success!!!"
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
