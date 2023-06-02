-- --------------------------------------------------------------------------------
-- Routine DDL
-- Note: comments before and after the routine body will not be stored by the server
-- --------------------------------------------------------------------------------
DELIMITER $$

CREATE PROCEDURE `SomatorioReceitasPorAno` (IN IdUsuario INT,IN ano INT)
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

END $$
DELIMITER ;