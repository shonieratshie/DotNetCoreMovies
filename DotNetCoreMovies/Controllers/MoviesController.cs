using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dapper.Application.Interfaces;
using Dapper.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreMovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public MoviesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await unitOfWork.Movies.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Movies.GetByIDAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Movie movie)
        {
            var isDuplicate = await unitOfWork.Movies.GetByNameAsync(movie.Name);
            if (isDuplicate != null)
            {
                return BadRequest("Movie with the same name already exists");

            }
            else
            {
                var data = await unitOfWork.Movies.AddAsync(movie);
                return Ok(data);
            }
    


            
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.Movies.DeleteAsync(id);
            return Ok(data);
        }

        [HttpGet("byrating/{ratingId}")]
        public async Task<IActionResult> GetByRatingId(int ratingId)
        {
            var data = await unitOfWork.Movies.GetByRatingIDAsync(ratingId);
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Movie movie)
        {
            var data = await unitOfWork.Movies.UpdateMovieAsync(movie);
            return Ok(data);
        }
    }
}
