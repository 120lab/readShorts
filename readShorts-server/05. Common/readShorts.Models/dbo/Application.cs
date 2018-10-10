namespace readShorts.Models.dbo
{
    public partial class Application : ModelBase
    {
        public int ApplicationNumber { get; set; }

        public int PersonalStatusCode { get; set; }
        
        public string LastName { get; set; }
        
        public string FirstApplicantIdentityNumber { get; set; }
        
        public string SecondApplicantIdentityNumber { get; set; }

        public int? MainPhoneAreaCode { get; set; }
        
        public string MainPhoneNumber { get; set; }

        public int CityCode { get; set; }

        public int StreetCode { get; set; }
        
        public string HouseNumber { get; set; }
        
        public string ZipCode { get; set; }
        
        public string PO_Box { get; set; }
        
        public string AddressDescription { get; set; }
        
        public string Email { get; set; }

        public int EnrollmentStatusCode { get; set; }

        public bool IsAllowSMS { get; set; }

        public byte? RemindersCounter { get; set; }

        public string Notes { get; set; }
        
        public string PasswordHash { get; set; }
        
        public bool? IsAllowedTargetPrice { get; set; }

        public bool? IsAllowedTenantPrice { get; set; }

        public bool? IsAllowedFutureParticipant1 { get; set; }

        public bool? IsAllowedFutureParticipant2 { get; set; }

        public bool? IsHomeless { get; set; }

        public int TenantPriceEligibilityTypeCode { get; set; }

        public int TargetPriceEligibilityTypeCode { get; set; }             

    }
}
