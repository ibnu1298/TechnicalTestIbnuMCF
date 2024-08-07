USE [TechnicalTestDB]
GO

/****** Object:  Table [dbo].[tr_bpkb]    Script Date: 06/08/2024 23:18:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tr_bpkb](
	[agreement_number] [nvarchar](100) NOT NULL,
	[bpkb_no] [nvarchar](100) NULL,
	[branch_id] [nvarchar](10) NULL,
	[bpkb_date] [datetime] NOT NULL,
	[faktur_no] [nvarchar](100) NULL,
	[faktur_date] [datetime] NOT NULL,
	[location_id] [nvarchar](10) NULL,
	[police_no] [nvarchar](20) NULL,
	[bpkb_date_in] [datetime] NOT NULL,
	[created_by] [nvarchar](20) NULL,
	[created_on] [datetime] NOT NULL,
	[last_updated_by] [nvarchar](20) NULL,
	[last_updated_on] [datetime] NOT NULL,
 CONSTRAINT [PK_tr_bpkb] PRIMARY KEY CLUSTERED 
(
	[agreement_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tr_bpkb] ADD  DEFAULT (getdate()) FOR [bpkb_date]
GO

ALTER TABLE [dbo].[tr_bpkb] ADD  DEFAULT (getdate()) FOR [faktur_date]
GO

ALTER TABLE [dbo].[tr_bpkb] ADD  DEFAULT (getdate()) FOR [bpkb_date_in]
GO

ALTER TABLE [dbo].[tr_bpkb] ADD  DEFAULT (getdate()) FOR [created_on]
GO

ALTER TABLE [dbo].[tr_bpkb] ADD  DEFAULT (getdate()) FOR [last_updated_on]
GO

ALTER TABLE [dbo].[tr_bpkb]  WITH CHECK ADD  CONSTRAINT [FK_tr_bpkb_ms_storage_location] FOREIGN KEY([location_id])
REFERENCES [dbo].[ms_storage_location] ([location_id])
GO

ALTER TABLE [dbo].[tr_bpkb] CHECK CONSTRAINT [FK_tr_bpkb_ms_storage_location]
GO


