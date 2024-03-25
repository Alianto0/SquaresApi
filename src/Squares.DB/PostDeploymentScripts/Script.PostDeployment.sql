/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
USE [Squares]
GO

IF OBJECT_ID(N'tempdb..#PointsCollectionTestData') IS NOT NULL
BEGIN
DROP TABLE #PointsCollectionTestData
END

IF OBJECT_ID(N'tempdb..#PointsTestData') IS NOT NULL
BEGIN
DROP TABLE #PointsTestData
END
GO

WITH ctePointsCollectionTestData AS
(
  SELECT '91BDABC6-B60C-43BF-9486-02DA55ED937D' AS [PointsCollectionId] UNION -- 2 points
  SELECT '835FD0CA-2407-4D06-8D5A-4C4A94B1D671' UNION -- 1 point
  SELECT '1294DAD6-0EB1-4B4F-8492-F298C54FE603' UNION --no points
  SELECT '6679D2C8-60AB-46E4-BFE2-2D77F226D8B1' UNION -- one square
   SELECT '3B459E70-659C-4CEC-916F-28EFFB7293B1' -- Multiple squares
)
SELECT *
INTO #PointsCollectionTestData
FROM ctePointsCollectionTestData;

WITH ctePointsTestData AS
(
  SELECT '91BDABC6-B60C-43BF-9486-02DA55ED937D' AS PointsCollectionId, 1 AS XCoordinate, 1 AS YCoordinate UNION
  SELECT '91BDABC6-B60C-43BF-9486-02DA55ED937D',  2, 2 UNION
  SELECT '835FD0CA-2407-4D06-8D5A-4C4A94B1D671',  1,2 UNION
  SELECT '6679D2C8-60AB-46E4-BFE2-2D77F226D8B1',  -1,1 UNION
  SELECT '6679D2C8-60AB-46E4-BFE2-2D77F226D8B1',  1,1 UNION
  SELECT '6679D2C8-60AB-46E4-BFE2-2D77F226D8B1',  1,-1 UNION
  SELECT '6679D2C8-60AB-46E4-BFE2-2D77F226D8B1',  -1,-1 UNION

  SELECT '3B459E70-659C-4CEC-916F-28EFFB7293B1',  0,0 UNION
  SELECT '3B459E70-659C-4CEC-916F-28EFFB7293B1',  10,0 UNION
  SELECT '3B459E70-659C-4CEC-916F-28EFFB7293B1',  20,0 UNION
  SELECT '3B459E70-659C-4CEC-916F-28EFFB7293B1',  0,10 UNION
  SELECT '3B459E70-659C-4CEC-916F-28EFFB7293B1',  10,10 UNION
  SELECT '3B459E70-659C-4CEC-916F-28EFFB7293B1',  20,10 UNION
  SELECT '3B459E70-659C-4CEC-916F-28EFFB7293B1',  0,20 UNION
  SELECT '3B459E70-659C-4CEC-916F-28EFFB7293B1',  10,20 UNION
  SELECT '3B459E70-659C-4CEC-916F-28EFFB7293B1',  20,20 
)
SELECT *
INTO #PointsTestData
FROM ctePointsTestData


-- Delete test data
DELETE FROM [dbo].[Point] WHERE PointsCollectionId IN (SELECT PointsCollectionId FROM #PointsCollectionTestData )
DELETE FROM [dbo].[PointsCollection] WHERE PointsCollectionId IN (SELECT PointsCollectionId FROM #PointsCollectionTestData )

-- Insert test data
INSERT INTO [dbo].[PointsCollection]
           (PointsCollectionId)
SELECT [PointsCollectionId] FROM #PointsCollectionTestData 

INSERT INTO [dbo].[Point]
           (PointsCollectionId, XCoordinate, YCoordinate)
SELECT PointsCollectionId, XCoordinate, YCoordinate FROM #PointsTestData 

GO
IF NOT EXISTS 
    (SELECT name  
     FROM master.sys.server_principals
     WHERE name = 'SquaresApi')
BEGIN
CREATE LOGIN [SquaresApi] WITH PASSWORD=N'FN+DHMly8u8UgHi3tsnsOIv+3lJxA0fjRbWwCZbs1Oo=', DEFAULT_DATABASE=[Squares], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
END
GO
USE [Squares]

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'SquaresApi')
BEGIN
CREATE USER [SquaresApi] FOR LOGIN [SquaresApi]
END
GO
USE [Squares]
GO
ALTER ROLE [db_owner] ADD MEMBER [SquaresApi]
GO

GO