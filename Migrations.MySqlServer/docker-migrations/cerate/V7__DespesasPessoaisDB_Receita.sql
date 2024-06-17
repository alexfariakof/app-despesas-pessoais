USE `DespesasPessoaisDB`;
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;