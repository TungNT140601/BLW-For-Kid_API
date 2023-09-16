using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.EntityModels;
using Repositories.Repository.Interface;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PremiumPackageController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IPremiumPackageService packageService;
        private readonly IMapper mapper;

        public PremiumPackageController(IPremiumPackageService packageService, IMapper mapper)
        {
            this.packageService = packageService;
            this.mapper = mapper;
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
        public async Task<IActionResult> GetAllPrePackage()
        {
            try
            {
                var list = packageService.GetAll();
                var pre = new List<PremiumPackage>();
                foreach (var item in list)
                {
                    pre.Add(mapper.Map<PremiumPackage>(item));
                }
                return Ok(new
                {
                    Status = 1,
                    Data = pre
                });
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPrePackage(string id)
        {
            try
            {
                var pre = packageService.Get(id);
                var check = mapper.Map<PremiumPackage>(pre);
                return Ok(new
                {
                    Status = 1,
                    Data = check
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPrePackage(PremiumPackageVM model)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == CommonValues.ADMIN)
                    {
                        if (string.IsNullOrEmpty(model.PackageName))
                        {
                            return StatusCode(400, new
                            {
                                Message = "Package Name cannot empty!!!"
                            });
                        }
                        else if (model.PackageAmount < 0)
                        {
                            return StatusCode(400, new
                            {
                                Message = "PackageAmount cannot small than 0!!!"
                            });
                        }
                        else if (model.PackageDiscount < 0)
                        {
                            return StatusCode(400, new
                            {
                                Message = "PackageDiscount cannot small than 0!!!"
                            });
                        }
                        else if (model.PackageMonth < 0)
                        {
                            return StatusCode(400, new
                            {
                                Message = "PackageMonth cannot small than 0!!!"
                            });
                        }
                        else
                        {
                            var pre = mapper.Map<PremiumPackage>(model);
                            var check = packageService.Add(pre);
                            return await check ? Ok(new
                            {
                                Message = "Add Success"
                            }) : Ok(new
                            {
                                Message = "Add Fail"
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePrePackage(PremiumPackageVM model)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == CommonValues.ADMIN)
                    {
                        if (string.IsNullOrEmpty(model.PackageName))
                        {
                            return StatusCode(400, new
                            {
                                Message = "Package Name cannot empty!!!"
                            });
                        }
                        else if (model.PackageAmount < 0)
                        {
                            return StatusCode(400, new
                            {
                                Message = "PackageAmount cannot small than 0!!!"
                            });
                        }
                        else if (model.PackageDiscount < 0)
                        {
                            return StatusCode(400, new
                            {
                                Message = "PackageDiscount cannot small than 0!!!"
                            });
                        }
                        else if (model.PackageMonth < 0)
                        {
                            return StatusCode(400, new
                            {
                                Message = "PackageMonth cannot small than 0!!!"
                            });
                        }
                        else
                        {
                            var pre = mapper.Map<PremiumPackage>(model);
                            var check = packageService.Update(pre);
                            return await check ? Ok(new
                            {
                                Message = "Update Success"
                            }) : Ok(new
                            {
                                Message = "Update Fail"
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePrePackage(string id)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == CommonValues.ADMIN)
                    {
                        var pre = packageService.Get(id);
                        if (pre != null)
                        {
                            var check = packageService.Delete(id);
                            return await check ? Ok(new
                            {
                                Message = "Delete Success"
                            }) : Ok(new
                            {
                                Message = "Delete Fail"
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
