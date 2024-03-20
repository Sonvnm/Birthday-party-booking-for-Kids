CREATE database BirthdayPartyBookingForKids_DB
Use BirthdayPartyBookingForKids_DB
drop database BirthdayPartyBookingForKids_DB
drop table Role

CREATE TABLE Role (
    RoleID nvarchar(20) PRIMARY KEY,
    RoleName nvarchar(50)
);
CREATE TABLE [User] (
    [UserID] nvarchar(100) PRIMARY KEY,
    UserName nvarchar(100),
    Email nvarchar(100),
    Password nvarchar(50),
	BirthDate date,
    Phone nvarchar(11),
    RoleID nvarchar(20) REFERENCES [Role] (RoleID)
	
);

CREATE TABLE Decoration (
    ItemID nvarchar(100) PRIMARY KEY,
    ItemName nvarchar(100),
    Description nvarchar(100),
    Price float,
    
);
CREATE TABLE Room (
/*location -> room*/
    LocationID nvarchar(100) PRIMARY KEY,
    LocationName nvarchar(100),
    Description nvarchar(100),
    Price float,
);

CREATE TABLE Menu (
    FoodID nvarchar(100) PRIMARY KEY,
    FoodName nvarchar(100),
    Description nvarchar(100),
    Price float,
);
/*them tolaprice*/
CREATE TABLE Service (
    ServiceID nvarchar(100) PRIMARY KEY,
    ServiceName nvarchar(100),
    Description nvarchar(100),
	FoodID nvarchar(100) REFERENCES Menu(FoodID),
	ItemID nvarchar(100) REFERENCES Decoration(ItemID),
	TotalPrice float,
);
drop TABLE Service

CREATE TABLE PaymentType (
    PaymentTypeID nvarchar(100) PRIMARY KEY,
    PaymentTypeName nvarchar(50),
);
CREATE TABLE Booking (
/*booking hợp với bookingdetail*/
    BookingID nvarchar(100) PRIMARY KEY,
    UserID nvarchar(100),
    ParticipateAmount int,
    TotalPrice float,
    FOREIGN KEY (UserID) REFERENCES [User](UserID),
    DateBooking date,
    LocationID nvarchar(100) REFERENCES Room(LocationID),
	ServiceID nvarchar(100) REFERENCES Service(ServiceID),
	KidBirthDay date,
	KidName nvarchar(100),
	KidGender nvarchar (10),
	time nvarchar(100),
	status int
);
drop TABLE Booking
CREATE TABLE Payment (
    PaymentID nvarchar(100) PRIMARY KEY,
    BankName nvarchar(100),
    BankID nvarchar(100),
    MoneyReceiver nvarchar(100),
    PaymentTypeID nvarchar(100),
    Amount int,
    FOREIGN KEY (PaymentTypeID) REFERENCES PaymentType(PaymentTypeID),
	BookingID nvarchar(100) REFERENCES Booking(BookingID),
);
drop table payment


/*CREATE TABLE BookingDetail (
    BookingDetailID nvarchar(100) PRIMARY KEY,
    Date date,
    BookingID nvarchar(100) REFERENCES Booking(BookingID),
    ServiceID nvarchar(100) REFERENCES Service(ServiceID),
	PaymentID nvarchar(100) REFERENCES Payment(PaymentID)
	
);*/

/*bỏ CREATE TABLE Deposit (
    BankName nvarchar(100),
    BankID nvarchar(100),
    MoneyReceiver nvarchar(100),
    PaymentType nvarchar(100),
    Amount int
);*/
INSERT INTO Role (RoleID, RoleName) VALUES
('1', 'Admin'),
('2', 'User');

INSERT INTO [User] (UserID, UserName, Email, Password, BirthDate, Phone, RoleID) VALUES
('U001', 'JohnDoe', 'john@example.com', 'password123', '1990-05-15', '1234567890', '1'),
('U002', 'JaneSmith', 'jane@example.com', 'password456', '1985-09-20', '0987654321', '2'),
('U003', 'AdminUser', 'admin@example.com', 'adminpass', '1980-01-01', '9876543210', '2'),
('U004', 'ManagerUser', 'manager@example.com', 'managerpass', '1975-06-10', '0123456789', '2'),
('U005', 'GuestUser', 'guest@example.com', 'guestpass', '2000-12-25', '1112223334', '2');

