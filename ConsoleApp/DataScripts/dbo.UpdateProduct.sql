CREATE PROCEDURE [dbo].[UpdateProduct]
    @ProductID INT,
    @NewProductName NVARCHAR(255),
    @NewPrice DECIMAL(10, 2),
    @NewCategoryID INT,
    @NewSupplierID INT,
    @NewAvailableQuantity INT
AS
BEGIN
    UPDATE [dbo].[Products]
    SET [ProductName] = @NewProductName,
        [Price] = @NewPrice,
        [CategoryID] = @NewCategoryID,
        [SupplierID] = @NewSupplierID,
        [AvailableQuantity] = @NewAvailableQuantity
    WHERE [ProductID] = @ProductID;
END;
