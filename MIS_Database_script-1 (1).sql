CREATE DATABASE STUFOOD_DB;

use STUFOOD_DB;


CREATE TABLE Employee (
    EmployeeID VARCHAR(50) PRIMARY KEY,
    EmployeeName NVARCHAR(255),
    Phone NVARCHAR(15),
    Email NVARCHAR(255),
    Address NVARCHAR(255),
    Job NVARCHAR(255),
    Position NVARCHAR(255),
    Salary DECIMAL(10, 2)
);

INSERT INTO Employee (EmployeeID, EmployeeName, Phone, Email, Address, Job, Position, Salary)
VALUES 
    ('NV01', N'Quyền Anh', '0123456789', N'Quyenanh@gmail.com', N'456 Lý Tự Trọng, Quận 1, TP.HCM', N'Chuyên viên sản xuất', N'Nhân Viên', 50000.00),
    ('NV02', N'Vương Hoá', '0987654321', N'Vuonghoa@gmail.com', N'789 Lê Duẩn, Quận 1, TP.HCM', N'Nhân viên vận chuyển', N'Nhân Viên', 40000.00),
    ('NV03', N'Quỳnh Như', '0112233445', N'Quynhnhu@gmail.com', N'890 Bến Thành, Quận 1, TP.HCM', N'Quản lý chất lượng', N'Trưởng Nhóm', 70000.00),
    ('NV04', N'Thanh Hà', '0112222333', N'ThanhHa@gmail.com', N'234 Hải Bà Trưng, Quận 10, TP.HCM', N'Kế toán viên', N'Chuyên Viên', 70000.00);


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
('SC01', N'THCS TRẦN QUỐC TUẤN', '0989012345', 'thcstranquoctuan@example.com', N'16/1 Bế Văn Cấm, Quận 7', 600),
('SC02', N'THCS HUỲNH TẤN PHÁT', '0978901234', 'thcshuynhtanphat@example.com', N'28/16 HUỲNH TẤN PHÁT, Quận 7', 300),
('SC03', N'THCS NGUYỄN HỮU THỌ', '0967890123', 'thcsnguyenhuutho@example.com', N'55/8A ĐƯỜNG 22, Phường Tân Kiểng, Quận 7', 200),
('SC04', N'THCS NGUYỄN HIỀN', '0956789012', 'thcsnguyenhien@example.com', N'Số 1 Lý Phục Man, Quận 7', 400);



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
('PR001', N'Phở Bò', N'Bún bò truyền thống Việt Nam', N'Đồ ăn', 4.9, 50000, 50),
('PR002', N'Bánh Mì Thịt Nướng', N'Bánh mì thịt heo nướng với rau tươi', N'Đồ ăn', 4.7, 50000, 80),
('PR003', N'Bún Riêu', N'Bún riêu cua, Nước dùng vị cà chua', N'Đồ ăn', 4.8, 75000, 40),
('PR004', N'Gỏi Cuốn', N'Chả giò tươi tôm rau thơm và bún', N'Đồ ăn', 4.6, 5000, 60),
('PR005', N'Cơm Gà Hải Nam', N'Cơm gà kiểu Hải Nam', N'Đồ ăn', 4.5, 60000, 45),
('PR006', N'Bánh Xèo', N'Bánh xèo Việt Nam với tôm và giá đỗ', N'Đồ ăn', 4.4, 45000, 55),
('PR007', N'Bún Bò Huế', N'Đặc sản của xứ Huế', N'Đồ ăn', 4.8, 73000, 30);

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
    ('OR001', N'Đã đặt hàng', '2023-03-01', '2023-03-02', 100000, 'NV01', 'SC01'),
    ('OR002', N'Đã đặt hàng', '2023-03-05', '2023-03-06', 150000, 'NV04', 'SC03'),
    ('OR003', N'Đã đặt hàng', '2023-04-01', '2023-04-02', 75000, 'NV03', 'SC02'),
    ('OR004', N'Đã đặt hàng', '2023-04-03', '2023-04-04', 120000, 'NV02', 'SC04'),
    ('OR005', N'Đã đặt hàng', '2023-05-01', '2023-05-02', 20000, 'NV01', 'SC03'),
    ('OR006', N'Đã đặt hàng', '2023-06-05', '2023-06-06', 45000, 'NV03', 'SC01'),
    ('OR007', N'Đã đặt hàng', '2023-06-01', '2023-07-02', 140000, 'NV02', 'SC02'),
    ('OR008', N'Đã đặt hàng', '2023-07-01', '2023-07-02', 180000, 'NV01', 'SC04'),
    ('OR009', N'Đã đặt hàng', '2023-08-01', '2023-08-02', 90000, 'NV02', 'SC01'),
    ('OR010', N'Đã đặt hàng', '2023-08-15', '2023-08-16', 120000, 'NV03', 'SC02'),
    ('OR011', N'Đã đặt hàng', '2023-09-01', '2023-09-02', 80000, 'NV04', 'SC03'),
    ('OR012', N'Đã đặt hàng', '2023-09-15', '2023-09-16', 95000, 'NV01', 'SC04'),
    ('OR013', N'Đã đặt hàng', '2023-10-01', '2023-10-02', 60000, 'NV02', 'SC01'),
    ('OR014', N'Đã đặt hàng', '2023-10-15', '2023-10-16', 75000, 'NV03', 'SC02'),
    ('OR015', N'Đã đặt hàng', '2023-11-01', '2023-11-02', 110000, 'NV04', 'SC03');

    
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
    ('ING001', N'Ba rọi bò mỹ', N'Thịt bò', N'Thịt', N'Giữ lạnh', 200000, 10),
    ('ING002', N'Bún sợi to', N'Bún gạo', N'Bún', N'Giữ khô', 50000, 20),
    ('ING003', N'Nạc vai heo', N'Thịt heo', N'Thịt', N'Giữ lạnh', 180000, 15),
    ('ING004', N'Bánh Mì Pháp', N'Bánh mì', N'Bánh mì', N'Giữ khô', 10000, 50),
    ('ING005', N'Cua thịt', N'Cua tươi', N'Hải sản', N'Giữ lạnh', 250000, 8),
    ('ING006', N'Gạo ST25', N'Gạo nấu cơm', N'Gạo', N'Giữ khô', 105000, 30),
    ('ING007', N'Tôm thẻ', N'Tôm tươi', N'Hải sản', N'Giữ lạnh', 180000, 12);


