USE `DespesasPessoaisDB`;
CREATE TABLE `Categoria` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Descricao` varchar(100) DEFAULT NULL,
  `UsuarioId` int NOT NULL,
  `TipoCategoria` smallint unsigned NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Categoria_UsuarioId` (`UsuarioId`),
  CONSTRAINT `FK_Categoria_Usuario_UsuarioId` FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;