USE [master]
GO
/****** Object:  Database [FileRepositorySystem_DEV]    Script Date: 10/12/2021 6:03:26 PM ******/
CREATE DATABASE [FileRepositorySystem_DEV]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FileRepositorySystem_DEV', FILENAME = N'C:\Users\fbatin\FileRepositorySystem_DEV.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FileRepositorySystem_DEV_log', FILENAME = N'C:\Users\fbatin\FileRepositorySystem_DEV_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FileRepositorySystem_DEV].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET ARITHABORT OFF 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET  MULTI_USER 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET QUERY_STORE = OFF
GO
USE [FileRepositorySystem_DEV]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [FileRepositorySystem_DEV]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Created] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](255) NULL,
	[Modified] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](255) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [nvarchar](10) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[EmailAddress] [nvarchar](255) NOT NULL,
	[Employee_Position] [int] NOT NULL,
	[Employee_Department] [int] NOT NULL,
	[Created] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](255) NULL,
	[Modified] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](255) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vEmployeeListPerDepartment]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vEmployeeListPerDepartment]
AS
SELECT        TOP (100) PERCENT dbo.Departments.Id, dbo.Departments.Code, dbo.Employees.EmployeeId, dbo.Employees.FirstName, dbo.Employees.MiddleName, dbo.Employees.LastName, dbo.Employees.Created, 
                         dbo.Employees.CreatedBy, dbo.Employees.Modified, dbo.Employees.ModifiedBy
FROM            dbo.Employees INNER JOIN
                         dbo.Departments ON dbo.Employees.Employee_Department = dbo.Departments.Id
