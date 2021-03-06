USE [band_tracker]
GO
/****** Object:  Table [dbo].[bands]    Script Date: 3/13/2017 7:11:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bands](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bands_venues]    Script Date: 3/13/2017 7:11:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bands_venues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[band_id] [int] NULL,
	[venue_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[venues]    Script Date: 3/13/2017 7:11:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[bands] ON 

INSERT [dbo].[bands] ([id], [name]) VALUES (1, N'Cameo')
INSERT [dbo].[bands] ([id], [name]) VALUES (2, N'Cameo')
INSERT [dbo].[bands] ([id], [name]) VALUES (3, N'Cameo')
INSERT [dbo].[bands] ([id], [name]) VALUES (4, N'Earth')
INSERT [dbo].[bands] ([id], [name]) VALUES (5, N'Cameo')
INSERT [dbo].[bands] ([id], [name]) VALUES (6, N'Ohio Players')
INSERT [dbo].[bands] ([id], [name]) VALUES (8, N'funkness')
SET IDENTITY_INSERT [dbo].[bands] OFF
SET IDENTITY_INSERT [dbo].[bands_venues] ON 

INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (1, 3, 5)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (2, 6, 4)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (3, 2, 1)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (4, 5, 2)
SET IDENTITY_INSERT [dbo].[bands_venues] OFF
SET IDENTITY_INSERT [dbo].[venues] ON 

INSERT [dbo].[venues] ([id], [name]) VALUES (1, N'Yoshis')
INSERT [dbo].[venues] ([id], [name]) VALUES (2, N'House SF')
INSERT [dbo].[venues] ([id], [name]) VALUES (3, N'Yoshis SF')
INSERT [dbo].[venues] ([id], [name]) VALUES (4, N'Yoshis')
INSERT [dbo].[venues] ([id], [name]) VALUES (5, N'Times Sq')
SET IDENTITY_INSERT [dbo].[venues] OFF
