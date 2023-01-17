USE [WasteRecyclingDb]
GO

INSERT INTO [dbo].[RecyclingPoints]
           ([Name]
           ,[Latitude]
           ,[Longitude])
     VALUES
           ('Recycling Point Test', 0, 0)
GO

INSERT INTO [dbo].[Containers]
           ([RecyclingPointId]
           ,[Type]
           ,[MeasureUnit]
           ,[TotalCapacity]
           ,[Occupied])
     VALUES
           (1, 'Paper', 'kg', 100, 0)  
GO

INSERT INTO [dbo].[Users]
           ([Username]
           ,[Password]
           ,[Role])
     VALUES
           ('User', 'qwaszx1234qwerty', 0)
GO

INSERT INTO [dbo].[Users]
           ([Username]
           ,[Password]
           ,[Role])
     VALUES
           ('Employee', 'qwaszx1234qwerty', 1)
GO