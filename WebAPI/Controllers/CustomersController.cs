﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    public class CustomersController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;
        public CustomersController(ICustomerService customerService, IMapper mapper, IConfiguration configuration)
        {
            this.customerService = customerService;
            this.mapper = mapper;
            this.configuration = configuration;
        }
        private CustomerAccount Validate(CustomerVM customerVM)
        {
            var err = "";
            try
            {
                if (string.IsNullOrEmpty(customerVM.Fullname) && customerVM.Fullname.Trim() == "")
                {
                    err += ";Tên không được để trống";
                }
                if (customerVM.Gender == null)
                {
                    err += ";Vui lòng chọn giới tính";
                }
                return mapper.Map<CustomerAccount>(customerVM);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(err))
                {
                    throw new Exception(err);
                }
            }
            return null;
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
        [HttpPost]
        public async Task<IActionResult> LoginEmail(CusLoginEmail cusLoginEmail)
        {
            try
            {
                var cus = customerService.LoginEmail(cusLoginEmail.Email, cusLoginEmail.GoogleSub, cusLoginEmail.Fullname, cusLoginEmail.Avatar).Result;
                return Ok(new
                {
                    Status = "Login Success",
                    Data = new
                    {
                        Fullname = cus.Fullname,
                        Avatar = cus.Avatar,
                        IsPremium = cus.IsPremium,
                    },
                    Token = GenerateJwtToken(cus.CustomerId)
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
        [HttpPost]
        public async Task<IActionResult> LoginPhone(CusLoginPhone cusLoginPhone)
        {
            try
            {
                var cus = customerService.LoginPhone(cusLoginPhone.Phone, cusLoginPhone.Password).Result;
                return Ok(new
                {
                    Status = "Login Success",
                    Data = new
                    {
                        Fullname = cus.Fullname,
                        Avatar = cus.Avatar,
                        IsPremium = cus.IsPremium,
                    },
                    Token = GenerateJwtToken(cus.CustomerId)
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
        [HttpPost]
        public async Task<IActionResult> LoginFacebook(CusLoginFacebook cusLoginFacebook)
        {
            try
            {
                var cus = customerService.LoginFacebook(cusLoginFacebook.FacebookId, cusLoginFacebook.Fullname, cusLoginFacebook.Avatar).Result;
                return Ok(new
                {
                    Status = "Login Success",
                    Data = new
                    {
                        Fullname = cus.Fullname,
                        Avatar = cus.Avatar,
                        IsPremium = cus.IsPremium,
                    },
                    Token = GenerateJwtToken(cus.CustomerId)
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
        [HttpPost]
        public async Task<IActionResult> RegisPhone(CusRegisPhone cusRegisPhone)
        {
            try
            {
                var cus = customerService.RegisPhone(cusRegisPhone.Phone, cusRegisPhone.Password, cusRegisPhone.Fullname).Result;
                return Ok(new
                {
                    Status = "Login Success",
                    Data = new
                    {
                        Fullname = cus.Fullname,
                        Avatar = cus.Avatar,
                        IsPremium = cus.IsPremium,
                    },
                    Token = GenerateJwtToken(cus.CustomerId)
                });
            }
            catch (AggregateException ae)
            {
                //List<string> errmsg = new List<string>();
                //foreach (var innerException in ae.InnerExceptions)
                //{
                //    errmsg.Add(innerException.Message);
                //}
                return StatusCode(400, new
                {
                    Status = "Error",
                    Data = new { },
                    Token = "",
                    //ErrorMessage = errmsg
                    ErrorMessage = ae.InnerExceptions[0].Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword changePassword)
        {
            try
            {
                if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "Customer")
                {
                    var cusId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    var check = customerService.ChangePassword(cusId, changePassword.OldPassword, changePassword.NewPassword).Result;
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
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            try
            {
                var check = customerService.ResetPassword(resetPassword.Phone, resetPassword.NewPassword).Result;
                return check ? Ok(new
                {
                    Status = "Reset Success"
                }) : Ok(new
                {
                    Status = "Reset Fail"
                });
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
        public async Task<IActionResult> UpdateInfo(CustomerVM customerVM)
        {
            try
            {
                if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "Customer")
                {
                    var cusId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    return await customerService.Update(Validate(customerVM)) ? Ok(new
                    {
                        Status = "Update Success"
                    }) : Ok(new
                    {
                        Status = "Update Fail"
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

    }
}