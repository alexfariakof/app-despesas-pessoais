USE `DespesasPessoaisDB`;
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;