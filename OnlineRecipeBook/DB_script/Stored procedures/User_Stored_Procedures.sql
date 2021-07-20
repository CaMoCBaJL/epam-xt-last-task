USE RecipeBookDataBase

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

CREATE PROCEDURE GetUserIdentities
AS
BEGIN
	SELECT * FROM [dbo].[UserIdentity]
END

CREATE PROCEDURE GetUserIdentity
@UserId INT
AS
BEGIN
	SELECT * FROM [dbo].[UserIdentity] WHERE [dbo].[UserIdentity].[UserId] = @UserId
END

CREATE PROCEDURE GetCommentAuthor
@CommentId INT
AS
BEGIN
	SELECT [dbo].[UserCommentaries].[UserId] FROM [dbo].[UserCommentaries]
	WHERE [dbo].[UserCommentaries].[CommentId] = @CommentId
END

CREATE PROCEDURE AddUser
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

CREATE PROCEDURE RemoveUser
@UserId INT
AS
BEGIN
	DELETE FROM [dbo].[AppUser] WHERE [dbo].[AppUser].[Id] = @UserId
END

CREATE PROCEDURE ChangeUserIdentity
@UserId INT,
@Login NVARCHAR(100),
@Password NVARCHAR(255) 
AS
BEGIN
	UPDATE [dbo].[UserIdentity]
	SET [dbo].[UserIdentity].[UserLogin] = @Login
	WHERE [dbo].[UserIdentity].[UserId] = @UserId

	IF(@Password IS NOT NULL)
		UPDATE [dbo].[UserIdentity]
		SET [dbo].[UserIdentity].[UserPassword] = @Password
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

CREATE PROCEDURE CheckIdentity
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

CREATE PROCEDURE GetUserAward
@UserId INT,
@RecipeId INT
AS
BEGIN
	SELECT [dbo].[RecipeAwards].[AwardValue] FROM [dbo].[RecipeAwards]
	WHERE [dbo].[RecipeAwards].[UserId] = @UserId AND [dbo].[RecipeAwards].[RecipeId] = @RecipeId
END

CREATE PROCEDURE GetRecipeAuthor
@RecipeId INT
AS
BEGIN
	SELECT [dbo].[UserRecipes].[UserId] FROM [dbo].[UserRecipes]
	WHERE [dbo].[UserRecipes].[RecipeId] = @RecipeId
END

