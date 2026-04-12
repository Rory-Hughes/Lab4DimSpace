-- Running for first time, highlight two lines only and execute
-- Those lines are: 
-- --> DROP DATABASE IF EXISTS dbName;
-- --> CREATE DATABASE dbName;
-- Rerunning and database exists, execute all lines

USE master;

-- set single user mode on db and 
-- all incomplete transactions will be rolled back
alter database DimSpace set single_user with rollback immediate

-- Highlight next two lines for first time script run
DROP DATABASE IF EXISTS DimSpace;
CREATE DATABASE DimSpace;
USE DimSpace;
DROP TABLE IF EXISTS Users, Courses, UserRoles, DropBoxStatus, DropBox

-- Courses table
CREATE TABLE Courses (
  CourseId INT IDENTITY(1000,10) PRIMARY KEY,
  Name VARCHAR(100) NOT NULL,
  Description VARCHAR(255)
);
--Permissions table
CREATE TABLE UserRoles (
	UserRoleId INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(30) NOT NULL
);

CREATE TABLE DropBoxStatus (
	StatusId INT IDENTITY(1,1) PRIMARY KEY,
	StatusName VARCHAR(30)
);
-- Users table
CREATE TABLE Users (
  UserId INT IDENTITY(100,1) PRIMARY KEY,
  Username VARCHAR(50) NOT NULL,
  Password VARCHAR(50) NOT NULL,
  Email VARCHAR(100) NOT NULL,
  UserRoleId INT NOT NULL,
  FOREIGN KEY (UserRoleId) REFERENCES UserRoles(UserRoleId)
);

-- CourseAccess table
CREATE TABLE CourseAccess (
  CourseAccessId INT IDENTITY(1,1) PRIMARY KEY,
  UserId INT NOT NULL,
  CourseId INT NOT NULL,
  FOREIGN KEY (UserId) REFERENCES Users(UserId),
  FOREIGN KEY (CourseId) REFERENCES Courses(CourseId)
);

-- DropBox table
CREATE TABLE DropBox (
  DropBoxId INT IDENTITY(1,1) PRIMARY KEY,
  CourseId INT,
  Name VARCHAR(100) NOT NULL,
  Description VARCHAR(255),
  DueDate DATE,
  FOREIGN KEY (CourseId) REFERENCES Courses(CourseId),

);

-- DropBoxItems table
CREATE TABLE DropBoxItems (
	DropBoxItemId INT IDENTITY(1,1) PRIMARY KEY,
	DropBoxId INT NOT NULL,
	StudentId INT NOT NULL,
	StatusId INT NOT NULL,
	FOREIGN KEY (DropBoxId) REFERENCES DropBox(DropBoxId),
	FOREIGN KEY (StudentId) REFERENCES Users(UserId),
	FOREIGN KEY (StatusId) REFERENCES DropBoxStatus(StatusId)
);

GO

-- Add Data to tables

INSERT INTO DropBoxStatus (StatusName) 
VALUES('Released'),('Submitted'),('Graded');

INSERT INTO UserRoles (Name)
VALUES('Student'),('Instructor'),('Administrator');

INSERT INTO Courses (Name,Description)
VALUES('Introduction to Programming', 'Learn the fundamentals of programming and software development.'),
  ('Database Management', 'Explore the principles and techniques of managing databases.'),
  ('Web Development', 'Learn how to build interactive websites using HTML, CSS, and JavaScript.'),
  ('Network Security', 'Study techniques to protect computer networks from unauthorized access.'),
  ('Data Science and Analytics', 'Learn how to analyze and interpret large datasets to gain insights.'),
  ('Cybersecurity', 'Explore techniques to secure computer systems and prevent cyber threats.'),
  ('IT Project Management', 'Learn how to effectively manage IT projects from initiation to completion.'),
  ('Cloud Computing', 'Explore the concepts and technologies behind cloud computing.'),
  ('Artificial Intelligence', 'Study the theory and applications of artificial intelligence.'),
  ('Mobile App Development', 'Learn how to develop mobile applications for iOS and Android.');

