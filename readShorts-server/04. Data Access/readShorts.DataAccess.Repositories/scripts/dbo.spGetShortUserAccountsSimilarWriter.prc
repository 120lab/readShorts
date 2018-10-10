USE [[db]]
GO

/****** Object:  StoredProcedure [dbo].[spGetShortUserAccountsSimilarWriter]    Script Date: 23/09/2016 00:56:38 ******/
DROP PROCEDURE [dbo].[spGetShortUserAccountsSimilarWriter]
GO

/****** Object:  StoredProcedure [dbo].[spGetShortUserAccountsSimilarWriter]    Script Date: 23/09/2016 00:56:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[spGetShortUserAccountsSimilarWriter]
	@userAccountKey bigint, 
	@shortKey bigint
as

--DECLARE	@userAccountKey bigint
--DECLARE	@shortKey bigint

--SET @userAccountKey =13
--SET @shortKey = 4



	SELECT	ShortUserAccount.RecordKey
			--,ShortUserAccount.ShortSendToUser
			--,ShortUserAccount.ShortSendToUserTimeStamp
			--,ShortUserAccount.ShortViewByUser
			--,ShortUserAccount.ShortViewByUserTimeStamp
			--,ShortUserAccount.ShortReadByUser
			--,ShortUserAccount.ShortReadByUserTimeInMiliSeconds
			--,ShortUserAccount.ShortReadByUserTimeStamp
			--,ShortUserAccount.ShortSignAsLike
			--,ShortUserAccount.ShortSignAsLikeTimeStamp
			--,ShortUserAccount.ShortSignAsBookmark
			--,ShortUserAccount.ShortSignAsBookmarkTimeStamp
			--,ShortUserAccount.UserSignNextShort
			--,ShortUserAccount.UserSignNextShortTimeStamp
			--,ShortUserAccount.UserSignWriterAsFollowed
			--,ShortUserAccount.UserSignWriterAsFollowedTimeStamp
			--,ShortUserAccount.ShortKey
			--,ShortUserAccount.UserAccountKey
			--,ShortUserAccount.CreatedTimeStamp
			--,ShortUserAccount.LastUpdateTimeStamp
			--,ShortUserAccount.IsRowDeleted
			--,ShortUserAccount.UniqueGuid			
	FROM	Short
			INNER JOIN Short AS ShortsByWriter
				ON Short.WriterUserKey = ShortsByWriter.WriterUserKey
			INNER JOIN ShortUserAccount
				ON ShortUserAccount.ShortKey = ShortsByWriter.RecordKey
				AND ShortUserAccount.UserAccountKey = @userAccountKey
	WHERE	Short.RecordKey = @shortKey
			AND Short.IsRowDeleted = 0




GO

