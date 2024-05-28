-- ----------------------------------------------------------------------------
-- MySQL Workbench Migration
-- Migrated Schemata: DespesasPessoaisDB
-- Source Schemata: DespesasPessoaisDB
-- Created: Fri Sep 29 17:57:49 2023
-- Workbench Version: 8.0.34
-- ----------------------------------------------------------------------------

SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------------------------------------------------------
-- Schema DespesasPessoaisDB
-- ----------------------------------------------------------------------------
DROP SCHEMA IF EXISTS `DespesasPessoaisDB` ;
CREATE SCHEMA IF NOT EXISTS `DespesasPessoaisDB` ;

-- ----------------------------------------------------------------------------
-- Table DespesasPessoaisDB.Categoria
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `DespesasPessoaisDB`.`Categoria` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Descricao` VARCHAR(100) NULL DEFAULT NULL,
  `UsuarioId` INT NOT NULL,
  `TipoCategoria` SMALLINT UNSIGNED NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_Categoria_UsuarioId` (`UsuarioId` ASC) VISIBLE,
  CONSTRAINT `FK_Categoria_Usuario_UsuarioId`
    FOREIGN KEY (`UsuarioId`)
    REFERENCES `DespesasPessoaisDB`.`Usuario` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 68
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------------------------------------------------------
-- Table DespesasPessoaisDB.ControleAcesso
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `DespesasPessoaisDB`.`ControleAcesso` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Login` VARCHAR(100) NOT NULL,
  `Senha` LONGTEXT NOT NULL,
  `UsuarioId` INT NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `IX_ControleAcesso_Login` (`Login` ASC) VISIBLE,
  INDEX `IX_ControleAcesso_UsuarioId` (`UsuarioId` ASC) VISIBLE,
  CONSTRAINT `FK_ControleAcesso_Usuario_UsuarioId`
    FOREIGN KEY (`UsuarioId`)
    REFERENCES `DespesasPessoaisDB`.`Usuario` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 6
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------------------------------------------------------
-- Table DespesasPessoaisDB.Despesa
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `DespesasPessoaisDB`.`Despesa` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Data` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Descricao` VARCHAR(100) NULL DEFAULT NULL,
  `Valor` DECIMAL(10,2) NOT NULL DEFAULT '0.00',
  `DataVencimento` TIMESTAMP NULL DEFAULT NULL,
  `UsuarioId` INT NOT NULL,
  `CategoriaId` INT NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_Despesa_CategoriaId` (`CategoriaId` ASC) VISIBLE,
  INDEX `IX_Despesa_UsuarioId` (`UsuarioId` ASC) VISIBLE,
  CONSTRAINT `FK_Despesa_Categoria_CategoriaId`
    FOREIGN KEY (`CategoriaId`)
    REFERENCES `DespesasPessoaisDB`.`Categoria` (`Id`)
    ON DELETE CASCADE,
  CONSTRAINT `FK_Despesa_Usuario_UsuarioId`
    FOREIGN KEY (`UsuarioId`)
    REFERENCES `DespesasPessoaisDB`.`Usuario` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 123
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------------------------------------------------------
-- Table DespesasPessoaisDB.ImagemPerfilUsuario
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `DespesasPessoaisDB`.`ImagemPerfilUsuario` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Url` VARCHAR(255) NOT NULL,
  `Name` VARCHAR(50) NOT NULL,
  `Type` VARCHAR(4) NOT NULL,
  `UsuarioId` INT NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `IX_ImagemPerfilUsuario_Name` (`Name` ASC) VISIBLE,
  UNIQUE INDEX `IX_ImagemPerfilUsuario_Url` (`Url` ASC) VISIBLE,
  UNIQUE INDEX `IX_ImagemPerfilUsuario_UsuarioId` (`UsuarioId` ASC) VISIBLE,
  CONSTRAINT `FK_ImagemPerfilUsuario_Usuario_UsuarioId`
    FOREIGN KEY (`UsuarioId`)
    REFERENCES `DespesasPessoaisDB`.`Usuario` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------------------------------------------------------
-- Table DespesasPessoaisDB.Lancamento
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `DespesasPessoaisDB`.`Lancamento` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Valor` DECIMAL(10,2) NOT NULL,
  `Data` TIMESTAMP NOT NULL,
  `Descricao` VARCHAR(100) NOT NULL,
  `UsuarioId` INT NOT NULL,
  `DespesaId` INT NULL DEFAULT NULL,
  `ReceitaId` INT NULL DEFAULT NULL,
  `CategoriaId` INT NOT NULL,
  `DataCriacao` TIMESTAMP NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_Lancamento_CategoriaId` (`CategoriaId` ASC) VISIBLE,
  INDEX `IX_Lancamento_DespesaId` (`DespesaId` ASC) VISIBLE,
  INDEX `IX_Lancamento_ReceitaId` (`ReceitaId` ASC) VISIBLE,
  INDEX `IX_Lancamento_UsuarioId` (`UsuarioId` ASC) VISIBLE,
  CONSTRAINT `FK_Lancamento_Categoria_CategoriaId`
    FOREIGN KEY (`CategoriaId`)
    REFERENCES `DespesasPessoaisDB`.`Categoria` (`Id`)
    ON DELETE CASCADE,
  CONSTRAINT `FK_Lancamento_Despesa_DespesaId`
    FOREIGN KEY (`DespesaId`)
    REFERENCES `DespesasPessoaisDB`.`Despesa` (`Id`),
  CONSTRAINT `FK_Lancamento_Receita_ReceitaId`
    FOREIGN KEY (`ReceitaId`)
    REFERENCES `DespesasPessoaisDB`.`Receita` (`Id`),
  CONSTRAINT `FK_Lancamento_Usuario_UsuarioId`
    FOREIGN KEY (`UsuarioId`)
    REFERENCES `DespesasPessoaisDB`.`Usuario` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------------------------------------------------------
-- Table DespesasPessoaisDB.Receita
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `DespesasPessoaisDB`.`Receita` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Data` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Descricao` VARCHAR(100) NULL DEFAULT NULL,
  `Valor` DECIMAL(10,2) NOT NULL DEFAULT '0.00',
  `UsuarioId` INT NOT NULL,
  `CategoriaId` INT NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_Receita_CategoriaId` (`CategoriaId` ASC) VISIBLE,
  INDEX `IX_Receita_UsuarioId` (`UsuarioId` ASC) VISIBLE,
  CONSTRAINT `FK_Receita_Categoria_CategoriaId`
    FOREIGN KEY (`CategoriaId`)
    REFERENCES `DespesasPessoaisDB`.`Categoria` (`Id`)
    ON DELETE CASCADE,
  CONSTRAINT `FK_Receita_Usuario_UsuarioId`
    FOREIGN KEY (`UsuarioId`)
    REFERENCES `DespesasPessoaisDB`.`Usuario` (`Id`)
    ON DELETE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 65
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------------------------------------------------------
-- Table DespesasPessoaisDB.Usuario
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `DespesasPessoaisDB`.`Usuario` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Nome` VARCHAR(50) NOT NULL,
  `SobreNome` VARCHAR(50) NOT NULL,
  `Telefone` VARCHAR(15) NULL DEFAULT NULL,
  `Email` VARCHAR(50) NOT NULL,
  `StatusUsuario` SMALLINT UNSIGNED NOT NULL,
  `PerfilUsuario` SMALLINT UNSIGNED NOT NULL DEFAULT '2',
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `IX_Usuario_Email` (`Email` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 6
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------------------------------------------------------
-- Table DespesasPessoaisDB.__EFMigrationsHistory
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `DespesasPessoaisDB`.`__EFMigrationsHistory` (
  `MigrationId` VARCHAR(150) NOT NULL,
  `ProductVersion` VARCHAR(32) NOT NULL,
  PRIMARY KEY (`MigrationId`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------------------------------------------------------
-- Routine DespesasPessoaisDB.GetSaldoByIdUsuario
-- ----------------------------------------------------------------------------
DELIMITER $$

DELIMITER $$
USE `DespesasPessoaisDB`$$
CREATE DEFINER=`root`@`%` PROCEDURE `GetSaldoByIdUsuario`(IN IdUsuario INT)
BEGIN
SET @receita := (SELECT CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END as Saldo FROM Receita Where UsuarioId = IdUsuario);
SET @despesa := (SELECT CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END as Saldo FROM Despesa Where UsuarioId = IdUsuario);
Select FORMAT(@receita - @despesa, 2) as Saldo;



END$$

DELIMITER ;

-- ----------------------------------------------------------------------------
-- Routine DespesasPessoaisDB.LancamentosPorMesAno
-- ----------------------------------------------------------------------------
DELIMITER $$

DELIMITER $$
USE `DespesasPessoaisDB`$$
CREATE DEFINER=`root`@`%` PROCEDURE `LancamentosPorMesAno`(IN IdUsuario INT,IN mes INT, IN ano INT)
BEGIN

Select cast(CONV(SUBSTRING(uuid(), 5, 5), 16, 10) as UNSIGNED) as id, lancamentos.* From (  
Select d.UsuarioId, data, d.CategoriaId, valor as valor, d.id as DespesaId, 0 as ReceitaId, d.descricao, now() as DataCriacao  
  FROM Despesa d  
 Inner Join Categoria c on d.CategoriaId = c.id  
 where d.UsuarioId = IdUsuario  
   and Month(data) = mes  
   and  Year(data) = ano  
 union  
Select r.UsuarioId, data, r.CategoriaId, valor,  0 as DespesaId, r.id as ReceitaId, r.descricao,  now() as DataCriacao  
  FROM Receita r  
 Inner Join Categoria cr on r.CategoriaId = cr.id  
 where r.UsuarioId = IdUsuario  
   and Month(data) = mes  
   and  Year(data) = ano  
) lancamentos Order by Data;

END$$

DELIMITER ;

-- ----------------------------------------------------------------------------
-- Routine DespesasPessoaisDB.SomatorioDespesasPorAno
-- ----------------------------------------------------------------------------
DELIMITER $$

DELIMITER $$
USE `DespesasPessoaisDB`$$
CREATE DEFINER=`root`@`%` PROCEDURE `SomatorioDespesasPorAno`(IN IdUsuario INT,IN ano INT)
BEGIN
Select  
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 1 and Year(data) = ano) as Janeiro,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 2 and Year(data) = ano) as Fevereiro,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 3 and Year(data) = ano) as Marco,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 4 and Year(data) = ano) as Abril,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 5 and Year(data) = ano) as Maio,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 6 and Year(data) = ano) as Junho,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 7 and Year(data) = ano) as Julho,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 8 and Year(data) = ano) as Agosto,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 9 and Year(data) = ano) as Setembro,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 10 and Year(data) = ano) as Outubro,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 11 and Year(data) = ano) as Novembro,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Despesa where UsuarioId = IdUsuario  and Month(data) = 12 and Year(data) = ano) as Dezembro;

END$$

DELIMITER ;

-- ----------------------------------------------------------------------------
-- Routine DespesasPessoaisDB.SomatorioReceitasPorAno
-- ----------------------------------------------------------------------------
DELIMITER $$

DELIMITER $$
USE `DespesasPessoaisDB`$$
CREATE DEFINER=`root`@`%` PROCEDURE `SomatorioReceitasPorAno`(IN IdUsuario INT,IN ano INT)
BEGIN

Select 
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 1 and Year(data) = ano) as Janeiro,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 2 and Year(data) = ano) as Fevereiro,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 3 and Year(data) = ano) as Marco,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 4 and Year(data) = ano) as Abril,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 5 and Year(data) = ano) as Maio,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 6 and Year(data) = ano) as Junho,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 7 and Year(data) = ano) as Julho,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 8 and Year(data) = ano) as Agosto,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 9 and Year(data) = ano) as Setembro,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 10 and Year(data) = ano) as Outubro,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 11 and Year(data) = ano) as Novembro,
(Select CASE WHEN sum(valor) >= 0 THEN sum(valor) ELSE 0 END from Receita where UsuarioId = IdUsuario  and Month(data) = 12 and Year(data) = ano) as Dezembro;

END$$

DELIMITER ;
SET FOREIGN_KEY_CHECKS = 1;
