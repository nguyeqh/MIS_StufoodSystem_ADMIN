CREATE DATABASE STUFOOD_DB;

use STUFOOD_DB;

CREATE TABLE Employee (
    EmployeeID VARCHAR(50) PRIMARY KEY,
    EmployeeName NVARCHAR(255),
    Phone VARCHAR(15),
    Email NVARCHAR(255),
    Address NVARCHAR(255),
    Job NVARCHAR(255),
    Position NVARCHAR(255),
    Salary DECIMAL(10, 2)
);

INSERT INTO Employee (EmployeeID, EmployeeName, Phone, Email, Address, Job, Position, Salary)
VALUES 
    ('NV01', 'Quyền Anh', '0123456789', 'Quyenanh@gmail.com', '456 Lý Tự Trọng, Quận 1, TP.HCM', 'Chuyên viên sản xuất', 'Nhân Viên', 50000.00),
    ('NV02', 'Vương Hoá', '0987654321', 'Vuonghoa@gmail.com', '789 Lê Duẩn, Quận 1, TP.HCM', 'Nhân viên vận chuyển', 'Nhân Viên', 40000.00),
    ('NV03', 'Quỳnh Như', '0112233445', 'Quynhnhu@gmail.com', '890 Bến Thành, Quận 1, TP.HCM', 'Quản lý chất lượng', 'Trưởng Nhóm', 70000.00),
    ('NV04', 'Thanh Hà', '0112222333', 'ThanhHa@gmail.com', '234 Hải Bà Trưng, Quận 10, TP.HCM', 'Kế toán viên', 'Chuyên Viên', 70000.00);


CREATE TABLE AffiliatedSchool (
    SchoolID VARCHAR(50) PRIMARY KEY,
    SchoolName NVARCHAR(255),
    Phone VARCHAR(15),
    Email NVARCHAR(255),
    Address NVARCHAR(255),
    NumberOfStudents INT
);

INSERT INTO AffiliatedSchool (SchoolID, SchoolName, Phone, Email, Address, NumberOfStudents)
VALUES
('SC01', 'THCS TRẦN QUỐC TUẤN', '0989012345', 'thcstranquoctuan@example.com', '16/1 Bế Văn Cấm, Quận 7', 600),
('SC02', 'THCS HUỲNH TẤN PHÁT', '0978901234', 'thcshuynhtanphat@example.com', '28/16 HUỲNH TẤN PHÁT, Quận 7', 300),
('SC03', 'THCS NGUYỄN HỮU THỌ', '0967890123', 'thcsnguyenhuutho@example.com', '55/8A ĐƯỜNG 22, Phường Tân Kiểng, Quận 7', 200),
('SC04', 'THCS NGUYỄN HIỀN', '0956789012', 'thcsnguyenhien@example.com', 'Số 1 Lý Phục Man, Quận 7', 400);


CREATE TABLE Product (
    ProductNumber VARCHAR(50) PRIMARY KEY,
    ProductName NVARCHAR(255),
    ProductDescription NVARCHAR(255),
    ProductCategory NVARCHAR(255),
    ProductRating DECIMAL(3, 2),
    Price DECIMAL(10, 2),
    QuantityAvailable INT
);

INSERT INTO Product (ProductNumber, ProductName, ProductDescription, ProductCategory, ProductRating, Price, QuantityAvailable)
VALUES
('PR001', 'Phở Bò', 'Bún bò truyền thống Việt Nam', 'Đồ ăn', 4.9, 50000, 50),
('PR002', 'Bánh Mì Thịt Nướng', 'Bánh mì thịt heo nướng với rau tươi', 'Đồ ăn', 4.7, 50000, 80),
('PR003', 'Bún Riêu', 'Bún riêu cua, Nước dùng vị cà chua', 'Đồ ăn', 4.8, 75000, 40),
('PR004', 'Gỏi Cuốn', 'Chả giò tươi tôm rau thơm và bún', 'Đồ ăn', 4.6, 5000, 60),
('PR005', 'Cơm Gà Hải Nam', 'Cơm gà kiểu Hải Nam', 'Đồ ăn', 4.5, 60000, 45),
('PR006', 'Bánh Xèo', 'Bánh xèo Việt Nam với tôm và giá đỗ', 'Đồ ăn', 4.4, 45000, 55),
('PR007', 'Bún Bò Huế', 'Đặc sản của xứ Huế', 'Đồ ăn', 4.8, 73000, 30);

