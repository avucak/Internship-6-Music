CREATE DATABASE Music

BEGIN TRANSACTION
CREATE TABLE Artist(
ArtistId INT PRIMARY KEY IDENTITY,
Name NVARCHAR(70) NOT NULL,
Nationality NVARCHAR(30)
)

CREATE TABLE Album(
AlbumId INT PRIMARY KEY IDENTITY,
Name NVARCHAR(30) NOT NULL,
YearPublished int NOT NULL
)

CREATE TABLE Song(
SongId INT PRIMARY KEY IDENTITY,
Name NVARCHAR(30) NOT NULL,
Length REAL NOT NULL
)

CREATE TABLE AlbumSong(
AlbumId INT FOREIGN KEY REFERENCES Album(AlbumId),
SongId INT FOREIGN KEY REFERENCES Song(SongId)
)
COMMIT

BEGIN TRANSACTION

ALTER TABLE Album
ADD ArtistId INT

ALTER TABLE Album
ADD FOREIGN KEY (ArtistId) REFERENCES Artist(ArtistId)

COMMIT

INSERT INTO Artist
(Name,Nationality)
VALUES
('Nothing But Thieves','English'),('Glass Animals','English')

INSERT INTO Album
(Name,YearPublished,ArtistId)
VALUES
('Nothing But Thieves',2015,1),('Broken Machine',2017,1),('How to Be a Human Being',2016,2),('Zaba',2014,2)

INSERT INTO Artist
(Name,Nationality)
VALUES
('The Fray','American')

INSERT INTO Album
(Name,YearPublished,ArtistId)
VALUES
('Helios',2014,3),('How to Save a Life',2005,3)

INSERT INTO Song
(Name,Length)
VALUES
('Excuse Me',3.38),('Ban All the Music',2.52),('Wake Up Call',2.45),
('Trip Switch',3.01),('Drawing Pins',3.37),
('Amsterdam',4.32),('Sorry',3.34),('Soda',3.56),('Particles',3.55),
('Afterlife',4.43),
('Life Itself',4.41),('Youth',3.51),('Season 2 Episode 3',4.04),
('Pork Soda',4.14),('Agnes',4.32), ('Hazey',3.42),('Black Mambo',4.08),
('Pools',4.48),('Gooey',4.49),('Toes',4.14),
('Hold My Hand',3.41),('Closer to Me',2.47),
('Wherever This Goes',3.38),('Shadow and a Dancer',4.48),
('She Is',3.56),('How to Save a Life',4.23),('Heaven Forbid',3.59),
('Little House',2.30),('Dead Wrong',3.05)

INSERT INTO AlbumSong
VALUES
(1,1),(1,2),(1,3),(1,4),(1,5),(1,7),(2,6),(2,7),(2,8),(2,9),(2,10),(2,1),(3,11),(3,12),(3,13),(3,14),(3,15),(3,16),(4,16),(4,17),(4,18),
(4,19),(4,20),(5,21),(5,22),(5,23),(5,24),(5,25),(5,28),(5,29),(6,25),(6,26),(6,27),(6,28),(6,29),(6,22)