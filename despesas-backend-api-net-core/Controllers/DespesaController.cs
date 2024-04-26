using Business.Dtos;
using Business.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace despesas_backend_api_net_core.Controllers;

[Route("[controller]")]
[ApiController]
public class DespesaController : AuthController
{
    private IBusiness<DespesaDto> _despesaBusiness;
    public DespesaController(IBusiness<DespesaDto> despesaBusiness)
    {
        _despesaBusiness = despesaBusiness;
    }

    [HttpGet]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(IList<DespesaDto>))]    
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Get()
    {
        try
        {
            return Ok(_despesaBusiness.FindAll(IdUsuario));
        }
        catch
        {
            return Ok(new List<DespesaDto>());
        }
    }

    [HttpGet("GetById/{id}")]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(DespesaDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Get([FromRoute]int id)
    {
        try
        {
            var _despesa = _despesaBusiness.FindById(id, IdUsuario);
            if (_despesa == null)
                return BadRequest("Nenhuma despesa foi encontrada.");

            return new OkObjectResult(_despesa);
        }
        catch
        {
            return BadRequest("Não foi possível realizar a consulta da despesa.");
        }
    }

    [HttpPost]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(DespesaDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Post([FromBody] DespesaDto despesa)
    {
        try
        {
            despesa.IdUsuario = IdUsuario;
            return new OkObjectResult(_despesaBusiness.Create(despesa));
        }
        catch
        {
            return BadRequest("Não foi possível realizar o cadastro da despesa.");
        }
    }

    [HttpPut]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(DespesaDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Put([FromBody] DespesaDto despesa)
    {
        try
        {
            despesa.IdUsuario = IdUsuario;
            var updateDespesa = _despesaBusiness.Update(despesa);
            if (updateDespesa == null)
                throw new Exception();            

            return new OkObjectResult(updateDespesa);
        }
        catch
        {
            return BadRequest("Não foi possível atualizar o cadastro da despesa.");
        }
    }

    [HttpDelete("{idDespesa}")]
    [Authorize("Bearer")]
    [ProducesResponseType((200), Type = typeof(bool))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Delete(int idDespesa)
    {
        try
        {
            DespesaDto despesa = _despesaBusiness.FindById(idDespesa, IdUsuario);
            if (despesa == null || IdUsuario != despesa.IdUsuario)
            {
                return BadRequest("Usuário não permitido a realizar operação!");
            }

            if (_despesaBusiness.Delete(despesa))
                return new OkObjectResult(true);
            else
                throw new Exception();
        }
        catch
        {
            return BadRequest("Erro ao excluir Despesa!");
        }
    }
}