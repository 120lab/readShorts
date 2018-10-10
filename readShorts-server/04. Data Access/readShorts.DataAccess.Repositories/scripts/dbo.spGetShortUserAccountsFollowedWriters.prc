USE [[db]]
GO

/****** Object:  StoredProcedure [dbo].[spGetShortUserAccountsFollowedWriters]    Script Date: 23/09/2016 00:56:17 ******/
DROP PROCEDURE [dbo].[spGetShortUserAccountsFollowedWriters]
GO

/****** Object:  StoredProcedure [dbo].[spGetShortUserAccountsFollowedWriters]    Script Date: 23/09/2016 00:56:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[spGetShortUserAccountsFollowedWriters]
	@userName NVARCHAR(200)
as

--DECLARE @userName NVARCHAR(200)
--SET		@userName = '@idan_gvili'

	SELECT	More.RecordKey AS ShortKey,
			More.WriterUserKey,
			UserAccount.RecordKey AS UserAccountKey
	FROM	ShortUserAccount
			INNER JOIN UserAccount
				ON ShortUserAccount.UserAccountKey = UserAccount.RecordKey
			INNER JOIN Short
				ON ShortUserAccount.ShortKey = Short.RecordKey
			LEFT JOIN Short AS More
				ON More.WriterUserKey = Short.WriterUserKey 
	WHERE	ShortUserAccount.UserSignWriterAsFollowed = 1
			AND UserAccount.UserName = @userName
			AND ShortUserAccount.IsRowDeleted = 0
	GROUP BY More.RecordKey,More.WriterUserKey,UserAccount.RecordKey



GO