CREATE TABLE IngredientPerProduct (
    IngredientPerProductID VARCHAR(50) PRIMARY KEY,
    ProductNumber VARCHAR(50),
    IngredientID VARCHAR(50),
    Quantity INT,
    Notes VARCHAR(255),
    FOREIGN KEY (ProductNumber) REFERENCES Product(ProductNumber),
    FOREIGN KEY (IngredientID) REFERENCES Ingredient(IngredientID)
);

INSERT INTO IngredientPerProduct (IngredientPerProductID, ProductNumber, IngredientID, Quantity, Notes)
VALUES
    ('IPP001', 'PR001', 'ING001', 1, N'Thịt bò tái'),
    ('IPP002', 'PR001', 'ING002', 1, N'Bún gạo'),
    ('IPP003', 'PR002', 'ING003', 1, N'Thịt heo nướng'),
    ('IPP004', 'PR002', 'ING004', 1, N'Bánh mì'),
    ('IPP005', 'PR003', 'ING001', 1, N'Thịt bò xay'),
    ('IPP006', 'PR003', 'ING002', 1, N'Bún gạo'),
    ('IPP007', 'PR004', 'ING003', 1, N'Thịt heo nướng'),
    ('IPP008', 'PR004', 'ING005', 1, N'Cua tươi'),
    ('IPP009', 'PR005', 'ING006', 1, N'Gạo nấu cơm'),
    ('IPP010', 'PR005', 'ING007', 1, N'Tôm thẻ'),
    ('IPP011', 'PR006', 'ING002', 1, N'Bún gạo'),
    ('IPP012', 'PR006', 'ING003', 1, N'Thịt heo nướng'),
    ('IPP013', 'PR007', 'ING001', 1, N'Thịt bò tái'),
    ('IPP014', 'PR007', 'ING002', 1, N'Bún gạo');

