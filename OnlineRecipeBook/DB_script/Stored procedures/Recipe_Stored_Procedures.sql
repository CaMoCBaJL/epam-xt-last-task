USE RecipeBookDataBase
GO

CREATE PROCEDURE GetRecipes
AS
BEGIN
	SELECT * FROM [dbo].[Recipe]
END
GO

CREATE PROCEDURE GetUserRecipes
@UserId INT
AS
BEGIN
	SELECT * FROM [dbo].[Recipe] WHERE [dbo].[Recipe].[Id] IN
	(SELECT [dbo].[UserRecipes].[RecipeId] FROM [dbo].[UserRecipes] 
	WHERE [dbo].[UserRecipes].[UserId] = @UserId)
END
GO

CREATE PROCEDURE FindRecipeWithUserComment -- BY comment id?
@CommentId INT
AS
BEGIN
	SELECT [dbo].[RecipeComments].[RecipeId] FROM [dbo].[RecipeComments] WHERE [dbo].[RecipeComments].[CommentId] = @CommentId
END
GO

CREATE PROCEDURE AddRecipe
@RecipeTitle NVARCHAR(100),
@Ingridients TEXT,
@CookingProcess TEXT,
@UserId INT
AS
BEGIN
	BEGIN TRANSACTION
		
		INSERT INTO [dbo].[Recipe] VALUES (@RecipeTitle, @Ingridients,
		@CookingProcess)

		IF (@@ERROR <> 0)
			ROLLBACK

		DECLARE @recipeId int = SCOPE_IDENTITY(); -- basically returns last value, created by IDENTITY column, see docs

		INSERT INTO [dbo].[UserRecipes] VALUES 
		(@UserId, @recipeId)

		IF (@@ERROR <> 0)
		ROLLBACK

	COMMIT
END
GO

CREATE PROCEDURE RateRecipe
@RecipeId INT,
@UserId INT,
@Award INT
AS 
BEGIN
	INSERT INTO [dbo].[RecipeAwards] VALUES (@RecipeId, @UserId, @Award)
END
GO

CREATE PROCEDURE RemoveRecipe
@RecipeId INT
AS
BEGIN
	DELETE FROM [dbo].[Recipe] WHERE [dbo].[Recipe].[Id] = @RecipeId
END
GO

CREATE PROCEDURE ChangeRecipe
@RecipeId INT,
@RecipeTitle NVARCHAR(255),
@CookingProcess TEXT,
@Ingridients TEXT
AS
BEGIN
	UPDATE [dbo].[Recipe]
	SET [dbo].[Recipe].[Title] = @RecipeTitle,
	[dbo].[Recipe].[CookingProcess] = @CookingProcess,
	[dbo].[Recipe].[Ingridients] = @Ingridients
	WHERE [dbo].[Recipe].[Id] = @RecipeId
END
GO

CREATE PROCEDURE GetRecipeAward
@RecipeId INT
AS
BEGIN
	SELECT CAST(SUM([dbo].[RecipeAwards].[AwardValue]) AS FLOAT) / COUNT([dbo].[RecipeAwards].[AwardValue]) AS Award -- you've invented AVG(CAST(AwardValue as float))
	FROM [dbo].[RecipeAwards]
	WHERE [dbo].[RecipeAwards].[RecipeId] = @RecipeId
END
