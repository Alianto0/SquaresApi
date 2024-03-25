﻿/*
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
  SELECT '91BDABC6-B60C-43BF-9486-02DA55ED937D' AS [PointsCollectionId] UNION
  SELECT '835FD0CA-2407-4D06-8D5A-4C4A94B1D671' UNION
  SELECT '1294DAD6-0EB1-4B4F-8492-F298C54FE603'
)
SELECT *
INTO #PointsCollectionTestData
FROM ctePointsCollectionTestData;

WITH ctePointsTestData AS
(
  SELECT '91BDABC6-B60C-43BF-9486-02DA55ED937D' AS PointsCollectionId, 1 AS XCoordinate, 1 AS YCoordinate UNION
  SELECT '91BDABC6-B60C-43BF-9486-02DA55ED937D',  2, 2 UNION
  SELECT '835FD0CA-2407-4D06-8D5A-4C4A94B1D671',  1,2
)
SELECT *
INTO #PointsTestData
FROM ctePointsTestData


-- Delete test data
DELETE FROM [dbo].[PointsCollection] WHERE PointsCollectionId IN (SELECT PointsCollectionId FROM #PointsCollectionTestData )

-- Insert test data
INSERT INTO [dbo].[PointsCollection]
           (PointsCollectionId)
SELECT [PointsCollectionId] FROM #PointsCollectionTestData 

INSERT INTO [dbo].[Point]
           (PointsCollectionId, XCoordinate, YCoordinate)
SELECT PointsCollectionId, XCoordinate, YCoordinate FROM #PointsTestData 

GO