INSERT INTO Users (Username, Password, Email, UserRoleId)
VALUES
  ('admin','admin','admin@mbcc.ca', 3),
  ('jsmith', 'jsmith', 'jsmith@mymbcc.ca', 1),
  ('ajohnson', 'ajohnson', 'ajohnson@mymbcc.ca', 1),
  ('rwilson', 'rwilson', 'rwilson@mymbcc.ca', 1),
  ('bthompson', 'bthompson', 'bthompson@mymbcc.ca', 1),
  ('krodriguez', 'krodriguez', 'krodriguez@mymbcc.ca', 1),
  ('mstewart', 'mstewart', 'mstewart@mymbcc.ca', 1),
  ('nlewis', 'nlewis', 'nlewis@mymbcc.ca', 1),
  ('pturner', 'pturner', 'pturner@mymbcc.ca', 1),
  ('rcook', 'rcook', 'rcook@mymbcc.ca', 1),
  ('cwalker', 'cwalker', 'cwalker@mymbcc.ca', 1),
  ('ehall', 'ehall', 'ehall@mymbcc.ca', 1),
  ('ggonzalez', 'ggonzalez', 'ggonzalez@mymbcc.ca', 1),
  ('jcollins', 'jcollins', 'jcollins@mymbcc.ca', 1),
  ('lprice', 'lprice', 'lprice@mymbcc.ca', 1),
  ('dward', 'dward', 'dward@mymbcc.ca', 1),
  ('mturner', 'mturner', 'mturner@mymbcc.ca', 1),
  ('rgray', 'rgray', 'rgray@mymbcc.ca', 1),
  ('bwright', 'bwright', 'bwright@mymbcc.ca', 1),
  ('jgreen', 'jgreen', 'jgreen@mymbcc.ca', 1),
  ('sthompson', 'sthompson', 'sthompson@mymbcc.ca', 1),
  ('tross', 'tross', 'tross@mymbcc.ca', 1),
  ('jramirez', 'jramirez', 'jramirez@mymbcc.ca', 1),
  ('mrogers', 'mrogers', 'mrogers@mymbcc.ca', 1),
  ('kward', 'kward', 'kward@mymbcc.ca', 1),
  ('jbutler', 'jbutler', 'jbutler@mymbcc.ca', 1),
  ('sharris', 'sharris', 'sharris@mymbcc.ca', 1),
  ('cnelson', 'cnelson', 'cnelson@mymbcc.ca', 1),
  ('bsmith', 'bsmith', 'bsmith@mymbcc.ca', 1),
  ('arojas', 'arojas', 'arojas@mymbcc.ca', 1),
  ('ehughes', 'ehughes', 'ehughes@mymbcc.ca', 1),
  ('gmyers', 'gmyers', 'gmyers@mymbcc.ca', 1),
  ('lparker', 'lparker', 'lparker@mymbcc.ca', 1),
  ('dtaylor', 'dtaylor', 'dtaylor@mymbcc.ca', 1),
  ('mhall', 'mhall', 'mhall@mymbcc.ca', 1),
  ('rcooper', 'rcooper', 'rcooper@mymbcc.ca', 1),
  ('jlee', 'jlee', 'jlee@mymbcc.ca', 1),
  ('bwilson', 'bwilson', 'bwilson@mymbcc.ca', 1),
  ('mrodriguez', 'mrodriguez', 'mrodriguez@mymbcc.ca', 1),
  ('jcollins', 'jcollins', 'jcollins@mymbcc.ca', 1),
  ('hturner', 'hturner', 'hturner@mymbcc.ca', 1),
  ('amitchell','amitchell','amitchell@mbcc.ca', 2),
  ('cburchill','cburchill','cburchill@mbcc.ca', 2),
  ('smonk','smonk','smonk@mbcc.ca', 2),
  ('clondon','clondon','clondon@mbcc.ca', 2),
  ('crendell','crendell','crendell@mbcc.ca', 2),
  ('mboutilier','mboutilier','mboutilier@mbcc.ca', 2);

  INSERT INTO DropBox (CourseId, Name, Description, DueDate)
