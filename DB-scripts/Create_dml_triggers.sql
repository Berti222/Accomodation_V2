PRINT 'Trieggers creation started...'

USE Accomodation;

CREATE TRIGGER trg_Room_NumberOfRoom ON dbo.Room
AFTER INSERT, UPDATE, DELETE
AS
	SET NOCOUNT ON;
	DECLARE @IdBefore int =  (SELECT RoomTypeId from deleted)
	DECLARE @IdAfter int = (SELECT RoomTypeId from inserted)

	--PRINT @IdBefore
	--print @IdAfter
	
	IF @IdBefore IS NOT NULL
		UPDATE dbo.RoomType
		SET NumberOfRoom = NumberOfRoom - 1
		WHERE id = @IdBefore

	IF @IdAfter IS NOT NULL
		UPDATE dbo.RoomType
		SET NumberOfRoom = NumberOfRoom + 1
		WHERE id = @IdAfter
GO

PRINT 'Trieggers creation were successfull'