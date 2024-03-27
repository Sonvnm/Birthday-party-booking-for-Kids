CREATE database BirthdayPartyBookingForKids_DB
drop database BirthdayPartyBookingForKids_DB
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
('2', 'Customer');

INSERT INTO [User] (UserID, UserName, Email, Password, BirthDate, Phone, RoleID) 
VALUES 
('U001', 'admin', 'admin@example.com', 'admin123', '1990-01-01', '1234567890', '1'),
('U002', 'huy', 'huy@example.com', 'huy123', '2000-05-15', '0987654321', '2'),
('U003', 'jane_smith', 'jane@example.com', 'jane123', '1995-09-20', '0123456789', '2'),
('U004', 'mike_wilson', 'mike@example.com', 'mike123', '1988-11-12', '9876543210', '2'),
('U005', 'sara_jones', 'sara@example.com', 'sara123', '2003-03-25', '1230987654', '2'),
('U006', 'david_brown', 'david@example.com', 'david123', '1998-07-08', '4567890123', '2'),
('U007', 'emily_white', 'emily@example.com', 'emily123', '2001-12-30', '7890123456', '2'),
('U008', 'alexander_clark', 'alexander@example.com', 'alex123', '1993-04-17', '2345678901', '2'),
('U009', 'olivia_martin', 'olivia@example.com', 'olivia123', '1997-10-10', '8901234567', '2'),
('U010', 'william_taylor', 'william@example.com', 'william123', '1992-06-05', '5678901234', '2');


INSERT INTO Decoration (ItemID, ItemName, Description, Price) 
VALUES 
('D001', 'Enchanted Fairy Lights', 'Twinkling lights to create a magical atmosphere.', 20.00),
('D002', 'Pirate Ship Cutout', 'A life-sized pirate ship for an adventurous backdrop.', 30.00),
('D003', 'Princess Tiaras', 'Sparkling tiaras for every little princess.', 15.00),
('D004', 'Superhero Capes', 'Colorful capes to transform into mighty heroes.', 25.00),
('D005', 'Jungle Vine Decorations', 'Lush jungle vines to bring the safari indoors.', 18.00),
('D006', 'Underwater Backdrop', 'An underwater scene complete with fish and coral.', 35.00),
('D007', 'Space Posters', 'Posters of planets and stars to decorate the room.', 28.00),
('D008', 'Candy Buffet Table', 'A table filled with assorted candies and treats.', 40.00),
('D009', 'Dinosaur Standees', 'Life-sized dinosaur standees to bring the prehistoric era alive.', 32.00),
('D010', 'Magic Trick Set', 'A set of magic tricks to entertain guests.', 45.00);


INSERT INTO Room (LocationID, LocationName, Description, Price) 
VALUES 
('R001', 'Enchanted Garden', 'A magical garden filled with fairy lights and colorful decorations.', 150.00),
('R002', 'Pirate Cove', 'A swashbuckling adventure awaits in this pirate-themed room.', 180.00),
('R003', 'Princess Palace', 'A royal palace fit for a little princess with tiaras and sparkles everywhere.', 200.00),
('R004', 'Superhero Hideout', 'Transform into superheroes in this action-packed room.', 170.00),
('R005', 'Jungle Safari', 'Embark on a wild safari adventure in this jungle-themed room.', 160.00),
('R006', 'Underwater World', 'Dive deep into the ocean with this underwater-themed room.', 190.00),
('R007', 'Space Odyssey', 'Explore the wonders of outer space in this cosmic room.', 220.00),
('R008', 'Candy Wonderland', 'Indulge in a sweet paradise filled with candies and treats.', 240.00),
('R009', 'Dinosaur Den', 'Roar into prehistoric times with this dinosaur-themed room.', 210.00),
('R010', 'Magician Lair', 'Be amazed by magic tricks and illusions in this mysterious room.', 230.00);



INSERT INTO Menu (FoodID, FoodName, Description, Price) 
VALUES
('F001', 'Pizza Party', 'Delicious assorted pizzas for everyone to enjoy', 80.00),
('F002', 'Burger Bonanza', 'Juicy burgers w all the fixings', 70.00),
('F003', 'Tasty Tacos', 'Tacos filled w flavorful ingredients', 60.00),
('F004', 'Fairy Bread', 'Colorful slices of bread topped with sprinkles and magic.', 40.00),
('F005', 'Pirate Plunder', 'Assorted snacks fit for hungry pirates.', 50.00),
('F006', 'Princess Pastries', 'Elegant pastries fit for royalty.', 90.00),
('F007', 'Superhero Snacks', 'Power-packed snacks to fuel superhero adventures.', 75.00),
('F008', 'Jungle Jamboree', 'Fresh fruits and jungle-inspired snacks.', 65.00),
('F009', 'Undersea Delights', 'Seafood-inspired treats from under the waves.', 85.00),
('F010', 'Space Sustenance', 'Cosmic snacks for intergalactic explorers.', 100.00);



