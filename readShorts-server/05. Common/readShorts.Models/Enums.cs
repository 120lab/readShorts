using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace readShorts.Models
{
    public static class Enums
    {
        public enum SysInterfaceLanguageEnum
        {
            English = 1,  // English
            Hebrew = 2   // עברית
        }

        public enum LUCountryEnum
        {
            Other = 250
        }

        public enum LUGenderEnum
        {
            Other = 3
        }

        public enum LULUPointTypeEnum
        {
            Points = 1,
            VirtualCoins = 2,
            Reading = 3,
            Sharing = 4,
            ReadingFromSharing = 5,
            Feedbacking = 6,
            RegisteredFromInvitation = 7,
            AcceptedWritersFromInvitation = 8,
            FollowWrtier = 9,
            Bookmark = 10,
            Like = 11
        }

        public enum LUSubscriptionTypeEnum
        {
            Anonymous = 1,
            Reader = 2,
            ReaderPremium = 3,
            WriterPremium = 4
        }

        public enum ShortsFeedType
        {
            Home = 0,
            Bookmarked,
            TopLikes,
            FollowedWriters,
            Liked,
            FriendsShares,
            AllKnownRecords,
            ViewedOnly
        }

        public enum LUUserVerificationType
        {
            Anonymous = 1,
            UserCreated,
            VerificationPending,
            Verified,
            Canceled
        }

        public enum LUEventType
        {
            UserInProfilePage = 11
        }
    }
}