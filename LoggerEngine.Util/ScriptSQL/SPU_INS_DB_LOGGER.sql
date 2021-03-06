/****** Object:  StoredProcedure [dbo].[SPU_INS_DB_LOGGER] ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SPU_INS_DB_LOGGER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SPU_INS_DB_LOGGER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SPU_INS_DB_LOGGER]
( 
	@LOG_TYPE VARCHAR (20),
	@LOG_MESSAGE VARCHAR(4000)
)
AS BEGIN TRY

DECLARE @CURRENT_DATE  DATETIME;
SET @CURRENT_DATE = GETDATE();

Insert into DB_LOGGER
(LOG_TYPE, CREATION_DATE, LOG_MESSAGE)
Values
(@LOG_TYPE, @CURRENT_DATE, @LOG_MESSAGE);

END TRY
BEGIN CATCH

    
  DECLARE @ERRMSG NVARCHAR(4000), @ERRSEVERITY INT
  SELECT @ERRMSG = ERROR_MESSAGE(),
         @ERRSEVERITY = ERROR_SEVERITY()
  RAISERROR(@ERRMSG, @ERRSEVERITY, 1)
  RETURN @ERRSEVERITY;
END CATCH


GO