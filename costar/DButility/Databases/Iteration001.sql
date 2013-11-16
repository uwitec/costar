CREATE TABLE [dbo].[AppliedUpdates](
	[UpdateID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Timestamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_AppliedUpdates] PRIMARY KEY CLUSTERED 
(
	[UpdateID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- *******************
-- START 20131024-SX-1
-- CREATE StoreAnimes Table
-- *******************
IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131024-SX-1')
BEGIN
	CREATE TABLE [dbo].[StoreAnimes](
		[AnimeID] [int] IDENTITY(1,1) NOT NULL,
		[AnimeName] [nvarchar](100) NULL,
		[TimeStamp] [timestamp] NULL,
	 CONSTRAINT [PK_StoreAnimes] PRIMARY KEY CLUSTERED 
	(
		[AnimeID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131024-SX-1')
BEGIN
   INSERT INTO AppliedUpdates(Name) Values ('20131024-SX-1')
END
GO
-- *******************
-- END 20131024-SX-1
-- CREATE StoreAnimes Table
-- *******************


-- *******************
-- START 20131024-SX-2
-- CREATE StoreColors Table
-- *******************
IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131024-SX-2')
BEGIN
	CREATE TABLE [dbo].[StoreColors](
		[ColorID] [int] IDENTITY(1,1) NOT NULL,
		[ColorName] [nvarchar](20) NULL,
		[Timestamp] [timestamp] NULL,
	 CONSTRAINT [PK_StoreColors] PRIMARY KEY CLUSTERED 
	(
		[ColorID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131024-SX-2')
BEGIN
   INSERT INTO AppliedUpdates(Name) Values ('20131024-SX-2')
END
GO
-- *******************
-- END 20131024-SX-2
-- CREATE StoreColors Table
-- *******************


-- *******************
-- START 20131024-SX-3
-- CREATE StoreVariantTypes Table
-- *******************
IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131024-SX-3')
BEGIN
	CREATE TABLE [dbo].[StoreVariantTypes](
		[VariantTypeID] [bigint] IDENTITY(1,1) NOT NULL,
		[GroupName] [nvarchar](50) NOT NULL,
		[Name] [nvarchar](1000) NOT NULL,
		[Timestamp] [timestamp] NOT NULL,
	 CONSTRAINT [PK_StoreVariantTypes] PRIMARY KEY CLUSTERED 
	(
		[VariantTypeID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131024-SX-3')
BEGIN
   INSERT INTO AppliedUpdates(Name) Values ('20131024-SX-3')
END
GO
-- *******************
-- END 20131024-SX-3
-- CREATE StoreVariantTypes Table
-- *******************



-- *******************
-- START 20131024-SX-4
-- CREATE StoreVariantTypeOptions Table
-- *******************
IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131024-SX-4')
BEGIN
	CREATE TABLE [dbo].[StoreVariantTypeOptions](
		[VariantOptionID] [bigint] IDENTITY(1,1) NOT NULL,
		[VariantTypeID] [bigint] NOT NULL,
		[Name] [nvarchar](200) NOT NULL,
		[SortOrder] [int] NOT NULL,
		[Timestamp] [timestamp] NOT NULL,
	 CONSTRAINT [PK_StoreVariantTypeOptions] PRIMARY KEY CLUSTERED 
	(
		[VariantOptionID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131024-SX-4')
BEGIN
	ALTER TABLE [dbo].[StoreVariantTypeOptions]  WITH CHECK ADD  CONSTRAINT [FK_StoreVariantTypeOptions_StoreVariantTypes] FOREIGN KEY([VariantTypeID])
	REFERENCES [dbo].[StoreVariantTypes] ([VariantTypeID])
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131024-SX-4')
BEGIN
	ALTER TABLE [dbo].[StoreVariantTypeOptions] CHECK CONSTRAINT [FK_StoreVariantTypeOptions_StoreVariantTypes]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131024-SX-4')
BEGIN
   INSERT INTO AppliedUpdates(Name) Values ('20131024-SX-4')
END
GO
-- *******************
-- END 20131024-SX-4
-- CREATE StoreVariantTypeOptions Table
-- *******************

-- *******************
-- START 20131024-SX-5
-- CREATE StoreProducts Table
-- *******************
IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131024-SX-5')
BEGIN
	CREATE TABLE [dbo].[StoreProducts](
		[ProductID] [bigint] IDENTITY(1,1) NOT NULL,
		[AnimeID] [int] NOT NULL,
		[ProductCode] [nvarchar](200) NULL,
		[Name] [nvarchar](200) NOT NULL,
		[Description] [nvarchar](max) NULL,
		[Title] [nvarchar](200) NULL,
		[PageName] [nvarchar](100) NULL,
		[IsActive] [bit] NOT NULL,
		[IsFeatured] [bit] NOT NULL,
		[ImageFile] [nvarchar](50) NULL,
		[ImageFile2] [nvarchar](50) NULL,
		[ImageFile3] [nvarchar](50) NULL,
		[ImageFile4] [nvarchar](50) NULL,
		[RetailPrice] [smallmoney] NULL,
		[SalePrice] [smallmoney] NOT NULL,
		[VIPPrice] [smallmoney] NULL,
		[DeleteDate] [datetime] NULL,
		[AddeDate] [datetime] NOT NULL,
		[Variant1TypeID] [bigint] NULL,
		[Variant2TypeID] [bigint] NULL,
		[Timestamp] [timestamp] NOT NULL,
	 CONSTRAINT [PK_StoreProducts] PRIMARY KEY CLUSTERED 
	(
		[ProductID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131024-SX-5')
BEGIN
	ALTER TABLE [dbo].[StoreProducts]  WITH CHECK ADD  CONSTRAINT [FK_StoreProducts_StoreAnimes] FOREIGN KEY([AnimeID])
	REFERENCES [dbo].[StoreAnimes] ([AnimeID])
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131024-SX-5')
BEGIN
	ALTER TABLE [dbo].[StoreProducts] CHECK CONSTRAINT [FK_StoreProducts_StoreAnimes]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131024-SX-5')
BEGIN
	ALTER TABLE [dbo].[StoreProducts]  WITH CHECK ADD  CONSTRAINT [FK_StoreProducts_StoreVariantTypes] FOREIGN KEY([Variant1TypeID])
	REFERENCES [dbo].[StoreVariantTypes] ([VariantTypeID])
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131024-SX-5')
BEGIN
	ALTER TABLE [dbo].[StoreProducts] CHECK CONSTRAINT [FK_StoreProducts_StoreVariantTypes]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131024-SX-5')
BEGIN
	ALTER TABLE [dbo].[StoreProducts]  WITH CHECK ADD  CONSTRAINT [FK_StoreProducts_StoreVariantTypes1] FOREIGN KEY([Variant2TypeID])
	REFERENCES [dbo].[StoreVariantTypes] ([VariantTypeID])
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131024-SX-5')
BEGIN
	ALTER TABLE [dbo].[StoreProducts] CHECK CONSTRAINT [FK_StoreProducts_StoreVariantTypes1]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131024-SX-5')
BEGIN
   INSERT INTO AppliedUpdates(Name) Values ('20131024-SX-5')
END
GO
-- *******************
-- END 20131024-SX-5
-- CREATE StoreProducts Table
-- *******************


