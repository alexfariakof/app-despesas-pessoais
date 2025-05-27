using Business.Dtos.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Business.Abstractions.Generic;

namespace Despesas.WebApi.Controllers.v1;

public class ReceitaController : AuthController
{
    private IBusiness<ReceitaDto, Receita> _receitaBusiness;
    public ReceitaController(IBusiness<ReceitaDto, Receita> receitaBusiness)
    {
        _receitaBusiness = receitaBusiness;
    }

    [HttpGet]
    [Authorize("Bearer", Roles = "User")]
    public IActionResult Get()
    {
        return Ok(_receitaBusiness.FindAll(UserIdentity));
    }

    [HttpGet("GetById/{id}")]
    [Authorize("Bearer", Roles = "User")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        try
        {
            var _receita = _receitaBusiness.FindById(id, UserIdentity);

            if (_receita == null)
                return BadRequest(new { message = "Nenhuma receita foi encontrada." });

            return new OkObjectResult(new { message = true, receita = _receita });
        }
        catch
        {
            return BadRequest(new { message = "Não foi possível realizar a consulta da receita." });
        }
    }

    [HttpPost]
    [Authorize("Bearer", Roles = "User")]
    public IActionResult Post([FromBody] ReceitaDto receita)
    {
        try
        {
            receita.UsuarioId = UserIdentity;
            return new OkObjectResult(new { message = true, receita = _receitaBusiness.Create(receita) });
        }
        catch
        {
            return BadRequest(new { message = "Não foi possível realizar o cadastro da receita!" });
        }
    }

    [HttpPut]
    [Authorize("Bearer", Roles = "User")]
    public IActionResult Put([FromBody] ReceitaDto receita)
    {

        receita.UsuarioId = UserIdentity;
        var updateReceita = _receitaBusiness.Update(receita);

        if (updateReceita == null)
            return BadRequest(new { message = "Não foi possível atualizar o cadastro da receita." });

        return new OkObjectResult(new { message = true, receita = updateReceita });
    }

    [HttpDelete("{idReceita}")]
    [Authorize("Bearer", Roles = "User")]
    public IActionResult Delete(Guid idReceita)
    {
        ReceitaDto receita = _receitaBusiness.FindById(idReceita, UserIdentity);
        if (receita == null || UserIdentity != receita.UsuarioId)
        {
            return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
        }

        if (_receitaBusiness.Delete(receita))
            return new OkObjectResult(new { message = true });
        else
            return BadRequest(new { message = "Erro ao excluir Receita!" });

    }
}