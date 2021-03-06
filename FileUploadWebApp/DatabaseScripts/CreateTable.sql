USE ImageDatabase
GO

DROP TABLE IF EXISTS ImageUploadData

CREATE TABLE ImageUploadData (
	ImageID		int				IDENTITY(1,1) NOT NULL,
	ImageName	varchar(50)		NOT NULL,
	ImageData	varbinary(max)	NOT NULL,
	CreatedOn	datetime		NOT NULL,
	CONSTRAINT PK_ImageUploadData_ImageID PRIMARY KEY CLUSTERED (ImageID ASC)
)