IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [ResidenceBooklet] (
    [ResidenceBookletId] int NOT NULL IDENTITY,
    [ResidenceBookletCode] nvarchar(100) NULL,
    [Address] nvarchar(250) NULL,
    [BookletArea] nvarchar(100) NULL,
    [CreateDate] date NULL,
    CONSTRAINT [PK__Residenc__AE46D5A2295AD5E1] PRIMARY KEY ([ResidenceBookletId])
);
GO

CREATE TABLE [Populations] (
    [PopulationId] int NOT NULL IDENTITY,
    [PopulationName] nvarchar(100) NULL,
    [PopulationNickName] nvarchar(100) NULL,
    [Gender] bit NULL,
    [DateOfBirth] date NULL,
    [BirthPlace] nvarchar(250) NULL,
    [Ethnicity] nvarchar(100) NULL,
    [Religion] nvarchar(100) NULL,
    [WorkPlace] nvarchar(250) NULL,
    [Occupation] nvarchar(100) NULL,
    [CitizenIdentificationCard] nvarchar(12) NULL,
    [DateOfIssue] date NULL,
    [PlaceOfIssue] nvarchar(250) NULL,
    [CreatedDate] date NULL,
    [ResidenceBookletId] int NULL,
    [Relationship] nvarchar(100) NULL,
    CONSTRAINT [PK__Populati__3A2E15E20414EC74] PRIMARY KEY ([PopulationId]),
    CONSTRAINT [FK_ResidenceBooklet] FOREIGN KEY ([ResidenceBookletId]) REFERENCES [ResidenceBooklet] ([ResidenceBookletId])
);
GO

CREATE TABLE [TemporarilyAbsent] (
    [TemporarilyAbsentId] int NOT NULL IDENTITY,
    [PopulationName] nvarchar(100) NULL,
    [Gender] bit NULL,
    [DateOfBirth] date NULL,
    [Nationality] nvarchar(250) NULL,
    [CitizenIdentificationCard] nvarchar(12) NULL,
    [PassportCode] nvarchar(100) NULL,
    [BirthPlace] nvarchar(250) NULL,
    [FromDate] date NULL,
    [Reason] nvarchar(max) NULL,
    [PopulationId] int NULL,
    CONSTRAINT [PK__Temporar__FB890209EFD0C9ED] PRIMARY KEY ([TemporarilyAbsentId]),
    CONSTRAINT [FK_PopulationAbsent] FOREIGN KEY ([PopulationId]) REFERENCES [Populations] ([PopulationId])
);
GO

CREATE TABLE [TemporarilyStaying] (
    [TemporarilyAbsentId] int NOT NULL IDENTITY,
    [PopulationName] nvarchar(100) NULL,
    [Gender] bit NULL,
    [DateOfBirth] date NULL,
    [Nationality] nvarchar(250) NULL,
    [CitizenIdentificationCard] nvarchar(12) NULL,
    [PassportCode] nvarchar(100) NULL,
    [TemporaryAddress] nvarchar(255) NULL,
    [BirthPlace] nvarchar(250) NULL,
    [FromDate] date NULL,
    [Reason] nvarchar(max) NULL,
    [PopulationId] int NULL,
    CONSTRAINT [PK__Temporar__FB89020962FB23B0] PRIMARY KEY ([TemporarilyAbsentId]),
    CONSTRAINT [FK_PopulationStaying] FOREIGN KEY ([PopulationId]) REFERENCES [Populations] ([PopulationId])
);
GO

CREATE INDEX [IX_Populations_ResidenceBookletId] ON [Populations] ([ResidenceBookletId]);
GO

CREATE INDEX [IX_TemporarilyAbsent_PopulationId] ON [TemporarilyAbsent] ([PopulationId]);
GO

CREATE INDEX [IX_TemporarilyStaying_PopulationId] ON [TemporarilyStaying] ([PopulationId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240403095723_V0', N'8.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ResidenceBooklet]') AND [c].[name] = N'CreateDate');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ResidenceBooklet] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ResidenceBooklet] ALTER COLUMN [CreateDate] datetime2 NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Populations]') AND [c].[name] = N'DateOfIssue');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Populations] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Populations] ALTER COLUMN [DateOfIssue] datetime2 NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Populations]') AND [c].[name] = N'DateOfBirth');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Populations] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Populations] ALTER COLUMN [DateOfBirth] datetime2 NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Populations]') AND [c].[name] = N'CreatedDate');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Populations] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Populations] ALTER COLUMN [CreatedDate] datetime2 NULL;
GO

ALTER TABLE [Populations] ADD [Image] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240404102235_V1', N'8.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Populations] ADD [alive] bit NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240405140442_V2', N'8.0.3');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [BookLet] (
    [Id] int NOT NULL IDENTITY,
    [ResidenceBookletId] int NOT NULL,
    [PopulationId] int NOT NULL,
    CONSTRAINT [PK_BookLet] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BookLet_Populations_PopulationId] FOREIGN KEY ([PopulationId]) REFERENCES [Populations] ([PopulationId]) ON DELETE CASCADE,
    CONSTRAINT [FK_BookLet_ResidenceBooklet_ResidenceBookletId] FOREIGN KEY ([ResidenceBookletId]) REFERENCES [ResidenceBooklet] ([ResidenceBookletId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_BookLet_PopulationId] ON [BookLet] ([PopulationId]);
GO

CREATE INDEX [IX_BookLet_ResidenceBookletId] ON [BookLet] ([ResidenceBookletId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240408053755_V3', N'8.0.3');
GO

COMMIT;
GO

