USE [master]
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'Accomodation')
	DROP DATABASE Accomodation
GO

PRINT 'Creating Database...'
CREATE DATABASE Accomodation
GO

PRINT 'Database created successfully!'
USE Accomodation
GO

PRINT 'Creating tables...'

CREATE TABLE RoomType
(
	Id int NOT NULL IDENTITY(1,1),
	Name nvarchar(255) NULL,
	Size int NULL,
	NumberOfBed int NULL,
	Discription nvarchar(2000) NULL,
	[Type] varchar(100) NULL,
	ImageUrl varchar(255) NULL,
	Capacity tinyint NULL,
	NumberOfRoom smallint NULL CONSTRAINT df_NumberOfRoom_SetZeroAtCreate DEFAULT 0,

	CONSTRAINT PK_RoomType PRIMARY KEY(ID)
)

CREATE TABLE RoomPrice
(
	Id int NOT NULL IDENTITY(1,1),
	Name varchar(100) NULL,
	Price money NULL,
	CreatedOn datetime2 NULL CONSTRAINT df_RoomPrice_CreatedDate DEFAULT GETDATE(),
	ExtraBedPrice money NULL,
	PeriodStrart datetime2 NUll,
	PeriodEnd datetime2 NULL,
	RoomTypeId int NULL,

	CONSTRAINT PK_RoomPrice PRIMARY KEY (ID),
	CONSTRAINT FK_RoomPrice_RoomType FOREIGN KEY(RoomTypeId)
		REFERENCES RoomType (Id),
	CONSTRAINT chk_RoomPrice_PriceIsGreaterThanZero CHECK(Price >= 0)
)

CREATE TABLE Room
(
	Id int NOT NULL IDENTITY(1, 1),
	Direction varchar(100) NULL,
	RoomNumber smallint NULL,
	RoomTypeId int NULL,

	CONSTRAINT PK_Room PRIMARY KEY(ID),
	CONSTRAINT FK_Room_RoomType FOREIGN KEY(RoomTypeId)
		REFERENCES RoomType (Id)
)

CREATE TABLE [Service]
(
	Id int NOT NULL IDENTITY(1,1),
	Name varchar(255) NULL,
	Price money NULL,
	TypeOfPayment varchar(100),
	Discription nvarchar(2000),

	CONSTRAINT PK_Service PRIMARY KEY (ID)
)

CREATE TABLE Food
(
	Id int NOT NULL IDENTITY(1,1),
	FoodType varchar(100) NULL,
	Name varchar(255) NULL,
	Description nvarchar(1000) NULL,

	CONSTRAINT PK_Food PRIMARY KEY (ID)
)

CREATE TABLE Allergenic
(
	Id int NOT NULL IDENTITY(1,1),
	Name varchar(100) NULL

	CONSTRAINT PK_Allergenic PRIMARY KEY (ID)
)

CREATE TABLE Reservation
(
	Id int NOT NULL IDENTITY(1,1),
	DateOfArrive datetime2 NULL,
	DateOfDeparture datetime2 NULL,
	CreatedOn datetime2 NULL CONSTRAINT df_Reservation_CreatedDate DEFAULT GETDATE(),
	Price money NULL,
	TotalPrice money NULL,
	Discount decimal(5,2),
	Type varchar(100) NULL,
	NumberOfAdoult smallint NULL,
	NumberOfChild smallint NULL,

	CONSTRAINT PK_Reservation PRIMARY KEY (ID),
	CONSTRAINT chk_Discount_min_max_value CHECK (Discount >= 0.00 and Discount <= 100.00),
	CONSTRAINT chk_Reservation_TotalpriceIsCorrect CHECK((TotalPrice >= 0 AND Type <> 'storno') or (TotalPrice <= 0 AND Type = 'storno')),
	CONSTRAINT chk_Reservation_PriceIsCorrect CHECK((Price >= 0 AND Type <> 'storno') or (Price <= 0 AND Type = 'storno'))
	--CONSTRAINT FK_Room
)

CREATE TABLE Guest
(
	Id int NOT NULL IDENTITY(1,1),
	FullName varchar(255) NULL,
	IdentityNumber varchar(50) NULL,
	Email varchar(255) NULL,
	PhoneNumer varchar(50) NULL,
	BirthDate date NULL,

	CONSTRAINT PK_Guest PRIMARY KEY (ID)
)

