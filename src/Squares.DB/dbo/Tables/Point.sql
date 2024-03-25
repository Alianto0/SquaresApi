CREATE TABLE [dbo].[Point] (
    [PointId] INT NOT NULL IDENTITY, 
    [PointsCollectionId] UNIQUEIDENTIFIER NOT NULL,
    [XCoordinate]       INT              NOT NULL,
    [YCoordinate]       INT              NOT NULL,
    
    CONSTRAINT [PK_Point] PRIMARY KEY ([PointId]), 
    CONSTRAINT [FK_Point_PointsCollection] FOREIGN KEY ([PointsCollectionId]) REFERENCES [PointsCollection]([PointsCollectionId]), 
    CONSTRAINT [CK_Point_Column] UNIQUE ([PointsCollectionId], [XCoordinate], [YCoordinate])
);

