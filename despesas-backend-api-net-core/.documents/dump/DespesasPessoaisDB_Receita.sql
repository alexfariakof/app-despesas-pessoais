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
-- Table structure for table `Receita`
--

DROP TABLE IF EXISTS `Receita`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Receita` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Data` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Descricao` varchar(100) DEFAULT NULL,
  `Valor` decimal(10,2) NOT NULL DEFAULT '0.00',
  `UsuarioId` int NOT NULL,
  `CategoriaId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Receita_CategoriaId` (`CategoriaId`),
  KEY `IX_Receita_UsuarioId` (`UsuarioId`),
  CONSTRAINT `FK_Receita_Categoria_CategoriaId` FOREIGN KEY (`CategoriaId`) REFERENCES `Categoria` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Receita_Usuario_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=65 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Receita`
--

LOCK TABLES `Receita` WRITE;
/*!40000 ALTER TABLE `Receita` DISABLE KEYS */;
INSERT INTO `Receita` (`Id`, `Data`, `Descricao`, `Valor`, `UsuarioId`, `CategoriaId`) VALUES (1,'2023-01-05 00:00:00','Salário',2000.00,2,22),(2,'2023-05-31 01:45:04','Teste Alteração Receita ',500.50,2,10),(3,'2023-01-20 00:00:00','Investimento',1000.00,2,24),(4,'2023-01-25 00:00:00','Benefício',300.00,2,25),(5,'2023-01-30 00:00:00','Outros ganhos',100.00,2,26),(6,'2023-02-05 00:00:00','Salário',2000.00,2,22),(7,'2023-02-15 00:00:00','Prêmio',500.00,2,23),(8,'2023-02-20 00:00:00','Investimento',1000.00,2,24),(9,'2023-02-25 00:00:00','Benefício',300.00,2,25),(10,'2023-02-28 00:00:00','Outros ganhos',100.00,2,26),(11,'2023-03-05 00:00:00','Salário',2000.00,2,22),(12,'2023-03-15 00:00:00','Prêmio',500.00,2,23),(13,'2023-03-20 00:00:00','Investimento',1000.00,2,24),(14,'2023-03-25 00:00:00','Benefício',300.00,2,25),(15,'2023-03-31 00:00:00','Outros ganhos',100.00,2,26),(16,'2023-04-05 00:00:00','Salário',2000.00,2,22),(17,'2023-04-15 00:00:00','Prêmio',500.00,2,23),(18,'2023-04-20 00:00:00','Investimento',1000.00,2,24),(19,'2023-04-25 00:00:00','Benefício',300.00,2,25),(20,'2023-04-30 00:00:00','Outros ganhos',100.00,2,26),(21,'2023-05-05 00:00:00','Salário',2000.00,2,22),(22,'2023-05-15 00:00:00','Prêmio',500.00,2,23),(23,'2023-05-20 00:00:00','Investimento',1000.00,2,24),(24,'2023-05-25 00:00:00','Benefício',300.00,2,25),(25,'2023-05-31 00:00:00','Outros ganhos',100.00,2,26),(26,'2023-06-05 00:00:00','Salário',2000.00,2,22),(27,'2023-06-15 00:00:00','Prêmio',500.00,2,23),(28,'2023-06-20 00:00:00','Investimento',1000.00,2,24),(29,'2023-06-25 00:00:00','Benefício',300.00,2,25),(30,'2023-06-30 00:00:00','Outros ganhos',100.00,2,26),(31,'2023-07-05 00:00:00','Salário',2000.00,2,22),(32,'2023-07-15 00:00:00','Prêmio',500.00,2,23),(33,'2023-07-20 00:00:00','Investimento',1000.00,2,24),(34,'2023-07-25 00:00:00','Benefício',300.00,2,25),(35,'2023-07-31 00:00:00','Outros ganhos',100.00,2,26),(36,'2023-08-05 00:00:00','Salário',2000.00,2,22),(37,'2023-08-15 00:00:00','Prêmio',500.00,2,23),(38,'2023-08-20 00:00:00','Investimento',1000.00,2,24),(39,'2023-08-25 00:00:00','Benefício',300.00,2,25),(40,'2023-08-31 00:00:00','Outros ganhos',100.00,2,26),(41,'2023-09-05 00:00:00','Salário',2000.00,2,22),(42,'2023-09-15 00:00:00','Prêmio',500.00,2,23),(43,'2023-09-20 00:00:00','Investimento',1000.00,2,24),(44,'2023-09-25 00:00:00','Benefício',300.00,2,25),(45,'2023-09-30 00:00:00','Outros ganhos',100.00,2,26),(46,'2023-10-05 00:00:00','Salário',2000.00,2,22),(47,'2023-10-15 00:00:00','Prêmio',500.00,2,23),(48,'2023-10-20 00:00:00','Investimento',1000.00,2,24),(49,'2023-10-25 00:00:00','Benefício',300.00,2,25),(50,'2023-10-31 00:00:00','Outros ganhos',100.00,2,26),(51,'2023-11-05 00:00:00','Salário',2000.00,2,22),(52,'2023-11-15 00:00:00','Prêmio',500.00,2,23),(53,'2023-11-20 00:00:00','Investimento',1000.00,2,24),(54,'2023-11-25 00:00:00','Benefício',300.00,2,25),(55,'2023-11-30 00:00:00','Outros ganhos',100.00,2,26),(56,'2023-12-05 00:00:00','Salário',2000.00,2,22),(57,'2023-12-15 00:00:00','Prêmio',500.00,2,23),(58,'2023-12-20 00:00:00','Investimento',1000.00,2,24),(59,'2023-12-25 00:00:00','Benefício',300.00,2,25),(60,'2023-12-31 00:00:00','Outros ganhos',100.00,2,26),(61,'2023-05-10 00:00:00','Salário mês de maio ',8500.98,1,9),(62,'2023-05-10 00:00:00','Salário mês de maio ',8500.98,2,9),(63,'2023-05-10 00:00:00','Salário mês de Junho ',8500.98,1,9),(64,'2023-05-10 00:00:00','Salário mês de Julho ',8500.98,1,9);
/*!40000 ALTER TABLE `Receita` ENABLE KEYS */;
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

-- Dump completed on 2023-09-29 23:13:29
