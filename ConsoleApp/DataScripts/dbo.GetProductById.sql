CREATE PROCEDURE [dbo].[GetProductByID]
    @ProductID INT
AS
BEGIN
    SELECT * FROM [dbo].[Products] WHERE [ProductID] = @ProductID;
END;
