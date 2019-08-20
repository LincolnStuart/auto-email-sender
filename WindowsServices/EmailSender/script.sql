CREATE TABLE [dbo].[Emails] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [To]               NVARCHAR (MAX) NULL,
    [Subject]          NVARCHAR (MAX) NULL,
    [Body]             NVARCHAR (MAX) NULL,
    [RegistrationDate] DATETIME2 (7)  NOT NULL,
    [SendDate]         DATETIME2 (7)  NULL,
    [Intention]        DATETIME2 (7)  NULL,
    [Attempts]         INT            NOT NULL,
    [UsingTemplate]    BIT            NOT NULL,
    [ErrorMessage]     NVARCHAR (MAX) NULL
);

CREATE TABLE [dbo].[EmailAttachment] (
    [Id]      INT             IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (MAX)  NULL,
    [Bytes]   VARBINARY (MAX) NULL,
    [EmailId] INT             NULL
);

ALTER TABLE [dbo].[EmailAttachment] 
ADD CONSTRAINT [FK_EmailAttachment_Emails_EmailId] 
FOREIGN KEY ([EmailId]) 
REFERENCES [dbo].[Emails] ([Id]);