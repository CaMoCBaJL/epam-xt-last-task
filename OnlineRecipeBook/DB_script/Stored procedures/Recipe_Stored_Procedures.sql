USE RecipeBookDataBase

CREATE PROCEDURE GetRecipes
AS
BEGIN
	SELECT * FROM [dbo].[Recipe]
END

CREATE PROCEDURE GetUserRecipes
@UserId INT
AS
BEGIN
	SELECT * FROM [dbo].[Recipe] WHERE [dbo].[Recipe].[Id] IN
	(SELECT [dbo].[UserRecipes].[RecipeId] FROM [dbo].[UserRecipes] 
	WHERE [dbo].[UserRecipes].[UserId] = @UserId)
END

CREATE PROCEDURE FindRecipeWithUserComment
@CommentId INT
AS
BEGIN
	SELECT [dbo].[RecipeComments].[RecipeId] FROM [dbo].[RecipeComments] WHERE [dbo].[RecipeComments].[CommentId] = @CommentId
END

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

		INSERT INTO [dbo].[UserRecipes] VALUES 
		(@UserId, 
		(SELECT MAX([dbo].[Recipe].[Id]) FROM [dbo].[Recipe]))

		IF (@@ERROR <> 0)
		ROLLBACK

	COMMIT
END

CREATE PROCEDURE RateRecipe
@RecipeId INT,
@UserId INT,
@Award INT
AS 
BEGIN
	INSERT INTO [dbo].[RecipeAwards] VALUES (@RecipeId, @UserId, @Award)
END

CREATE PROCEDURE RemoveRecipe
@RecipeId INT
AS
BEGIN
	DELETE FROM [dbo].[Recipe] WHERE [dbo].[Recipe].[Id] = @RecipeId
END

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

CREATE PROCEDURE GetRecipeAward
@RecipeId INT
AS
BEGIN
	SELECT CAST(SUM([dbo].[RecipeAwards].[AwardValue]) AS FLOAT) /
	COUNT([dbo].[RecipeAwards].[AwardValue]) AS Award
	FROM [dbo].[RecipeAwards]
	WHERE [dbo].[RecipeAwards].[RecipeId] = @RecipeId
END
