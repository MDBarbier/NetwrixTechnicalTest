using System;

namespace TechnicalTestApp.ViewModels
{
    public class ErrorViewModel
    {
        public string ErrorMessage { get; set; }
        public string RouteOfException { get; set; }
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
