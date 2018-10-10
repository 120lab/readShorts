namespace readShorts.DataAccess.Migrations
{
    using Entities.dbo;
    using Entities.LOOKUP;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<readShorts.DataAccess.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(readShorts.DataAccess.DataContext context)
        {
            //  This method will be called after migrating to the latest version.
            //Remove all constraints
            //context.Database.ExecuteSqlCommand("sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'");

            FillActivities(context);
            FillCountries(context);
            FillGenders(context);
            FillGroups(context);
            FillPointTypes(context);
            FillShortAgeRestriction(context);
            FillShortChannelType(context);
            FillShortReportType(context);
            FillShortShareType(context);
            FillShortTag(context);
            FillSubscriptionTypes(context);
            FillSysInterfaceLanguages(context);
            FillQuoteType(context);
            FillStoryType(context);
            FillLUEventType(context);
            FillLUUserVerificationType(context);
            FillStartUser(context);
            // Enable constraints
            //context.Database.ExecuteSqlCommand("EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'");

            context.SaveChanges();
        }

        private void FillStartUser(DataContext context)
        {
            if (context.UserAccounts.Count() > 0)
                return;

            //context.UserAccounts.Add(new UserAccount() {  UserName = "@anonymous", BirthDate = DateTime.UtcNow, EmailForNewSAndUpdates = false, EmailForShortFollowingWriter = false, EmailForShortIMightLike = false, EmailForShortOfTheWeek = false , FirstName = "Guest", LastName = " ", IsAnonimousConnect = true, IsEmailConnect = false, IsFBConnect = false, IsGGLConnect = false, IsTWConnect = false, LastActitiyDate = DateTime.UtcNow, LUCountryKey = 250 /*Other*/, LUSubscriptionTypeKey = 1 /*Anonymous*/, LUGenderKey = 3 /*Other*/, LUSysInterfacelanguageKey = 2, LUUserVerificationTypeKey = 4, UniqueGuid = new Guid().ToString() });
        }

        private void FillLUUserVerificationType(DataContext context)
        {
            if (context.LUUserVerificationType.Count() > 0)
                return;

            context.LUUserVerificationType.Add(new LUUserVerificationType() { RecordKey = 1, Description = "Anonymous" });
            context.LUUserVerificationType.Add(new LUUserVerificationType() { RecordKey = 2, Description = "User created" });
            context.LUUserVerificationType.Add(new LUUserVerificationType() { RecordKey = 3, Description = "Verification Pending" });
            context.LUUserVerificationType.Add(new LUUserVerificationType() { RecordKey = 4, Description = "Verified" });
            context.LUUserVerificationType.Add(new LUUserVerificationType() { RecordKey = 5, Description = "Canceled" });
        }

        private void FillLUEventType(DataContext context)
        {
            //if (context.LUEventTypes.Count() > 0)
            //    return;

            foreach (var item in context.LUEventTypes)
            {
                context.LUEventTypes.Remove(item);
            }

            context.LUEventTypes.Add(new LUEventType() { RecordKey = 1, Description = "User enter to app" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 2, Description = "User login to app" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 3, Description = "User login to app by Email" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 4, Description = "User login to app by Facebook account" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 5, Description = "User Get Shorts" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 6, Description = "User Enter to Maim menu" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 7, Description = "User In Home feed" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 8, Description = "User In Bookmark feed" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 9, Description = "User In Followed writer feed" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 10, Description = "User In Top feed" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 11, Description = "User In Profile page" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 12, Description = "User In PLM page" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 13, Description = "User In Settings page" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 14, Description = "User In About page" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 15, Description = "User In Publish Short page" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 16, Description = "User In Contact page" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 17, Description = "User In Terms page" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 18, Description = "User Share Short" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 19, Description = "User Share Short by FaceBook" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 20, Description = "User Share Short by Twitter" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 21, Description = "User Share Short by SMS" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 22, Description = "User Share Short by Whatsup" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 23, Description = "User Share Short by Mail" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 24, Description = "User Bookmarked Short" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 25, Description = "User Liked Short" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 26, Description = "User Followed writer" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 27, Description = "User Enter to Short's text" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 28, Description = "User Used the facebook comment" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 29, Description = "User Enter the readshort's Facebook account" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 30, Description = "User Enter the readshort's Twitter account" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 31, Description = "User Enter the readshort's Instagram account" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 32, Description = "User Update Profile info page" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 33, Description = "User Update Settings page" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 34, Description = "User Update Profile info" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 35, Description = "User sent contact message" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 36, Description = "Writer sent message share" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 37, Description = "User viewed transfer page" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 38, Description = "User view the short" });
            context.LUEventTypes.Add(new LUEventType() { RecordKey = 39, Description = "User read all the short" });
        }

        private void FillStoryType(DataContext context)
        {
            foreach (var item in context.LUStoryType)
            {
                context.LUStoryType.Remove(item);
            }

            context.LUStoryType.AddOrUpdate(u => u.RecordKey, new LUStoryType() { RecordKey = 1, Description = "Poems" });
            context.LUStoryType.AddOrUpdate(u => u.RecordKey, new LUStoryType() { RecordKey = 2, Description = "Shorts" });
            context.LUStoryType.AddOrUpdate(u => u.RecordKey, new LUStoryType() { RecordKey = 3, Description = "Micro shorts" });
            context.LUStoryType.AddOrUpdate(u => u.RecordKey, new LUStoryType() { RecordKey = 4, Description = "Stories" });
        }

        private void FillQuoteType(DataContext context)
        {
            if (context.LUQuoteType.Count() > 0)
                return;

            context.LUQuoteType.AddOrUpdate(u => u.RecordKey, new LUQuoteType() { RecordKey = 1, Description = "First Paragraph" });
            context.LUQuoteType.AddOrUpdate(u => u.RecordKey, new LUQuoteType() { RecordKey = 2, Description = "Part of the story" });
            context.LUQuoteType.AddOrUpdate(u => u.RecordKey, new LUQuoteType() { RecordKey = 3, Description = "The entire story" });
        }

        private void FillShortShareType(DataContext context)
        {
            if (context.LUShortShareTypes.Count() > 0)
                return;

            context.LUShortShareTypes.AddOrUpdate(u => u.RecordKey, new LUShortShareType() { RecordKey = 1, Description = "Sharing" });
            context.LUShortShareTypes.AddOrUpdate(u => u.RecordKey, new LUShortShareType() { RecordKey = 2, Description = "Recomending" });
        }

        private void FillShortTag(DataContext context)
        {
            // Adding data from short uploading
        }

        private void FillShortChannelType(DataContext context)
        {
            //if (context.LUShortCategoryTypes.Count() > 0)
            //    return;

            foreach (var item in context.LUShortCategoryTypes)
            {
                context.LUShortCategoryTypes.Remove(item);
            }

            context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { RecordKey = 1, Description = "Life's Just Happened", AditionalData = "1784631.jpg" });
            context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { RecordKey = 2, Description = "Moments of Memories", AditionalData = "1787582.jpg" });
            context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { RecordKey = 3, Description = "In Equivilant World", AditionalData = "1854909.jpg" });
            context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { RecordKey = 4, Description = "Small Letters", AditionalData = "1776562.jpg" });
            context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { RecordKey = 5, Description = "Mini Dialouges", AditionalData = "1693949.jpg" });
            context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { RecordKey = 6, Description = "Self Reflections", AditionalData = "1779758.jpg" });
            context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { RecordKey = 7, Description = "Real people Fake Stories", AditionalData = "1793674.jpg" });
            context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { RecordKey = 8, Description = "Journies", AditionalData = "1785465.jpg" });
            context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { RecordKey = 9, Description = "Watching from Aside", AditionalData = "1786400.jpg" });
            context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { RecordKey = 10, Description = "About Writing", AditionalData = "1776562.jpg" });
            context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { RecordKey = 11, Description = "Talking about Things", AditionalData = "1788339.jpg" });
            context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { RecordKey = 12, Description = "Looking Around", AditionalData = "1780519.jpg" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Action" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Thrillers" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Mystery" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Tension" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Science Fiction" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Fantasy" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Roman" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Journeys" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Discovering" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Periodic" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "History" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Taken from Life" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Studying" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Inspiration" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Dialogues" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "One sentence stories" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Word games" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Philosophy" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Criticism" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Society" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Culture" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "About Writing" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Poems" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Songs" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Comic" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Graphic Novels" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Art" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Design" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Kids" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Youth" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Food Stories" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Women" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Parenting" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Pride" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Sexuality" });
            //context.LUShortCategoryTypes.AddOrUpdate(u => u.RecordKey, new LUShortCategoryType() { Description = "Erotic" });
        }

        private void FillShortAgeRestriction(DataContext context)
        {
            //foreach (var item in context.LUShortAgeRestrictions)
            //{
            //    context.LUShortAgeRestrictions.Remove(item);
            //}


            context.LUShortAgeRestrictions.AddOrUpdate(u => u.RecordKey, new LUShortAgeRestriction() { RecordKey = 1, Description = "All stories (13+)" });
            context.LUShortAgeRestrictions.AddOrUpdate(u => u.RecordKey, new LUShortAgeRestriction() { RecordKey = 2, Description = "Restricted (16+)" });
            context.LUShortAgeRestrictions.AddOrUpdate(u => u.RecordKey, new LUShortAgeRestriction() { RecordKey = 3, Description = "Adult Contect (18+)" });
        }

        private void FillShortReportType(DataContext context)
        {
            // **********************
            // Table not relevant now
            // **********************

            //if (context.LUShortReportTypes.Count() > 0)
            //    return;

            //context.LUShortReportTypes.AddOrUpdate(u => u.RecordKey, new LUShortReportType() { RecordKey = 1, Description = "Copyrights violation" });
            //context.LUShortReportTypes.AddOrUpdate(u => u.RecordKey, new LUShortReportType() { RecordKey = 2, Description = "Inappropriate" });
            //context.LUShortReportTypes.AddOrUpdate(u => u.RecordKey, new LUShortReportType() { RecordKey = 3, Description = "Other" });
        }

        private void FillPointTypes(DataContext context)
        {
            // **********************
            // Table not relevant now
            // **********************

            //if (context.LUPointTypes.Count() > 0)
            //    return;

            ///// Lookup.PointType
            //context.LUPointTypes.AddOrUpdate(u => u.RecordKey, new LUPointType() { RecordKey = 1, Description = "Points" });
            //context.LUPointTypes.AddOrUpdate(u => u.RecordKey, new LUPointType() { RecordKey = 2, Description = "Virtual coins (writers)" });
            //context.LUPointTypes.AddOrUpdate(u => u.RecordKey, new LUPointType() { RecordKey = 3, Description = "Reading (full ERT)" });
            //context.LUPointTypes.AddOrUpdate(u => u.RecordKey, new LUPointType() { RecordKey = 4, Description = "Sharing" });
            //context.LUPointTypes.AddOrUpdate(u => u.RecordKey, new LUPointType() { RecordKey = 5, Description = "Reading from sharing" });
            //context.LUPointTypes.AddOrUpdate(u => u.RecordKey, new LUPointType() { RecordKey = 6, Description = "Feedbacking" });
            //context.LUPointTypes.AddOrUpdate(u => u.RecordKey, new LUPointType() { RecordKey = 7, Description = "Registered from invitation" });
            //context.LUPointTypes.AddOrUpdate(u => u.RecordKey, new LUPointType() { RecordKey = 8, Description = "Accepted writers from invitation (writers only)" });
            //context.LUPointTypes.AddOrUpdate(u => u.RecordKey, new LUPointType() { RecordKey = 9, Description = "Achieving badges" });
            //context.LUPointTypes.AddOrUpdate(u => u.RecordKey, new LUPointType() { RecordKey = 10, Description = "Virtual Coins:" });
            //context.LUPointTypes.AddOrUpdate(u => u.RecordKey, new LUPointType() { RecordKey = 11, Description = "Read story (%)" });
            //context.LUPointTypes.AddOrUpdate(u => u.RecordKey, new LUPointType() { RecordKey = 12, Description = "Shared story (%)" });
            //context.LUPointTypes.AddOrUpdate(u => u.RecordKey, new LUPointType() { RecordKey = 13, Description = "Followers (new)" });
            //context.LUPointTypes.AddOrUpdate(u => u.RecordKey, new LUPointType() { RecordKey = 14, Description = "Purchased book (future)" });
        }

        private void FillActivities(DataContext context)
        {
            // **********************
            // Table not relevant now
            // **********************

            //if (context.LUActivities.Count() > 0)
            //    return;

            ///// Lookup.Activity
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "Entering" });
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "TimeStamp " });
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "Registering (with or without invitation)" });
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "Applying (for writers) + Accepted / Rejected" });
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "Inviting" });
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "Updating profile" });
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "Updating settings" });
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "Visited pages" });
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "Full reading of story" });
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "Unread Story (under ERT)" });
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "Bookmark" });
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "Sharing (and Recommending)" });
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "Feedbacking (per each type)" });
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "Choosing quote" });
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "Favorite" });
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "Report" });
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "View setting" });
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "Following" });
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "Unfollowing" });
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "Upgrading (Premium, Pro)" });
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "Printing (eBooks / ePub, PDF’s)" });
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "Receiving points a/o virtual points" });
            //context.LUActivities.AddOrUpdate(u => u.RecordKey, new LUActivity() { Description = "Receiving badge" });
        }

        private void FillGroups(DataContext context)
        {
            // **********************
            // Table not relevant now
            // **********************

            //if (context.LUGroups.Count() > 0)
            //    return;

            ///// Lookup.Group
            //context.LUGroups.AddOrUpdate(u => u.RecordKey, new LUGroup() { Description = "Cynical" });
            //context.LUGroups.AddOrUpdate(u => u.RecordKey, new LUGroup() { Description = "Romantic" });
            //context.LUGroups.AddOrUpdate(u => u.RecordKey, new LUGroup() { Description = "Reflective" });
            //context.LUGroups.AddOrUpdate(u => u.RecordKey, new LUGroup() { Description = "About Life" });
            //context.LUGroups.AddOrUpdate(u => u.RecordKey, new LUGroup() { Description = "Criticism" });
            //context.LUGroups.AddOrUpdate(u => u.RecordKey, new LUGroup() { Description = "Funny" });
            //context.LUGroups.AddOrUpdate(u => u.RecordKey, new LUGroup() { Description = "Biographer" });
        }

        private void FillCountries(DataContext context)
        {
            if (context.LUCountries.Count() > 0)
                return;

            /// Lookup.Country
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 1, Description = "Andorra" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 2, Description = "United Arab Emirates" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 3, Description = "Afghanistan" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 4, Description = "Antigua and Barbuda" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 5, Description = "Anguilla" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 6, Description = "Albania" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 7, Description = "Armenia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 8, Description = "Angola" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 9, Description = "Antarctica" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 10, Description = "Argentina" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 11, Description = "American Samoa" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 12, Description = "Austria" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 13, Description = "Australia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 14, Description = "Aruba" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 15, Description = "Åland Islands" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 16, Description = "Azerbaijan" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 17, Description = "Bosnia and Herzegovina" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 18, Description = "Barbados" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 19, Description = "Bangladesh" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 20, Description = "Belgium" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 21, Description = "Burkina Faso" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 22, Description = "Bulgaria" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 23, Description = "Bahrain" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 24, Description = "Burundi" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 25, Description = "Benin" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 26, Description = "Saint Barthélemy" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 27, Description = "Bermuda" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 28, Description = "Brunei Darussalam" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 29, Description = "Bolivia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 30, Description = "Caribbean Netherlands" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 31, Description = "Brazil" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 32, Description = "Bahamas" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 33, Description = "Bhutan" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 34, Description = "Bouvet Island" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 35, Description = "Botswana" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 36, Description = "Belarus" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 37, Description = "Belize" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 38, Description = "Canada" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 39, Description = "Cocos (Keeling) Islands" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 40, Description = "Congo, Democratic Republic of" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 41, Description = "Central African Republic" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 42, Description = "Congo" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 43, Description = "Switzerland" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 44, Description = "Côte d'Ivoire" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 45, Description = "Cook Islands" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 46, Description = "Chile" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 47, Description = "Cameroon" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 48, Description = "China" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 49, Description = "Colombia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 50, Description = "Costa Rica" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 51, Description = "Cuba" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 52, Description = "Cape Verde" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 53, Description = "Curaçao" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 54, Description = "Christmas Island" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 55, Description = "Cyprus" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 56, Description = "Czech Republic" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 57, Description = "Germany" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 58, Description = "Djibouti" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 59, Description = "Denmark" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 60, Description = "Dominica" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 61, Description = "Dominican Republic" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 62, Description = "Algeria" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 63, Description = "Ecuador" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 64, Description = "Estonia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 65, Description = "Egypt" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 66, Description = "Western Sahara" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 67, Description = "Eritrea" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 68, Description = "Spain" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 69, Description = "Ethiopia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 70, Description = "Finland" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 71, Description = "Fiji" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 72, Description = "Falkland Islands" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 73, Description = "Micronesia, Federated States of" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 74, Description = "Faroe Islands" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 75, Description = "France" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 76, Description = "Gabon" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 77, Description = "United Kingdom" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 78, Description = "Grenada" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 79, Description = "Georgia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 80, Description = "French Guiana" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 81, Description = "Guernsey" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 82, Description = "Ghana" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 83, Description = "Gibraltar" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 84, Description = "Greenland" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 85, Description = "Gambia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 86, Description = "Guinea" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 87, Description = "Guadeloupe" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 88, Description = "Equatorial Guinea" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 89, Description = "Greece" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 90, Description = "South Georgia and the South Sandwich Islands" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 91, Description = "Guatemala" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 92, Description = "Guam" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 93, Description = "Guinea-Bissau" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 94, Description = "Guyana" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 95, Description = "Hong Kong" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 96, Description = "Heard and McDonald Islands" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 97, Description = "Honduras" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 98, Description = "Croatia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 99, Description = "Haiti" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 100, Description = "Hungary" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 101, Description = "Indonesia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 102, Description = "Ireland" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 103, Description = "Israel" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 104, Description = "Isle of Man" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 105, Description = "India" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 106, Description = "British Indian Ocean Territory" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 107, Description = "Iraq" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 108, Description = "Iran" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 109, Description = "Iceland" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 110, Description = "Italy" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 111, Description = "Jersey" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 112, Description = "Jamaica" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 113, Description = "Jordan" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 114, Description = "Japan" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 115, Description = "Kenya" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 116, Description = "Kyrgyzstan" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 117, Description = "Cambodia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 118, Description = "Kiribati" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 119, Description = "Comoros" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 120, Description = "Saint Kitts and Nevis" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 121, Description = "North Korea" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 122, Description = "South Korea" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 123, Description = "Kuwait" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 124, Description = "Cayman Islands" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 125, Description = "Kazakhstan" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 126, Description = "Lao People's Democratic Republic" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 127, Description = "Lebanon" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 128, Description = "Saint Lucia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 129, Description = "Liechtenstein" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 130, Description = "Sri Lanka" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 131, Description = "Liberia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 132, Description = "Lesotho" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 133, Description = "Lithuania" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 134, Description = "Luxembourg" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 135, Description = "Latvia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 136, Description = "Libya" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 137, Description = "Morocco" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 138, Description = "Monaco" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 139, Description = "Moldova" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 140, Description = "Montenegro" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 141, Description = "Saint-Martin (France)" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 142, Description = "Madagascar" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 143, Description = "Marshall Islands" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 144, Description = "Macedonia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 145, Description = "Mali" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 146, Description = "Myanmar" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 147, Description = "Mongolia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 148, Description = "Macau" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 149, Description = "Northern Mariana Islands" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 150, Description = "Martinique" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 151, Description = "Mauritania" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 152, Description = "Montserrat" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 153, Description = "Malta" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 154, Description = "Mauritius" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 155, Description = "Maldives" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 156, Description = "Malawi" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 157, Description = "Mexico" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 158, Description = "Malaysia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 159, Description = "Mozambique" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 160, Description = "Namibia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 161, Description = "New Caledonia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 162, Description = "Niger" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 163, Description = "Norfolk Island" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 164, Description = "Nigeria" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 165, Description = "Nicaragua" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 166, Description = "The Netherlands" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 167, Description = "Norway" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 168, Description = "Nepal" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 169, Description = "Nauru" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 170, Description = "Niue" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 171, Description = "New Zealand" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 172, Description = "Oman" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 173, Description = "Panama" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 174, Description = "Peru" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 175, Description = "French Polynesia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 176, Description = "Papua New Guinea" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 177, Description = "Philippines" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 178, Description = "Pakistan" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 179, Description = "Poland" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 180, Description = "St. Pierre and Miquelon" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 181, Description = "Pitcairn" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 182, Description = "Puerto Rico" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 183, Description = "Palestine, State of" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 184, Description = "Portugal" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 185, Description = "Palau" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 186, Description = "Paraguay" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 187, Description = "Qatar" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 188, Description = "Réunion" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 189, Description = "Romania" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 190, Description = "Serbia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 191, Description = "Russian Federation" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 192, Description = "Rwanda" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 193, Description = "Saudi Arabia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 194, Description = "Solomon Islands" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 195, Description = "Seychelles" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 196, Description = "Sudan" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 197, Description = "Sweden" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 198, Description = "Singapore" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 199, Description = "Saint Helena" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 200, Description = "Slovenia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 201, Description = "Svalbard and Jan Mayen Islands" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 202, Description = "Slovakia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 203, Description = "Sierra Leone" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 204, Description = "San Marino" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 205, Description = "Senegal" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 206, Description = "Somalia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 207, Description = "Suriname" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 208, Description = "South Sudan" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 209, Description = "Sao Tome and Principe" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 210, Description = "El Salvador" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 211, Description = "Sint Maarten (Dutch part)" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 212, Description = "Syria" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 213, Description = "Swaziland" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 214, Description = "Turks and Caicos Islands" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 215, Description = "Chad" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 216, Description = "French Southern Territories" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 217, Description = "Togo" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 218, Description = "Thailand" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 219, Description = "Tajikistan" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 220, Description = "Tokelau" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 221, Description = "Timor-Leste" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 222, Description = "Turkmenistan" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 223, Description = "Tunisia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 224, Description = "Tonga" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 225, Description = "Turkey" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 226, Description = "Trinidad and Tobago" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 227, Description = "Tuvalu" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 228, Description = "Taiwan" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 229, Description = "Tanzania" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 230, Description = "Ukraine" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 231, Description = "Uganda" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 232, Description = "United States Minor Outlying Islands" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 233, Description = "United States" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 234, Description = "Uruguay" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 235, Description = "Uzbekistan" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 236, Description = "Vatican" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 237, Description = "Saint Vincent and the Grenadines" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 238, Description = "Venezuela" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 239, Description = "Virgin Islands (British)" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 240, Description = "Virgin Islands (U.S.)" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 241, Description = "Vietnam" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 242, Description = "Vanuatu" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 243, Description = "Wallis and Futuna Islands" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 244, Description = "Samoa" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 245, Description = "Yemen" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 246, Description = "Mayotte" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 247, Description = "South Africa" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 248, Description = "Zambia" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 249, Description = "Zimbabwe" });
            context.LUCountries.AddOrUpdate(u => u.RecordKey, new LUCountry() { RecordKey = 250, Description = "Other" });
        }

        private void FillGenders(DataContext context)
        {
            if (context.LUGenders.Count() > 0)
                return;

            /// Lookup.Gender
            context.LUGenders.AddOrUpdate(u => u.RecordKey, new LUGender() { RecordKey = 1, Description = "Male" });
            context.LUGenders.AddOrUpdate(u => u.RecordKey, new LUGender() { RecordKey = 2, Description = "Female" });
            context.LUGenders.AddOrUpdate(u => u.RecordKey, new LUGender() { RecordKey = 3, Description = "Other" });
        }

        private void FillSubscriptionTypes(DataContext context)
        {
            if (context.LUSubscriptionTypes.Count() > 0)
                return;

            /// Lookup.SubscriptionType
            context.LUSubscriptionTypes.AddOrUpdate(u => u.RecordKey, new LUSubscriptionType() { RecordKey = 1, Description = "Anonymous" });
            context.LUSubscriptionTypes.AddOrUpdate(u => u.RecordKey, new LUSubscriptionType() { RecordKey = 2, Description = "Reader" });
            context.LUSubscriptionTypes.AddOrUpdate(u => u.RecordKey, new LUSubscriptionType() { RecordKey = 3, Description = "Reader premium" });
            context.LUSubscriptionTypes.AddOrUpdate(u => u.RecordKey, new LUSubscriptionType() { RecordKey = 4, Description = "Writer premium" });
            context.LUSubscriptionTypes.AddOrUpdate(u => u.RecordKey, new LUSubscriptionType() { RecordKey = 5, Description = "Admin" });
        }

        private void FillSysInterfaceLanguages(DataContext context)
        {
            if (context.LUSysInterfaceLanguages.Count() > 0)
                return;

            /// Lookup.SysInterfaceLanguage
            context.LUSysInterfaceLanguages.AddOrUpdate(u => u.RecordKey, new LUSysInterfaceLanguage() { RecordKey = 1, Description = "English" });
            context.LUSysInterfaceLanguages.AddOrUpdate(u => u.RecordKey, new LUSysInterfaceLanguage() { RecordKey = 2, Description = "עברית" });
        }
    }
}