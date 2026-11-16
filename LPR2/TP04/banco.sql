-- Matéria: Linguagem de Programação 2 (CBTLPR2)
-- Dupla: Brandon Oliveira e Eriel de Souza
-- Trabalho Prático 04 - Script do banco SQL Server "aulajava"

IF DB_ID('aulajava') IS NULL
    CREATE DATABASE aulajava;
GO
USE aulajava;
GO

IF OBJECT_ID('tbfuncs') IS NOT NULL DROP TABLE tbfuncs;
IF OBJECT_ID('tbcargos') IS NOT NULL DROP TABLE tbcargos;
GO

CREATE TABLE tbcargos (
    cod_cargo SMALLINT NOT NULL PRIMARY KEY,
    ds_cargo  CHAR(20) NULL
);

CREATE TABLE tbfuncs (
    cod_func  DECIMAL(9,0) NOT NULL PRIMARY KEY,
    nome_func CHAR(30) NULL,
    sal_func  MONEY NULL,
    cod_cargo SMALLINT NULL,
    CONSTRAINT fk_func_cargo FOREIGN KEY (cod_cargo) REFERENCES tbcargos (cod_cargo)
);
GO

INSERT INTO tbcargos (cod_cargo, ds_cargo) VALUES
 (1, 'Administrativo'),
 (2, 'Analista'),
 (3, 'Gerente'),
 (4, 'Programador');

INSERT INTO tbfuncs (cod_func, nome_func, sal_func, cod_cargo) VALUES
 (1, 'Marcelo Silva',    2000.00, 1),
 (2, 'Marcela Souza',    3500.00, 2),
 (3, 'Marcos Pereira',   8000.00, 3),
 (4, 'Maria Oliveira',   4500.00, 4),
 (5, 'Carlos Marcelino', 2500.00, 1),
 (6, 'Ana Santos',       5200.00, 2);
GO
