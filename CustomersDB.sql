CREATE TABLE Customers (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255),
    PhoneNumber NVARCHAR(20)
);

CREATE TABLE Addresses (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Street NVARCHAR(255),
    City NVARCHAR(255),
    ZipCode NVARCHAR(10),
    CustomerId INT FOREIGN KEY REFERENCES Customers(Id)
);

CREATE TABLE Orders (
    Id INT PRIMARY KEY IDENTITY(1,1),
    OrderDate DATETIME,
    CustomerId INT FOREIGN KEY REFERENCES Customers(Id)
);

CREATE TABLE OrderItems (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(255),
    Price DECIMAL(18, 2),
    Quantity INT,
    OrderId INT FOREIGN KEY REFERENCES Orders(Id)
);

CREATE TABLE Payments (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Amount DECIMAL(18, 2),
    PaymentDate DATETIME,
    OrderId INT FOREIGN KEY REFERENCES Orders(Id)
);