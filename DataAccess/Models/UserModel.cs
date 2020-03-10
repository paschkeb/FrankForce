using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        //public int UserId { get; set; }
        public int OrganizationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
