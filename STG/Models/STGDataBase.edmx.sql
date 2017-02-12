
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/12/2017 13:20:29
-- Generated from EDMX file: C:\Users\Gabriel Rogala\Dropbox\INŻ\Projekt\STGweb\STG\Models\STGDataBase.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [aspnet-STG-20170209064115];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomTypesRooms]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Rooms] DROP CONSTRAINT [FK_RoomTypesRooms];
GO
IF OBJECT_ID(N'[dbo].[FK_SchoolsTeachers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Teachers] DROP CONSTRAINT [FK_SchoolsTeachers];
GO
IF OBJECT_ID(N'[dbo].[FK_SchoolsGroups]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Groups] DROP CONSTRAINT [FK_SchoolsGroups];
GO
IF OBJECT_ID(N'[dbo].[FK_SchoolsRooms]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Rooms] DROP CONSTRAINT [FK_SchoolsRooms];
GO
IF OBJECT_ID(N'[dbo].[FK_SchoolsRoomTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoomTypes] DROP CONSTRAINT [FK_SchoolsRoomTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_SchoolsSubjectTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SubjectTypes] DROP CONSTRAINT [FK_SchoolsSubjectTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_SchoolsSubjects]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Subjects] DROP CONSTRAINT [FK_SchoolsSubjects];
GO
IF OBJECT_ID(N'[dbo].[FK_SubjectTypesSubjects]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Subjects] DROP CONSTRAINT [FK_SubjectTypesSubjects];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUsersSchools]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Schools] DROP CONSTRAINT [FK_AspNetUsersSchools];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupsSubGroupTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SubGroupTypes] DROP CONSTRAINT [FK_GroupsSubGroupTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupsGroups]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Groups] DROP CONSTRAINT [FK_GroupsGroups];
GO
IF OBJECT_ID(N'[dbo].[FK_SubGroupTypesGroups]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Groups] DROP CONSTRAINT [FK_SubGroupTypesGroups];
GO
IF OBJECT_ID(N'[dbo].[FK_SubjectsLessons]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lessons] DROP CONSTRAINT [FK_SubjectsLessons];
GO
IF OBJECT_ID(N'[dbo].[FK_TeachersLessons]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lessons] DROP CONSTRAINT [FK_TeachersLessons];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupsLessons]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lessons] DROP CONSTRAINT [FK_GroupsLessons];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomsLessons]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lessons] DROP CONSTRAINT [FK_RoomsLessons];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomTypesLessons]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lessons] DROP CONSTRAINT [FK_RoomTypesLessons];
GO
IF OBJECT_ID(N'[dbo].[FK_SchoolsTimetables]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Timetables] DROP CONSTRAINT [FK_SchoolsTimetables];
GO
IF OBJECT_ID(N'[dbo].[FK_SchoolsLessons]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lessons] DROP CONSTRAINT [FK_SchoolsLessons];
GO
IF OBJECT_ID(N'[dbo].[FK_LessonsTimetables]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Timetables] DROP CONSTRAINT [FK_LessonsTimetables];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomsTimetables]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Timetables] DROP CONSTRAINT [FK_RoomsTimetables];
GO
IF OBJECT_ID(N'[dbo].[FK_STGConfigSchools]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Schools] DROP CONSTRAINT [FK_STGConfigSchools];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[C__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[C__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[Teachers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Teachers];
GO
IF OBJECT_ID(N'[dbo].[Groups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Groups];
GO
IF OBJECT_ID(N'[dbo].[Rooms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rooms];
GO
IF OBJECT_ID(N'[dbo].[Schools]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Schools];
GO
IF OBJECT_ID(N'[dbo].[RoomTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoomTypes];
GO
IF OBJECT_ID(N'[dbo].[SubjectTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SubjectTypes];
GO
IF OBJECT_ID(N'[dbo].[Subjects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Subjects];
GO
IF OBJECT_ID(N'[dbo].[SubGroupTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SubGroupTypes];
GO
IF OBJECT_ID(N'[dbo].[Lessons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Lessons];
GO
IF OBJECT_ID(N'[dbo].[Timetables]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Timetables];
GO
IF OBJECT_ID(N'[dbo].[STGConfig]', 'U') IS NOT NULL
    DROP TABLE [dbo].[STGConfig];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'Teachers'
CREATE TABLE [dbo].[Teachers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SchoolsId] int  NOT NULL,
    [BlockedHours] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Groups'
CREATE TABLE [dbo].[Groups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Amount] int  NOT NULL,
    [SchoolsId] int  NOT NULL,
    [ParentGroup] int  NULL,
    [SubGroupTypesId] int  NULL,
    [BlockedHours] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Rooms'
CREATE TABLE [dbo].[Rooms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [RoomTypesId] int  NOT NULL,
    [SchoolsId] int  NOT NULL,
    [Amount] int  NOT NULL,
    [BlockedHours] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Schools'
CREATE TABLE [dbo].[Schools] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AspNetUsersId] nvarchar(128)  NOT NULL,
    [NumberOfDays] int  NOT NULL,
    [NumberOfHours] int  NOT NULL,
    [STGConfigId] int  NOT NULL
);
GO

-- Creating table 'RoomTypes'
CREATE TABLE [dbo].[RoomTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SchoolsId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SubjectTypes'
CREATE TABLE [dbo].[SubjectTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Priority] int  NOT NULL,
    [SchoolsId] int  NOT NULL
);
GO

-- Creating table 'Subjects'
CREATE TABLE [dbo].[Subjects] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SchoolsId] int  NOT NULL,
    [SubjectTypesId] int  NOT NULL
);
GO

-- Creating table 'SubGroupTypes'
CREATE TABLE [dbo].[SubGroupTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [GroupsId] int  NOT NULL
);
GO

-- Creating table 'Lessons'
CREATE TABLE [dbo].[Lessons] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SubjectsId] int  NOT NULL,
    [TeachersId] int  NOT NULL,
    [GroupsId] int  NOT NULL,
    [RoomsId] int  NULL,
    [RoomTypesId] int  NOT NULL,
    [Schedule] nvarchar(max)  NOT NULL,
    [SchoolsId] int  NOT NULL
);
GO

-- Creating table 'Timetables'
CREATE TABLE [dbo].[Timetables] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SchoolsId] int  NOT NULL,
    [LessonsId] int  NOT NULL,
    [Day] int  NOT NULL,
    [Hour] int  NOT NULL,
    [RoomsId] int  NOT NULL,
    [Size] int  NOT NULL,
    [Part] int  NOT NULL
);
GO

-- Creating table 'STGConfig'
CREATE TABLE [dbo].[STGConfig] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PopulationSize] int  NOT NULL,
    [EpocheSize] int  NOT NULL,
    [NumberOfLessonToPositioning] int  NOT NULL,
    [BottomBorderOfBestSlots] int  NOT NULL,
    [TopBorderOfBestSlots] int  NOT NULL,
    [ProbabilityOfMutation] int  NOT NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [RoleId] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Teachers'
ALTER TABLE [dbo].[Teachers]
ADD CONSTRAINT [PK_Teachers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [PK_Groups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [PK_Rooms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Schools'
ALTER TABLE [dbo].[Schools]
ADD CONSTRAINT [PK_Schools]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RoomTypes'
ALTER TABLE [dbo].[RoomTypes]
ADD CONSTRAINT [PK_RoomTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SubjectTypes'
ALTER TABLE [dbo].[SubjectTypes]
ADD CONSTRAINT [PK_SubjectTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Subjects'
ALTER TABLE [dbo].[Subjects]
ADD CONSTRAINT [PK_Subjects]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SubGroupTypes'
ALTER TABLE [dbo].[SubGroupTypes]
ADD CONSTRAINT [PK_SubGroupTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Lessons'
ALTER TABLE [dbo].[Lessons]
ADD CONSTRAINT [PK_Lessons]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Timetables'
ALTER TABLE [dbo].[Timetables]
ADD CONSTRAINT [PK_Timetables]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'STGConfig'
ALTER TABLE [dbo].[STGConfig]
ADD CONSTRAINT [PK_STGConfig]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([AspNetRoles_Id], [AspNetUsers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles]
    FOREIGN KEY ([AspNetRoles_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUsers'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUsers]
ON [dbo].[AspNetUserRoles]
    ([AspNetUsers_Id]);
GO

-- Creating foreign key on [RoomTypesId] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [FK_RoomTypesRooms]
    FOREIGN KEY ([RoomTypesId])
    REFERENCES [dbo].[RoomTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomTypesRooms'
CREATE INDEX [IX_FK_RoomTypesRooms]
ON [dbo].[Rooms]
    ([RoomTypesId]);
GO

-- Creating foreign key on [SchoolsId] in table 'Teachers'
ALTER TABLE [dbo].[Teachers]
ADD CONSTRAINT [FK_SchoolsTeachers]
    FOREIGN KEY ([SchoolsId])
    REFERENCES [dbo].[Schools]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SchoolsTeachers'
CREATE INDEX [IX_FK_SchoolsTeachers]
ON [dbo].[Teachers]
    ([SchoolsId]);
GO

-- Creating foreign key on [SchoolsId] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [FK_SchoolsGroups]
    FOREIGN KEY ([SchoolsId])
    REFERENCES [dbo].[Schools]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SchoolsGroups'
CREATE INDEX [IX_FK_SchoolsGroups]
ON [dbo].[Groups]
    ([SchoolsId]);
GO

-- Creating foreign key on [SchoolsId] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [FK_SchoolsRooms]
    FOREIGN KEY ([SchoolsId])
    REFERENCES [dbo].[Schools]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SchoolsRooms'
CREATE INDEX [IX_FK_SchoolsRooms]
ON [dbo].[Rooms]
    ([SchoolsId]);
GO

-- Creating foreign key on [SchoolsId] in table 'RoomTypes'
ALTER TABLE [dbo].[RoomTypes]
ADD CONSTRAINT [FK_SchoolsRoomTypes]
    FOREIGN KEY ([SchoolsId])
    REFERENCES [dbo].[Schools]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SchoolsRoomTypes'
CREATE INDEX [IX_FK_SchoolsRoomTypes]
ON [dbo].[RoomTypes]
    ([SchoolsId]);
GO

-- Creating foreign key on [SchoolsId] in table 'SubjectTypes'
ALTER TABLE [dbo].[SubjectTypes]
ADD CONSTRAINT [FK_SchoolsSubjectTypes]
    FOREIGN KEY ([SchoolsId])
    REFERENCES [dbo].[Schools]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SchoolsSubjectTypes'
CREATE INDEX [IX_FK_SchoolsSubjectTypes]
ON [dbo].[SubjectTypes]
    ([SchoolsId]);
GO

-- Creating foreign key on [SchoolsId] in table 'Subjects'
ALTER TABLE [dbo].[Subjects]
ADD CONSTRAINT [FK_SchoolsSubjects]
    FOREIGN KEY ([SchoolsId])
    REFERENCES [dbo].[Schools]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SchoolsSubjects'
CREATE INDEX [IX_FK_SchoolsSubjects]
ON [dbo].[Subjects]
    ([SchoolsId]);
GO

-- Creating foreign key on [SubjectTypesId] in table 'Subjects'
ALTER TABLE [dbo].[Subjects]
ADD CONSTRAINT [FK_SubjectTypesSubjects]
    FOREIGN KEY ([SubjectTypesId])
    REFERENCES [dbo].[SubjectTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SubjectTypesSubjects'
CREATE INDEX [IX_FK_SubjectTypesSubjects]
ON [dbo].[Subjects]
    ([SubjectTypesId]);
GO

-- Creating foreign key on [AspNetUsersId] in table 'Schools'
ALTER TABLE [dbo].[Schools]
ADD CONSTRAINT [FK_AspNetUsersSchools]
    FOREIGN KEY ([AspNetUsersId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUsersSchools'
CREATE INDEX [IX_FK_AspNetUsersSchools]
ON [dbo].[Schools]
    ([AspNetUsersId]);
GO

-- Creating foreign key on [GroupsId] in table 'SubGroupTypes'
ALTER TABLE [dbo].[SubGroupTypes]
ADD CONSTRAINT [FK_GroupsSubGroupTypes]
    FOREIGN KEY ([GroupsId])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupsSubGroupTypes'
CREATE INDEX [IX_FK_GroupsSubGroupTypes]
ON [dbo].[SubGroupTypes]
    ([GroupsId]);
GO

-- Creating foreign key on [ParentGroup] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [FK_GroupsGroups]
    FOREIGN KEY ([ParentGroup])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupsGroups'
CREATE INDEX [IX_FK_GroupsGroups]
ON [dbo].[Groups]
    ([ParentGroup]);
GO

-- Creating foreign key on [SubGroupTypesId] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [FK_SubGroupTypesGroups]
    FOREIGN KEY ([SubGroupTypesId])
    REFERENCES [dbo].[SubGroupTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SubGroupTypesGroups'
CREATE INDEX [IX_FK_SubGroupTypesGroups]
ON [dbo].[Groups]
    ([SubGroupTypesId]);
GO

-- Creating foreign key on [SubjectsId] in table 'Lessons'
ALTER TABLE [dbo].[Lessons]
ADD CONSTRAINT [FK_SubjectsLessons]
    FOREIGN KEY ([SubjectsId])
    REFERENCES [dbo].[Subjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SubjectsLessons'
CREATE INDEX [IX_FK_SubjectsLessons]
ON [dbo].[Lessons]
    ([SubjectsId]);
GO

-- Creating foreign key on [TeachersId] in table 'Lessons'
ALTER TABLE [dbo].[Lessons]
ADD CONSTRAINT [FK_TeachersLessons]
    FOREIGN KEY ([TeachersId])
    REFERENCES [dbo].[Teachers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TeachersLessons'
CREATE INDEX [IX_FK_TeachersLessons]
ON [dbo].[Lessons]
    ([TeachersId]);
GO

-- Creating foreign key on [GroupsId] in table 'Lessons'
ALTER TABLE [dbo].[Lessons]
ADD CONSTRAINT [FK_GroupsLessons]
    FOREIGN KEY ([GroupsId])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupsLessons'
CREATE INDEX [IX_FK_GroupsLessons]
ON [dbo].[Lessons]
    ([GroupsId]);
GO

-- Creating foreign key on [RoomsId] in table 'Lessons'
ALTER TABLE [dbo].[Lessons]
ADD CONSTRAINT [FK_RoomsLessons]
    FOREIGN KEY ([RoomsId])
    REFERENCES [dbo].[Rooms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomsLessons'
CREATE INDEX [IX_FK_RoomsLessons]
ON [dbo].[Lessons]
    ([RoomsId]);
GO

-- Creating foreign key on [RoomTypesId] in table 'Lessons'
ALTER TABLE [dbo].[Lessons]
ADD CONSTRAINT [FK_RoomTypesLessons]
    FOREIGN KEY ([RoomTypesId])
    REFERENCES [dbo].[RoomTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomTypesLessons'
CREATE INDEX [IX_FK_RoomTypesLessons]
ON [dbo].[Lessons]
    ([RoomTypesId]);
GO

-- Creating foreign key on [SchoolsId] in table 'Timetables'
ALTER TABLE [dbo].[Timetables]
ADD CONSTRAINT [FK_SchoolsTimetables]
    FOREIGN KEY ([SchoolsId])
    REFERENCES [dbo].[Schools]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SchoolsTimetables'
CREATE INDEX [IX_FK_SchoolsTimetables]
ON [dbo].[Timetables]
    ([SchoolsId]);
GO

-- Creating foreign key on [SchoolsId] in table 'Lessons'
ALTER TABLE [dbo].[Lessons]
ADD CONSTRAINT [FK_SchoolsLessons]
    FOREIGN KEY ([SchoolsId])
    REFERENCES [dbo].[Schools]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SchoolsLessons'
CREATE INDEX [IX_FK_SchoolsLessons]
ON [dbo].[Lessons]
    ([SchoolsId]);
GO

-- Creating foreign key on [LessonsId] in table 'Timetables'
ALTER TABLE [dbo].[Timetables]
ADD CONSTRAINT [FK_LessonsTimetables]
    FOREIGN KEY ([LessonsId])
    REFERENCES [dbo].[Lessons]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LessonsTimetables'
CREATE INDEX [IX_FK_LessonsTimetables]
ON [dbo].[Timetables]
    ([LessonsId]);
GO

-- Creating foreign key on [RoomsId] in table 'Timetables'
ALTER TABLE [dbo].[Timetables]
ADD CONSTRAINT [FK_RoomsTimetables]
    FOREIGN KEY ([RoomsId])
    REFERENCES [dbo].[Rooms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomsTimetables'
CREATE INDEX [IX_FK_RoomsTimetables]
ON [dbo].[Timetables]
    ([RoomsId]);
GO

-- Creating foreign key on [STGConfigId] in table 'Schools'
ALTER TABLE [dbo].[Schools]
ADD CONSTRAINT [FK_STGConfigSchools]
    FOREIGN KEY ([STGConfigId])
    REFERENCES [dbo].[STGConfig]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_STGConfigSchools'
CREATE INDEX [IX_FK_STGConfigSchools]
ON [dbo].[Schools]
    ([STGConfigId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------