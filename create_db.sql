-- *******************************************
-- This script creates the CVGS Club database
-- for our PROG3050 project by Chris Symons, 
-- Nathaniel Saunders and Leunard Gervalla 
-- *******************************************

-- Create the database
--DROP DATABASE IF EXISTS cvgs_club;
--CREATE DATABASE cvgs_club;

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