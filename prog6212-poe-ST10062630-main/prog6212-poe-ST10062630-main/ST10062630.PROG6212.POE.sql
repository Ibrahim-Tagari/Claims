USE ST10062630_PROG6212_POE;

-- Step 1: Create the Roles table
CREATE TABLE Roles (
    UserRoleId INT PRIMARY KEY IDENTITY, -- Primary Key
    RoleName NVARCHAR(50) NOT NULL       -- Role Name
);

-- Step 2: Create the Users table with a foreign key reference to Roles
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY,       -- Primary Key
    UserName NVARCHAR(100) NOT NULL,       -- User's Name
    UserRoleId INT,                        -- Foreign Key to Roles
    FOREIGN KEY (UserRoleId) REFERENCES Roles(UserRoleId)
);

-- Step 3: Create the Claims table
CREATE TABLE Claims (
    ClaimId INT PRIMARY KEY IDENTITY,       -- Primary Key
    Lecturer NVARCHAR(100) NOT NULL,        -- Name of Lecturer
    HoursWorked INT NOT NULL,               -- Total Hours Worked
    HourlyRate DECIMAL(10, 2) NOT NULL,     -- Hourly Rate
    Amount DECIMAL(10, 2) NOT NULL,         -- Calculated Amount
    Notes NVARCHAR(MAX),                    -- Additional Notes
    Status NVARCHAR(20) DEFAULT 'Pending',  -- Claim Status
    Month NVARCHAR(20),                     -- Month of Claim
    DocumentPath NVARCHAR(200),             -- Path to Uploaded Document
    SubmittedDate DATETIME DEFAULT GETDATE(), -- Submission Timestamp
    UserRoleId INT,                         -- Foreign Key to Roles (UserRoleId)
    FOREIGN KEY (UserRoleId) REFERENCES Roles(UserRoleId) -- Relate to Roles by UserRoleId
);

-- Step 4: Create the Approvals table with foreign key reference to Claims
CREATE TABLE Approvals (
    ApprovalId INT PRIMARY KEY IDENTITY,      -- Primary Key
    ClaimId INT NOT NULL,                     -- Foreign Key to Claims
    ApprovedBy NVARCHAR(100),                 -- Name of Approver
    ApprovalStatus NVARCHAR(20),              -- Approval Status
    ApprovalDate DATETIME,                    -- Approval Timestamp
    FOREIGN KEY (ClaimId) REFERENCES Claims(ClaimId)
);

-- Step 5: Insert predefined roles into the Roles table
INSERT INTO Roles (RoleName) 
VALUES 
('Lecturer'), 
('Programme Coordinator'), 
('Academic Manager');

-- Step 6: Insert sample data into the Claims table for testing
INSERT INTO Claims (Lecturer, HoursWorked, HourlyRate, Amount, Notes, Status, Month, DocumentPath, UserRoleId)
VALUES 
('John Doe', 10, 50.00, 500.00, 'First claim', 'Pending', 'November', 'path/to/document1.pdf', 1),
('Jane Smith', 20, 60.00, 1200.00, 'Second claim', 'Pending', 'November', 'path/to/document2.pdf', 2),
('Alice Brown', 20, 60.00, 1200.00, 'Third claim', 'Pending', 'November', 'path/to/document3.pdf', 3);

-- Step 7: Verify data insertion
SELECT * FROM Roles;
SELECT * FROM Users;
SELECT * FROM Claims;
SELECT * FROM Approvals;

-- Step 8: Verify foreign key relationships and columns in the Users, Claims, and Approvals tables

-- Foreign keys for Users table
SELECT 
    kcu.CONSTRAINT_NAME, 
    kcu.TABLE_NAME AS FK_TABLE, 
    kcu.COLUMN_NAME AS FK_COLUMN, 
    ccu.TABLE_NAME AS REFERENCED_TABLE, 
    ccu.COLUMN_NAME AS REFERENCED_COLUMN
FROM 
    INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu
JOIN 
    INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc
    ON kcu.CONSTRAINT_NAME = rc.CONSTRAINT_NAME
JOIN 
    INFORMATION_SCHEMA.KEY_COLUMN_USAGE ccu
    ON rc.UNIQUE_CONSTRAINT_NAME = ccu.CONSTRAINT_NAME
WHERE 
    kcu.TABLE_NAME = 'Users';

-- Foreign keys for Claims table
SELECT 
    kcu.CONSTRAINT_NAME, 
    kcu.TABLE_NAME AS FK_TABLE, 
    kcu.COLUMN_NAME AS FK_COLUMN, 
    ccu.TABLE_NAME AS REFERENCED_TABLE, 
    ccu.COLUMN_NAME AS REFERENCED_COLUMN
FROM 
    INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu
JOIN 
    INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc
    ON kcu.CONSTRAINT_NAME = rc.CONSTRAINT_NAME
JOIN 
    INFORMATION_SCHEMA.KEY_COLUMN_USAGE ccu
    ON rc.UNIQUE_CONSTRAINT_NAME = ccu.CONSTRAINT_NAME
WHERE 
    kcu.TABLE_NAME = 'Claims';

-- Foreign keys for Approvals table
SELECT 
    kcu.CONSTRAINT_NAME, 
    kcu.TABLE_NAME AS FK_TABLE, 
    kcu.COLUMN_NAME AS FK_COLUMN, 
    ccu.TABLE_NAME AS REFERENCED_TABLE, 
    ccu.COLUMN_NAME AS REFERENCED_COLUMN
FROM 
    INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu
JOIN 
    INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc
    ON kcu.CONSTRAINT_NAME = rc.CONSTRAINT_NAME
JOIN 
    INFORMATION_SCHEMA.KEY_COLUMN_USAGE ccu
    ON rc.UNIQUE_CONSTRAINT_NAME = ccu.CONSTRAINT_NAME
WHERE 
    kcu.TABLE_NAME = 'Approvals';
