use Gistapp;
go
-- Table pour les projets (déjà existante)
CREATE TABLE Projects (
    ProjectId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    Progress INT NOT NULL
);
go
-- Table pour les ressources existantes (si besoin)
CREATE TABLE Resources (
    ResourceId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Role NVARCHAR(50),
    Email NVARCHAR(100)
);
go
-- Table pour l'association entre projets et ressources
CREATE TABLE ProjectResources (
    ProjectId INT NOT NULL,
    ResourceId INT NOT NULL,
    Allocation INT NOT NULL,
    PRIMARY KEY (ProjectId, ResourceId),
    FOREIGN KEY (ProjectId) REFERENCES Projects(ProjectId),
    FOREIGN KEY (ResourceId) REFERENCES Resources(ResourceId)
);
go
-- Table pour les membres du projet
CREATE TABLE Members (
    MemberId INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL
    -- Ajoutez d'autres colonnes si nécessaire
);
go
-- Table associative pour lier les membres aux projets (relation Many-to-Many)
CREATE TABLE ProjectMembers (
    ProjectId INT NOT NULL,
    MemberId INT NOT NULL,
    Role NVARCHAR(50), -- Le rôle spécifique du membre dans le projet
    PRIMARY KEY (ProjectId, MemberId),
    FOREIGN KEY (ProjectId) REFERENCES Projects(ProjectId),
    FOREIGN KEY (MemberId) REFERENCES Members(MemberId)
);
go
-- Table pour les tâches
CREATE TABLE Tasks (
    TaskId INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    DueDate DATETIME,
    IsCompleted BIT NOT NULL,
    ProjectId INT NOT NULL,
    MemberId INT NULL,  -- Peut être null si la tâche n'est pas assignée
    FOREIGN KEY (ProjectId) REFERENCES Projects(ProjectId),
    FOREIGN KEY (MemberId) REFERENCES Members(MemberId)
);
go
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL
    -- Ajoutez d'autres colonnes au besoin
);

