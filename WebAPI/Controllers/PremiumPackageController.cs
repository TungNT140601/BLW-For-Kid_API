using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.EntityModels;
using Repositories.Repository.Interface;
using Services;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PremiumPackageController : ControllerBase
    {
        private readonly IPremiumPackageService packageService;
        private readonly IMapper mapper;

        public PremiumPackageController(IPremiumPackageService packageService, IMapper mapper)
        {
            this.packageService = packageService;
            this.mapper = mapper;
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
                var pre = packageService.Get(id);
                if (pre != null)
                {
                    var check =  packageService.Delete(id);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