CREATE TABLE Order1 (
    OrderNumber VARCHAR(50) PRIMARY KEY,
    OrderStatus NVARCHAR(255),
    DateOrdered DATE,
    DateReceived DATE,
    OrderTotal DECIMAL(10, 2),
    EmployeeID VARCHAR(50),
    SchoolID VARCHAR(50),
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID),
    FOREIGN KEY (SchoolID) REFERENCES AffiliatedSchool(SchoolID)
);

INSERT INTO Order1 (OrderNumber, OrderStatus, DateOrdered, DateReceived, OrderTotal, EmployeeID, SchoolID)
VALUES
    ('OR001', 'Đã đặt hàng', '2023-03-01', '2023-03-02', 100000, 'NV01', 'SC01'),
    ('OR002', 'Đã đặt hàng', '2023-03-05', '2023-03-06', 150000, 'NV04', 'SC03'),
    ('OR003', 'Đã đặt hàng', '2023-04-01', '2023-04-02', 75000, 'NV03', 'SC02'),
    ('OR004', 'Đã đặt hàng', '2023-04-03', '2023-04-04', 120000, 'NV02', 'SC04'),
    ('OR005', 'Đã đặt hàng', '2023-05-01', '2023-05-02', 20000, 'NV01', 'SC03'),
    ('OR006', 'Đã đặt hàng', '2023-06-05', '2023-06-06', 45000, 'NV03', 'SC01'),
    ('OR007', 'Đã đặt hàng', '2023-06-01', '2023-07-02', 140000, 'NV02', 'SC02'),
    ('OR008', 'Đã đặt hàng', '2023-07-01', '2023-07-02', 180000, 'NV01', 'SC04'),
    ('OR009', 'Đã đặt hàng', '2023-08-01', '2023-08-02', 90000, 'NV02', 'SC01'),
    ('OR010', 'Đã đặt hàng', '2023-08-15', '2023-08-16', 120000, 'NV03', 'SC02'),
    ('OR011', 'Đã đặt hàng', '2023-09-01', '2023-09-02', 80000, 'NV04', 'SC03'),
    ('OR012', 'Đã đặt hàng', '2023-09-15', '2023-09-16', 95000, 'NV01', 'SC04'),
    ('OR013', 'Đã đặt hàng', '2023-10-01', '2023-10-02', 60000, 'NV02', 'SC01'),
    ('OR014', 'Đã đặt hàng', '2023-10-15', '2023-10-16', 75000, 'NV03', 'SC02'),
    ('OR015', 'Đã đặt hàng', '2023-11-01', '2023-11-02', 110000, 'NV04', 'SC03');
    
CREATE TABLE OrderDetail (
    OrderDetailID VARCHAR(50) PRIMARY KEY,
    OrderNumber VARCHAR(50),
    ProductNumber VARCHAR(50),
    Quantity INT,
    FOREIGN KEY (OrderNumber) REFERENCES Order1(OrderNumber),
    FOREIGN KEY (ProductNumber) REFERENCES Product(ProductNumber)
);

INSERT INTO OrderDetail (OrderDetailID, OrderNumber, ProductNumber, Quantity)
VALUES
    ('OD001', 'OR001', 'PR001', 2),
    ('OD002', 'OR001', 'PR002', 3),
    ('OD003', 'OR002', 'PR003', 1),
    ('OD004', 'OR003', 'PR005', 2),
    ('OD005', 'OR004', 'PR004', 4),
    ('OD006', 'OR005', 'PR006', 1),
    ('OD007', 'OR005', 'PR007', 2),
    ('OD008', 'OR006', 'PR002', 2),
    ('OD009', 'OR007', 'PR003', 3),
    ('OD010', 'OR008', 'PR001', 1),
    ('OD011', 'OR009', 'PR002', 3),
    ('OD012', 'OR010', 'PR003', 2),
    ('OD013', 'OR011', 'PR004', 1),
    ('OD014', 'OR012', 'PR005', 2),
    ('OD015', 'OR013', 'PR006', 4);

CREATE TABLE SpecialOrderDetails (
    SpecialOrderNumber VARCHAR(50) PRIMARY KEY,
    OrderNumber VARCHAR(50),
    ProductNumber VARCHAR(50),
    Quantity INT,
    DiscountPercent DECIMAL(5, 2),
    FOREIGN KEY (OrderNumber) REFERENCES Order1(OrderNumber),
    FOREIGN KEY (ProductNumber) REFERENCES Product(ProductNumber)
);

