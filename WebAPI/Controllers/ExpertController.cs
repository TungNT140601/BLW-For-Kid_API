using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.EntityModels;
using Services;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExpertController : ControllerBase
    {
        private readonly IExpertService expertService;
        private readonly IMapper mapper;

        public ExpertController(IExpertService expertService, IMapper mapper)
        {
            this.expertService = expertService;
            this.mapper = mapper;
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
                return Ok(new
                {
                    Status = "Get ID Success",
                    Data = expertService.Get(id)
                });
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
                //var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                //if (!string.IsNullOrEmpty(role))
                //{
                //if (role == "ADMIN" && role == "STAFF")
                //{
                var expert = Validate(expertVM);
                var check = expertService.Add(expert);
                return await check ? Ok(new
                {
                    Status = 1,
                    Message = "Add Expert Success"
                }) : Ok(new
                {
                    Status = 0,
                    Message = "Add Expert Fail"
                });
                //}
                //else
                // return   //Ok(new
                //{
                //    Status = 0,
                //    Message = "Role Denied",
                //    Data = new { }
                //});

                //else
                //{
                //    return Unauthorized();
                //}
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExpert(string id, ExpertVM expertVM)
        {
            if (id != expertVM.ExpertId)
            {
                return BadRequest();
            }

            try
            {
                //var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                //if (!string.IsNullOrEmpty(role))
                //{
                //    if (role == "ADMIN" && role == "STAFF")
                //    {
                var expert = Validate(expertVM);
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
                //}
                //    else
                //    {
                //        return Ok(new
                //        {
                //            Status = 0,
                //            Message = "Role Denied",
                //            Data = new { }
                //        });
                //    }
                //}
                //else
                //{
                //    return Unauthorized();
                //}
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteExpert(string id)
        {
            //if (ingredientService.Get(id) == null)
            //{
            //    return BadRequest();
            //}

            try
            {
                //var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                //if (!string.IsNullOrEmpty(role))
                //{
                //if (role == "ADMIN" && role == "STAFF")
                //{
                var check = expertService.Delete(id);
                return await check ? Ok(new
                {
                    Status = 1,
                    Message = "Delete Expert Success"
                }) : Ok(new
                {
                    Status = 0,
                    Message = "Delete Expert Fail"
                });
                //}
                //else
                //{
                //    return Ok(new
                //    {
                //        Status = 0,
                //        Message = "Role Denied",
                //        Data = new { }
                //    });
                //}
                //else
                //{
                //    return BadRequest();
                //}
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
                var check = expertService.ResetPassword(resetPassword.Phone, resetPassword.NewPassword).Result;
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

        private Expert Validate(ExpertVM expertVM)
        {
            if (string.IsNullOrEmpty(expertVM.Email.Trim()))
            {
                throw new Exception("Expert Email cannot be empty!!!");
            }
            if (string.IsNullOrEmpty(expertVM.GoogleId.Trim()))
            {
                throw new Exception("Expert GoogleId cannot be empty!!!");
            }
            if (string.IsNullOrEmpty(expertVM.FacebookId.Trim()))
            {
                throw new Exception("Expert GoogleId cannot be empty!!!");
            }
            if (string.IsNullOrEmpty(expertVM.PhoneNum.Trim()))
            {
                throw new Exception("Expert GoogleId cannot be a negative number!!!");
            }
            if (string.IsNullOrEmpty(expertVM.Avatar.Trim()))
            {
                throw new Exception("Expert Avatar cannot be a negative number!!!");
            }
            if (expertVM.DateOfBirth == DateTime.Now)
            {
                throw new Exception("Expert DateOfBirth cannot be a date now!!!");
            }
            if (expertVM.Gender < 0)
            {
                throw new Exception("Expert Gender cannot be a date now!!!");
            }
            if (string.IsNullOrEmpty(expertVM.Username.Trim()))
            {
                throw new Exception("Expert Username cannot be a negative number!!!");
            }
            if (string.IsNullOrEmpty(expertVM.Password.Trim()))
            {
                throw new Exception("Expert Password cannot be empty!!!");
            }

            if (string.IsNullOrEmpty(expertVM.Name.Trim()))
            {
                throw new Exception("Expert Name cannot be empty!!!");
            }
            if (string.IsNullOrEmpty(expertVM.Title.Trim()))
            {
                throw new Exception("Expert Title cannot be empty!!!");
            }
            if (string.IsNullOrEmpty(expertVM.Position.Trim()))
            {
                throw new Exception("Expert Position cannot be empty!!!");
            }
            if (string.IsNullOrEmpty(expertVM.WorkUnit.Trim()))
            {
                throw new Exception("Expert WorkUnit cannot be empty!!!");
            }
            if (string.IsNullOrEmpty(expertVM.Description.Trim()))
            {
                throw new Exception("Expert Description cannot be empty!!!");
            }
            if (string.IsNullOrEmpty(expertVM.ProfessionalQualification.Trim()))
            {
                throw new Exception("Expert ProfessionalQualification cannot be empty!!!");
            }
            if (string.IsNullOrEmpty(expertVM.WorkProgress.Trim()))
            {
                throw new Exception("Expert WorkProgress cannot be empty!!!");
            }
            if (string.IsNullOrEmpty(expertVM.Achievements.Trim()))
            {
                throw new Exception("Expert Achievements cannot be empty!!!");
            }
            return mapper.Map<Expert>(expertVM);
        }
    }
}
