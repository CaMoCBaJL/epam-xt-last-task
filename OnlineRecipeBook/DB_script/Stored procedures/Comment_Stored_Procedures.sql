USE RecipeBookDataBase

CREATE PROCEDURE GetComments
AS
BEGIN
	SELECT * FROM [dbo].[Commentary]
END

CREATE PROCEDURE GetRecipeComments
@RecipeId INT
AS 
BEGIN
	SELECT * FROM [dbo].[Commentary] JOIN [dbo].[RecipeComments]
	ON [dbo].[Commentary].[Id] = [dbo].[RecipeComments].[CommentId]
	WHERE [dbo].[RecipeComments].[RecipeId] = @RecipeId
END

CREATE PROCEDURE GetUserComments
@UserId INT
AS
BEGIN
	SELECT * FROM [dbo].[Commentary] WHERE [dbo].[Commentary].[Id] IN
	(SELECT [dbo].[UserCommentaries].[CommentId] FROM [dbo].[UserCommentaries] 
	WHERE [dbo].[UserCommentaries].[UserId] = @UserId)
END

CREATE PROCEDURE AddCommentary
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

CREATE PROCEDURE LikeTheComment
@UserId INT,
@CommentaryId INT
AS
BEGIN
	EXEC CommentAwarding @CommentaryId, @UserId, 1
END

CREATE PROCEDURE DislikeTheComment
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

CREATE PROCEDURE GetCommentLikes
@CommentId INT
AS
BEGIN
	SELECT COUNT([dbo].[UserReactions].[Award]) as LikesCounter
	FROM [dbo].[UserReactions]
	WHERE [dbo].[UserReactions].[Award] = 1 AND [dbo].[UserReactions].[CommentId] = @CommentId
END

CREATE PROCEDURE GetCommentDislikes
@CommentId INT
AS
BEGIN
	SELECT COUNT([dbo].[UserReactions].[Award]) as DislikesCounter
	FROM [dbo].[UserReactions]
	WHERE [dbo].[UserReactions].[Award] = 0 AND [dbo].[UserReactions].[CommentId] = @CommentId
END

CREATE PROCEDURE RemoveComment
@CommentId INT
AS
BEGIN
	DELETE FROM [dbo].[Commentary] WHERE [dbo].[Commentary].[Id] = @CommentId
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


