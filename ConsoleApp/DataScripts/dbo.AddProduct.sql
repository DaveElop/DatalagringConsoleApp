CREATE PROCEDURE [dbo].[InsertProduct]
    @ProductName NVARCHAR(MAX),
    @Price DECIMAL(18, 2),
    @AvailableQuantity INT,
    @SupplierID INT
AS
BEGIN
    INSERT INTO [dbo].[Products] ([ProductName], [Price], [AvailableQuantity], [SupplierID])
    VALUES (@ProductName, @Price, @AvailableQuantity, @SupplierID);
END;
