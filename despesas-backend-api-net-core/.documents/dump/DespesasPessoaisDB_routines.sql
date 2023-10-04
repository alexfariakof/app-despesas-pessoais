CREATE DATABASE  IF NOT EXISTS `DespesasPessoaisDB` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `DespesasPessoaisDB`;
-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: db-mysql-server.ckoywvv9hary.us-east-1.rds.amazonaws.com    Database: DespesasPessoaisDB
-- ------------------------------------------------------
-- Server version	8.0.32

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
SET @MYSQLDUMP_TEMP_LOG_BIN = @@SESSION.SQL_LOG_BIN;
SET @@SESSION.SQL_LOG_BIN= 0;

--
-- GTID state at the beginning of the backup 
--

SET @@GLOBAL.GTID_PURGED=/*!80000 '+'*/ '';

--
-- Dumping routines for database 'DespesasPessoaisDB'
--
/*!50003 DROP PROCEDURE IF EXISTS `GetSaldoByIdUsuario` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb3 */ ;
/*!50003 SET character_set_results = utf8mb3 */ ;
/*!50003 SET collation_connection  = utf8mb3_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `GetSaldoByIdUsuario`(IN IdUsuario INT)
BEGIN
SET @receita := (SELECT CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END as Saldo FROM Receita Where UsuarioId = IdUsuario);
SET @despesa := (SELECT CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END as Saldo FROM Despesa Where UsuarioId = IdUsuario);
Select FORMAT(@receita - @despesa, 2) as Saldo;



END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `LancamentosPorMesAno` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb3 */ ;
/*!50003 SET character_set_results = utf8mb3 */ ;
/*!50003 SET collation_connection  = utf8mb3_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ENGINE_SUBSTITUTION' */ ;
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
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SomatorioDespesasPorAno` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb3 */ ;
/*!50003 SET character_set_results = utf8mb3 */ ;
/*!50003 SET collation_connection  = utf8mb3_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ENGINE_SUBSTITUTION' */ ;
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
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `SomatorioReceitasPorAno` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb3 */ ;
/*!50003 SET character_set_results = utf8mb3 */ ;
/*!50003 SET collation_connection  = utf8mb3_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ENGINE_SUBSTITUTION' */ ;
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
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
SET @@SESSION.SQL_LOG_BIN = @MYSQLDUMP_TEMP_LOG_BIN;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-09-29 23:14:11
