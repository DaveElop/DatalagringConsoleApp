-- Drop Customers table if it exists
IF OBJECT_ID('dbo.Customers', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Customers;
END

-- Create Customers table
CREATE TABLE dbo.Customers
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(20)
);

-- Drop Products table if it exists
IF OBJECT_ID('dbo.Products', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Products;
END

-- Create Products table
CREATE TABLE dbo.Products
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(18, 2) NOT NULL,
    CreatedAt DATETIME2 NOT NULL
);

-- Drop Orders table if it exists
IF OBJECT_ID('dbo.Orders', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Orders;
END

-- Create Orders table
CREATE TABLE dbo.Orders
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    CustomerId INT FOREIGN KEY REFERENCES dbo.Customers(Id),
    OrderDate DATETIME2 DEFAULT GETDATE(),
    TotalAmount DECIMAL(18, 2) NOT NULL
);

-- Create OrderItems table
CREATE TABLE dbo.OrderItems
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT FOREIGN KEY REFERENCES dbo.Orders(Id),
    ProductId INT FOREIGN KEY REFERENCES dbo.Products(Id),
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18, 2) NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL
);

-- Create Payments table
CREATE TABLE dbo.Payments
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT FOREIGN KEY REFERENCES dbo.Orders(Id),
    PaymentDate DATETIME2 DEFAULT GETDATE(),
    Amount DECIMAL(18, 2) NOT NULL
);
