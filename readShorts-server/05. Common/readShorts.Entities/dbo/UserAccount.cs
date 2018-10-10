using readShorts.Entities.LOOKUP;
using System;
using System.Collections.Generic;

namespace readShorts.Entities.dbo
{
    public partial class UserAccount : EntityBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserAccount()
        {
        }

        /// <summary>
        /// Columns
        /// </summary>
        public string UserSecurityNumber { get; set; }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShortBio { get; set; }
        public string PicturePath { get; set; }
        public string HashedPassword { get; set; }
        public string ClientIP { get; set; }
        public string EmailAddress { get; set; }
        public string MobileSerialNumber { get; set; }
        public DateTime LastActitiyDate { get; set; }
        public bool IsAnonimousConnect { get; set; }
        public bool IsFBConnect { get; set; }
        public bool IsTWConnect { get; set; }
        public bool IsGGLConnect { get; set; }
        public bool IsEmailConnect { get; set; }
        public DateTime BirthDate { get; set; }
        public bool EmailForShortIMightLike { get; set; }
        public bool EmailForShortOfTheWeek { get; set; }
        public bool EmailForShortFollowingWriter { get; set; }
        public bool EmailForNewSAndUpdates { get; set; }
        public string ExternalLink { get; set; }
        public string ExternalLinkText { get; set; }
        public string PersonalId { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }

        /// FK
        public Int64 LUSubscriptionTypeKey { get; set; }

        public Int64 LUSysInterfacelanguageKey { get; set; }
        public Int64 LUGenderKey { get; set; }
        public Int64 LUCountryKey { get; set; }
        public Int64 LUUserVerificationTypeKey { get; set; }
    }
}