using System;

namespace readShorts.Models.dbo
{
    public partial class CustomerMessage : DboBase
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public DateTime ActitiyDate { get; set; }


    }
}
