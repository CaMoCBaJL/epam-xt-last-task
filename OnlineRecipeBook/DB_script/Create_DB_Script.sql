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
	[Ingridients] TEXT NOT NULL, -- for SQL Server it's recommended to use nvarchar(MAX) instead - they're better optimized (docs.microsoft.com/en-us/sql/t-sql/data-types/ntext-text-and-image-transact-sql, stackoverflow.com/questions/834788/ddg#834863)
	[CookingProcess] TEXT NOT NULL,
)

CREATE TABLE Commentary
(
	[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Text] TEXT NOT NULL
)

CREATE TABLE UserCommentaries -- Usually to show that it's a relation table people use names like 'UsersToComments' or real terms for such relations (like AuthOptions for a relation between users and auth providers)
(
	[UserId] INT FOREIGN KEY REFERENCES AppUser(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	[CommentId] INT FOREIGN KEY REFERENCES Commentary(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT UserCommentsAreUnique UNIQUE (UserId, CommentId) -- unique indexes are named like 'UQ_Table_Columns' (hungarian notation, yes, but still useful)
	-- if you use this combination in queries, better to use Primary Key (it also enforces unique pairs):
	-- CONSTRAINT PK_UserComments PRIMARY KEY ([UserId], [CommentId])
)

CREATE TABLE UserRecipes
(
	[UserId] INT FOREIGN KEY REFERENCES AppUser(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	[RecipeId] INT FOREIGN KEY REFERENCES Recipe(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT UserRecipesAreUnique UNIQUE (UserId, RecipeId)
)

CREATE TABLE UserIdentity
(
	[UserId] INT FOREIGN KEY REFERENCES AppUser(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	[UserLogin] NVARCHAR(100) NOT NULL,
	[UserPassword] NVARCHAR(255) NOT NULL -- SHA256 hashes can be stored in binary(16) type (byte[] in C#)
)

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