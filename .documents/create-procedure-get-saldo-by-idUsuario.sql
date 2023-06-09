delimiter $$

CREATE PROCEDURE `GetSaldoByIdUsuario`(IN IdUsuario INT)
BEGIN
SET @receita := (SELECT CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END as Saldo FROM Receita Where UsuarioId = IdUsuario);
SET @despesa := (SELECT CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END as Saldo FROM Despesa Where UsuarioId = IdUsuario);
Select FORMAT(@receita - @despesa, 2) as Saldo;



END$$

