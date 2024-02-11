ALTER PROCEDURE [dbo].[UpdateProduct]
    @ProductID INT,
    @ProductName NVARCHAR(255),
    @Price DECIMAL(10, 2),
    @CategoryID INT,
    @SupplierID INT,
    @AvailableQuantity INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Products]
    SET
        [ProductName] = @ProductName,
        [Price] = @Price,
        [CategoryID] = @CategoryID,
        [SupplierID] = @SupplierID,
        [AvailableQuantity] = @AvailableQuantity
    WHERE
        [ProductID] = @ProductID;
END;
