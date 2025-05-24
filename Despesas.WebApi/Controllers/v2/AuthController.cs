using Asp.Versioning;
using Despesas.WebApi.Controllers.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Despesas.WebApi.Controllers.v2;

[ApiVersion("2")]
[Route("v{version:apiVersion}/[controller]")]
[ApiController]
public class AuthController : BaseAuthController
{
    public AuthController(): base() { }    
}