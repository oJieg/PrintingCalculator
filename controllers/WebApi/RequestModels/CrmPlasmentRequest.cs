using Microsoft.AspNetCore.Mvc;

namespace printing_calculator.controllers.WebApi.RequestModels
{
    public class CrmPlasmentRequest
    {
        [FromForm(Name = "AUTH_ID")]
        public string AuthorizationId  { get; set; } = string.Empty;

        [FromForm(Name = "PLACEMENT_OPTIONS")]
        public string PlacementOptions { get; set; }

    }
}
