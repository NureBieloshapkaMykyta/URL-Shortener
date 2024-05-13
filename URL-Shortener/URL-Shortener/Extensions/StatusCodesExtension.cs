using Application.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace URL_Shortener.Extensions;

public static class StatusCodesExtension
{
    public static IActionResult HandleResult<T>(this ControllerBase controller, Result<T> result)
    {
        if (result.IsSuccessful && result.Data != null)
            return controller.Ok(result.Data);

        if (result.IsSuccessful && result.Data == null)
            return controller.NotFound();

        return controller.BadRequest(result.Message);
    }
}
