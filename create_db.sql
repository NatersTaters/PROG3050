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
	member_id				VARCHAR(60) 	PRIMARY KEY     NOT NULL,
    display_name			VARCHAR(60)		DEFAULT NULL,
    first_name				VARCHAR(60)		DEFAULT NULL,
    last_name				VARCHAR(60)		DEFAULT NULL,
    email					VARCHAR(255)	NOT NULL,
    password				VARCHAR(60)		NOT NULL,
    gender					CHAR(1)			DEFAULT NULL,			
    birth_date				DATE			DEFAULT NULL,
    receive_emails			BIT				DEFAULT 0,
    mailing_address_id		INT				DEFAULT NULL,
	shipping_address_id		INT				DEFAULT NULL,
    card_type				VARCHAR(50)		DEFAULT NULL,
    card_number				CHAR(16)		DEFAULT NULL,
    card_expires			CHAR(7)			DEFAULT NULL
);

CREATE TABLE gameReviews
(
	review_id					INT				IDENTITY(1,1)		PRIMARY KEY     NOT NULL,
	member_id					VARCHAR(60) 	NOT NULL,
	game_id						INT				NOT NULL,
    game_review					VARCHAR(100)	NOT NULL,
	CONSTRAINT gameReviews_fk_members
		FOREIGN KEY (member_id)
		REFERENCES members (member_id),
	CONSTRAINT gameReviews_fk_games
		FOREIGN KEY (game_id)
		REFERENCES games (game_id)
); 

CREATE TABLE gamesLibrary
(
	libraryGame_id				INT				IDENTITY(1,1)		PRIMARY KEY     NOT NULL,
	member_id					VARCHAR(60) 	NOT NULL,
	game_id						INT				NOT NULL,
	CONSTRAINT gamesLibrary_fk_members
		FOREIGN KEY (member_id)
		REFERENCES members (member_id),
	CONSTRAINT gamesLibrary_fk_games
		FOREIGN KEY (game_id)
		REFERENCES games (game_id)
);

CREATE TABLE addresses
(
	address_id		INT				IDENTITY(1,1)		PRIMARY KEY		NOT NULL,
    member_id		VARCHAR(60)		NOT NULL,
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

CREATE TABLE events
(
	event_id		INT			IDENTITY(1,1)	PRIMARY KEY		NOT NULL,
	event_name		VARCHAR(60) NOT NULL,
    event_date		DATE		NOT NULL,
    start_time		TIME		NOT NULL,
    end_time		TIME		NOT NULL,
    capacity		INT			NOT NULL,
);

INSERT dbo.events (event_name, event_date, start_time, end_time, capacity)
	VALUES ('E3 2020', '2020-09-12', '12:00 PM', '22:00 PM', 5000)
GO

INSERT dbo.events (event_name, event_date, start_time, end_time, capacity)
	VALUES ('Nintendo Direct', '2020-03-15', '10:00 AM', '13:00 PM', 500)
GO

INSERT dbo.events (event_name, event_date, start_time, end_time, capacity)
	VALUES ('Minecon 2020', '2020-10-24', '09:00 AM', '16:00 PM', 800)
GO

CREATE TABLE memberEvents
(
	event_id		INT			PRIMARY KEY		NOT NULL,
	member_id		VARCHAR(60)	NOT NULL
	CONSTRAINT events_fk_members
		FOREIGN KEY (member_id)
		REFERENCES members (member_id),
	CONSTRAINT events_fk_events
		FOREIGN KEY (event_id)
		REFERENCES events (event_id)
);

CREATE TABLE wish_lists
(
	wish_id			INT			IDENTITY(1,1)	PRIMARY KEY		NOT NULL,
	member_id		VARCHAR(60)	NOT NULL,
    game_id			INT 		NOT NULL,
    CONSTRAINT wish_lists_fk_members
		FOREIGN KEY (member_id)
		REFERENCES members (member_id),
	CONSTRAINT wish_lists_fk_games
		FOREIGN KEY (game_id)
		REFERENCES games (game_id)
);

CREATE TABLE friends_family
(
	friend_family_id	INT			IDENTITY(1,1)	 PRIMARY KEY	NOT NULL,
	member_id			VARCHAR(60)	NOT NULL,
	friend_id			VARCHAR(60)	NOT NULL
    CONSTRAINT friends_family_fk_members
		FOREIGN KEY (member_id)
		REFERENCES members (member_id)
);