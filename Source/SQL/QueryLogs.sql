/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [LogId]
      ,[UtcTime]
      ,[IPAddress]
      ,[SessionId]
      ,[Action]
      ,[Parameters]
      ,[FormattedMessage]
  FROM [MrKupido.DataAccess.MrKupidoContext].[dbo].[Logs]
  WHERE ACTION NOT LIKE 'URL%'
  ORDER BY [UtcTime] DESC