ORDER BY dbo.Employees.Modified DESC, dbo.Employees.Created DESC
GO
/****** Object:  Table [dbo].[Positions]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Positions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Created] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](255) NULL,
	[Modified] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](255) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_Positions_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vEmployeeListPerPosition]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vEmployeeListPerPosition]
AS
SELECT        TOP (100) PERCENT dbo.Positions.Id, dbo.Employees.EmployeeId, dbo.Employees.FirstName, dbo.Employees.MiddleName, dbo.Employees.LastName, dbo.Positions.Name, dbo.Positions.Description, dbo.Employees.Created, 
                         dbo.Employees.CreatedBy, dbo.Employees.Modified, dbo.Employees.ModifiedBy
FROM            dbo.Positions INNER JOIN
                         dbo.Employees ON dbo.Positions.Id = dbo.Employees.Employee_Position
ORDER BY dbo.Employees.Modified DESC, dbo.Employees.Created DESC
GO
/****** Object:  Table [dbo].[BusinessUnits]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessUnits](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Created] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](255) NULL,
	[Modified] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](255) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_BusinessUnits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeFiles]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeFiles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](255) NOT NULL,
	[FileBytes] [varbinary](max) NOT NULL,
	[ContentType] [nvarchar](255) NULL,
	[EmployeeId] [int] NOT NULL,
	[Created] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](255) NULL,
	[Modified] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](255) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_EmployeeFiles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeProjects]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeProjects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeProject_Project] [int] NOT NULL,
	[EmployeeProject_Employee] [int] NOT NULL,
	[Created] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](255) NULL,
	[Modified] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](255) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_EmployeeProjects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeSalaries]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeSalaries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Salary] [money] NOT NULL,
	[EmployeeSalary_EmpId] [int] NOT NULL,
	[Created] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](255) NULL,
	[Modified] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](255) NULL,
	[RowVersion] [nchar](10) NULL,
 CONSTRAINT [PK_EmployeeSalaries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ErrorLogs]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorLogs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ErrorNumber] [bigint] NOT NULL,
	[ErrorMessage] [nvarchar](max) NOT NULL,
	[MessageType] [nvarchar](50) NOT NULL,
	[MethodName] [nvarchar](500) NOT NULL,
	[FriendlyErrorMessage] [nvarchar](max) NULL,
	[Exception] [nvarchar](max) NULL,
	[Created] [datetimeoffset](7) NULL,
	[Createdby] [nvarchar](255) NULL,
	[Modified] [datetimeoffset](7) NULL,
	[Modifieddby] [nvarchar](255) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK__ErrorLog__3214EC27C4BC783B] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NULL,
	[Duration]  AS (datediff(day,[StartDate],[EndDate])),
	[Project_BusinessUnit] [int] NOT NULL,
	[Created] [datetimeoffset](7) NULL,
	[CreatedBy] [nvarchar](255) NULL,
	[Modified] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](255) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EmployeeProjects] ADD  CONSTRAINT [DF_EmployeeProjects_EmployeeProject_Project]  DEFAULT ((0)) FOR [EmployeeProject_Project]
GO
ALTER TABLE [dbo].[EmployeeProjects] ADD  CONSTRAINT [DF_EmployeeProjects_EmployeeProject_Employee]  DEFAULT ((0)) FOR [EmployeeProject_Employee]
GO
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [DF_Employees_Employee_Position]  DEFAULT ((1)) FOR [Employee_Position]
GO
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [DF_Employees_Employee_Department]  DEFAULT ((1)) FOR [Employee_Department]
GO
ALTER TABLE [dbo].[EmployeeFiles]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeFiles_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmployeeFiles] CHECK CONSTRAINT [FK_EmployeeFiles_Employees]
GO
ALTER TABLE [dbo].[EmployeeProjects]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeProjects_Employees] FOREIGN KEY([EmployeeProject_Employee])
REFERENCES [dbo].[Employees] ([Id])
ON DELETE SET DEFAULT
GO
ALTER TABLE [dbo].[EmployeeProjects] CHECK CONSTRAINT [FK_EmployeeProjects_Employees]
GO
ALTER TABLE [dbo].[EmployeeProjects]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeProjects_Projects] FOREIGN KEY([EmployeeProject_Project])
REFERENCES [dbo].[Projects] ([Id])
ON DELETE SET DEFAULT
GO
ALTER TABLE [dbo].[EmployeeProjects] CHECK CONSTRAINT [FK_EmployeeProjects_Projects]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Departments] FOREIGN KEY([Employee_Department])
REFERENCES [dbo].[Departments] ([Id])
ON DELETE SET DEFAULT
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Departments]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Positions] FOREIGN KEY([Employee_Position])
REFERENCES [dbo].[Positions] ([Id])
ON DELETE SET DEFAULT
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Positions]
GO
ALTER TABLE [dbo].[EmployeeSalaries]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeSalaries_Employees] FOREIGN KEY([EmployeeSalary_EmpId])
REFERENCES [dbo].[Employees] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmployeeSalaries] CHECK CONSTRAINT [FK_EmployeeSalaries_Employees]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_BusinessUnits] FOREIGN KEY([Project_BusinessUnit])
REFERENCES [dbo].[BusinessUnits] ([Id])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_BusinessUnits]
GO
/****** Object:  StoredProcedure [dbo].[BU_DeleteUnusedExistingRec]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE Procedure [dbo].[BU_DeleteUnusedExistingRec](@Id int)
AS
BEGIN

 DELETE FROM [dbo].[BusinessUnits]
      WHERE NOT EXISTS(SELECT * FROM [dbo].[Projects] WHERE [Project_BusinessUnit] = @Id) 
	  AND [Id] = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[DEPT_CreateNew]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[DEPT_CreateNew](
@Code nvarchar(10)
,@Name nvarchar(255)
,@Created datetimeoffset(7) = null
,@CreatedBy nvarchar(255) = null
,@Modified datetimeoffset(7) = null
,@ModifiedBy nvarchar(255) = null
)
AS
BEGIN

INSERT INTO [dbo].[Departments]
           ([Code]
           ,[Name]
           ,[Created]
           ,[CreatedBy]
           ,[Modified]
           ,[ModifiedBy])
     VALUES
           (@Code 
           ,@Name 
           ,@Created 
           ,@CreatedBy 
           ,@Modified 
           ,@ModifiedBy)
END
GO
/****** Object:  StoredProcedure [dbo].[DEPT_DeleteUnusedExistingRec]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create Procedure [dbo].[DEPT_DeleteUnusedExistingRec](@Id int)
AS
BEGIN

DELETE FROM [dbo].[Departments]
      WHERE NOT EXISTS(SELECT * FROM [dbo].[Employees] WHERE [Employee_Department] = @Id) 
	  AND [Id] = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[DEPT_ListAll]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[DEPT_ListAll]
AS
BEGIN

SELECT [Id]
      ,[Code]
      ,[Name] AS DEPARTMENT
      ,[Created]
      ,[CreatedBy]
      ,[Modified]
      ,[ModifiedBy]
      ,[RowVersion]
  FROM [dbo].[Departments]

END
GO
/****** Object:  StoredProcedure [dbo].[DEPT_ListAllAssignedEmployee]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DEPT_ListAllAssignedEmployee]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		D.Name As DEPARTMENT,
		E.EmployeeId As ID,
		Concat(E.FirstName, + ' ' + E.MiddleName, + ' ' + E.LastName)  AS Name, 
		E.EmailAddress As Email,
		P.Name As Position
	FROM Departments AS D 
			RIGHT JOIN Employees  AS E 
		ON D.Id = E.Employee_Department 
			INNER JOIN Positions AS P
		ON P.Id = E.Employee_Position
	ORDER BY D.Name


END
GO
/****** Object:  StoredProcedure [dbo].[DEPT_UpdateExistingDetails]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[DEPT_UpdateExistingDetails](
@Code nvarchar(10) = NULL
,@Name nvarchar(255) = NULL
,@Created datetimeoffset(7) = NULL
,@CreatedBy nvarchar(255) = NULL
,@Modified datetimeoffset(7) = NULL
,@ModifiedBy nvarchar(255) = NULL
,@Id int)
AS
BEGIN
UPDATE [dbo].[Departments]
   SET [Code] = ISNULL(@Code, [Code])
      ,[Name] = ISNULL(@Name, [Name])
      ,[Created] = ISNULL(@Created, [Created])
      ,[CreatedBy] = ISNULL(@CreatedBy, [CreatedBy])
      ,[Modified] = ISNULL(@Modified, [Modified])
      ,[ModifiedBy] = ISNULL(@ModifiedBy, [ModifiedBy])
 WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[EMP_AssignDepartment]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[EMP_AssignDepartment]
(@Employee_Department int = NULL
,@Id int)

AS
BEGIN

UPDATE [dbo].[Employees]
   SET 
      [Employee_Department] = @Employee_Department
 WHERE Id = @Id


END
GO
/****** Object:  StoredProcedure [dbo].[EMP_AssignManager]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[EMP_AssignManager]
(@Id int
,@ManagerId int)
AS
BEGIN
	IF EXISTS (SELECT E.Id
				  ,E.FirstName
				  ,E.MiddleName
				  ,E.LastName
				  ,P.Name AS Position
					FROM [Employees] AS E
					LEFT JOIN Positions AS P
					ON E.Employee_Position = P.Id
					WHERE P.Name LIKE '%Manager' And E.Id = @ManagerId)
		BEGIN
		UPDATE [dbo].[Employees]
		   SET 
			  [Manager] = (SELECT E.Id
							  FROM [Employees] AS E 
							  WHERE Id = @ManagerId)
		 WHERE Id = @Id
		
		END

END
GO
/****** Object:  StoredProcedure [dbo].[EMP_CreateNew]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE Procedure [dbo].[EMP_CreateNew](
@EmployeeId nvarchar(7)
,@FirstName nvarchar(50)
,@MiddleName nvarchar(50) = NULL
,@LastName nvarchar(50)
,@EmailAddress nvarchar(255) 
,@Employee_Position int 
,@Employee_Department int 
,@Created datetimeoffset(7) = NULL
,@CreatedBy nvarchar(255) = NULL
,@Modified datetimeoffset(7) = NULL
,@ModifiedBy nvarchar(255) = NULL
,@Manager nvarchar(150) = NULL
)
AS
BEGIN


INSERT INTO [dbo].[Employees]
           ([EmployeeId]
           ,[FirstName]
           ,[MiddleName]
           ,[LastName]
           ,[EmailAddress]
           ,[Employee_Position]
           ,[Employee_Department]
           ,[Created]
           ,[CreatedBy]
           ,[Modified]
           ,[ModifiedBy]
           ,[Manager])
     VALUES
			(@EmployeeId
           ,@FirstName
           ,@MiddleName
           ,@LastName 
           ,@EmailAddress
           ,@Employee_Position
           ,@Employee_Department
           ,@Created 
           ,@CreatedBy
           ,@Modified
           ,@ModifiedBy
           ,@Manager )



END
GO
/****** Object:  StoredProcedure [dbo].[EMP_DeleteExistingRecord]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[EMP_DeleteExistingRecord]
(@Id int)

AS
BEGIN

DELETE FROM [dbo].[Employees]
WHERE NOT EXISTS(SELECT * FROM [dbo].[EmployeeProjects] WHERE [EmployeeProject_Employee] = @Id) 
AND NOT EXISTS(SELECT * FROM [dbo].[EmployeeFiles] WHERE [EmployeeId] = @Id) 
AND [Id] = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[EMP_listAllEmp_deptName]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[EMP_listAllEmp_deptName]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here


SELECT E.EmployeeId
      ,CONCAT(E.FirstName + ' ',  E.MiddleName + ' ' ,E.LastName) AS Name
      ,D.Name AS Department
	  ,(SELECT CONCAT(FirstName + ' ',  MiddleName + ' ' ,LastName) FROM Employees WHERE Id = E.Manager ) AS Manager
  FROM [dbo].[Employees] As E
  LEFT JOIN [dbo].[Departments] As D
  ON E.Employee_Department = D.Id

END

  
GO
/****** Object:  StoredProcedure [dbo].[EMP_UpdateExistingDetails]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[EMP_UpdateExistingDetails]
(@EmployeeId nvarchar(7) = NULL
,@FirstName nvarchar(50) = NULL
,@MiddleName nvarchar(50) = NULL
,@LastName nvarchar(50) = NULL
,@EmailAddress nvarchar(255) = NULL
,@Employee_Position int = NULL
,@Employee_Department int = NULL
,@Created datetimeoffset(7) = NULL
,@CreatedBy nvarchar(255) = NULL
,@Modified datetimeoffset(7) = NULL
,@ModifiedBy nvarchar(255) = NULL
,@Manager nvarchar(150) = NULL
,@Id int)

AS
BEGIN

UPDATE [dbo].[Employees]
   SET [EmployeeId] = @EmployeeId 
      ,[FirstName] = @FirstName 
      ,[MiddleName] = @MiddleName 
      ,[LastName] = @LastName
      ,[EmailAddress] = @EmailAddress 
      ,[Employee_Position] = @Employee_Position
      ,[Employee_Department] = @Employee_Department
      ,[Created] = @Created
      ,[CreatedBy] = @CreatedBy
      ,[Modified] = @Modified 
      ,[ModifiedBy] = @ModifiedBy
      ,[Manager] = @Manager
 WHERE Id = @Id


END
GO
/****** Object:  StoredProcedure [dbo].[FILE_EMP_CreateNewRecord]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[FILE_EMP_CreateNewRecord](
@FileName nvarchar(255)
,@pathfile nvarchar(4000)
,@ContentType nvarchar(255) = null
,@EmployeeId int
,@Created datetimeoffset(7) = null
,@CreatedBy nvarchar(255) = null
,@Modified datetimeoffset(7) = null
,@ModifiedBy nvarchar(255) = null)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
DECLARE @file AS nvarchar(4000);
SET @file = CONCAT(@pathfile, + '\' + @FileName)

INSERT INTO [dbo].[EmployeeFiles]
	([FileName]
	,[FileBytes]
	,[ContentType]
	,[EmployeeId]
	,[Created]
	,[CreatedBy]
	,[Modified]
	,[ModifiedBy])
SELECT @FileName, 
		BulkColumn, 
		@ContentType, 
		@EmployeeId, 
		@Created, 
		@CreatedBy,
		@Modified, 
		@ModifiedBy
 
FROM OPENROWSET(Bulk N'@file' , SINGLE_BLOB) AS BLOB

REturn @file
END
GO
/****** Object:  StoredProcedure [dbo].[POS_CreateNew]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[dept_CreateNew]    Script Date: 08/11/2021 8:57:03 AM ******/
Create Procedure [dbo].[POS_CreateNew](
@Name nvarchar(255)
,@Description nvarchar(500) = null
,@Created datetimeoffset(7) = null
,@CreatedBy nvarchar(255) = null
,@Modified datetimeoffset(7) = null
,@ModifiedBy nvarchar(255) = null
)
AS
BEGIN


