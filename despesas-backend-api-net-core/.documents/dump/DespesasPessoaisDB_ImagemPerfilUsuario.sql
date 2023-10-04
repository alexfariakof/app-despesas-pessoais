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
-- Table structure for table `ImagemPerfilUsuario`
--

DROP TABLE IF EXISTS `ImagemPerfilUsuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ImagemPerfilUsuario` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Url` varchar(255) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Type` varchar(4) NOT NULL,
  `UsuarioId` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_ImagemPerfilUsuario_Name` (`Name`),
  UNIQUE KEY `IX_ImagemPerfilUsuario_Url` (`Url`),
  UNIQUE KEY `IX_ImagemPerfilUsuario_UsuarioId` (`UsuarioId`),
  CONSTRAINT `FK_ImagemPerfilUsuario_Usuario_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ImagemPerfilUsuario`
--

LOCK TABLES `ImagemPerfilUsuario` WRITE;
/*!40000 ALTER TABLE `ImagemPerfilUsuario` DISABLE KEYS */;
INSERT INTO `ImagemPerfilUsuario` (`Id`, `Url`, `Name`, `Type`, `UsuarioId`) VALUES (2,'https://bucket-usuario-perfil.s3.amazonaws.com/perfil-usuarioId-1-20230703','perfil-usuarioId-1-20230702','jpg',1),(3,'https://bucket-usuario-perfil.s3.amazonaws.com/perfil-usuarioId-2-20230907','perfil-usuarioId-2-20230719','png',2);
/*!40000 ALTER TABLE `ImagemPerfilUsuario` ENABLE KEYS */;
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

-- Dump completed on 2023-09-29 23:14:00
