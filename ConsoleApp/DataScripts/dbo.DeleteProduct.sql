CREATE PROCEDURE [dbo].[DeleteProduct]
    @ProductID INT
AS
BEGIN
    DELETE FROM [dbo].[Products] WHERE [ProductID] = @ProductID;
END;
