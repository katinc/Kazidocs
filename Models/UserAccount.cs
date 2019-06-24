using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kazidocs2.Models
{
    public class UserAccounts
    {  
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime DateCreated { get; set; }
        public string AccountId { get; set; }
        public string IsActive { get; set; }
        public string IsArchieved { get; set; }
        public Boolean CanLogin { get; set; }
        public Boolean AccountBlocked { get; set; }

    }
}
