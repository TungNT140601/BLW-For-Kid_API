﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.EntityModels;
using Services;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffAccountController : ControllerBase
    {
        protected readonly IStaffAccountService _staffAccountService;
        private readonly IMapper _mapper;

        public StaffAccountController(IStaffAccountService staffAccountService, IMapper mapper)
        {
            _staffAccountService = staffAccountService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
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
                else if(string.IsNullOrEmpty(model.Password))
                {
                    return StatusCode(400, new
                    {
                        Message = "Password cannot be empty!!!"
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
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(string id, StaffAccountUpdateVM model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Email))
                {
                    return StatusCode(400, new
                    {
                        Message = "Email cannot be empty!!!"
                    });
                }
                else if (string.IsNullOrEmpty(model.Password))
                {
                    return StatusCode(400, new
                    {
                        Message = "Password cannot be empty!!!"
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

        [HttpDelete("{id}")]
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
    }
}
