USE Innowacja;

-- Usuniêcie tabel, jeœli istniej¹
DROP TABLE IF EXISTS ProductShortages;
DROP TABLE IF EXISTS Shelf;
DROP TABLE IF EXISTS Department;

-- Tworzenie tabeli Department
CREATE TABLE Department (
    DepartmentId INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(255) NOT NULL
);

-- Tworzenie tabeli Shelf
CREATE TABLE Shelf (
    shopShelfId INT PRIMARY KEY IDENTITY(1,1),
    DepartmentId INT NOT NULL,
    ShelfUnit INT NOT NULL,
    FOREIGN KEY (DepartmentId) REFERENCES Department(DepartmentId)
);

-- Tworzenie tabeli ProductShortages
CREATE TABLE ProductShortages (
    ShortageId INT PRIMARY KEY IDENTITY(1,1),
    shopShelfId INT NOT NULL,
    ProductName NVARCHAR(255),
    ProductNumber INT,
	ShelfNumber INT,
    Xmin FLOAT,
    Xmax FLOAT,
    Ymin FLOAT,
    Ymax FLOAT,
    FilePath NVARCHAR(255),
    FOREIGN KEY (shopShelfId) REFERENCES Shelf(shopShelfId)
);

