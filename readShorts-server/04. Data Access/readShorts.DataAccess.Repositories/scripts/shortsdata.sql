	drop table #tmptags
	
SELECT 
		shortkey,
		STUFF((
		SELECT ', ' + CAST(lookup.LUShortTagType.description AS VARCHAR(MAX)) 
		FROM shorttag 
		inner join lookup.LUShortTagType
			on lookup.LUShortTagType.recordkey = shorttag.LUShortTagTypeKey
		WHERE (shorttag.shortkey = Results.shortkey) 
		FOR XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)')
		,1,2,'') AS NameValues
into	#tmpTags
FROM	shorttag Results
GROUP BY shortkey

select
		WriterUserKey as [Writer Key],
		useraccount.emailaddress as[Writer Email],
		short.[Title],
		short.[Text] AS [Text],
		short.[Quote] AS [Quote],
		lookup.LUQuoteType.description [Quote Type],
		lookup.LUShortAgeRestriction.description [Age Restriction],
		lookup.LUSysInterfaceLanguage.description [Language],
		lookup.LUStoryType.description [Story Type],
		#tmpTags.NameValues [Multi Tags],
		lookup.LUShortCategoryType.description [Category],
		null as [Other Category],
		CategoryPicturePath as [Picture URL]
from	useraccount
		inner join short
			on useraccount.recordkey = short.WriterUserKey
		left join lookup.LUQuoteType
			on short.LUQuoteTypeKey = lookup.LUQuoteType.recordkey
		left join lookup.LUShortAgeRestriction
			on short.LUShortAgeRestrictionKey = lookup.LUShortAgeRestriction.recordkey
		left join lookup.LUSysInterfaceLanguage
			on short.LUSysInterfacelanguageKey = lookup.LUSysInterfaceLanguage.recordkey
		left join lookup.LUStoryType
			on short.LUStoryTypeKey = lookup.LUStoryType.recordkey
		left join #tmpTags
			on #tmpTags.shortkey = short.recordkey
		left join lookup.LUShortCategoryType
			on short.LUCategoryTypeKey = lookup.LUShortCategoryType.recordkey