INSERT INTO Decoration (ItemID, ItemName, Description, Price) VALUES
('D001', 'Fairy Lights', 'String lights for decoration', 20.50),
('D002', 'Balloons', 'Colorful balloons for parties', 15.99),
('D003', 'Table Centerpiece', 'Elegant centerpiece for tables', 30.75),
('D004', 'Wall Art', 'Artistic decorations for walls', 45.25),
('D005', 'Party Banner', 'Customizable banners for celebrations', 10.00);

INSERT INTO Room (LocationID, LocationName, Description, Price) VALUES
('L001', 'Banquet Hall', 'Spacious hall for events', 500.00),
('L002', 'Conference Room', 'Professional setup for meetings', 200.00),
('L003', 'Party Room', 'Colorful setup for parties', 300.00),
('L004', 'VIP Lounge', 'Exclusive lounge area', 1000.00),
('L005', 'Garden Area', 'Outdoor space for gatherings', 400.00);


INSERT INTO Menu (FoodID, FoodName, Description, Price) VALUES
('F001', 'Steak Dinner', 'Juicy steak with sides', 25.99),
('F002', 'Vegetarian Pasta', 'Pasta with assorted veggies', 18.50),
('F003', 'Seafood Platter', 'Assortment of seafood delicacies', 35.75),
('F004', 'Chicken Caesar Salad', 'Classic salad with grilled chicken', 15.25),
('F005', 'Dessert Sampler', 'Variety of sweet treats', 12.00);


INSERT INTO Service (ServiceID, ServiceName, Description, FoodID, ItemID, TotalPrice) VALUES
('S001', 'Event Catering', 'Professional catering service', 'F001', 'D001', 150.49),
('S002', 'Party Decoration', 'Decor setup for parties', NULL, 'D002', 45.99),
('S003', 'Corporate Event Package', 'All-in-one package for corporate events', 'F002', 'D003', 250.25),
('S004', 'Wedding Package', 'Special package for weddings', 'F003', 'D004', 600.50),
('S005', 'Birthday Party Package', 'Package deal for birthday parties', 'F004', NULL, 120.75);


INSERT INTO PaymentType (PaymentTypeID, PaymentTypeName) VALUES
('P001', 'Credit Card'),
('P002', 'Debit Card'),
('P003', 'Cash'),
('P004', 'Bank Transfer'),
('P005', 'PayPal');


INSERT INTO Booking (BookingID, UserID, ParticipateAmount, TotalPrice, DateBooking, LocationID, ServiceID, KidBirthDay, KidName, KidGender, time, status) VALUES
('B001', 'U001', 50, 1500.00, '2024-05-10', 'L001', 'S001', NULL, NULL, NULL, '10:00 AM', 1),
('B002', 'U002', 30, 1000.00, '2024-06-20', 'L003', 'S002', '2010-08-15', 'Emma Smith', 'Female', '02:00 PM', 1),
('B003', 'U003', 10, 600.00, '2024-07-15', 'L004', 'S004', NULL, NULL, NULL, '12:00 PM', 1),
('B004', 'U004', 20, 800.00, '2024-08-05', 'L002', 'S003', NULL, NULL, NULL, '09:00 AM', 1),
('B005', 'U005', 5, 200.00, '2024-09-10', 'L005', 'S005', '2019-12-25', 'Noah Brown', 'Male', '04:00 PM', 1);


INSERT INTO Payment (PaymentID, BankName, BankID, MoneyReceiver, PaymentTypeID, Amount, BookingID) VALUES
('PAY001', 'ABC Bank', '123456', 'John Doe', 'P001', 1500, 'B001'),
('PAY002', 'XYZ Bank', '789012', 'Jane Smith', 'P002', 1000, 'B002'),
('PAY003', 'PQR Bank', '345678', 'Admin User', 'P003', 600, 'B003'),
('PAY004', 'LMN Bank', '901234', 'Manager User', 'P004', 800, 'B004'),
('PAY005', 'DEF Bank', '567890', 'Guest User', 'P005', 200, 'B005');