CREATE TABLE Suppliers (
    SupplierID VARCHAR(50) PRIMARY KEY,
    SupplierName NVARCHAR(255),
    Phone VARCHAR(15),
    Email VARCHAR(255),
    Address NVARCHAR(255),
    IngredientProvided NVARCHAR(255),
    Rate DECIMAL(5, 2)
);

Delete from Suppliers;

INSERT INTO Suppliers (SupplierID, SupplierName, Phone, Email, Address, IngredientProvided, Rate)
VALUES
    ('NCC001', N'Nhà cung cấp thịt bò', '0123456789', 'nccthitbo@example.com', N'24 Đường Hải Bà Trưng, Quận 10, TP.HCM', 'ING001', 4.5),
    ('NCC002', N'Nhà cung cấp bún gạo', '0987654321', 'nccbungao@example.com', N'9 Đường Nguyễn Huệ, Quận 5, TP.HCM', 'ING002', 4.2),
    ('NCC003', N'Nhà cung cấp thịt heo', '0112233445', 'nccthitheo@example.com', N'57 Đường Cách Mạng Tháng Tám, Quận Bình Thạnh, TP.HCM', 'ING003', 4.7),
    ('NCC004', N'Nhà cung cấp bánh mì', '0112222333', 'nccbanhmi@example.com', N'79 Đường Lê Duẩn, Quận 1, TP.HCM', 'ING004', 4.0),
    ('NCC005', N'Nhà cung cấp cua tươi', '0989012345', 'ncccuatuoi@example.com', N'46 Đường Lý Tự Trọng, Quận 1, TP.HCM', 'ING005', 4.8),
    ('NCC006', N'Nhà cung cấp gạo nấu cơm', '0978901234', 'nccgao@example.com', N'89 Đường Bến Thành, Quận 1, TP.HCM', 'ING006', 4.6),
    ('NCC007', N'Nhà cung cấp tôm tươi', '0967890123', 'ncctomtuoi@example.com', N'13 Đường Võ Văn Tần, Quận 3, TP.HCM', 'ING007', 4.9),
    ('NCC008', N'Nhà cung cấp thịt đông lạnh', '099645771', 'nccthitdong@example.com', N'16 Nguyễn Văn Linh, Quận 7, TP.HCM', 'ING001,ING002', 4.9);

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
    ('PU001', N'Đã mua', '2023-03-01', '2023-03-02', 50000, 'NCC001', 'NV01'),
    ('PU002', N'Đã mua', '2023-03-05', '2023-03-06', 75000, 'NCC002', 'NV04'),
    ('PU003', N'Đã mua', '2023-04-01', '2023-04-02', 40000, 'NCC003', 'NV03'),
    ('PU004', N'Đã mua', '2023-04-03', '2023-04-04', 60000, 'NCC004', 'NV02'),
    ('PU005', N'Đã mua', '2023-05-01', '2023-05-02', 30000, 'NCC005', 'NV01'),
    ('PU006', N'Đã mua', '2023-06-05', '2023-06-06', 45000, 'NCC006', 'NV03'),
    ('PU007', N'Đã mua', '2023-06-01', '2023-07-02', 70000, 'NCC007', 'NV02');

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

create table Manufacturing(
	ManufacturingID VARCHAR(50) PRIMARY KEY,
	ManufacturingDate DATE,
	ProductID VARCHAR(50),
	Quantity int,
	SupervisoryStaffID VARCHAR(50),
	FactoryStatus NVARCHAR(350),
	FOREIGN KEY(ProductID) REFERENCES Product(ProductNumber),
	FOREIGN KEY(SupervisoryStaffID) REFERENCES EMPLOYEE(EmployeeID)

);

insert into Manufacturing(ManufacturingID,ManufacturingDate,ProductID,SupervisoryStaffID,FactoryStatus)VALUES
	('M001','2023-03-01','PR001','NV01',N'Đã hoàn thành'),
	('M002','2023-03-01','PR002','NV01',N'Đã hoàn thành'),
	('M003','2023-06-01','PR002','NV01',N'Đã hoàn thành'),
	('M004','2023-06-01','PR001','NV01',N'Đã hoàn thành');
