CREATE SCHEMA usmg
GO

/****** Object:  Table [dbo].[MSreplication_options]    Script Date: 18.03.2021 11:28:34 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [usmg].[Users](
	[UserId] uniqueidentifier NOT NULL,
	[RegisteredClientId] nvarchar(10) NOT NULL,
	[LoginName] nvarchar(250) NOT NULL,
	[FirstName] nvarchar(250) NULL,
	[LastName] nvarchar(250) NULL,
	[Email] nvarchar(250) NULL,
	[Password] nvarchar(250) NULL,
	[PasswordLastChangedDate] datetime2 NULL,
	[Language] nvarchar(2) NULL,
	[LastLoginDate] datetime2 NULL,
	[IsActive] [bit] DEFAULT((1)) NOT NULL,
	[CreationDate] datetime2 NOT NULL,
	[CreationUser] nvarchar(250) NOT NULL,
	[CreationTimestamp] bigint IDENTITY NOT NULL,
	[UpdateDate] datetime2 NULL,
	[UpdateUser] nvarchar(250) NULL,
	[UpdateTimestamp] timestamp NOT NULL,
	CONSTRAINT PK_USMG_Users PRIMARY KEY ([UserId]),
	CONSTRAINT UC_USMG_ClientLogin UNIQUE ([RegisteredClientId],[LoginName])
)
GO

ALTER TABLE usmg.Users
ADD CONSTRAINT FK_USMG_Users_RegisteredClientId
FOREIGN KEY ([RegisteredClientId]) REFERENCES dbo.RegisteredClients([RegisteredClientId]);
GO

ALTER TABLE [usmg].[Users] ADD 
	[EmailIsValidated] [bit] DEFAULT((0)) NOT NULL
GO