USE `DespesasPessoaisDB`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `GetSaldoByIdUsuario`(IN IdUsuario INT)
BEGIN
SET @receita := (SELECT CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END as Saldo FROM Receita Where UsuarioId = IdUsuario);
SET @despesa := (SELECT CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END as Saldo FROM Despesa Where UsuarioId = IdUsuario);
Select FORMAT(@receita - @despesa, 2) as Saldo;
END ;;
DELIMITER ;

DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `LancamentosPorMesAno`(IN IdUsuario INT,IN mes INT, IN ano INT)
BEGIN

Select cast(CONV(SUBSTRING(uuid(), 5, 5), 16, 10) as UNSIGNED) as id, lancamentos.* From (  
Select d.UsuarioId, data, d.CategoriaId, valor as valor, d.id as DespesaId, 0 as ReceitaId, d.descricao, now() as DataCriacao  
  FROM Despesa d  
 Inner Join Categoria c on d.CategoriaId = c.id  
 where d.UsuarioId = IdUsuario  
   and Month(data) = mes  
   and  Year(data) = ano  
 union  
Select r.UsuarioId, data, r.CategoriaId, valor,  0 as DespesaId, r.id as ReceitaId, r.descricao,  now() as DataCriacao  
  FROM Receita r  
 Inner Join Categoria cr on r.CategoriaId = cr.id  
 where r.UsuarioId = IdUsuario  
   and Month(data) = mes  
   and  Year(data) = ano  
) lancamentos Order by Data;

END ;;
DELIMITER ;

DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `SomatorioDespesasPorAno`(IN IdUsuario INT,IN ano INT)
BEGIN
Select  
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 1 and Year(data) = ano) as Janeiro,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 2 and Year(data) = ano) as Fevereiro,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 3 and Year(data) = ano) as Marco,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 4 and Year(data) = ano) as Abril,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 5 and Year(data) = ano) as Maio,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 6 and Year(data) = ano) as Junho,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 7 and Year(data) = ano) as Julho,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 8 and Year(data) = ano) as Agosto,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 9 and Year(data) = ano) as Setembro,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 10 and Year(data) = ano) as Outubro,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 11 and Year(data) = ano) as Novembro,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 12 and Year(data) = ano) as Dezembro;

END ;;
DELIMITER ;

DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `SomatorioReceitasPorAno`(IN IdUsuario INT,IN ano INT)
BEGIN

Select 
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 1 and Year(data) = ano) as Janeiro,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 2 and Year(data) = ano) as Fevereiro,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 3 and Year(data) = ano) as Marco,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 4 and Year(data) = ano) as Abril,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 5 and Year(data) = ano) as Maio,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 6 and Year(data) = ano) as Junho,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 7 and Year(data) = ano) as Julho,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 8 and Year(data) = ano) as Agosto,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 9 and Year(data) = ano) as Setembro,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 10 and Year(data) = ano) as Outubro,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 11 and Year(data) = ano) as Novembro,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 12 and Year(data) = ano) as Dezembro;

END ;;
DELIMITER ;