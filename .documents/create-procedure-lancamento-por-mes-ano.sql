delimiter $$

CREATE PROCEDURE `LancamentosPorMesAno`(IN IdUsuario INT,IN mes INT, IN ano INT)
BEGIN

Select cast(CONV(SUBSTRING(uuid(), 4, 4), 16, 10) as UNSIGNED) as id, lancamentos.* From (  
Select d.UsuarioId, data, d.CategoriaId, valor as valor, d.id as DespesaId, 0 as ReceitaId, d.descricao, null as DataCriacao  
  FROM Despesa d  
 Inner Join Categoria c on d.CategoriaId = c.id  
 where d.UsuarioId = IdUsuario  
   and Month(data) = mes  
   and  Year(data) = ano  
 union  
Select r.UsuarioId, data, r.CategoriaId, valor,  0 as DespesaId, r.id as ReceitaId, r.descricao,  null as DataCriacao  
  FROM Receita r  
 Inner Join Categoria cr on r.CategoriaId = cr.id  
 where r.UsuarioId = IdUsuario  
   and Month(data) = mes  
   and  Year(data) = ano  
) lancamentos ;

END$$

