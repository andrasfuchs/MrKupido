/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [LogId]
      ,[UtcTime]
      ,[IPAddress]
      ,[SessionId]
      ,[Action]
      ,[Parameters]
      ,[FormattedMessage]
  FROM [andrasfuchs_mrkupido].[dbo].[Logs]
  WHERE ACTION NOT LIKE 'URL%'
  ORDER BY [UtcTime] DESC