using Microsoft.AspNetCore.Mvc;
using SecurityApi.Application.Interfaces;
using SecurityApi.Presentation.ViewModels;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;

namespace SecurityApi.Presentation.Controllers;

[ApiController]
[ExcludeFromCodeCoverage]
[Route("securityprices")]
public class SecurityPriceController : Controller
{
    private readonly ISecurityPriceService _services;
    public SecurityPriceController(ISecurityPriceService services)
    {
        _services = services;
    }
    [HttpPost("")]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(IEnumerable<bool>), "application/json")]
    public async Task<IActionResult> AddISIN([FromBody] SecurityPriceViewModel vm)
    {
        var result = await _services.AddSecurityPrices(vm.ISINs);
        return Ok(new
        {
            Data = result
        });
    }
}
