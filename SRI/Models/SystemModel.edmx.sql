
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/02/2020 23:19:17
-- Generated from EDMX file: C:\Users\nicol\source\repos\nicolaslopez1000\ProyectoIntegrador\Integrador\Web\Models\SystemModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [db_SRI];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_FuncionarioIncidente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Incidente] DROP CONSTRAINT [FK_FuncionarioIncidente];
GO
IF OBJECT_ID(N'[dbo].[FK_IncidenteMailDestinatarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Destinatarios] DROP CONSTRAINT [FK_IncidenteMailDestinatarios];
GO
IF OBJECT_ID(N'[dbo].[FK_IncidentePalabraClave]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PalabraClave] DROP CONSTRAINT [FK_IncidentePalabraClave];
GO
IF OBJECT_ID(N'[dbo].[FK_HorarioFuncionario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Funcionario] DROP CONSTRAINT [FK_HorarioFuncionario];
GO
IF OBJECT_ID(N'[dbo].[FK_IncidenteMail_inherits_Incidente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncidenteMail] DROP CONSTRAINT [FK_IncidenteMail_inherits_Incidente];
GO
IF OBJECT_ID(N'[dbo].[FK_IncidenteLlamado_inherits_Incidente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncidenteLlamado] DROP CONSTRAINT [FK_IncidenteLlamado_inherits_Incidente];
GO
IF OBJECT_ID(N'[dbo].[FK_IncidenteChatWpp_inherits_Incidente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncidenteChatWpp] DROP CONSTRAINT [FK_IncidenteChatWpp_inherits_Incidente];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Funcionario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Funcionario];
GO
IF OBJECT_ID(N'[dbo].[Horario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Horario];
GO
IF OBJECT_ID(N'[dbo].[Incidente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Incidente];
GO
IF OBJECT_ID(N'[dbo].[Destinatarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Destinatarios];
GO
IF OBJECT_ID(N'[dbo].[PalabraClave]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PalabraClave];
GO
IF OBJECT_ID(N'[dbo].[Incidente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Incidente];
GO
IF OBJECT_ID(N'[dbo].[IncidenteLlamado]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncidenteLlamado];
GO
IF OBJECT_ID(N'[dbo].[IncidenteChatWpp]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncidenteChatWpp];
GO
IF OBJECT_ID(N'[dbo].[IncidenteMail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncidenteMail];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Funcionario'
CREATE TABLE [dbo].[Funcionario] (
    [nombre] nvarchar(max)  NOT NULL,
    [ci] nvarchar(50) NOT NULL,
    [mail] nvarchar(50) UNIQUE NOT NULL,
    [celular] nvarchar(max)  NOT NULL,
    [password] nvarchar(max)  NOT NULL,
    [rol] int  NOT NULL,
    [apellido] nvarchar(max)  NOT NULL,
    [Horario_Id] int  NOT NULL,
    [is_eliminado] bit default 'FALSE'
);
GO

-- Creating table 'Horario'
CREATE TABLE [dbo].[Horario] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [hora_inicio] datetime  NOT NULL,
    [hora_fin] datetime  NOT NULL
);
GO

-- Creating table 'Incidente'
CREATE TABLE [dbo].[Incidente] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [fecha_suceso] datetime  NOT NULL,
    [fecha_creacion] datetime NOT NULL,
    [emocion] int  NOT NULL,
    [Funcionario_ci] nvarchar(50)  NOT NULL,
    [resolucion] nvarchar(50)  ,
    [descripcion] nvarchar(200)  ,
    [tipo] int NOT NULL  

);
GO

-- Creating table 'Destinatarios'
CREATE TABLE [dbo].[Destinatarios] (
    [valor] nvarchar(max)  NOT NULL,
    [tipo] int  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL,
    [IncidenteMail_Id] int  NOT NULL
);
GO

-- Creating table 'PalabraClave'
CREATE TABLE [dbo].[PalabraClave] (
    [valor] nvarchar(max)  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL,
    [Incidente_Id] int  NOT NULL
);
GO

-- Creating table 'IncidenteMail'
CREATE TABLE [dbo].[IncidenteMail] (
    [asunto] nvarchar(max)  NOT NULL,
    [respuesta] nvarchar(max)  NOT NULL,
    [contenido] nvarchar(max)  NOT NULL,
    [remitente] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'IncidenteLlamado'
CREATE TABLE [dbo].[IncidenteLlamado] (
    [telefono_saliente] nvarchar(max)  NOT NULL,
    [telefono_entrante] nvarchar(max)  NOT NULL,
    [hora_inicio] datetime  NOT NULL,
    [hora_fin] datetime  NOT NULL,
    [nombre_persona_llama] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'IncidenteChatWpp'
CREATE TABLE [dbo].[IncidenteChatWpp] (
    [respuesta] nvarchar(max)  NOT NULL,
    [telefono_entrante] nvarchar(max)  NOT NULL,
    [telefono_saliente] nvarchar(max)  NOT NULL,
    [nombre_persona_llama] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ci] in table 'Funcionario'
ALTER TABLE [dbo].[Funcionario]
ADD CONSTRAINT [PK_Funcionario]
    PRIMARY KEY CLUSTERED ([ci] ASC);
GO

-- Creating primary key on [Id] in table 'Horario'
ALTER TABLE [dbo].[Horario]
ADD CONSTRAINT [PK_Horario]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Incidente'
ALTER TABLE [dbo].[Incidente]
ADD CONSTRAINT [PK_Incidente]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Destinatarios'
ALTER TABLE [dbo].[Destinatarios]
ADD CONSTRAINT [PK_Destinatarios]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PalabraClave'
ALTER TABLE [dbo].[PalabraClave]
ADD CONSTRAINT [PK_PalabraClave]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncidenteMail'
ALTER TABLE [dbo].[IncidenteMail]
ADD CONSTRAINT [PK_IncidenteMail]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncidenteLlamado'
ALTER TABLE [dbo].[IncidenteLlamado]
ADD CONSTRAINT [PK_IncidenteLlamado]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncidenteChatWpp'
ALTER TABLE [dbo].[IncidenteChatWpp]
ADD CONSTRAINT [PK_IncidenteChatWpp]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Funcionario_ci] in table 'Incidente'
ALTER TABLE [dbo].[Incidente]
ADD CONSTRAINT [FK_FuncionarioIncidente]
    FOREIGN KEY ([Funcionario_ci])
    REFERENCES [dbo].[Funcionario]
        ([ci])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FuncionarioIncidente'
CREATE INDEX [IX_FK_FuncionarioIncidente]
ON [dbo].[Incidente]
    ([Funcionario_ci]);
GO

-- Creating foreign key on [IncidenteMail_Id] in table 'Destinatarios'
ALTER TABLE [dbo].[Destinatarios]
ADD CONSTRAINT [FK_IncidenteMailDestinatarios]
    FOREIGN KEY ([IncidenteMail_Id])
    REFERENCES [dbo].[IncidenteMail]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IncidenteMailDestinatarios'
CREATE INDEX [IX_FK_IncidenteMailDestinatarios]
ON [dbo].[Destinatarios]
    ([IncidenteMail_Id]);
GO

-- Creating foreign key on [Incidente_Id] in table 'PalabraClave'
ALTER TABLE [dbo].[PalabraClave]
ADD CONSTRAINT [FK_IncidentePalabraClave]
    FOREIGN KEY ([Incidente_Id])
    REFERENCES [dbo].[Incidente]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IncidentePalabraClave'
CREATE INDEX [IX_FK_IncidentePalabraClave]
ON [dbo].[PalabraClave]
    ([Incidente_Id]);
GO

-- Creating foreign key on [Horario_Id] in table 'Funcionario'
ALTER TABLE [dbo].[Funcionario]
ADD CONSTRAINT [FK_HorarioFuncionario]
    FOREIGN KEY ([Horario_Id])
    REFERENCES [dbo].[Horario]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HorarioFuncionario'
CREATE INDEX [IX_FK_HorarioFuncionario]
ON [dbo].[Funcionario]
    ([Horario_Id]);
GO

-- Creating foreign key on [Id] in table 'Incidente_IncidenteMail'
ALTER TABLE [dbo].[IncidenteMail]
ADD CONSTRAINT [FK_IncidenteMail_inherits_Incidente]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Incidente]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Incidente_IncidenteLlamado'
ALTER TABLE [dbo].[IncidenteLlamado]
ADD CONSTRAINT [FK_IncidenteLlamado_inherits_Incidente]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Incidente]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'IncidenteChatWpp'
ALTER TABLE [dbo].[IncidenteChatWpp]
ADD CONSTRAINT [FK_IncidenteChatWpp_inherits_Incidente]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Incidente]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------




