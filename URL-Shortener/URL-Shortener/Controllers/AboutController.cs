using Application.Abstractions;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using URL_Shortener.DTOs.Requests;
using URL_Shortener.DTOs.ViewModels;
using URL_Shortener.Extensions;
using URL_Shortener.Helpers;

namespace URL_Shortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AboutController : Controller
    {
        private readonly IRepository<AlhoritmInfo> _repository;

        public AboutController(IRepository<AlhoritmInfo > repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string? name = BaseAlhoritmConstants.Name)
        {
            var displayDescription = await _repository.GetAllAsync(x=>x.Name == name);
            if (!displayDescription.IsSuccessful)
            {
                return BadRequest(displayDescription.Message);
            }

            return Ok(displayDescription.Data.FirstOrDefault());
        }

        [Authorize(Roles = RolesConstants.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]Guid id,[FromBody] UpdateDescriptionRequest request)
        {
            var alhoritm = await _repository.GetAllAsync(x=>x.Id==id);
            if (!alhoritm.IsSuccessful) 
            {
                return BadRequest(alhoritm.Message);
            }

            var item = alhoritm.Data.First();
            item.Description = request.Description;
            var updateResult = await _repository.UpdateItemAsync(item);

            return this.HandleResult(updateResult);
        }
    }
}
