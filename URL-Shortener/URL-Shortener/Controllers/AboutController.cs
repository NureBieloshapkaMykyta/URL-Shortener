using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using URL_Shortener.DTOs.ViewModels;
using URL_Shortener.Helpers;

namespace URL_Shortener.Controllers
{
    [Route("[controller]")]
    public class AboutController : Controller
    {
        [HttpGet("Get")]
        public IActionResult Get()
        {
            var displayDescriptionVM = new DisplayAlhoritmDescriptionVM() { Description = AlhoritmDescription.Description };

            return View(displayDescriptionVM);
        }

        [Authorize(Roles = RolesConstants.Admin)]
        [HttpPost("Update")]
        public IActionResult Update([FromBody] string description)
        {
            AlhoritmDescription.Description = description;

            return RedirectToAction("Get");
        }
    }
}
