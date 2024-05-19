USE `DespesasPessoaisDB`;
CREATE TABLE `Usuario` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(50) NOT NULL,
  `SobreNome` varchar(50) NOT NULL,
  `Telefone` varchar(15) DEFAULT NULL,
  `Email` varchar(50) NOT NULL,
  `StatusUsuario` smallint unsigned NOT NULL,
  `PerfilUsuario` smallint unsigned NOT NULL DEFAULT '2',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Usuario_Email` (`Email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;