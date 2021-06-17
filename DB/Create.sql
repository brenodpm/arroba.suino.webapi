-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema arroba.suino.db
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema arroba.suino.db
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `arroba.suino.db` ;
USE `arroba.suino.db` ;

-- -----------------------------------------------------
-- Table `arroba.suino.db`.`Empresa`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `arroba.suino.db`.`Empresa` (
  `CodEmpresa` VARCHAR(36) NOT NULL,
  `Ativo` TINYINT NOT NULL,
  `NomeEmpresa` VARCHAR(45) NOT NULL,
  `Security` VARCHAR(36) NOT NULL,
  `Email` VARCHAR(100) NOT NULL,
  `EmailValidado` TINYINT NOT NULL,
  `DataCadastro` DATETIME NOT NULL DEFAULT NOW(),
  PRIMARY KEY (`CodEmpresa`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `arroba.suino.db`.`Grupo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `arroba.suino.db`.`Grupo` (
  `CodGrupo` VARCHAR(36) NOT NULL,
  `CodEmpresa` VARCHAR(36) NOT NULL,
  `NomeGrupo` VARCHAR(45) NOT NULL,
  `Ativo` TINYINT NOT NULL,
  `Permissoes` JSON NOT NULL,
  PRIMARY KEY (`CodGrupo`),
  INDEX `fk_Grupo_Empresa1_idx` (`CodEmpresa` ASC) VISIBLE,
  CONSTRAINT `fk_Grupo_Empresa1`
    FOREIGN KEY (`CodEmpresa`)
    REFERENCES `arroba.suino.db`.`Empresa` (`CodEmpresa`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `arroba.suino.db`.`Usuario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `arroba.suino.db`.`Usuario` (
  `CodUsuario` VARCHAR(36) NOT NULL,
  `CodGrupo` VARCHAR(36) NOT NULL,
  `Nome` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`CodUsuario`),
  INDEX `fk_Usuario_Grupo1_idx` (`CodGrupo` ASC) VISIBLE,
  CONSTRAINT `fk_Usuario_Grupo1`
    FOREIGN KEY (`CodGrupo`)
    REFERENCES `arroba.suino.db`.`Grupo` (`CodGrupo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `arroba.suino.db`.`Desenvolvedor`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `arroba.suino.db`.`Desenvolvedor` (
  `CodDesenvolvedor` VARCHAR(36) NOT NULL,
  `Nome` VARCHAR(100) NOT NULL,
  `Usuario` VARCHAR(45) NOT NULL,
  `Senha` VARCHAR(2048) NOT NULL,
  `Ativo` TINYINT NOT NULL,
  `DataCriacao` DATETIME NOT NULL,
  PRIMARY KEY (`CodDesenvolvedor`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `arroba.suino.db`.`Cliente`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `arroba.suino.db`.`Cliente` (
  `ApiKey` VARCHAR(36) NOT NULL,
  `Nome` VARCHAR(45) NOT NULL,
  `ApiSecret` VARCHAR(36) NOT NULL,
  `Ativo` TINYINT NOT NULL,
  `CodDesenvolvedor` VARCHAR(36) NOT NULL,
  PRIMARY KEY (`ApiKey`, `CodDesenvolvedor`),
  INDEX `fk_Cliente_Desenvolvedor1_idx` (`CodDesenvolvedor` ASC) VISIBLE,
  CONSTRAINT `fk_Cliente_Desenvolvedor1`
    FOREIGN KEY (`CodDesenvolvedor`)
    REFERENCES `arroba.suino.db`.`Desenvolvedor` (`CodDesenvolvedor`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `arroba.suino.db`.`Sessao`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `arroba.suino.db`.`Sessao` (
  `CodSessao` VARCHAR(36) NOT NULL,
  `CodEmpresa` VARCHAR(36) NOT NULL,
  `CodUsuario` VARCHAR(36) NOT NULL,
  `CodGrupo` VARCHAR(36) NOT NULL,
  `ApiKey` VARCHAR(36) NOT NULL,
  `Segredo` VARCHAR(36) NOT NULL,
  `Ativo` TINYINT NOT NULL,
  `PrimeiroAcesso` DATETIME NOT NULL DEFAULT NOW(),
  `UltimoAcesso` DATETIME NOT NULL,
  `Descricao` VARCHAR(45) NOT NULL,
  `Localizacao` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`CodSessao`),
  INDEX `fk_Sessao_Empresa1_idx` (`CodEmpresa` ASC) VISIBLE,
  INDEX `fk_Sessao_Usuario1_idx` (`CodUsuario` ASC) VISIBLE,
  INDEX `fk_Sessao_Cliente1_idx` (`ApiKey` ASC) VISIBLE,
  INDEX `fk_Sessao_Grupo1_idx` (`CodGrupo` ASC) VISIBLE,
  CONSTRAINT `fk_Sessao_Empresa1`
    FOREIGN KEY (`CodEmpresa`)
    REFERENCES `arroba.suino.db`.`Empresa` (`CodEmpresa`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Sessao_Usuario1`
    FOREIGN KEY (`CodUsuario`)
    REFERENCES `arroba.suino.db`.`Usuario` (`CodUsuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Sessao_Cliente1`
    FOREIGN KEY (`ApiKey`)
    REFERENCES `arroba.suino.db`.`Cliente` (`ApiKey`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Sessao_Grupo1`
    FOREIGN KEY (`CodGrupo`)
    REFERENCES `arroba.suino.db`.`Grupo` (`CodGrupo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `arroba.suino.db`.`Licenca`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `arroba.suino.db`.`Licenca` (
  `CodLicenca` VARCHAR(36) NOT NULL,
  `DataCriacao` DATE NOT NULL,
  `DataValidade` DATE NOT NULL,
  `Ativo` TINYINT NOT NULL,
  `Descricao` VARCHAR(45) NOT NULL,
  `CodEmpresa` VARCHAR(36) NOT NULL,
  PRIMARY KEY (`CodLicenca`),
  INDEX `fk_Licenca_Empresa1_idx` (`CodEmpresa` ASC) VISIBLE,
  CONSTRAINT `fk_Licenca_Empresa1`
    FOREIGN KEY (`CodEmpresa`)
    REFERENCES `arroba.suino.db`.`Empresa` (`CodEmpresa`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
