using Asp.Versioning;
using Business.Abstractions;
using Business.Dtos.v2;
using Business.HyperMedia.Filters;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers.v2;

[ApiVersion("2")]
[Route("v{version:apiVersion}/[controller]")]
public class ReceitaController : AuthController
{
    private readonly BusinessBase<ReceitaDto, Receita> _receitaBusiness;
    public ReceitaController(BusinessBase<ReceitaDto, Receita> receitaBusiness)
    {
        _receitaBusiness = receitaBusiness;
    }

    [HttpGet]
    [Authorize("Bearer")]
    [ProducesResponseType(200, Type = typeof(IList<ReceitaDto>))]
    [ProducesResponseType(401, Type = typeof(UnauthorizedResult))]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Get()
    {
        try
        {
            return Ok(_receitaBusiness.FindAll(IdUsuario));
        }
        catch
        {
            return Ok(new List<ReceitaDto>());
        }
    }

    [HttpGet("GetById/{id}")]
    [Authorize("Bearer")]
    [ProducesResponseType(200, Type = typeof(ReceitaDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401, Type = typeof(UnauthorizedResult))]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult GetById([FromRoute] int id)
    {
        try
        {
            var _receita = _receitaBusiness.FindById(id, IdUsuario);

            if (_receita == null)
                return BadRequest("Nenhuma receita foi encontrada.");

            return new OkObjectResult(_receita);
        }
        catch
        {
            return BadRequest("Não foi possível realizar a consulta da receita.");
        }
    }

    [HttpPost]
    [Authorize("Bearer")]
    [ProducesResponseType(200, Type = typeof(ReceitaDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401, Type = typeof(UnauthorizedResult))]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Post([FromBody] ReceitaDto receita)
    {
        try
        {
            receita.IdUsuario = IdUsuario;
            return new OkObjectResult(_receitaBusiness.Create(receita));
        }
        catch
        {
            return BadRequest("Não foi possível realizar o cadastro da receita!");
        }
    }

    [HttpPut]
    [Authorize("Bearer")]
    [ProducesResponseType(200, Type = typeof(ReceitaDto))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401, Type = typeof(UnauthorizedResult))]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Put([FromBody] ReceitaDto receita)
    {
        try
        {
            receita.IdUsuario = IdUsuario;
            var updateReceita = _receitaBusiness.Update(receita);
            if (updateReceita == null) throw new Exception();
            return new OkObjectResult(updateReceita);
        }
        catch
        {
            return BadRequest("Não foi possível atualizar o cadastro da receita.");
        }
    }

    [HttpDelete("{idReceita}")]
    [Authorize("Bearer")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(401, Type = typeof(UnauthorizedResult))]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Delete(int idReceita)
    {
        try
        {
            ReceitaDto receita = _receitaBusiness.FindById(idReceita, IdUsuario);
            if (receita == null || IdUsuario != receita.IdUsuario)
                return BadRequest("Usuário não permitido a realizar operação!");

            if (_receitaBusiness.Delete(receita))
                return new OkObjectResult(true);
            else
                throw new Exception();
        }
        catch
        {
            return BadRequest("Erro ao excluir Receita!");
        }
    }
}