INSERT INTO Service (ServiceID, ServiceName, Description, FoodID, ItemID, TotalPrice) 
VALUES 
('S001', 'Fairy Tale Feast', 'A magical dining experience with fairy tale-themed food.', 'F001', 'D001', 230.00),
('S002', 'Pirate Party Pack', 'An adventurous feast for little pirates.', 'F002', 'D002', 250.00),
('S003', 'Princess Tea Party', 'An elegant tea party fit for little princesses.', 'F003', 'D003', 210.00),
('S004', 'Superhero Buffet', 'A buffet spread worthy of mighty superheroes.', 'F004', 'D004', 190.00),
('S005', 'Jungle Safari Snack Pack', 'Snacks to keep jungle explorers energized.', 'F005', 'D005', 180.00),
('S006', 'Underwater Adventure Delight', 'Delicious seafood treats for underwater adventurers.', 'F006', 'D006', 240.00),
('S007', 'Galactic Gourmet Experience', 'Out-of-this-world dining for space voyagers.', 'F007', 'D007', 220.00),
('S008', 'Candy Wonderland Extravaganza', 'A sweet feast in a candy-filled wonderland.', 'F008', 'D008', 270.00),
('S009', 'Dino-mite Dining Adventure', 'A roaring feast for little dinosaur enthusiasts.', 'F009', 'D009', 260.00),
('S010', 'Magician Mystery Meal', 'A magical dining experience filled with illusions and surprises.', 'F010', 'D010', 280.00);



INSERT INTO PaymentType (PaymentTypeID, PaymentTypeName) 
VALUES 
('PT001', 'Credit Card'),
('PT002', 'Debit Card'),
('PT003', 'PayPal'),
('PT004', 'Bank Transfer');



INSERT INTO Booking (BookingID, UserID, ParticipateAmount, TotalPrice, DateBooking, LocationID, ServiceID, KidBirthDay, KidName, KidGender, time, status) 
VALUES 
('B001', 'U002', 12, 300.00, '2024-04-15', 'R001', 'S001', '2019-05-15', 'Emma', 'Female', '10:00 AM', 1),
('B002', 'U003', 15, 350.00, '2024-05-20', 'R002', 'S002', '2020-08-20', 'Liam', 'Male', '11:30 AM', 1),
('B003', 'U004', 10, 280.00, '2024-06-10', 'R003', 'S003', '2021-03-12', 'Olivia', 'Female', '02:00 PM', 1),
('B004', 'U005', 8, 240.00, '2024-07-05', 'R004', 'S004', '2022-01-10', 'Noah', 'Male', '03:30 PM', 1),
('B005', 'U006', 20, 400.00, '2024-08-12', 'R005', 'S005', '2020-12-05', 'Sophia', 'Female', '09:00 AM', 1),
('B006', 'U007', 18, 380.00, '2024-09-18', 'R006', 'S006', '2021-07-20', 'Mason', 'Male', '10:45 AM', 1),
('B007', 'U008', 14, 320.00, '2024-10-22', 'R007', 'S007', '2022-02-18', 'Isabella', 'Female', '12:15 PM', 1),
('B008', 'U009', 16, 360.00, '2024-11-30', 'R008', 'S008', '2021-10-30', 'Ethan', 'Male', '01:45 PM', 1),
('B009', 'U010', 22, 420.00, '2024-12-25', 'R009', 'S009', '2020-05-25', 'Ava', 'Female', '04:00 PM', 1),
('B010', 'U002', 25, 450.00, '2025-01-05', 'R010', 'S010', '2023-02-28', 'William', 'Male', '06:30 PM', 1);



INSERT INTO Payment (PaymentID, BankName, BankID, MoneyReceiver, PaymentTypeID, Amount, BookingID) 
VALUES 
('P001', 'ABC Bank', '1234567890', 'PartyPlanner Inc.', 'PT001', 300, 'B001'),
('P002', 'XYZ Bank', '9876543210', 'PartyPlanner Inc.', 'PT002', 350, 'B002'),
('P003', 'DEF Bank', '4567890123', 'PartyPlanner Inc.', 'PT003', 280, 'B003'),
('P004', 'GHI Bank', '0123456789', 'PartyPlanner Inc.', 'PT004', 240, 'B004'),
('P005', 'JKL Bank', '2345678901', 'PartyPlanner Inc.', 'PT001', 400, 'B005'),
('P006', 'MNO Bank', '7890123456', 'PartyPlanner Inc.', 'PT002', 380, 'B006'),
('P007', 'PQR Bank', '8901234567', 'PartyPlanner Inc.', 'PT003', 320, 'B007'),
('P008', 'STU Bank', '5678901234', 'PartyPlanner Inc.', 'PT004', 360, 'B008'),
('P009', 'VWX Bank', '3456789012', 'PartyPlanner Inc.', 'PT001', 420, 'B009'),
('P010', 'YZ Bank', '9012345678', 'PartyPlanner Inc.', 'PT002', 450, 'B010');
;

