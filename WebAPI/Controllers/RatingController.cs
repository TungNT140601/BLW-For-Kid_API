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
    public class RatingController : ControllerBase
    {
        private readonly IRatingService ratingService;
        private readonly IMapper mapper;

        public RatingController(IRatingService ratingService, IMapper mapper)
        {
            this.ratingService = ratingService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = ratingService.GetAllRatingOfOneCus();
                var rating = new List<Rating>();
                foreach (var item in list)
                {
                    rating.Add(mapper.Map<Rating>(item));
                }
                return Ok(new
                {
                    Status = 1,
                    Data = rating
            });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRatingOfCus(string id)
        {
            try
            {
                var rating = ratingService.GetRating(id);
                return Ok(new
                {
                    Status = 1,
                    Data = rating
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRating(RatingAddVM model)
        {
            try
            {
                var rating = mapper.Map<Rating>(model);
                var check = ratingService.Add(rating);
                return await check ? Ok(new
                {
                    Message = "Add Success!!!"
                }) : Ok(new
                {
                    Message = "Add Fail"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRating(RatingUpdateVM model)
        {
            try
            {
                var rating = mapper.Map<Rating>(model);
                var check = ratingService.Update(rating);
                return await check ? Ok(new
                {
                    Message = "Update Success!!!"
                }) : Ok(new
                {
                    Message = "Update Fail"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRating(string id)
        {
            try
            {              
                var check = ratingService.Delete(id);
                return await check ? Ok(new
                {
                    Message = "Delete Success!!!"
                }) : Ok(new
                {
                    Message = "Delete Fail"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    } 
}
