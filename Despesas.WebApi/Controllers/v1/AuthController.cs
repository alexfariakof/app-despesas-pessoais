using Asp.Versioning;
using Despesas.WebApi.Controllers.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Despesas.WebApi.Controllers.v1;

[ApiController]
[ApiVersion("1")]
[Route("v1/[controller]")]

public class AuthController : BaseAuthController
{
    public AuthController() : base() { }
}