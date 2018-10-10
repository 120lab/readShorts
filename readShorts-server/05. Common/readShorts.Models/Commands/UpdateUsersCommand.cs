using Framework.Core.Interfaces.CQRS;
using readShorts.Models.dbo;
using System;
using System.Collections.Generic;

namespace readShorts.Models.Commands
{
    public class UpdateUsersCommand : ICommand
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? EmailForShortIMightLike { get; set; }
        public bool? EmailForShortOfTheWeek { get; set; }
        public bool? EmailForShortFollowingWriter { get; set; }
        public bool? EmailForNewSAndUpdates { get; set; }

        /// FK
        public Int64? LUSubscriptionTypeKey { get; set; }

        //public Int64 LUSysInterfacelanguageKey { get; set; }
        public Int64? LUGenderKey { get; set; }

        public Int64? LUCountryKey { get; set; }

        public Int64? LUUserVerificationTypeKey { get; set; }

        public Int64? LUSysInterfacelanguageKey { get; set; }
    }
}