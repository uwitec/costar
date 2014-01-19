-- *******************
-- START 20131030-SX-1
-- CREATE StoreProductColors Table
-- *******************
IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131030-SX-1')
BEGIN
	CREATE TABLE [dbo].[StoreProductColors](
		[ColorID] [int] NOT NULL,
		[ProductID] [bigint] NOT NULL,
		[Timestamp] [timestamp] NOT NULL,
	 CONSTRAINT [PK_StoreProductColors] PRIMARY KEY CLUSTERED 
	(
		[ColorID] ASC,
		[ProductID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131030-SX-1')
BEGIN
	ALTER TABLE [dbo].[StoreProductColors]  WITH CHECK ADD  CONSTRAINT [FK_StoreProductColors_StoreColors] FOREIGN KEY([ColorID])
	REFERENCES [dbo].[StoreColors] ([ColorID])
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131030-SX-1')
BEGIN
	ALTER TABLE [dbo].[StoreProductColors] CHECK CONSTRAINT [FK_StoreProductColors_StoreColors]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131030-SX-1')
BEGIN
	ALTER TABLE [dbo].[StoreProductColors]  WITH CHECK ADD  CONSTRAINT [FK_StoreProductColors_StoreProducts] FOREIGN KEY([ProductID])
	REFERENCES [dbo].[StoreProducts] ([ProductID])
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131030-SX-1')
BEGIN
	ALTER TABLE [dbo].[StoreProductColors] CHECK CONSTRAINT [FK_StoreProductColors_StoreProducts]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131030-SX-1')
BEGIN
   INSERT INTO AppliedUpdates(Name) Values ('20131030-SX-1')
END
GO
-- *******************
-- END 20131030-SX-1
-- CREATE StoreProductColors Table
-- *******************

-- *******************
-- START 20131103-SX-1
-- UPDATE StoreProducts Table
-- *******************
IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131103-SX-1')
BEGIN
	ALTER TABLE StoreProducts ALTER COLUMN RetailPrice numeric(9, 2) NULL
	ALTER TABLE StoreProducts ALTER COLUMN SalePrice numeric(9, 2) NOT NULL
	ALTER TABLE StoreProducts ALTER COLUMN VIPPrice numeric(9, 2) NULL
END
GO
	
IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131103-SX-1')
BEGIN
   INSERT INTO AppliedUpdates(Name) Values ('20131103-SX-1')
END
GO
-- *******************
-- END 20131103-SX-1
-- UPDATE StoreProducts Table
-- *******************

-- *******************
-- START 20131111-SX-1
-- CREATE StoreProductInventories Table
-- *******************
-- QtyAvail�ǲ�Ʒ�������
-- QtySold�ǲ�Ʒ��������
-- QtyOnHold�ǲ�Ʒ�ڹ��ﳵ�е�����
-- SortOrder������ֵ�����༭ҳ������������ֵ��Ŀ���Ǳ�֤��ʾʱ���ܰ����Ƕ����˳����ʾ������M,L,XXL
-- Variant1OptionID��StoreVariantTypeOptions���������ʾ�ÿ������ԣ�����M�ŵ��·�
-- Variant2OptionIDͬ�������������M�ŵĶ�ͯ���·�
IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131111-SX-1')
BEGIN
	CREATE TABLE [dbo].[StoreProductInventories](
		[InventoryID] [bigint] IDENTITY(1,1) NOT NULL,
		[ProductID] [bigint] NOT NULL,
		[QtyAvail] [int] NOT NULL,
		[QtySold] [int] NOT NULL,
		[QtyOnHold] [int] NOT NULL,
		[SortOrder] [int] NOT NULL,
		[Variant1OptionID] [bigint] NULL,
		[Variant2OptionID] [bigint] NULL,
		[Timestamp] [timestamp] NOT NULL,
	 CONSTRAINT [PK_StoreProductInventories] PRIMARY KEY CLUSTERED 
	(
		[InventoryID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131111-SX-1')
BEGIN
	ALTER TABLE [dbo].[StoreProductInventories]  WITH CHECK ADD  CONSTRAINT [FK_StoreProductInventories_StoreVariantTypeOptions] FOREIGN KEY([Variant1OptionID])
	REFERENCES [dbo].[StoreVariantTypeOptions] ([VariantOptionID])
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131111-SX-1')
BEGIN
	ALTER TABLE [dbo].[StoreProductInventories] CHECK CONSTRAINT [FK_StoreProductInventories_StoreVariantTypeOptions]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131111-SX-1')
BEGIN
	ALTER TABLE [dbo].[StoreProductInventories]  WITH CHECK ADD  CONSTRAINT [FK_StoreProductInventories_StoreVariantTypeOptions1] FOREIGN KEY([Variant2OptionID])
	REFERENCES [dbo].[StoreVariantTypeOptions] ([VariantOptionID])
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131111-SX-1')
BEGIN
	ALTER TABLE [dbo].[StoreProductInventories] CHECK CONSTRAINT [FK_StoreProductInventories_StoreVariantTypeOptions1]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131111-SX-1')
BEGIN
	ALTER TABLE [dbo].[StoreProductInventories] ADD  CONSTRAINT [DF_StoreProductInventories_QtyAvail]  DEFAULT ((0)) FOR [QtyAvail]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131111-SX-1')
BEGIN
	ALTER TABLE [dbo].[StoreProductInventories] ADD  CONSTRAINT [DF_StoreProductInventories_QtySold]  DEFAULT ((0)) FOR [QtySold]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131111-SX-1')
BEGIN
	ALTER TABLE [dbo].[StoreProductInventories] ADD  CONSTRAINT [DF_StoreProductInventories_QtyOnHold]  DEFAULT ((0)) FOR [QtyOnHold]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131111-SX-1')
BEGIN
	ALTER TABLE [dbo].[StoreProductInventories] ADD  CONSTRAINT [DF_StoreProductInventories_SortOrder]  DEFAULT ((1)) FOR [SortOrder]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131111-SX-1')
BEGIN
   INSERT INTO AppliedUpdates(Name) Values ('20131111-SX-1')
END
GO
-- *******************
-- END 20131111-SX-1
-- CREATE StoreProductInventories Table
-- *******************

-- *******************
-- START 20131116-SX-1
-- CREATE NewsLetters Table
-- *******************
IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131116-SX-1')
BEGIN
	CREATE TABLE [dbo].[NewsLetters](
		[NewsLetterID] [bigint] IDENTITY(1,1) NOT NULL,
		[Subject] [nvarchar](250) NOT NULL,
		[Body] [nvarchar](max) NOT NULL,
		[IsSent] [bit] NOT NULL,
		[CreateDate] [datetime] NOT NULL,
		[SendDate] [datetime] NULL,
		[CampaignCode] [nvarchar](20) NULL,
	 CONSTRAINT [PK_NewsLetters] PRIMARY KEY CLUSTERED 
	(
		[NewsLetterID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131116-SX-1')
BEGIN
	ALTER TABLE [dbo].[NewsLetters]  WITH CHECK ADD  CONSTRAINT [FK_NewsLetters_NewsLetters] FOREIGN KEY([NewsLetterID])
	REFERENCES [dbo].[NewsLetters] ([NewsLetterID])
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131116-SX-1')
BEGIN
	ALTER TABLE [dbo].[NewsLetters] CHECK CONSTRAINT [FK_NewsLetters_NewsLetters]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131116-SX-1')
BEGIN
	ALTER TABLE [dbo].[NewsLetters] ADD  CONSTRAINT [DF_NewsLetters_IsSent]  DEFAULT ((0)) FOR [IsSent]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131116-SX-1')
BEGIN
	ALTER TABLE [dbo].[NewsLetters] ADD  CONSTRAINT [DF_NewsLetters_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131116-SX-1')
BEGIN
   INSERT INTO AppliedUpdates(Name) Values ('20131116-SX-1')
END
GO
-- *******************
-- END 20131116-SX-1
-- CREATE NewsLetters Table
-- *******************

-- *******************
-- START 20131116-SX-2
-- CREATE Account Table
-- *******************
IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131116-SX-2')
BEGIN
CREATE TABLE [dbo].[Accounts](
	[AccountID] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[MobileCountryID] [int] NOT NULL,
	[Mobile] [nvarchar](50) NULL,
	[Password] [nvarchar](250) NULL,
	[BirthDate] [smalldatetime] NOT NULL,
	[IsMale] [bit] NOT NULL,
	[IsDeactivated] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131116-SX-2')
BEGIN
   INSERT INTO AppliedUpdates(Name) Values ('20131116-SX-2')
END
GO
-- *******************
-- END 20131116-SX-2
-- CREATE Account Table
-- *******************

-- *******************
-- START 20131116-SX-3
-- CREATE AccountNewsletters Table
-- *******************
IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131116-SX-3')
BEGIN
	CREATE TABLE [dbo].[AccountNewsletters](
		[AccountID] [bigint] NOT NULL,
		[NewsletterID] [bigint] NOT NULL,
		[Opened] [int] NOT NULL,
		[FirstOpenDate] [datetime] NULL,
		[ErrorMessage] [nvarchar](250) NULL,
		[IsSent] [bit] NOT NULL,
	 CONSTRAINT [PK_AccountNewsletters] PRIMARY KEY CLUSTERED 
	(
		[AccountID] ASC,
		[NewsletterID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131116-SX-3')
BEGIN
	ALTER TABLE [dbo].[AccountNewsletters]  WITH CHECK ADD  CONSTRAINT [FK_AccountNewsletters_Accounts] FOREIGN KEY([AccountID])
	REFERENCES [dbo].[Accounts] ([AccountID])
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131116-SX-3')
BEGIN
	ALTER TABLE [dbo].[AccountNewsletters] CHECK CONSTRAINT [FK_AccountNewsletters_Accounts]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131116-SX-3')
BEGIN
	ALTER TABLE [dbo].[AccountNewsletters]  WITH CHECK ADD  CONSTRAINT [FK_AccountNewsletters_NewsLetters] FOREIGN KEY([NewsletterID])
	REFERENCES [dbo].[NewsLetters] ([NewsLetterID])
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131116-SX-3')
BEGIN
	ALTER TABLE [dbo].[AccountNewsletters] CHECK CONSTRAINT [FK_AccountNewsletters_NewsLetters]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131116-SX-3')
BEGIN
	ALTER TABLE [dbo].[AccountNewsletters] ADD  CONSTRAINT [DF_AccountNewsletters_Opened]  DEFAULT ((0)) FOR [Opened]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131116-SX-3')
BEGIN
	ALTER TABLE [dbo].[AccountNewsletters] ADD  CONSTRAINT [DF_AccountNewsletters_IsSent]  DEFAULT ((0)) FOR [IsSent]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20131116-SX-3')
BEGIN
   INSERT INTO AppliedUpdates(Name) Values ('20131116-SX-3')
END
GO
-- *******************
-- END 20131116-SX-3
-- CREATE AccountNewsletters Table
-- *******************


-- *******************
-- START 20140102-SX-1
-- CREATE StoreShippingOptions Table
-- *******************
IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20140102-SX-1')
BEGIN
	CREATE TABLE [dbo].[StoreShippingOptions](
		[ShippingOptionID] [bigint] IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](100) NOT NULL, --���ͷ�ʽ����
		[TrackingUrl] [nvarchar](250) NULL, --���ͷ�ʽ������ַ
		[IsActive] [bit] NOT NULL, --�Ƿ񼤻�
		[PerOrderFlatRate] [smallmoney] NULL, --ÿ������Ǯ
		[PerItemFlatRate] [smallmoney] NULL, --ÿ����Ʒ����Ǯ
		[PerKGRate] [smallmoney] NULL, --ÿ�����Ǯ
		[Instruction] [nvarchar](2000) NULL, --���ͷ�ʽ˵��
		[Timestamp] [timestamp] NOT NULL,
	 CONSTRAINT [PK_StoreShippingOptions] PRIMARY KEY CLUSTERED 
	(
		[ShippingOptionID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20140102-SX-1')
BEGIN
	ALTER TABLE [dbo].[StoreShippingOptions] ADD  CONSTRAINT [DF_StoreShippingOptions_IsActive]  DEFAULT ((0)) FOR [IsActive]
END
GO

IF NOT EXISTS (SELECT * FROM AppliedUpdates WHERE Name = '20140102-SX-1')
BEGIN
   INSERT INTO AppliedUpdates(Name) Values ('20140102-SX-1')
END
GO
-- *******************
-- END 20140102-SX-1
-- CREATE StoreShippingOptions Table
-- *******************