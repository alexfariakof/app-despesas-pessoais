USE `DespesasPessoaisDB`;
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