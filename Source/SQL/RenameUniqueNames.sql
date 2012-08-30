UPDATE [MrKupido.DataAccess.MrKupidoContext].[dbo].[ImportedRecipes]
SET [UniqueName] = [UniqueName] + '-hun'
WHERE [UniqueName] = 'brownies-iii' 
AND [Language] = 'hun'

