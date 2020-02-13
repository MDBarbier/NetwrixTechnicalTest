using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechnicalTestApp.ViewModels;

namespace TechnicalTestApp.Controllers
{
    public class ErrorsController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HandleErrorCode(int statusCode)
        {
            var statusCodeData = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var errorViewModel = new ErrorViewModel()
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            switch (statusCode)
            {
                case 404:
                    errorViewModel.ErrorMessage = "404 Sorry that page could not be found!";
                    errorViewModel.RouteOfException = statusCodeData.OriginalPath;
                    break;
                case 500:
                    errorViewModel.ErrorMessage = "500 internal server error";
                    errorViewModel.RouteOfException = statusCodeData.OriginalPath;
                    break;
                default:
                    errorViewModel.ErrorMessage = "500 internal server error";                    
                    if (statusCodeData != null) errorViewModel.RouteOfException = statusCodeData.OriginalPath;
                    break;
            }

            return View(errorViewModel);
        }
    }
}