INSERT INTO [dbo].[Positions]
           ([Name]
           ,[Description]
           ,[Created]
           ,[CreatedBy]
           ,[Modified]
           ,[ModifiedBy])
     VALUES
           (@Name
           ,@Description
           ,@Created
           ,@CreatedBy
           ,@Modified 
           ,@ModifiedBy)

END
GO
/****** Object:  StoredProcedure [dbo].[POS_DeleteUnusedExistingRec]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE Procedure [dbo].[POS_DeleteUnusedExistingRec](@Id int)
AS
BEGIN

DELETE FROM [dbo].[Positions]
      WHERE NOT EXISTS(SELECT * FROM [dbo].[Employees] WHERE [Employee_Position] = @Id) 
	  AND [Id] = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[POS_ListAll]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[POS_ListAll]
AS
BEGIN


SELECT [Id]
      ,[Name] AS POSITION
      ,[Description]
      ,[Created]
      ,[CreatedBy]
      ,[Modified]
      ,[ModifiedBy]
      ,[RowVersion]
  FROM [dbo].[Positions]



END
GO
/****** Object:  StoredProcedure [dbo].[POS_ListAllAssignedEmployee]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[POS_ListAllAssignedEmployee]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		P.Name As POSITION,
		E.EmployeeId As ID,
		Concat(E.FirstName, + ' ' + E.MiddleName, + ' ' + E.LastName)  AS Name, 
		E.EmailAddress As Email,
		D.Name As Department
	FROM Positions AS P 
		RIGHT JOIN Employees  AS E 
	ON P.Id = E.Employee_Position
		INNER JOIN Departments AS D
	ON D.Id = E.Employee_Department
	ORDER BY P.Name


END
GO
/****** Object:  StoredProcedure [dbo].[POS_UpdateExistingDetails]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[POS_UpdateExistingDetails](
@Name nvarchar(255) = NULL
,@Description nvarchar(500) = NULL
,@Created datetimeoffset(7) = NULL
,@CreatedBy nvarchar(255) = NULL
,@Modified datetimeoffset(7) = NULL
,@ModifiedBy nvarchar(255) = NULL
,@Id int)
AS
BEGIN



UPDATE [dbo].[Positions]
   SET [Name] = ISNULL(@Name, [Name])
      ,[Description] = ISNULL(@Description, [Description])
      ,[Created] = ISNULL(@Created, [Created])
      ,[CreatedBy] = ISNULL(@CreatedBy, [CreatedBy])
      ,[Modified] = ISNULL(@Modified, [Modified])
      ,[ModifiedBy] = ISNULL(@ModifiedBy, [ModifiedBy])
 WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[PROJECT_DeleteExistingRecord]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[PROJECT_DeleteExistingRecord]
(@Id int)

AS
BEGIN

DELETE FROM [dbo].[Projects]
WHERE NOT EXISTS(SELECT * FROM [dbo].[EmployeeProjects] WHERE [EmployeeProject_Project] = @Id) 
AND [Id] = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[sp_BU_DeleteUnusedExistingRec]    Script Date: 10/12/2021 6:03:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Fritz Batin
-- Create date: Nov 24, 2021
-- Description:	Delete unused existing records
-- =============================================
CREATE Procedure [dbo].[sp_BU_DeleteUnusedExistingRec](@Id int)
AS
BEGIN

 DELETE FROM [dbo].[BusinessUnits]
      WHERE NOT EXISTS(SELECT * FROM [dbo].[Projects] WHERE [Project_BusinessUnit] = @Id) 
	  AND [Id] = @Id

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Employees"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 247
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Departments"
            Begin Extent = 
               Top = 11
               Left = 396
               Bottom = 141
               Right = 566
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 4320
         Alias = 900
         Table = 1245
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vEmployeeListPerDepartment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vEmployeeListPerDepartment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Positions"
            Begin Extent = 
               Top = 44
               Left = 40
               Bottom = 174
               Right = 210
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Employees"
            Begin Extent = 
               Top = 8
               Left = 283
               Bottom = 229
               Right = 492
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 4035
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vEmployeeListPerPosition'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vEmployeeListPerPosition'
GO
USE [master]
GO
ALTER DATABASE [FileRepositorySystem_DEV] SET  READ_WRITE 
GO
