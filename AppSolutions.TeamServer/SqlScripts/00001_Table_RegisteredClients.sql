/****** Object:  Table [dbo].[MSreplication_options]    Script Date: 18.03.2021 11:28:34 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RegisteredClients](
	[RegisteredClientId] nvarchar(10) NOT NULL UNIQUE,
	[State] [int] DEFAULT((0)) NOT NULL,
	[Country] nvarchar(2) NOT NULL,
	[CompanyName] nvarchar(250) NOT NULL,
	[CreationDate] datetime2 NOT NULL,
	[CreationUser] nvarchar(250) NOT NULL,
	[UpdateDate] datetime2 NULL,
	[UpdateUser] nvarchar(250) NULL,
	CONSTRAINT PK_RegisteredClients PRIMARY KEY ([RegisteredClientId])
)
GO