INSERT INTO SpecialOrderDetails (SpecialOrderNumber, OrderNumber, ProductNumber, Quantity, DiscountPercent)
VALUES
    ('SOD001', 'OR001', 'PR001', 2, 10.00),
    ('SOD002', 'OR002', 'PR002', 3, 15.00),
    ('SOD003', 'OR003', 'PR003', 1, 5.00),
    ('SOD004', 'OR004', 'PR005', 2, 8.00),
    ('SOD005', 'OR005', 'PR004', 4, 12.00),
    ('SOD006', 'OR006', 'PR006', 1, 7.50),
    ('SOD007', 'OR007', 'PR007', 2, 10.00),
    ('SOD008', 'OR008', 'PR002', 2, 15.00),
    ('SOD009', 'OR009', 'PR003', 3, 5.00),
    ('SOD010', 'OR010', 'PR004', 1, 8.00),
    ('SOD011', 'OR011', 'PR005', 2, 12.00),
    ('SOD012', 'OR012', 'PR006', 4, 7.50),
    ('SOD013', 'OR013', 'PR007', 1, 10.00),
    ('SOD014', 'OR014', 'PR001', 2, 15.00),
    ('SOD015', 'OR015', 'PR002', 3, 5.00);


CREATE TABLE Ingredient (
    IngredientID VARCHAR(50) PRIMARY KEY,
    IngredientName NVARCHAR(255),
    IngredientDescription NVARCHAR(255),
    IngredientCategory NVARCHAR(255),
    IngredientPreservation NVARCHAR(255),
    Price DECIMAL(10, 2),
    QuantityAvailable INT
);

INSERT INTO Ingredient (IngredientID, IngredientName, IngredientDescription, IngredientCategory, IngredientPreservation, Price, QuantityAvailable)
VALUES
    ('ING001', 'Ba rọi bò mỹ', 'Thịt bò', 'Thịt', 'Giữ lạnh', 200000, 10),
    ('ING002', 'Bún sợi to', 'Bún gạo', 'Bún', 'Giữ khô', 50000, 20),
    ('ING003', 'Nạc vai heo ', 'Thịt heo', 'Thịt', 'Giữ lạnh', 180000, 15),
    ('ING004', 'Bánh Mì Pháp', 'Bánh mì', 'Bánh mì', 'Giữ khô', 10000, 50),
    ('ING005', 'Cua thịt', 'Cua tươi', 'Hải sản', 'Giữ lạnh', 250000, 8),
    ('ING006', 'Gạo ST25', 'Gạo nấu cơm', 'Gạo', 'Giữ khô', 105000, 30),
    ('ING007', 'Tôm thẻ', 'Tôm tươi', 'Hải sản', 'Giữ lạnh', 180000, 12);

CREATE TABLE IngredientPerProduct (
    IngredientPerProductID VARCHAR(50) PRIMARY KEY,
    ProductNumber VARCHAR(50),
    IngredientID VARCHAR(50),
    Quantity INT,
    Notes VARCHAR(255),
    FOREIGN KEY (ProductNumber) REFERENCES Product(ProductNumber),
    FOREIGN KEY (IngredientID) REFERENCES Ingredient(IngredientID)
);

INSERT INTO IngredientPerProduct (IngredientPerProductID, IngredientID, ProductNumber, Quantity, Notes)
VALUES
    ('IPP001', 'PR001', 'ING001', 1, 'Thịt bò tái'),
    ('IPP002', 'PR001', 'ING002', 1, 'Bún gạo'),
    ('IPP003', 'PR002', 'ING003', 1, 'Thịt heo nướng'),
    ('IPP004', 'PR002', 'ING004', 1, 'Bánh mì'),
    ('IPP005', 'PR003', 'ING001', 1, 'Thịt bò xay'),
    ('IPP006', 'PR003', 'ING002', 1, 'Bún gạo'),
    ('IPP007', 'PR004', 'ING003', 1, 'Thịt heo nướng'),
    ('IPP008', 'PR004', 'ING005', 1, 'Cua tươi'),
    ('IPP009', 'PR005', 'ING006', 1, 'Gạo nấu cơm'),
    ('IPP010', 'PR005', 'ING007', 1, 'Tôm thẻ'),
    ('IPP011', 'PR006', 'ING002', 1, 'Bún gạo'),
    ('IPP012', 'PR006', 'ING003', 1, 'Thịt heo nướng'),
    ('IPP013', 'PR007', 'ING001', 1, 'Thịt bò tái'),
    ('IPP014', 'PR007', 'ING002', 1, 'Bún gạo');

