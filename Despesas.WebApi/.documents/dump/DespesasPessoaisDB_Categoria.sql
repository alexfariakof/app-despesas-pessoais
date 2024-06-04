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
-- Table structure for table `Categoria`
--

DROP TABLE IF EXISTS `Categoria`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Categoria` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Descricao` varchar(100) DEFAULT NULL,
  `UsuarioId` int NOT NULL,
  `TipoCategoria` smallint unsigned NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Categoria_UsuarioId` (`UsuarioId`),
  CONSTRAINT `FK_Categoria_Usuario_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=68 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Categoria`
--

LOCK TABLES `Categoria` WRITE;
/*!40000 ALTER TABLE `Categoria` DISABLE KEYS */;
INSERT INTO `Categoria` (`Id`, `Descricao`, `UsuarioId`, `TipoCategoria`) VALUES (1,'Alimentação',1,1),(2,'Casa',1,1),(3,'Serviços',1,1),(4,'Saúde',1,1),(5,'Imposto',1,1),(6,'Transporte',1,1),(7,'Lazer',1,1),(8,'Outros',1,1),(9,'Salário',1,2),(10,'Prêmio',1,2),(11,'Investimento',1,2),(12,'Benefício',1,2),(13,'Outros',1,2),(14,'Alimentação',2,1),(15,'Casa',2,1),(16,'Serviços',2,1),(17,'Saúde',2,1),(18,'Imposto',2,1),(19,'Transporte',2,1),(20,'Lazer',2,1),(21,'Outros',2,1),(22,'Salário',2,2),(23,'Prêmio',2,2),(24,'Investimento',2,2),(25,'Benefício',2,2),(26,'Outros',2,2),(27,'Alimentação',3,1),(28,'Casa',3,1),(29,'Serviços',3,1),(30,'Saúde',3,1),(31,'Imposto',3,1),(32,'Transporte',3,1),(33,'Lazer',3,1),(34,'Outros',3,1),(35,'Salário',3,2),(36,'Prêmio',3,2),(37,'Investimento',3,2),(38,'Benefício',3,2),(39,'Outros',3,2),(41,'Alimentação',4,1),(42,'Casa',4,1),(43,'Serviços',4,1),(44,'Saúde',4,1),(45,'Imposto',4,1),(46,'Transporte',4,1),(47,'Lazer',4,1),(48,'Outros',4,1),(49,'Salário',4,2),(50,'Prêmio',4,2),(51,'Investimento',4,2),(52,'Benefício',4,2),(53,'Outros',4,2),(54,'Alimentação',5,1),(55,'Casa',5,1),(56,'Serviços',5,1),(57,'Saúde',5,1),(58,'Imposto',5,1),(59,'Transporte',5,1),(60,'Lazer',5,1),(61,'Outros',5,1),(62,'Salário',5,2),(63,'Prêmio',5,2),(64,'Investimento',5,2),(65,'Benefício',5,2),(66,'Outros',5,2),(67,'teste',2,1);
/*!40000 ALTER TABLE `Categoria` ENABLE KEYS */;
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

-- Dump completed on 2023-09-29 23:13:45
