
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/18/2017 17:05:18
-- Generated from EDMX file: D:\epam\Blog1\ORM\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BlogDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [RoleId] int  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [RoleId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Posts'
CREATE TABLE [dbo].[Posts] (
    [PostId] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Tags'
CREATE TABLE [dbo].[Tags] (
    [TagId] int IDENTITY(1,1) NOT NULL,
    [TagName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'post_tag'
CREATE TABLE [dbo].[post_tag] (
    [PostId] int  NOT NULL,
    [TagId] int  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [CommentId] int IDENTITY(1,1) NOT NULL,
    [commentText] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL,
    [PostId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [RoleId] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([RoleId] ASC);
GO

-- Creating primary key on [PostId] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [PK_Posts]
    PRIMARY KEY CLUSTERED ([PostId] ASC);
GO

-- Creating primary key on [TagId] in table 'Tags'
ALTER TABLE [dbo].[Tags]
ADD CONSTRAINT [PK_Tags]
    PRIMARY KEY CLUSTERED ([TagId] ASC);
GO

-- Creating primary key on [PostId], [TagId] in table 'post_tag'
ALTER TABLE [dbo].[post_tag]
ADD CONSTRAINT [PK_post_tag]
    PRIMARY KEY CLUSTERED ([PostId], [TagId] ASC);
GO

-- Creating primary key on [CommentId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([CommentId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PostId] in table 'post_tag'
ALTER TABLE [dbo].[post_tag]
ADD CONSTRAINT [FK_Postpost_tag]
    FOREIGN KEY ([PostId])
    REFERENCES [dbo].[Posts]
        ([PostId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [TagId] in table 'post_tag'
ALTER TABLE [dbo].[post_tag]
ADD CONSTRAINT [FK_Tagpost_tag]
    FOREIGN KEY ([TagId])
    REFERENCES [dbo].[Tags]
        ([TagId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Tagpost_tag'
CREATE INDEX [IX_FK_Tagpost_tag]
ON [dbo].[post_tag]
    ([TagId]);
GO

-- Creating foreign key on [UserId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_UserComment]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserComment'
CREATE INDEX [IX_FK_UserComment]
ON [dbo].[Comments]
    ([UserId]);
GO

-- Creating foreign key on [PostId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_PostComment]
    FOREIGN KEY ([PostId])
    REFERENCES [dbo].[Posts]
        ([PostId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostComment'
CREATE INDEX [IX_FK_PostComment]
ON [dbo].[Comments]
    ([PostId]);
GO

-- Creating foreign key on [RoleId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_RoleUser]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]
        ([RoleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleUser'
CREATE INDEX [IX_FK_RoleUser]
ON [dbo].[Users]
    ([RoleId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------