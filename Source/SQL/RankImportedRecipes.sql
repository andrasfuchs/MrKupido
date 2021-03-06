/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 100 -- of 26972
      [UniqueName]
      ,[Rating]
      ,([Favourited]+[Forwarded]) AS Likes
      ,[Ingredients]
      ,[Tags]
      --,(Favourited+Forwarded)*1000 / DATEDIFF(DAY, UploadedOn, GETDATE()) AS Points
      ,(Favourited+Forwarded) AS Points
      ,[Directions]
  FROM [MrKupido.DataAccess.MrKupidoContext].[dbo].[ImportedRecipes]
  WHERE [UploadedOn] < '2011-01-01'
    AND [Favourited] > 500
	AND [Rating] >= 5
	AND [Language] = 'hun'
  ORDER BY Points DESC
  
  
  SELECT TOP 175 -- of 47113
      [UniqueName]
      ,[Rating]
      ,[ReviewCount]
      ,[Ingredients]
      ,[Tags]
  FROM [MrKupido.DataAccess.MrKupidoContext].[dbo].[ImportedRecipes]
  WHERE [ReviewCount] > 500
	AND [Rating] >= 4
	AND [Language] = 'eng'
  ORDER BY [Rating] DESC, [ReviewCount] DESC