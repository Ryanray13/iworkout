
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/09/2014 15:45:30
-- Generated from EDMX file: C:\Projects\Project\iWorkoutRelease\iWorkoutTFVC\DataLayer\iWorkoutEDM.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [iWorkoutAzureDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserTypeUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_UserTypeUser];
GO
IF OBJECT_ID(N'[dbo].[FK_UserCustomerRelationship]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerRelationships] DROP CONSTRAINT [FK_UserCustomerRelationship];
GO
IF OBJECT_ID(N'[dbo].[FK_UserCustomerRelationship1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerRelationships] DROP CONSTRAINT [FK_UserCustomerRelationship1];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerRelationshipContract]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Contracts] DROP CONSTRAINT [FK_CustomerRelationshipContract];
GO
IF OBJECT_ID(N'[dbo].[FK_ProgramProgramProfile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProgramProfiles] DROP CONSTRAINT [FK_ProgramProgramProfile];
GO
IF OBJECT_ID(N'[dbo].[FK_ActivityTypeActivity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Activities] DROP CONSTRAINT [FK_ActivityTypeActivity];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfileDetailProgramProfile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProgramProfiles] DROP CONSTRAINT [FK_ProfileDetailProgramProfile];
GO
IF OBJECT_ID(N'[dbo].[FK_UserProgram]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Programs] DROP CONSTRAINT [FK_UserProgram];
GO
IF OBJECT_ID(N'[dbo].[FK_ActivityProfileActivity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProfileActivities] DROP CONSTRAINT [FK_ActivityProfileActivity];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfileDetailProfileActivity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProfileActivities] DROP CONSTRAINT [FK_ProfileDetailProfileActivity];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfileActivityActivityLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActivityLogs] DROP CONSTRAINT [FK_ProfileActivityActivityLog];
GO
IF OBJECT_ID(N'[dbo].[FK_ProgramSubscription]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Subscriptions] DROP CONSTRAINT [FK_ProgramSubscription];
GO
IF OBJECT_ID(N'[dbo].[FK_UserSubscription]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Subscriptions] DROP CONSTRAINT [FK_UserSubscription];
GO
IF OBJECT_ID(N'[dbo].[FK_SubscriptionActivityLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActivityLogs] DROP CONSTRAINT [FK_SubscriptionActivityLog];
GO
IF OBJECT_ID(N'[dbo].[FK_UserProfileDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProfileDetails] DROP CONSTRAINT [FK_UserProfileDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_UserActivity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Activities] DROP CONSTRAINT [FK_UserActivity];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[UserTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserTypes];
GO
IF OBJECT_ID(N'[dbo].[CustomerRelationships]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerRelationships];
GO
IF OBJECT_ID(N'[dbo].[Contracts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contracts];
GO
IF OBJECT_ID(N'[dbo].[Programs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Programs];
GO
IF OBJECT_ID(N'[dbo].[ProgramProfiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProgramProfiles];
GO
IF OBJECT_ID(N'[dbo].[ProfileDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProfileDetails];
GO
IF OBJECT_ID(N'[dbo].[Activities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Activities];
GO
IF OBJECT_ID(N'[dbo].[ActivityTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActivityTypes];
GO
IF OBJECT_ID(N'[dbo].[ActivityLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActivityLogs];
GO
IF OBJECT_ID(N'[dbo].[ProfileActivities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProfileActivities];
GO
IF OBJECT_ID(N'[dbo].[Subscriptions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Subscriptions];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] uniqueidentifier  NOT NULL,
    [Handle] nvarchar(max)  NOT NULL,
    [UserTypeKey_Id] int  NOT NULL
);
GO

-- Creating table 'UserTypes'
CREATE TABLE [dbo].[UserTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CustomerRelationships'
CREATE TABLE [dbo].[CustomerRelationships] (
    [SelfCoached] bit  NOT NULL,
    [ClientKey] uniqueidentifier  NOT NULL,
    [CoachKey] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Contracts'
CREATE TABLE [dbo].[Contracts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ClientKey] uniqueidentifier  NOT NULL,
    [CoachKey] uniqueidentifier  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'Programs'
CREATE TABLE [dbo].[Programs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [UserOwnerKey_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'ProgramProfiles'
CREATE TABLE [dbo].[ProgramProfiles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProgramKey_Id] int  NOT NULL,
    [ProfileDetailKey_Id] int  NOT NULL
);
GO

-- Creating table 'ProfileDetails'
CREATE TABLE [dbo].[ProfileDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Priority] int  NULL,
    [CycleLength] int  NOT NULL,
    [CycleRepeatCount] int  NOT NULL,
    [UserOwnerKey_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Activities'
CREATE TABLE [dbo].[Activities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [ActivityTypeKey_Id] int  NOT NULL,
    [UserOwnerKey_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'ActivityTypes'
CREATE TABLE [dbo].[ActivityTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ActivityLogs'
CREATE TABLE [dbo].[ActivityLogs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Accomplished] bit  NOT NULL,
    [Memo] nvarchar(max)  NULL,
    [Date] datetime  NOT NULL,
    [ProfileActivityKey_Id] int  NOT NULL,
    [SubscriptionKey_Id] int  NOT NULL
);
GO

-- Creating table 'ProfileActivities'
CREATE TABLE [dbo].[ProfileActivities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Priority] int  NOT NULL,
    [IsActive] bit  NOT NULL,
    [ActivityKey_Id] int  NOT NULL,
    [ProfileDetailKey_Id] int  NOT NULL
);
GO

-- Creating table 'Subscriptions'
CREATE TABLE [dbo].[Subscriptions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IsActive] bit  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [ProgramKey_Id] int  NOT NULL,
    [UserClientKey_Id] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserTypes'
ALTER TABLE [dbo].[UserTypes]
ADD CONSTRAINT [PK_UserTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ClientKey], [CoachKey] in table 'CustomerRelationships'
ALTER TABLE [dbo].[CustomerRelationships]
ADD CONSTRAINT [PK_CustomerRelationships]
    PRIMARY KEY CLUSTERED ([ClientKey], [CoachKey] ASC);
GO

-- Creating primary key on [Id] in table 'Contracts'
ALTER TABLE [dbo].[Contracts]
ADD CONSTRAINT [PK_Contracts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Programs'
ALTER TABLE [dbo].[Programs]
ADD CONSTRAINT [PK_Programs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProgramProfiles'
ALTER TABLE [dbo].[ProgramProfiles]
ADD CONSTRAINT [PK_ProgramProfiles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProfileDetails'
ALTER TABLE [dbo].[ProfileDetails]
ADD CONSTRAINT [PK_ProfileDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Activities'
ALTER TABLE [dbo].[Activities]
ADD CONSTRAINT [PK_Activities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ActivityTypes'
ALTER TABLE [dbo].[ActivityTypes]
ADD CONSTRAINT [PK_ActivityTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ActivityLogs'
ALTER TABLE [dbo].[ActivityLogs]
ADD CONSTRAINT [PK_ActivityLogs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProfileActivities'
ALTER TABLE [dbo].[ProfileActivities]
ADD CONSTRAINT [PK_ProfileActivities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Subscriptions'
ALTER TABLE [dbo].[Subscriptions]
ADD CONSTRAINT [PK_Subscriptions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserTypeKey_Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_UserTypeUser]
    FOREIGN KEY ([UserTypeKey_Id])
    REFERENCES [dbo].[UserTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserTypeUser'
CREATE INDEX [IX_FK_UserTypeUser]
ON [dbo].[Users]
    ([UserTypeKey_Id]);
GO

-- Creating foreign key on [ClientKey] in table 'CustomerRelationships'
ALTER TABLE [dbo].[CustomerRelationships]
ADD CONSTRAINT [FK_UserCustomerRelationship]
    FOREIGN KEY ([ClientKey])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CoachKey] in table 'CustomerRelationships'
ALTER TABLE [dbo].[CustomerRelationships]
ADD CONSTRAINT [FK_UserCustomerRelationship1]
    FOREIGN KEY ([CoachKey])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserCustomerRelationship1'
CREATE INDEX [IX_FK_UserCustomerRelationship1]
ON [dbo].[CustomerRelationships]
    ([CoachKey]);
GO

-- Creating foreign key on [ClientKey], [CoachKey] in table 'Contracts'
ALTER TABLE [dbo].[Contracts]
ADD CONSTRAINT [FK_CustomerRelationshipContract]
    FOREIGN KEY ([ClientKey], [CoachKey])
    REFERENCES [dbo].[CustomerRelationships]
        ([ClientKey], [CoachKey])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerRelationshipContract'
CREATE INDEX [IX_FK_CustomerRelationshipContract]
ON [dbo].[Contracts]
    ([ClientKey], [CoachKey]);
GO

-- Creating foreign key on [ProgramKey_Id] in table 'ProgramProfiles'
ALTER TABLE [dbo].[ProgramProfiles]
ADD CONSTRAINT [FK_ProgramProgramProfile]
    FOREIGN KEY ([ProgramKey_Id])
    REFERENCES [dbo].[Programs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProgramProgramProfile'
CREATE INDEX [IX_FK_ProgramProgramProfile]
ON [dbo].[ProgramProfiles]
    ([ProgramKey_Id]);
GO

-- Creating foreign key on [ActivityTypeKey_Id] in table 'Activities'
ALTER TABLE [dbo].[Activities]
ADD CONSTRAINT [FK_ActivityTypeActivity]
    FOREIGN KEY ([ActivityTypeKey_Id])
    REFERENCES [dbo].[ActivityTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActivityTypeActivity'
CREATE INDEX [IX_FK_ActivityTypeActivity]
ON [dbo].[Activities]
    ([ActivityTypeKey_Id]);
GO

-- Creating foreign key on [ProfileDetailKey_Id] in table 'ProgramProfiles'
ALTER TABLE [dbo].[ProgramProfiles]
ADD CONSTRAINT [FK_ProfileDetailProgramProfile]
    FOREIGN KEY ([ProfileDetailKey_Id])
    REFERENCES [dbo].[ProfileDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfileDetailProgramProfile'
CREATE INDEX [IX_FK_ProfileDetailProgramProfile]
ON [dbo].[ProgramProfiles]
    ([ProfileDetailKey_Id]);
GO

-- Creating foreign key on [UserOwnerKey_Id] in table 'Programs'
ALTER TABLE [dbo].[Programs]
ADD CONSTRAINT [FK_UserProgram]
    FOREIGN KEY ([UserOwnerKey_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserProgram'
CREATE INDEX [IX_FK_UserProgram]
ON [dbo].[Programs]
    ([UserOwnerKey_Id]);
GO

-- Creating foreign key on [ActivityKey_Id] in table 'ProfileActivities'
ALTER TABLE [dbo].[ProfileActivities]
ADD CONSTRAINT [FK_ActivityProfileActivity]
    FOREIGN KEY ([ActivityKey_Id])
    REFERENCES [dbo].[Activities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActivityProfileActivity'
CREATE INDEX [IX_FK_ActivityProfileActivity]
ON [dbo].[ProfileActivities]
    ([ActivityKey_Id]);
GO

-- Creating foreign key on [ProfileDetailKey_Id] in table 'ProfileActivities'
ALTER TABLE [dbo].[ProfileActivities]
ADD CONSTRAINT [FK_ProfileDetailProfileActivity]
    FOREIGN KEY ([ProfileDetailKey_Id])
    REFERENCES [dbo].[ProfileDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfileDetailProfileActivity'
CREATE INDEX [IX_FK_ProfileDetailProfileActivity]
ON [dbo].[ProfileActivities]
    ([ProfileDetailKey_Id]);
GO

-- Creating foreign key on [ProfileActivityKey_Id] in table 'ActivityLogs'
ALTER TABLE [dbo].[ActivityLogs]
ADD CONSTRAINT [FK_ProfileActivityActivityLog]
    FOREIGN KEY ([ProfileActivityKey_Id])
    REFERENCES [dbo].[ProfileActivities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfileActivityActivityLog'
CREATE INDEX [IX_FK_ProfileActivityActivityLog]
ON [dbo].[ActivityLogs]
    ([ProfileActivityKey_Id]);
GO

-- Creating foreign key on [ProgramKey_Id] in table 'Subscriptions'
ALTER TABLE [dbo].[Subscriptions]
ADD CONSTRAINT [FK_ProgramSubscription]
    FOREIGN KEY ([ProgramKey_Id])
    REFERENCES [dbo].[Programs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProgramSubscription'
CREATE INDEX [IX_FK_ProgramSubscription]
ON [dbo].[Subscriptions]
    ([ProgramKey_Id]);
GO

-- Creating foreign key on [UserClientKey_Id] in table 'Subscriptions'
ALTER TABLE [dbo].[Subscriptions]
ADD CONSTRAINT [FK_UserSubscription]
    FOREIGN KEY ([UserClientKey_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserSubscription'
CREATE INDEX [IX_FK_UserSubscription]
ON [dbo].[Subscriptions]
    ([UserClientKey_Id]);
GO

-- Creating foreign key on [SubscriptionKey_Id] in table 'ActivityLogs'
ALTER TABLE [dbo].[ActivityLogs]
ADD CONSTRAINT [FK_SubscriptionActivityLog]
    FOREIGN KEY ([SubscriptionKey_Id])
    REFERENCES [dbo].[Subscriptions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SubscriptionActivityLog'
CREATE INDEX [IX_FK_SubscriptionActivityLog]
ON [dbo].[ActivityLogs]
    ([SubscriptionKey_Id]);
GO

-- Creating foreign key on [UserOwnerKey_Id] in table 'ProfileDetails'
ALTER TABLE [dbo].[ProfileDetails]
ADD CONSTRAINT [FK_UserProfileDetail]
    FOREIGN KEY ([UserOwnerKey_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserProfileDetail'
CREATE INDEX [IX_FK_UserProfileDetail]
ON [dbo].[ProfileDetails]
    ([UserOwnerKey_Id]);
GO

-- Creating foreign key on [UserOwnerKey_Id] in table 'Activities'
ALTER TABLE [dbo].[Activities]
ADD CONSTRAINT [FK_UserActivity]
    FOREIGN KEY ([UserOwnerKey_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserActivity'
CREATE INDEX [IX_FK_UserActivity]
ON [dbo].[Activities]
    ([UserOwnerKey_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------