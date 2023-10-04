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
-- Table structure for table `Lancamento`
--

DROP TABLE IF EXISTS `Lancamento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Lancamento` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Valor` decimal(10,2) NOT NULL,
  `Data` timestamp NOT NULL,
  `Descricao` varchar(100) NOT NULL,
  `UsuarioId` int NOT NULL,
  `DespesaId` int DEFAULT NULL,
  `ReceitaId` int DEFAULT NULL,
  `CategoriaId` int NOT NULL,
  `DataCriacao` timestamp NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Lancamento_CategoriaId` (`CategoriaId`),
  KEY `IX_Lancamento_DespesaId` (`DespesaId`),
  KEY `IX_Lancamento_ReceitaId` (`ReceitaId`),
  KEY `IX_Lancamento_UsuarioId` (`UsuarioId`),
  CONSTRAINT `FK_Lancamento_Categoria_CategoriaId` FOREIGN KEY (`CategoriaId`) REFERENCES `Categoria` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Lancamento_Despesa_DespesaId` FOREIGN KEY (`DespesaId`) REFERENCES `Despesa` (`Id`),
  CONSTRAINT `FK_Lancamento_Receita_ReceitaId` FOREIGN KEY (`ReceitaId`) REFERENCES `Receita` (`Id`),
  CONSTRAINT `FK_Lancamento_Usuario_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Lancamento`
--

LOCK TABLES `Lancamento` WRITE;
/*!40000 ALTER TABLE `Lancamento` DISABLE KEYS */;
/*!40000 ALTER TABLE `Lancamento` ENABLE KEYS */;
UNLOCK TABLES;
SET @@SESSION.SQL_LOG_BIN = @MYSQLDUMP_TEMP_LOG_BIN;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-09-29 23:13:24
