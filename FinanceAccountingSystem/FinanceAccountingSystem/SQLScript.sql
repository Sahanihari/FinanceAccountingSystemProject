IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'FinanceAccountingDB')
BEGIN
    CREATE DATABASE FinanceAccountingDB;
END
GO

/****** Object:  Table [dbo].[Addresses]    Script Date: 12-03-2026 21:59:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[AddressId] [int] IDENTITY(1,1) NOT NULL,
	[Street] [nvarchar](200) NULL,
	[City] [nvarchar](100) NULL,
	[PostCode] [nvarchar](20) NULL,
	[Country] [nvarchar](50) NULL CONSTRAINT [DF_Addresses_Country]  DEFAULT (N'United Kingdom'),
PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customers]    Script Date: 12-03-2026 21:59:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[AddressId] [int] NULL,
	[ContactNumber] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 12-03-2026 21:59:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[InvoiceId] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceNumber] [nvarchar](50) NULL,
	[InvoiceDate] [datetime] NULL,
	[SupplierId] [int] NULL,
	[CustomerId] [int] NULL,
	[Amount] [decimal](18, 2) NULL,
	[TaxRate] [decimal](10, 2) NULL,
	[TaxAmount] [decimal](18, 2) NULL,
	[TotalAmount] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 12-03-2026 21:59:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[SupplierId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[AddressId] [int] NULL,
	[ContactNumber] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[TaxId] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Addresses] ON 

GO
INSERT [dbo].[Addresses] ([AddressId], [Street], [City], [PostCode], [Country]) VALUES (3, N'Kotiya', N'Surajpur', N'276306', N'India')
GO
INSERT [dbo].[Addresses] ([AddressId], [Street], [City], [PostCode], [Country]) VALUES (4, N'Shanti Kunj', N'New Delhi', N'227601', N'India')
GO
SET IDENTITY_INSERT [dbo].[Addresses] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

GO
INSERT [dbo].[Customers] ([CustomerId], [Name], [AddressId], [ContactNumber], [Email]) VALUES (3, N'Hariram Sahani', 4, N'08173046570', N'hariramsahani064@gmail.com')
GO
INSERT [dbo].[Customers] ([CustomerId], [Name], [AddressId], [ContactNumber], [Email]) VALUES (4, N'Vijayi Sahani Updated', 3, N'08173046570', N'Vijay064@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[Invoices] ON 

GO
INSERT [dbo].[Invoices] ([InvoiceId], [InvoiceNumber], [InvoiceDate], [SupplierId], [CustomerId], [Amount], [TaxRate], [TaxAmount], [TotalAmount]) VALUES (1, N'INV001', CAST(N'2026-03-12 00:00:00.000' AS DateTime), 3, 3, CAST(150.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(10, 2)), CAST(27.00 AS Decimal(18, 2)), CAST(177.00 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[Invoices] OFF
GO
SET IDENTITY_INSERT [dbo].[Suppliers] ON 

GO
INSERT [dbo].[Suppliers] ([SupplierId], [Name], [AddressId], [ContactNumber], [Email], [TaxId]) VALUES (3, N'Apple Inc.', 3, N'1234567890', N'apple@icloud.com', N'TX6548971')
GO
INSERT [dbo].[Suppliers] ([SupplierId], [Name], [AddressId], [ContactNumber], [Email], [TaxId]) VALUES (4, N'Hariram Sahani', 3, N'08173046570', N'hariramsahani064@gmail.com', N'86345345')
GO
SET IDENTITY_INSERT [dbo].[Suppliers] OFF
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD FOREIGN KEY([AddressId])
REFERENCES [dbo].[Addresses] ([AddressId])
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Suppliers] ([SupplierId])
GO
ALTER TABLE [dbo].[Suppliers]  WITH CHECK ADD FOREIGN KEY([AddressId])
REFERENCES [dbo].[Addresses] ([AddressId])
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteSupplier]    Script Date: 12-03-2026 21:59:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteSupplier]
(
@SupplierId INT
)
AS
BEGIN

DELETE FROM Suppliers
WHERE SupplierId=@SupplierId

END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetSuppliers]    Script Date: 12-03-2026 21:59:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetSuppliers]
AS
BEGIN

SELECT * FROM Suppliers

END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertSupplier]    Script Date: 12-03-2026 21:59:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertSupplier]
(
@Name NVARCHAR(200),
@AddressId INT,
@ContactNumber NVARCHAR(20),
@Email NVARCHAR(100),
@TaxId NVARCHAR(50)
)
AS
BEGIN

INSERT INTO Suppliers
(Name,AddressId,ContactNumber,Email,TaxId)

VALUES
(@Name,@AddressId,@ContactNumber,@Email,@TaxId)

END
GO
