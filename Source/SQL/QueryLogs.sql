/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [LogId]
      ,[UtcTime]
      ,[IPAddress]
      ,[SessionId]
      ,[Action]
      ,[Parameters]
      ,[FormattedMessage]
  FROM [andrasfuchs_mrkupido].[dbo].[Logs]
  --WHERE ACTION NOT LIKE 'URL%'
  WHERE ACTION LIKE 'URL%q=%'
  ORDER BY [UtcTime] DESC

  /****** Script for SelectTopNRows command from SSMS  ******/
SELECT [SessionId]
      ,COUNT(*) AS Aktivitas
	  ,CAST([UtcTime] AS Date)
	  ,MIN([UtcTime])
	  ,MAX([UtcTime])	  
  FROM [andrasfuchs_mrkupido].[dbo].[Logs]
  --WHERE ACTION NOT LIKE 'URL%'
  --WHERE ACTION LIKE 'URL%q=%'
  WHERE [UtcTime] > '2013-04-01'
  GROUP BY [SessionId], CAST([UtcTime] AS Date)
  ORDER BY [SessionId], [Aktivitas] DESC