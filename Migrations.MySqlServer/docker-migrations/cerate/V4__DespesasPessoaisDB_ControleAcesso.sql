USE `DespesasPessoaisDB`;
CREATE TABLE `ControleAcesso` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Login` varchar(100) NOT NULL,
  `Senha` longtext NOT NULL,
  `UsuarioId` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_ControleAcesso_Login` (`Login`),
  KEY `IX_ControleAcesso_UsuarioId` (`UsuarioId`),
  CONSTRAINT `FK_ControleAcesso_Usuario_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;