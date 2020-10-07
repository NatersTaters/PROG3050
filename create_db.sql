-- *******************************************
-- This script creates the CVGS Club database
-- for our PROG3050 project by Chris Symons, 
-- Nathaniel Saunders and Leunard Gervalla 
-- *******************************************

-- Create the database
-- DROP DATABASE IF EXISTS cvgs_club;
-- CREATE DATABASE cvgs_club;

-- Select the database
USE cvgs_club;

-- Create the tables
CREATE TABLE games
(
	game_id						INT				IDENTITY(1,1)		PRIMARY KEY     NOT NULL,
    game_name					VARCHAR(50)		NOT NULL		UNIQUE,
    list_price 					DECIMAL(10,2)	NOT NULL,
    content_rating				VARCHAR(4)		NOT NULL		DEFAULT 'RP',
    genre						VARCHAR(50)		NOT NULL,
    available_platforms			VARCHAR(50)		NOT NULL,
    max_players					VARCHAR(25)		NOT NULL
); 

INSERT dbo.games (game_name, list_price, content_rating, genre, available_platforms, max_players)
	VALUES ('Call of Duty: Modern Warfare', 79.99, 'M', 'Action', 'PC, Xbox One, PS4', '50')
GO

INSERT dbo.games (game_name, list_price, genre, available_platforms, max_players)
	VALUES ('Cyberpunk 2077', 79.99, 'Adventure', 'PC, Xbox One, PS4', '1')
GO

INSERT dbo.games (game_name, list_price, content_rating, genre, available_platforms, max_players)
	VALUES ('Minecraft', 29.99, 'E', 'Sandbox', 'PC, Xbox One, PS4, Nintendo Switch', '1')
GO

INSERT dbo.games (game_name, list_price, content_rating, genre, available_platforms, max_players)
	VALUES ('The Elder Scrolls 5: Skyrim', 29.99, 'T', 'Adventure', 'PC, Xbox One, PS4, Nintendo Switch', '1')
GO

CREATE TABLE members
(
	member_id				INT 			IDENTITY(1,1)		PRIMARY KEY     NOT NULL,
    display_name			VARCHAR(60)		NOT NULL		UNIQUE,
    first_name				VARCHAR(60)		NOT NULL,
    last_name				VARCHAR(60)		NOT NULL,
    email					VARCHAR(255)	UNIQUE,
    password				VARCHAR(60)		NOT NULL,
    gender					CHAR(1),			
    birth_date				DATE,
    receive_emails			BIT				DEFAULT 0,
    mailing_address_id		INT				DEFAULT NULL,
	shipping_address_id		INT				DEFAULT NULL,
    card_type				VARCHAR(50),
    card_number				CHAR(16),
    card_expires			CHAR(7)
);

--Insert Members
INSERT dbo.members (display_name, first_name, last_name, email, password, gender, birth_date, receive_emails, card_type, card_number, card_expires)
	VALUES ('Nathaniel Saunders', 'Nathaniel', 'Saunders', 'NSaunders4659@conestogac.on.ca', 'nathan4659', 'm', '1998-11-15', 1, 'Mastercard', '4565235797547456', '05/24')
GO

INSERT dbo.members (display_name, first_name, last_name, email, password, gender, birth_date, receive_emails, card_type, card_number, card_expires)
	VALUES ('Leunard Gervalla', 'Leunard', 'Gervalla', 'LGervalla8340@conestogac.on.ca', 'leunard8340', 'm', '1998-05-24', 1, 'Visa', '5684246823512433', '08/21')
GO

INSERT dbo.members (display_name, first_name, last_name, email, password, gender, birth_date, receive_emails, card_type, card_number, card_expires)
	VALUES ('Chris Symons', 'Chris', 'Symons', 'CSymons1806@conestogac.on.ca', 'chris1806', 'm', '1999-01-20', 1, 'Mastercard', '5435125478423568', '11/24')
GO

INSERT dbo.members (display_name, first_name, last_name, email, password, gender, birth_date, receive_emails, card_type, card_number, card_expires)
	VALUES ('Andrew Truong', 'Andrew', 'Truong', 'ATruong7429@conestogac.on.ca', 'andrew7429', 'm', '1998-07-09', 1, 'Mastercard', '4535678534562348', '02/23')
