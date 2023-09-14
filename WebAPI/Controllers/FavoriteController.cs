using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.EntityModels;
using Services;
using System;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;
        private readonly IMapper _mapper;

        public FavoriteController(IFavoriteService favoriteService, IMapper mapper)
        {
            _favoriteService = favoriteService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecipeFavorite(string cusId)
        {
            try
            {
                var list = _favoriteService.GetAllRecipeFavoriteOfOneCus(cusId);
                var favorite = new List<Favorite>();
                foreach (var item in list)
                {
                    favorite.Add(_mapper.Map<Favorite>(item));
                }
                return Ok(new
                {
                    Status = 1,
                    Message = "Success",
                    Data = favorite
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipeFavorite(FavoriteVM model)
        {
            try
            {
                var favorite = _mapper.Map<Favorite>(model);
                var check = _favoriteService.Add(favorite);
                return await check ? Ok(new
                {
                    Message = "Add Success!!!"
                }) : Ok(new
                {
                    Message = "Add Fail"
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFavorite(string cusId, string recipeId)
        {
            try
            {
                return await _favoriteService.Delete(cusId, recipeId) ? Ok(new
                {
                    Message = "Delete Success!!!"
                }) : Ok(new
                {
                    Message = "Delete Fail"
                });
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
