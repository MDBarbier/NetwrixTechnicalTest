using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TechnicalTestApp.Controllers
{
    public class ErrorsController : Controller
    {
        //[Route("Error/500")]
        //public IActionResult Error500()
        //{
        //    var exceptionData = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        //    if (exceptionData != null)
        //    {
        //        ViewBag.ErrorMessage = exceptionData.Error.Message;
        //        ViewBag.RouteOfException = exceptionData.Path;
        //    }

        //    return View();
        //}

        [Route("Error/{statusCode}")]
        public IActionResult HandleErrorCode(int statusCode)
        {
            var statusCodeData = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "404 Sorry that page could not be found!";
                    ViewBag.RouteOfException = statusCodeData.OriginalPath;
                    break;
                case 500:
                    ViewBag.ErrorMessage = "500 internal server error";
                    ViewBag.RouteOfException = statusCodeData.OriginalPath;
                    break;
                default:
                    break;
            }

            return View();
        }
    }
}