GO

CREATE TABLE addresses
(
	address_id		INT				IDENTITY(1,1)		PRIMARY KEY		NOT NULL,
    member_id		INT				NOT NULL,
    line1			VARCHAR(60)		NOT NULL,
    line2			VARCHAR(60)		DEFAULT NULL,
    city			VARCHAR(40)		NOT NULL,
    province		VARCHAR(2)		NOT NULL,
    postal_code		VARCHAR(10)		NOT NULL,
    phone			VARCHAR(12)		NOT NULL,
    CONSTRAINT addresses_fk_members
		FOREIGN KEY (member_id)
		REFERENCES members (member_id)
);

--Insert Addresses for each Member
INSERT dbo.addresses (member_id, line1, city, province, postal_code, phone)
	VALUES (1, '143 Gerber Meadows Drive', 'Wellesley', 'ON', 'N0B2T0', '5198975499')
GO

INSERT dbo.addresses (member_id, line1, line2, city, province, postal_code, phone)
	VALUES (2, '99 Crescent Drive', 'apt 24', 'Waterloo', 'ON', 'N2T2X9', '5192403664')
GO

INSERT dbo.addresses (member_id, line1, city, province, postal_code, phone)
	VALUES (3, '23 Village Road', 'Waterloo', 'ON', 'N4F5S1', '5194568744')
GO

INSERT dbo.addresses (member_id, line1, city, province, postal_code, phone)
	VALUES (4, '129 Nafziger Way', 'Kitchener', 'ON', 'B3S7G2', '5194567625')
GO

CREATE TABLE events
(
	event_id		INT			IDENTITY(1,1)	PRIMARY KEY		NOT NULL,
	member_id		INT			NOT NULL,
    event_date		DATE		NOT NULL,
    start_time		TIME		NOT NULL,
    end_time		TIME		NOT NULL,
    capacity		INT			NOT NULL,
	CONSTRAINT events_fk_members
		FOREIGN KEY (member_id)
		REFERENCES members (member_id)
);

INSERT dbo.events (member_id, event_date, start_time, end_time, capacity)
	VALUES (1, '2020-12-10', '10:00', '22:00', 1000)
GO

INSERT dbo.events (member_id, event_date, start_time, end_time, capacity)
	VALUES (2, '2021-02-24', '08:00', '12:00', 600)
GO

INSERT dbo.events (member_id, event_date, start_time, end_time, capacity)
	VALUES (3, '2021-05-20', '12:00', '16:00', 550)
GO

INSERT dbo.events (member_id, event_date, start_time, end_time, capacity)
	VALUES (4, '2021-03-24', '14:00', '20:00', 1000)
GO

CREATE TABLE wish_lists
(
	member_id		INT		NOT NULL,
    game_id			INT 	NOT NULL,
    CONSTRAINT wish_lists_fk_members
		FOREIGN KEY (member_id)
		REFERENCES members (member_id),
	CONSTRAINT wish_lists_fk_games
		FOREIGN KEY (game_id)
		REFERENCES games (game_id)
);

INSERT dbo.wish_lists (member_id, game_id)
	VALUES (1, 1)
GO

INSERT dbo.wish_lists (member_id, game_id)
	VALUES (1, 2)
GO

INSERT dbo.wish_lists (member_id, game_id)
	VALUES (2, 3)
GO

INSERT dbo.wish_lists (member_id, game_id)
	VALUES (3, 2)
GO

CREATE TABLE friends_family
(
	member_id			INT		NOT NULL,
    friend_family_id	INT 	NOT NULL,
    CONSTRAINT friends_family_fk_members
		FOREIGN KEY (member_id)
		REFERENCES members (member_id)
);

INSERT dbo.friends_family(member_id, friend_family_id)
	VALUES (1, 2)
GO

INSERT dbo.friends_family(member_id, friend_family_id)
	VALUES (1, 3)
GO

INSERT dbo.friends_family(member_id, friend_family_id)
	VALUES (2, 3)
GO

INSERT dbo.friends_family(member_id, friend_family_id)
	VALUES (3, 4)
GO