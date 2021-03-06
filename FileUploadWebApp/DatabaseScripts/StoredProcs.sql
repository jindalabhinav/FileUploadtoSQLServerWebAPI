USE ImageDatabase;
GO

--SELECT TOP (1000) [ImageID]
--      ,[ImageName]
--      ,[ImageData]
--      ,[CreatedOn]
--  FROM [ImageDatabase].[dbo].[ImageUploadData]

SET ANSI_NULLS ON  
GO  
SET QUOTED_IDENTIFIER ON  
GO  
-- =============================================  
-- Author:      Abhinav Jindal  
-- Create date: 28-Feb-2021  
-- Description: To insert an image into ImageDatabase.dbo.ImageUploadData
-- ============================================= 

CREATE PROCEDURE uspInsertImage
	@ImageName	varchar(50),
	@ImageData	varbinary(max),
	@CreatedOn	datetime
AS
	BEGIN
		INSERT INTO dbo.ImageUploadData (ImageName, ImageData, CreatedOn)
		VALUES (@ImageName, @ImageData, @CreatedOn);
	END
GO


-- =============================================  
-- Author:      Abhinav Jindal  
-- Create date: 28-Feb-2021  
-- Description: To get all image names from ImageDatabase.dbo.ImageUploadData
-- ============================================= 

CREATE PROCEDURE uspGetImageNames
AS
	SELECT ImageName FROM dbo.ImageUploadData;
GO


-- =============================================  
-- Author:      Abhinav Jindal  
-- Create date: 28-Feb-2021  
-- Description: To get all image names in ascending order from ImageDatabase.dbo.ImageUploadData
-- ============================================= 

CREATE PROCEDURE uspGetImageNamesAsc
AS
	SELECT ImageName FROM dbo.ImageUploadData
	ORDER BY ImageID ASC;
GO


-- =============================================  
-- Author:      Abhinav Jindal  
-- Create date: 28-Feb-2021  
-- Description: To get all image names in descending order from ImageDatabase.dbo.ImageUploadData
-- ============================================= 

CREATE PROCEDURE uspGetImageNamesDes
AS
	SELECT ImageName FROM dbo.ImageUploadData
	ORDER BY ImageID DESC;
GO


-- =============================================  
-- Author:      Abhinav Jindal  
-- Create date: 28-Feb-2021  
-- Description: To search the images= names using a keyword from ImageDatabase.dbo.ImageUploadData
-- ============================================= 

CREATE PROCEDURE uspSearchImageNames
	@SearchKeyword	varchar(50)
AS
	SELECT ImageName FROM dbo.ImageUploadData
	WHERE ImageName LIKE '%' + @SearchKeyword + '%';
GO
