CREATE DATABASE RecipeBookDataBase

USE RecipeBookDataBase

CREATE TABLE AppUser
(
	[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[UserName] NVARCHAR(255) NOT NULL,
	[Age] INT NOT NULL
)

CREATE TABLE Recipe
(
	[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Title] NVARCHAR(100),
	[Ingridients] TEXT NOT NULL,
	[CookingProgress] TEXT NOT NULL,
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

CREATE TABLE UserIdentitiy
(
	[UserId] INT FOREIGN KEY REFERENCES AppUser(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	[UserLogin] NVARCHAR(100) NOT NULL,
	[UserPassword] NVARCHAR(100) NOT NULL
)

CREATE TABLE RecipeAwards
(
	[RecipeId] INT FOREIGN KEY REFERENCES Recipe(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	[UserId] INT FOREIGN KEY REFERENCES AppUser(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	[AwardValue] FLOAT
)

CREATE TABLE UserReactions
(
	[UserId] INT FOREIGN KEY REFERENCES AppUser(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	[CommentId] INT FOREIGN KEY REFERENCES Commentary(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	[Award] binary
)

CREATE TABLE RecipeComments
(
	[CommentId] INT FOREIGN KEY REFERENCES Commentary(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	[RecipeId] INT FOREIGN KEY REFERENCES Recipe(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT CommentsAreUnique UNIQUE (CommentId, RecipeId)
)

CREATE PROCEDURE GetUsers
AS
BEGIN
	SELECT* FROM [dbo].[AppUser]
END

CREATE PROCEDURE GetRecipes
AS
BEGIN
	SELECT* FROM [dbo].[Recipe]
END

CREATE PROCEDURE GetUserComments
@UserId INT
AS
BEGIN
	SELECT * FROM [dbo].[Commentary] WHERE [dbo].[Commentary].[Id] IN
	(SELECT [dbo].[UserCommentaries].[CommentId] FROM [dbo].[UserCommentaries] 
	WHERE [dbo].[UserCommentaries].[UserId] = @UserId)
END

CREATE PROCEDURE GetUserRecipes
@UserId INT
AS
BEGIN
	SELECT * FROM [dbo].[Recipe] WHERE [dbo].[Recipe].[Id] IN
	(SELECT [dbo].[UserRecipes].[RecipeId] FROM [dbo].[UserRecipes] 
	WHERE [dbo].[UserRecipes].[RecipeId] = @UserId)
END

CREATE PROCEDURE AddUser
@UserName NVARCHAR(255),
@UserAge int,
@Login NVARCHAR(100),
@PasswordHashSum NVARCHAR(100)
AS
BEGIN
	BEGIN TRANSACTION

		INSERT INTO [dbo].[AppUser] VALUES (@UserName, @UserAge)

		IF (@@ERROR <> 0)
		ROLLBACK

		INSERT INTO [dbo].[UserIdentitiy] VALUES 
		((SELECT COUNT([dbo].[AppUser].[Id]) FROM [dbo].[AppUser]),
		@Login, @PasswordHashSum)

		IF (@@ERROR <> 0)
		ROLLBACK

	COMMIT
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
		(SELECT COUNT([dbo].[Recipe].[Id]) FROM [dbo].[Recipe]))

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
		(SELECT COUNT([dbo].[Commentary].[Id]) FROM [dbo].[Commentary]))

		IF (@@ERROR <> 0)
		ROLLBACK

		INSERT INTO [dbo].[RecipeComments] VALUES
		((SELECT COUNT([dbo].[Commentary].[Id]) FROM [dbo].[Commentary]),
		@RecipeId)

		IF (@@ERROR <> 0)
		ROLLBACK
END

ALTER PROCEDURE LikeTheComment
@UserId int,
@CommentaryId int
AS
BEGIN
	IF ((SELECT [dbo].[UserReactions].[Award]
		FROM [dbo].[UserReactions]
		WHERE [dbo].[UserReactions].[CommentId] = @CommentaryId AND
		[dbo].[UserReactions].[UserId] = @UserId) <> NULL)
		BEGIN
			DELETE FROM [dbo].[UserReactions]
			WHERE [dbo].[UserReactions].[UserId] = @UserId AND
			[dbo].[UserReactions].[CommentId] = @CommentaryId
		END
	ELSE
		BEGIN
			INSERT INTO [dbo].[UserReactions] VALUES
			(@UserId, @CommentaryId, 1)
		END
END

ALTER PROCEDURE DislikeTheComment
@UserId int,
@CommentaryId int
AS
BEGIN
	IF ((SELECT [dbo].[UserReactions].[Award]
		FROM [dbo].[UserReactions]
		WHERE [dbo].[UserReactions].[CommentId] = @CommentaryId AND
		[dbo].[UserReactions].[UserId] = @UserId) <> NULL)
		BEGIN
			DELETE FROM [dbo].[UserReactions]
			WHERE [dbo].[UserReactions].[UserId] = @UserId AND
			[dbo].[UserReactions].[CommentId] = @CommentaryId
		END
	ELSE
		BEGIN
			INSERT INTO [dbo].[UserReactions] VALUES
			(@UserId, @CommentaryId, 0)
		END
END

CREATE PROCEDURE GetCommentaryLikes
@CommentId INT
AS
BEGIN
	SELECT COUNT([dbo].[UserReactions].[Award])
	FROM [dbo].[UserReactions]
	WHERE [dbo].[UserReactions].[Award] = 1
END

CREATE PROCEDURE GetCommentaryDislikes
@CommentId INT
AS
BEGIN
	SELECT COUNT([dbo].[UserReactions].[Award])
	FROM [dbo].[UserReactions]
	WHERE [dbo].[UserReactions].[Award] = 0
END

CREATE PROCEDURE RateRecipe
@RecipeId INT,
@UserId INT,
@Award FLOAT
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

CREATE PROCEDURE ChangeUserIdentity
@UserId INT,
@Login NVARCHAR(100),
@Password NVARCHAR(100)
AS
BEGIN
	UPDATE [dbo].[UserIdentitiy]
	SET [dbo].[UserIdentitiy].[UserLogin] = @Login,
	[dbo].[UserIdentitiy].[UserPassword] = @Password
	WHERE [dbo].[UserIdentitiy].[UserId] = @UserId
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

CREATE PROCEDURE ChangeRecipe
@RecipeId INT,
@RecipeTitle NVARCHAR(255),
@CookingProcess TEXT,
@Ingridients TEXT
AS
BEGIN
	UPDATE [dbo].[Recipe]
	SET [dbo].[Recipe].[Title] = @RecipeTitle,
	[dbo].[Recipe].[CookingProgress] = @CookingProcess,
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

CREATE PROCEDURE GetAllCommentRecipes
@RecipeId
AS 
BEGIN
	SELECT * FROM [dbo].[Commentary] JOIN [dbo].[RecipeComments]
	ON [dbo].[Commentary].[Id] = [dbo].[RecipeComments].[CommentId]
END

CREATE PROCEDURE GetRecipeAward
@RecipeId
AS
BEGIN

END

CREATE PROCEDURE CheckIdentity
@UserId int,
@PasswordHashSum nvarchar(255)
AS
BEGIN
	IF (SELECT [dbo].[UserIdentitiy].[UserPassword] FROM [dbo].[UserIdentity]
	WHERE @UserId = [dbo].[UserIdentity].[UserId]) = @PasswordHashSum
		RETURN 1
	ELSE
		RETURN 0
END

IF ((SELECT [dbo].[UserReactions].[Award] FROM [dbo].[UserReactions]) = NULL)
print 'NULL'
ELSE
print 'NOT NULL'