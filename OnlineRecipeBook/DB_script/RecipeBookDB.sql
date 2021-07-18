CREATE DATABASE RecipeBookDataBase

USE RecipeBookDataBase

CREATE TABLE AppUser
(
	[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[UserName] NVARCHAR(255) NOT NULL,
	[Age] INT NOT NULL
)

select * from Recipe

CREATE TABLE Recipe
(
	[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Title] NVARCHAR(100),
	[Ingridients] TEXT NOT NULL,
	[CookingProcess] TEXT NOT NULL,
)

CREATE TABLE Commentary
(
	[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Text] TEXT NOT NULL
)

CREATE TABLE UserCommentaries
(
	[UserId] INT FOREIGN KEY REFERENCES AppUser(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	[CommentId] INT FOREIGN KEY REFERENCES Commentary(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT UserCommentsAreUnique UNIQUE (UserId, CommentId)
)

CREATE TABLE UserRecipes
(
	[UserId] INT FOREIGN KEY REFERENCES AppUser(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	[RecipeId] INT FOREIGN KEY REFERENCES Recipe(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT UserRecipesAreUnique UNIQUE (UserId, RecipeId)
)

exec AddRecipe 'Кашка наташка', 'жопа-попа, крюк-хук', 'Берем берем берем берем', 16

 select * from UserRecipes

CREATE TABLE UserIdentity
(
	[UserId] INT FOREIGN KEY REFERENCES AppUser(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	[UserLogin] NVARCHAR(100) NOT NULL,
	[UserPassword] NVARCHAR(255) NOT NULL
)

exec AddRecipe 'ЖОПА', 'МОЛОЧКО', 'ЗАМИСИ', 1

exec GetUserRecipes 1

DECLARE @variable INT

exec @variable = CheckIdentity 3, '76 208 246 126 53 74 39 55 223 167 67 206 44 35 186 202 24 237 141 110 240 228 153 55 166 169 140 139 180 6 144 113'

print @variable

select * from UserIdentity

delete from UserIdentity

drop table RecipeAwards

CREATE TABLE RecipeAwards
(
	[RecipeId] INT FOREIGN KEY REFERENCES Recipe(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	[UserId] INT FOREIGN KEY REFERENCES AppUser(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	[AwardValue] INT
)

CREATE TABLE UserReactions
(
	[UserId] INT FOREIGN KEY REFERENCES AppUser(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	[CommentId] INT FOREIGN KEY REFERENCES Commentary(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	[Award] BIT,
	CONSTRAINT UserReactionsAreUnique UNIQUE (UserId, CommentId)
)

CREATE TABLE RecipeComments
(
	[CommentId] INT FOREIGN KEY REFERENCES Commentary(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	[RecipeId] INT FOREIGN KEY REFERENCES Recipe(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT CommentsAreUnique UNIQUE (CommentId, RecipeId)
)

CREATE PROCEDURE GetAppUser
@UserId INT
AS
BEGIN
	SELECT * FROM [dbo].[AppUser] WHERE [dbo].[AppUser].[Id] = @UserId
END

CREATE PROCEDURE GetUsers
AS
BEGIN
	SELECT * FROM [dbo].[AppUser]
END

ALTER PROCEDURE GetUserIdentities
AS
BEGIN
	SELECT * FROM [dbo].[UserIdentity]
END

exec GetUserIdentity 1

ALTER PROCEDURE	GetUserIdentity
@UserId INT
AS
BEGIN
	SELECT * FROM [dbo].[UserIdentity] WHERE [dbo].[UserIdentity].[UserId] = @UserId
END

CREATE PROCEDURE GetRecipes
AS
BEGIN
	SELECT * FROM [dbo].[Recipe]
END

CREATE PROCEDURE GetComments
AS
BEGIN
	SELECT * FROM [dbo].[Commentary]
END

CREATE PROCEDURE GetUserComments
@UserId INT
AS
BEGIN
	SELECT * FROM [dbo].[Commentary] WHERE [dbo].[Commentary].[Id] IN
	(SELECT [dbo].[UserCommentaries].[CommentId] FROM [dbo].[UserCommentaries] 
	WHERE [dbo].[UserCommentaries].[UserId] = @UserId)
END

ALTER PROCEDURE GetUserRecipes
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

select * from Recipe

EXEC AddRecipe 'Рецепт 3', '1:1 2:2', 'ВАРИТЬСЯ', 15

EXEC GetUserRecipes 1

select * from UserIdentity

ALTER PROCEDURE AddUser
@UserName NVARCHAR(255),
@UserAge int,
@Login NVARCHAR(100),
@PasswordHashSum NVARCHAR(255)
AS
BEGIN
	BEGIN TRANSACTION

		INSERT INTO [dbo].[AppUser] VALUES (@UserName, @UserAge)

		IF (@@ERROR <> 0)
		ROLLBACK

		INSERT INTO [dbo].[UserIdentity] VALUES 
		((SELECT MAX([dbo].[AppUser].[Id]) FROM [dbo].[AppUser]),
		@Login, @PasswordHashSum)

		IF (@@ERROR <> 0)
		ROLLBACK

	COMMIT
END

ALTER PROCEDURE AddRecipe
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

ALTER PROCEDURE AddCommentary
@RecipeId INT,
@Text TEXT,
@UserId INT
AS
BEGIN
		INSERT INTO [dbo].[Commentary] VALUES (@Text)

		IF (@@ERROR <> 0)
		ROLLBACK

		INSERT INTO [dbo].[UserCommentaries] VALUES 
		(@UserId, 
		(SELECT MAX([dbo].[Commentary].[Id]) FROM [dbo].[Commentary]))

		IF (@@ERROR <> 0)
		ROLLBACK

		INSERT INTO [dbo].[RecipeComments] VALUES
		((SELECT MAX([dbo].[Commentary].[Id]) FROM [dbo].[Commentary]),
		@RecipeId)

		IF (@@ERROR <> 0)
		ROLLBACK
END

ALTER PROCEDURE LikeTheComment
@UserId INT,
@CommentaryId INT
AS
BEGIN
	EXEC CommentAwarding @CommentaryId, @UserId, 1
END

ALTER PROCEDURE DislikeTheComment
@UserId INT,
@CommentaryId INT
AS
BEGIN
	EXEC CommentAwarding @CommentaryId, @UserId, 0
END

CREATE PROCEDURE CommentAwarding
@CommentId INT,
@UserId INT,
@NewAwardValue BIT
AS
BEGIN
	DECLARE @AwardValue BIT

	SELECT @AwardValue=[dbo].[UserReactions].[Award]
	FROM [dbo].[UserReactions]
	WHERE [dbo].[UserReactions].[CommentId] = @CommentId AND
	[dbo].[UserReactions].[UserId] = @UserId 

	IF (@AwardValue = NULL)
		BEGIN
			INSERT INTO [dbo].[UserReactions] VALUES
			(@UserId, @CommentId, @NewAwardValue)
		END
	ELSE
		BEGIN
			IF(@AwardValue = @NewAwardValue)
				DELETE FROM [dbo].[UserReactions]
				WHERE [dbo].[UserReactions].[UserId] = @UserId AND
				[dbo].[UserReactions].[CommentId] = @CommentId 
			ELSE
				BEGIN
					DELETE FROM [dbo].[UserReactions]
					WHERE [dbo].[UserReactions].[UserId] = @UserId AND
					[dbo].[UserReactions].[CommentId] = @CommentId

					INSERT INTO [dbo].[UserReactions] VALUES
					(@UserId, @CommentId, @NewAwardValue)
				END
		END
END

select * from UserReactions

exec GetCommentLikes 2

exec LikeTheComment 1, 6

ALTER PROCEDURE GetCommentLikes
@CommentId INT
AS
BEGIN
	SELECT COUNT([dbo].[UserReactions].[Award]) as LikesCounter
	FROM [dbo].[UserReactions]
	WHERE [dbo].[UserReactions].[Award] = 1 AND [dbo].[UserReactions].[CommentId] = @CommentId
END

ALTER PROCEDURE GetCommentDislikes
@CommentId INT
AS
BEGIN
	SELECT COUNT([dbo].[UserReactions].[Award]) as DislikesCounter
	FROM [dbo].[UserReactions]
	WHERE [dbo].[UserReactions].[Award] = 0 AND [dbo].[UserReactions].[CommentId] = @CommentId
END

ALTER PROCEDURE RateRecipe
@RecipeId INT,
@UserId INT,
@Award INT
AS 
BEGIN
	INSERT INTO [dbo].[RecipeAwards] VALUES (@RecipeId, @UserId, @Award)
END

CREATE PROCEDURE RemoveComment
@CommentId INT
AS
BEGIN
	DELETE FROM [dbo].[Commentary] WHERE [dbo].[Commentary].[Id] = @CommentId
END

CREATE PROCEDURE RemoveUser
@UserId INT
AS
BEGIN
	DELETE FROM [dbo].[AppUser] WHERE [dbo].[AppUser].[Id] = @UserId
END

CREATE PROCEDURE RemoveRecipe
@RecipeId INT
AS
BEGIN
	DELETE FROM [dbo].[Recipe] WHERE [dbo].[Recipe].[Id] = @RecipeId
END

ALTER PROCEDURE ChangeUserIdentity
@UserId INT,
@Login NVARCHAR(100),
@Password NVARCHAR(100)
AS
BEGIN
	UPDATE [dbo].[UserIdentity]
	SET [dbo].[UserIdentity].[UserLogin] = @Login,
	[dbo].[UserIdentity].[UserPassword] = @Password
	WHERE [dbo].[UserIdentity].[UserId] = @UserId
END

CREATE PROCEDURE ChangeAppUser
@UserId INT,
@UserName NVARCHAR(255),
@UserAge INT
AS
BEGIN
	UPDATE [dbo].[AppUser]
	SET [dbo].[AppUser].[UserName] = @UserName,
	[dbo].[AppUser].[Age] = @UserAge
	WHERE [dbo].[AppUser].[Id] = @UserId
END

ALTER PROCEDURE ChangeRecipe
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

CREATE PROCEDURE ChangeComment
@CommentId INT,
@Text TEXT
AS
BEGIN
	UPDATE [dbo].[Commentary]
	SET [dbo].[Commentary].[Text] = @Text
	WHERE [dbo].[Commentary].[Id] = @CommentId
END

ALTER PROCEDURE GetRecipeComments
@RecipeId INT
AS 
BEGIN
	SELECT * FROM [dbo].[Commentary] JOIN [dbo].[RecipeComments]
	ON [dbo].[Commentary].[Id] = [dbo].[RecipeComments].[CommentId]
	WHERE [dbo].[RecipeComments].[RecipeId] = @RecipeId
END

ALTER PROCEDURE GetRecipeAward
@RecipeId INT
AS
BEGIN
	SELECT SUM([dbo].[RecipeAwards].[AwardValue]) /
	COUNT([dbo].[RecipeAwards].[AwardValue]) AS Award
	FROM [dbo].[RecipeAwards]
	WHERE [dbo].[RecipeAwards].[RecipeId] = @RecipeId
END

ALTER PROCEDURE CheckIdentity
@UserId int,
@PasswordHashSum nvarchar(255)
AS
BEGIN
	IF (SELECT [dbo].[UserIdentity].[UserPassword] FROM [dbo].[UserIdentity]
	WHERE @UserId = [dbo].[UserIdentity].[UserId]) = @PasswordHashSum
		RETURN 1
	ELSE
		RETURN 0
END

exec LikeTheComment 1, 1

CREATE PROCEDURE GetCommentAuthor
@CommentId INT
AS
BEGIN
	SELECT [dbo].[UserCommentaries].[UserId] FROM [dbo].[UserCommentaries]
	WHERE [dbo].[UserCommentaries].[CommentId] = @CommentId
END

CREATE PROCEDURE GetUserAward
@UserId INT
AS
BEGIN
	SELECT [dbo].[RecipeAwards].[AwardValue] FROM [dbo].[RecipeAwards]
	WHERE [dbo].[RecipeAwards].[UserId] = @UserId
END

CREATE PROCEDURE GetRecipeAuthor
@RecipeId INT
AS
BEGIN
	SELECT [dbo].[UserRecipes].[UserId] FROM [dbo].[UserRecipes]
	WHERE [dbo].[UserRecipes].[RecipeId] = @RecipeId
END

exec AddCommentary 1, 'ляляля', 1

IF ((SELECT [dbo].[UserReactions].[Award] FROM [dbo].[UserReactions]) = NULL)
print 'NULL'
ELSE
print 'NOT NULL'