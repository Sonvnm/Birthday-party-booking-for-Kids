CREATE database BirthdayPartyBookingForKids_DB
Use database BirthdayPartyBookingForKids_DB
drop database BirthdayPartyBookingForKids_DB
drop table Role

CREATE TABLE Role (
    RoleID nvarchar(20) PRIMARY KEY,
    RoleName nvarchar(50)
);
CREATE TABLE [User] (
    [UserID] int identity(1,1) PRIMARY KEY,
    UserName nvarchar(100),
    Email nvarchar(100),
    Password nvarchar(50),
	BirthDate date,
    Phone nvarchar(11),
	RoleID nvarchar(20),
    FOREIGN KEY (RoleID) REFERENCES [Role] (RoleID)
	 
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
    UserID int,
	FOREIGN KEY (UserID) REFERENCES [User](UserID),
    ParticipateAmount int,
    TotalPrice float,
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