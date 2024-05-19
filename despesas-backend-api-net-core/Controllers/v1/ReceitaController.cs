using Asp.Versioning;
<<<<<<< HEAD
using Business.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Business.Generic;
=======
using Business.Dtos.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Business.Abstractions.Generic;
>>>>>>> feature/Create-Migrations-AZURE_SQL_SERVER

namespace despesas_backend_api_net_core.Controllers.v1;

[ApiVersion("1")]
[Route("v1/[controller]")]
[ApiController]
public class ReceitaController : AuthController
{
<<<<<<< HEAD
    private IBusiness<ReceitaDto> _receitaBusiness;
    public ReceitaController(IBusiness<ReceitaDto> receitaBusiness)
=======
    private IBusiness<ReceitaDto, Receita> _receitaBusiness;
    public ReceitaController(IBusiness<ReceitaDto, Receita> receitaBusiness)
>>>>>>> feature/Create-Migrations-AZURE_SQL_SERVER
    {
        _receitaBusiness = receitaBusiness;
    }

    [HttpGet]
    [Authorize("Bearer")]
    public IActionResult Get()
    { 
        return Ok(_receitaBusiness.FindAll(IdUsuario));
    }

    [HttpGet("GetById/{id}")]
    [Authorize("Bearer")]
    public IActionResult GetById([FromRoute]int id)
    {
        try
        {
            var _receita = _receitaBusiness.FindById(id, IdUsuario);

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
    [Authorize("Bearer")]
    public IActionResult Post([FromBody] ReceitaDto receita)
    {
        try
        {
<<<<<<< HEAD
            receita.IdUsuario = IdUsuario;
=======
            receita.UsuarioId = IdUsuario;
>>>>>>> feature/Create-Migrations-AZURE_SQL_SERVER
            return new OkObjectResult(new { message = true, receita = _receitaBusiness.Create(receita) });
        }
        catch
        {
            return BadRequest(new { message = "Não foi possível realizar o cadastro da receita!" });
        }            
    }

    [HttpPut]
    [Authorize("Bearer")]
    public IActionResult Put([FromBody] ReceitaDto receita)
    {

<<<<<<< HEAD
        receita.IdUsuario = IdUsuario;
=======
        receita.UsuarioId = IdUsuario;
>>>>>>> feature/Create-Migrations-AZURE_SQL_SERVER
        var updateReceita = _receitaBusiness.Update(receita);

        if (updateReceita == null)
            return BadRequest(new { message = "Não foi possível atualizar o cadastro da receita." });

        return new OkObjectResult(new { message = true, receita = updateReceita });
    }

    [HttpDelete("{idReceita}")]
    [Authorize("Bearer")]
    public IActionResult Delete(int idReceita)
    {
        ReceitaDto receita = _receitaBusiness.FindById(idReceita, IdUsuario);
<<<<<<< HEAD
        if (receita == null || IdUsuario != receita.IdUsuario)
=======
        if (receita == null || IdUsuario != receita.UsuarioId)
>>>>>>> feature/Create-Migrations-AZURE_SQL_SERVER
        {
            return BadRequest(new { message = "Usuário não permitido a realizar operação!" });
        }

        if (_receitaBusiness.Delete(receita))
            return new OkObjectResult(new { message = true });
        else
            return BadRequest(new { message = "Erro ao excluir Receita!" });            
        
    }
}