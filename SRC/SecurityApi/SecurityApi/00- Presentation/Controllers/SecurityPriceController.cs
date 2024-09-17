using Microsoft.AspNetCore.Mvc;
using SecurityApi.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;

namespace SecurityApi.Presentation.Controllers;

[ApiController]
[ExcludeFromCodeCoverage]
[Route("securityprice")]
public class SecurityPriceController : Controller
{
    private readonly ISecurityPriceService _services;
    public SecurityPriceController(ISecurityPriceService services)
    {
        _services = services;
    }
    [HttpPatch("{isin}")]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(IEnumerable<bool>), "application/json")]
    public async Task<IActionResult> AddISIN([FromRoute] string isin)
    {
        var result = await _services.AddSecurityPrices(isin);


        return Ok(new
        {
            Data = result
        });
    }
}