CREATE TABLE Suppliers (
    SupplierID VARCHAR(50) PRIMARY KEY,
    SupplierName NVARCHAR(255),
    Phone VARCHAR(15),
    Email VARCHAR(255),
    Address NVARCHAR(255),
    IngredientProvided NVARCHAR(255),
    Rate DECIMAL(5, 2)
);

INSERT INTO Suppliers (SupplierID, SupplierName, Phone, Email, Address, IngredientProvided, Rate)
VALUES
    ('NCC001', 'Nhà cung cấp thịt bò', '0123456789', 'nccthitbo@example.com', '24 Đường Hải Bà Trưng, Quận 10, TP.HCM', 'Thịt bò', 4.5),
    ('NCC002', 'Nhà cung cấp bún gạo', '0987654321', 'nccbungao@example.com', '9 Đường Nguyễn Huệ, Quận 5, TP.HCM', 'Bún gạo', 4.2),
    ('NCC003', 'Nhà cung cấp thịt heo', '0112233445', 'nccthitheo@example.com', '57 Đường Cách Mạng Tháng Tám, Quận Bình Thạnh, TP.HCM', 'Thịt heo', 4.7),
    ('NCC004', 'Nhà cung cấp bánh mì', '0112222333', 'nccbanhmi@example.com', '79 Đường Lê Duẩn, Quận 1, TP.HCM', 'Bánh mì', 4.0),
    ('NCC005', 'Nhà cung cấp cua tươi', '0989012345', 'ncccuatuoi@example.com', '46 Đường Lý Tự Trọng, Quận 1, TP.HCM', 'Cua tươi', 4.8),
    ('NCC006', 'Nhà cung cấp gạo nấu cơm', '0978901234', 'nccgao@example.com', '89 Đường Bến Thành, Quận 1, TP.HCM', 'Gạo nấu cơm', 4.6),
    ('NCC007', 'Nhà cung cấp tôm tươi', '0967890123', 'ncctomtuoi@example.com', '13 Đường Võ Văn Tần, Quận 3, TP.HCM', 'Tôm tươi', 4.9);

CREATE TABLE Purchase (
    PurchaseNumber VARCHAR(50) PRIMARY KEY,
    PurchaseStatus NVARCHAR(255),
    DatePurchase DATE,
    DateReceived DATE,
    PurchaseTotal DECIMAL(10, 2),
    SupplierID VARCHAR(50),
    EmployeeID VARCHAR(50),
    FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID),
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID)
);

INSERT INTO Purchase (PurchaseNumber, PurchaseStatus, DatePurchase, DateReceived, PurchaseTotal, SupplierID, EmployeeID)
VALUES
    ('PU001', 'Đã mua', '2023-03-01', '2023-03-02', 50000, 1, 'NV01'),
    ('PU002', 'Đã mua', '2023-03-05', '2023-03-06', 75000, 2, 'NV04'),
    ('PU003', 'Đã mua', '2023-04-01', '2023-04-02', 40000, 3, 'NV03'),
    ('PU004', 'Đã mua', '2023-04-03', '2023-04-04', 60000, 4, 'NV02'),
    ('PU005', 'Đã mua', '2023-05-01', '2023-05-02', 30000, 5, 'NV01'),
    ('PU006', 'Đã mua', '2023-06-05', '2023-06-06', 45000, 6, 'NV03'),
    ('PU007', 'Đã mua', '2023-06-01', '2023-07-02', 70000, 7, 'NV02');

CREATE TABLE PurchaseDetail (
    PurchaseDetailNumber VARCHAR(50) PRIMARY KEY,
    IngredientID VARCHAR(50),
    PurchaseNumber VARCHAR(50),
	Quantity int,
    FOREIGN KEY (IngredientID) REFERENCES Ingredient(IngredientID),
    FOREIGN KEY (PurchaseNumber) REFERENCES Purchase(PurchaseNumber)
);

INSERT INTO PurchaseDetail (PurchaseDetailNumber, IngredientID, PurchaseNumber, Quantity)
VALUES
    ('PD001', 'ING001', 'PU001',10),
    ('PD002', 'ING002', 'PU001',8),
    ('PD003', 'ING003', 'PU002',10),
    ('PD004', 'ING004', 'PU003', 9),
    ('PD005', 'ING005', 'PU004',15),
    ('PD006', 'ING006', 'PU005',12),
    ('PD007', 'ING007', 'PU006',7),
    ('PD008', 'ING001', 'PU007',1);

