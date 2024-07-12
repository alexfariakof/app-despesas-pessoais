using Asp.Versioning;
using Business.Dtos.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Business.Abstractions.Generic;

namespace Despesas.WebApi.Controllers.v1;

[ApiVersion("1")]
[Route("v1/[controller]")]
[ApiController]
public class DespesaController : AuthController
{
    private IBusiness<DespesaDto, Despesa> _despesaBusiness;
    public DespesaController(IBusiness<DespesaDto, Despesa> despesaBusiness)
    {
        _despesaBusiness = despesaBusiness;
    }

    [HttpGet]
    [Authorize("Bearer", Roles = "User")]
    public IActionResult Get()
    {
        return Ok(_despesaBusiness.FindAll(UserIdentity));
    }

    [HttpGet("GetById/{id}")]
    [Authorize("Bearer", Roles = "User")]
    public IActionResult Get([FromRoute] Guid id)
    {
        try
        {
            var _despesa = _despesaBusiness.FindById(id, UserIdentity);

            if (_despesa == null)
                return BadRequest(new { message = "Nenhuma despesa foi encontrada." });

            return new OkObjectResult(new { message = true, despesa = _despesa });
        }
        catch
        {
            return BadRequest(new { message = "Não foi possível realizar a consulta da despesa." });
        }
    }

    [HttpPost]
    [Authorize("Bearer", Roles = "User")]
    public IActionResult Post([FromBody] DespesaDto despesa)
    {
        try
        {
            despesa.UsuarioId = UserIdentity;
            return new OkObjectResult(new { message = true, despesa = _despesaBusiness.Create(despesa) });
        }
        catch
        {
            return BadRequest(new { message = "Não foi possível realizar o cadastro da despesa." });
        }
    }

    [HttpPut]
    [Authorize("Bearer", Roles = "User")]
    public IActionResult Put([FromBody] DespesaDto despesa)
    {
        despesa.UsuarioId = UserIdentity;
        var updateDespesa = _despesaBusiness.Update(despesa);
        if (updateDespesa == null)
            return BadRequest(new { message = "Não foi possível atualizar o cadastro da despesa." });

        return new OkObjectResult(new { message = true, despesa = updateDespesa });
    }

    [HttpDelete("{idDespesa}")]
    [Authorize("Bearer", Roles = "User")]
    public IActionResult Delete(Guid idDespesa)
    {
        DespesaDto despesa = _despesaBusiness.FindById(idDespesa, UserIdentity);
        if (despesa == null || UserIdentity != despesa.UsuarioId)
        {
            return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
        }

        if (_despesaBusiness.Delete(despesa))
            return new OkObjectResult(new { message = true });
        else
            return BadRequest(new { message = "Erro ao excluir Despesa!" });
    }
}