CREATE TABLE Meal
(
	Id int NOT NULL IDENTITY(1,1),
	MealType varchar(100) NULL,
	Price money NULL,
	ServiceDate date NULL,
	GuestId int NULL,
	ReservationId int NULL,

	CONSTRAINT PK_Meal PRIMARY KEY (ID),
	CONSTRAINT FK_Meal_Guest FOREIGN KEY (GuestId)
		REFERENCES Guest (ID),
	CONSTRAINT FK_Meal_Reservation FOREIGN KEY (ReservationId)
		REFERENCES Reservation (Id),
	CONSTRAINT chk_Price_GreaterThanZero CHECK (Price > 0)
)

CREATE TABLE Equipment
(
	Id int NOT NULL IDENTITY(1,1),
	Name varchar(100) NULL,
	Type varchar(100) NULL,

	CONSTRAINT PK_Equipment PRIMARY KEY (Id)
)

-- N:N relationship   Food - Allergenic
CREATE TABLE FoodAllergenic
(
	FoodId int NOT NULL,
	AllergenicId int NOT NULL,

	CONSTRAINT PK_FoodAllergenicIds PRIMARY KEY (FoodId, AllergenicId),
	CONSTRAINT FK_FoodAllergenic_Food FOREIGN KEY(FoodId)
		REFERENCES Food (Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	CONSTRAINT FK_FoodAllergenic_Allergenic FOREIGN KEY(AllergenicId)
		REFERENCES Allergenic (Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
)

-- N:N relationship   Guest - Allergenic
CREATE TABLE GuestAllergenic
(
	GuestId int NOT NULL,
	AllergenicId int NOT NULL,

	CONSTRAINT PK_GuestAllergenicIds PRIMARY KEY (GuestId, AllergenicId),
	CONSTRAINT FK_GuestAllergenic_Guest FOREIGN KEY(GuestId)
		REFERENCES Guest (Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	CONSTRAINT FK_GuestAllergenic_Allergenic FOREIGN KEY(AllergenicId)
		REFERENCES Allergenic (Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
)

-- N:N relationship   Meal - Food
CREATE TABLE MealFood
(
	MealId int NOT NULL,
	FoodId int NOT NULL,

	CONSTRAINT PK_MealFoodIds PRIMARY KEY (MealId, FoodId),
	CONSTRAINT FK_MealFood_Meal FOREIGN KEY(MealId)
		REFERENCES Meal (Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	CONSTRAINT FK_MealFood_Food FOREIGN KEY(FoodId)
		REFERENCES Food (Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE
)

-- N:N relationship   Guest - Reservation
CREATE TABLE GuestReservation
(
	GuestId int NOT NULL,
	ReservationId int NOT NULL,

	CONSTRAINT PK_GuestReservationIds PRIMARY KEY (GuestId, ReservationId),
	CONSTRAINT FK_GuestReservation_Guest FOREIGN KEY(GuestId)
		REFERENCES Guest (Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	CONSTRAINT FK_GuestReservation_Reservation FOREIGN KEY(ReservationId)
		REFERENCES Reservation (Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE
)

-- N:N relationship   Room - Reservation
CREATE TABLE RoomReservation
(
	RoomId int NOT NULL,
	ReservationId int NOT NULL,

	CONSTRAINT PK_RoomReservationIds PRIMARY KEY (RoomId, ReservationId),
	CONSTRAINT FK_RoomReservation_Guest FOREIGN KEY(RoomId)
		REFERENCES Room (Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	CONSTRAINT FK_RoomReservation_Reservation FOREIGN KEY(ReservationId)
		REFERENCES Reservation (Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE
)

-- N:N relationship   Equipment - Room
CREATE TABLE EquipmentRoom
(
	EquipmentId int NOT NULL,
	RoomId int NOT NULL,

	CONSTRAINT PK_EquipmentRoomIds PRIMARY KEY (EquipmentId, RoomId),
	CONSTRAINT FK_EquipmentRoom_Equipment FOREIGN KEY(EquipmentId)
		REFERENCES Equipment (Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	CONSTRAINT FK_EquipmentRoom_Room FOREIGN KEY(RoomId)
		REFERENCES Room (Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
)

GO
PRINT 'Tables creation were successfull'
