BULK
INSERT [MrKupido.DataAccess.MrKupidoContext].[dbo].[ImportedRecipes]
FROM 'c:\Work\MrKupido\DB\data\MrKupido_ImportedRecipes_data.csv'
WITH
(
FIELDTERMINATOR = ',',
ROWTERMINATOR = '\n',
KEEPIDENTITY,
FIRSTROW = 2,
--FORMATFILE = 'c:\Work\MrKupido\DB\data\MrKupido_ImportedRecipes_format.xml',
KEEPNULLS
)
GO