VALUES
  (1000, 'Programming Assignment 1', 'Implement basic algorithms in Python.', '2023-05-31'),
  (1010, 'Database Design Project', 'Design a relational database schema.', '2023-06-10'),
  (1020, 'Website Development Assignment', 'Create a responsive website using HTML and CSS.', '2023-06-15'),
  (1030, 'Network Security Lab', 'Perform network vulnerability assessments.', '2023-06-20'),
  (1040, 'Data Analysis Project', 'Analyze a dataset using statistical techniques.', '2023-06-30'),
  (1050, 'Cybersecurity Quiz', 'Test your knowledge of cybersecurity concepts.', '2023-07-05'),
  (1060, 'Project Proposal Submission', 'Submit your project proposal document.', '2023-07-10'),
  (1070, 'Cloud Infrastructure Setup', 'Set up a virtual machine on a cloud platform.', '2023-07-15'),
  (1080, 'AI Algorithm Implementation', 'Implement a machine learning algorithm.', '2023-07-20'),
  (1090, 'Mobile App Wireframing', 'Create wireframes for a mobile application.', '2023-07-25'),
  (1000, 'Programming Assignment 2', 'Implement advanced algorithms in Python.', '2023-07-31'),
  (1010, 'Database Query Optimization', 'Optimize SQL queries for performance.', '2023-08-05'),
  (1020, 'Website Deployment', 'Deploy the website on a web server.', '2023-08-10'),
  (1030, 'Network Security Report', 'Write a report on network security best practices.', '2023-08-15'),
  (1040, 'Data Visualization Project', 'Create visualizations for a dataset.', '2023-08-20'),
  (1050, 'Cybersecurity Case Study', 'Analyze a real-world cybersecurity case.', '2023-08-25'),
  (1060, 'Project Progress Presentation', 'Present the progress of your project.', '2023-08-31'),
  (1070, 'Cloud Resource Scaling', 'Scale cloud resources based on demand.', '2023-09-05'),
  (1080, 'AI Model Training', 'Train a machine learning model on a dataset.', '2023-09-10'),
  (1090, 'Mobile App Prototype', 'Create a functional prototype of a mobile app.', '2023-09-15'),
  (1000, 'Programming Assignment 3', 'Implement object-oriented programming concepts.', '2023-09-20'),
  (1010, 'Database Backup and Recovery', 'Create a backup and recovery plan for a database.', '2023-09-25'),
  (1020, 'Web Application Security', 'Implement security measures for web applications.', '2023-09-30'),
  (1030, 'Network Penetration Testing', 'Perform penetration testing on a network.', '2023-10-05'),
  (1040, 'Data Mining Project', 'Apply data mining techniques to extract insights.', '2023-10-10'),
  (1050, 'Cybersecurity Incident Response', 'Develop an incident response plan.', '2023-10-15'),
  (1060, 'Project Implementation', 'Implement the project according to the plan.', '2023-10-20'),
  (1070, 'Cloud Data Storage', 'Implement data storage solutions in the cloud.', '2023-10-25'),
  (1080, 'AI Model Evaluation', 'Evaluate the performance of a machine learning model.', '2023-10-30'),
  (1090, 'Mobile App Testing', 'Test the functionality and performance of a mobile app.', '2023-11-05'),
  (1000, 'Programming Assignment 4', 'Implement advanced programming concepts.', '2023-11-10'),
  (1010, 'Database Performance Tuning', 'Optimize database performance.', '2023-11-15'),
  (1020, 'Web Application Deployment', 'Deploy the web application on a server.', '2023-11-20'),
  (1030, 'Network Security Audit', 'Conduct a security audit on a network.', '2023-11-25'),
  (1040, 'Data Analysis Report', 'Create a report on data analysis findings.', '2023-11-30'),
  (1050, 'Cybersecurity Risk Assessment', 'Assess and mitigate cybersecurity risks.', '2023-12-05'),
  (1060, 'Project Documentation', 'Create documentation for the project.', '2023-12-10'),
  (1070, 'Cloud Cost Optimization', 'Optimize cloud resource costs.', '2023-12-15'),
  (1080, 'AI Model Deployment', 'Deploy a machine learning model in production.', '2023-12-20'),
  (1090, 'Mobile App Release', 'Prepare and release a mobile app to app stores.', '2023-12-25');

  INSERT INTO CourseAccess (UserId, CourseId)
