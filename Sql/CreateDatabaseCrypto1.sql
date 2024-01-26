
USE Crypto
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

CREATE TABLE dbo.UserName (
    UserID INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(120),
    EmailAddress NVARCHAR(500),
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
	FOREIGN KEY (UserID) REFERENCES dbo.UserName(UserID)
)
GO