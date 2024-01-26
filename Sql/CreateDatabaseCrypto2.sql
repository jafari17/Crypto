
CREATE TABLE dbo.UserService (
    UserID INT IDENTITY(1,1) NOT NULL,
    UserIdentity [nvarchar](450) NOT NULL,
    [Name] [nvarchar](256),
    EmailAddress [nvarchar](256),
    IsActive BIT,
	PRIMARY KEY (UserID),
) 
GO

CREATE TABLE dbo.Alert(
    AlertID INT IDENTITY(1,1) NOT NULL,
	UserID INT,
    DateRegisterTime DATETIME,
    price DECIMAL,
    [Description] NVARCHAR(500),
    LastTouchPrice DATETIME,
    IsCrossedUp BIT,
    PriceDifference DECIMAL,
    IsActive BIT,
    IsTemproprySuspended BIT,
    PRIMARY KEY (AlertID),
	FOREIGN KEY (UserID) REFERENCES dbo.UserService(UserID)
)
GO

GO
CREATE TABLE dbo.Candle(
	CandleID INT IDENTITY(1,1) NOT NULL,
	OpenTime BIGINT,
	OpenPrice DECIMAL,
	HighPrice DECIMAL,
	LowPrice DECIMAL,
	ClosePrice DECIMAL,
	Volume INT,
	CloseTime BIGINT,
	--QuoteAssetVolume DECIMAL,
	--NumberOfTrades BIGINT,
	--TakerBuyBaseAssetVolume DECIMAL,
	--TakerBuyQuoteAssetVolume DECIMAL,
	--Ignore INT,
	PRIMARY KEY (CandleID)
) 
GO