VALUES
  (100, 1000),
  (100, 1010),
  (100, 1020),
  (100, 1030),
  (100, 1040),
  (100, 1050),
  (100, 1060),
  (100, 1070),
  (100, 1080),
  (100, 1090),
  (141, 1000),
  (140, 1010),
  (142, 1020),
  (145, 1030),
  (144, 1040),
  (143, 1050),
  (144, 1060),
  (140, 1070),
  (143, 1080),
  (145, 1090),
  (146, 1020),
  (146, 1030),
  (101, 1000),
  (101, 1010),
  (101, 1020),
  (101, 1030),
  (102, 1040),
  (102, 1050),
  (102, 1060),
  (102, 1070),
  (102, 1080),
  (103, 1090),
  (103, 1000),
  (103, 1010),
  (104, 1020),
  (104, 1030),
  (104, 1040),
  (104, 1050),
  (105, 1060),
  (105, 1070),
  (105, 1080),
  (105, 1000),
  (106, 1010),
  (107, 1020),
  (107, 1030),
  (107, 1040),
  (107, 1050),
  (108, 1060),
  (108, 1070),
  (108, 1080),
  (109, 1090),
  (109, 1090),
  (110, 1000),
  (111, 1010),
  (112, 1020),
  (113, 1030),
  (114, 1040),
  (115, 1050),
  (116, 1060),
  (117, 1070),
  (118, 1080),
  (119, 1090),
  (120, 1000),
  (121, 1010),
  (122, 1020),
  (123, 1030),
  (124, 1040),
  (125, 1050),
  (126, 1060),
  (127, 1070),
  (128, 1080),
  (129, 1090),
  (130, 1000),
  (131, 1010),
  (132, 1020),
  (133, 1030),
  (134, 1040),
  (135, 1050),
  (136, 1060),
  (137, 1070),
  (138, 1080),
  (139, 1090),
  (121, 1000),
  (122, 1010),
  (123, 1020),
  (124, 1030),
  (125, 1040),
  (126, 1050),
  (127, 1060),
  (128, 1070),
  (129, 1080),
  (130, 1090),
  (131, 1000),
  (132, 1010),
  (133, 1020),
  (134, 1030),
  (135, 1040),
  (136, 1050),
  (137, 1060),
  (138, 1070),
  (139, 1080),
  (139, 1050),
  (107, 1000),
  (110, 1010),
  (111, 1020),
  (112, 1030),
  (113, 1040),
  (114, 1050),
  (115, 1060),
  (116, 1070),
  (117, 1080),
  (118, 1090),
  (119, 1000),
  (121, 1020),
  (122, 1030),
  (123, 1040),
  (124, 1050),
  (125, 1060),
  (126, 1070),
  (127, 1080),
  (128, 1090),
  (129, 1040);

  -- Insert Items into DropBoxItems table

-- Ensure that only students with access to the course can have items in the DropBox
INSERT INTO DropBoxItems (DropBoxId, StudentId, StatusId)
SELECT d.DropBoxId, ca.UserId, 1 -- assuming status '1' is 'Released'
FROM DropBox d
JOIN CourseAccess ca ON d.CourseId = ca.CourseId
JOIN Users u ON ca.UserId = u.UserId
WHERE u.UserRoleId = (SELECT UserRoleId FROM UserRoles WHERE Name = 'Student')


-- Update the Status of some records to 'Submitted'
UPDATE DropBoxItems
SET StatusId = 2 -- assuming status '2' is 'Submitted'
WHERE DropBoxItemId IN (
    SELECT DropBoxItemId
    FROM DropBoxItems
    ORDER BY NEWID()
    OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY -- update 50 random records
);
-- Update the Status of some records to 'Graded'
UPDATE DropBoxItems
SET StatusId = 3 -- assuming status '3' is 'Graded'
WHERE DropBoxItemId IN (
    SELECT DropBoxItemId
    FROM DropBoxItems
    ORDER BY NEWID()
    OFFSET 0 ROWS FETCH NEXT 50 ROWS ONLY -- update 50 random records
);

-- --- Update Due Dates for DropBox to current year (up to end of June) ---
-- This script will update all existing DueDates in the DropBox table
-- to a random date between January 1st of the current year and June 30th of the current year.

DECLARE @StartDate DATE;
DECLARE @EndDate DATE;
DECLARE @CurrentYear INT = YEAR(GETDATE());

-- Set the start date to January 1st of the current year
SET @StartDate = DATEFROMPARTS(@CurrentYear, 1, 1);

-- Set the end date to June 30th of the current year
SET @EndDate = DATEFROMPARTS(@CurrentYear, 6, 30);

-- Update the DueDate column in the DropBox table
-- Generates a random number of days within set range. 
-- NEWID() creates a unique GUID, CHECKSUM() converts it to an integer
-- ABS() ensures it's positive, and the modulo operator (%) scales it to the range
UPDATE DropBox
SET DueDate = DATEADD(
    day,
    ABS(CHECKSUM(NEWID())) % (1 + DATEDIFF(day, @StartDate, @EndDate)),
    @StartDate
);

-- --- End of Date Update Script ---
