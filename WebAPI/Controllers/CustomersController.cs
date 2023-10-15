using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Repositories.EntityModels;
using Repositories.Ultilities;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.PaymentSecurity.MoMo;
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
            try
            {
                var err = "";
                if (string.IsNullOrEmpty(customerVM.Fullname) && customerVM.Fullname.Trim() == "")
                {
                    err += ";Tên không được để trống";
                }
                if (customerVM.Gender == null)
                {
                    err += ";Vui lòng chọn giới tính";
                }
                if (!string.IsNullOrWhiteSpace(err))
                {
                    throw new Exception(err);
                }
                return mapper.Map<CustomerAccount>(customerVM);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
                var cus = await customerService.LoginEmail(cusLoginEmail.Email, cusLoginEmail.GoogleSub, cusLoginEmail.Fullname, cusLoginEmail.Avatar);
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
                var cus = await customerService.LoginPhone(cusLoginPhone.Phone, cusLoginPhone.Password);
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
                var cus = await customerService.LoginFacebook(cusLoginFacebook.FacebookId, cusLoginFacebook.Fullname, cusLoginFacebook.Avatar);
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
                var cus = await customerService.RegisPhone(cusRegisPhone.Phone, cusRegisPhone.Password, cusRegisPhone.Fullname);
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
                    var check = await customerService.ChangePassword(cusId, changePassword.OldPassword, changePassword.NewPassword);
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
                var check = await customerService.ResetPassword(resetPassword.Phone, resetPassword.NewPassword);
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
        [HttpGet]
        public async Task<IActionResult> GetInfo()
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == "Customer")
                    {
                        var cus = await customerService.Get(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
                        return Ok(new
                        {
                            Status = "Success",
                            Data = mapper.Map<CustomerVM>(cus)
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
                else
                {
                    return StatusCode(401, new
                    {
                        Status = "Not Login",
                        ErrorMessage = "You are not login"
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
        public async Task<IActionResult> UpdateInfo(CustomerVM customerVM)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == "Customer")
                    {
                        var cusId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                        customerVM.CustomerId = cusId;
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
                else
                {
                    return StatusCode(401, new
                    {
                        Status = "Not Login",
                        ErrorMessage = "You are not login"
                    });
                }
            }
            catch (Exception ae)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    ErrorMessage = ae.Message
                });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCus()
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                if (role == null)
                {
                    return StatusCode(401, new
                    {
                        Status = "Unauthorize",
                        Message = "Not Login"
                    });
                }

                if (!(role == CommonValues.ADMIN || role == CommonValues.STAFF))
                {
                    throw new Exception("Role Denied");
                }

                var cuss = customerService.GetAll();
                var cusVMs = new List<CustomerVM>();
                foreach (var cus in cuss)
                {
                    cusVMs.Add(mapper.Map<CustomerVM>(cus));
                }
                return StatusCode(200, new
                {
                    Status = "Success",
                    Data = cusVMs
                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    Message = ex.Message,
                });
            }
        }
        [HttpDelete]
        public async Task<IActionResult> BanUnbanCus(string id)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                if (role == null)
                {
                    return StatusCode(401, new
                    {
                        Status = "Unauthorize",
                        Message = "Not Login"
                    });
                }

                if (!(role == CommonValues.ADMIN || role == CommonValues.STAFF))
                {
                    throw new Exception("Role Denied");
                }

                if (string.IsNullOrEmpty(id))
                {
                    throw new Exception("Id empty");
                }
                var check = await customerService.BanUnban(id);

                return check ? StatusCode(200, new
                {
                    Status = "Success",
                    Message = "Ban/Unban Success"
                }) : StatusCode(200, new
                {
                    Status = "Fail",
                    Message = "Ban/Unban Fail"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    Message = ex.Message,
                });
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCus(string id)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                if (role == null)
                {
                    return StatusCode(401, new
                    {
                        Status = "Unauthorize",
                        Message = "Not Login"
                    });
                }

                if (!(role == CommonValues.ADMIN || role == CommonValues.STAFF))
                {
                    throw new Exception("Role Denied");
                }

                if (string.IsNullOrEmpty(id))
                {
                    throw new Exception("Id empty");
                }
                var check = await customerService.Delete(id);

                return check ? StatusCode(200, new
                {
                    Status = "Success",
                    Message = "Delete Success"
                }) : StatusCode(200, new
                {
                    Status = "Fail",
                    Message = "Delete Fail"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    Message = ex.Message,
                });
            }
        }
    }
}
