using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kazidocs2.Models
{
    public class UserTokens
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public DateTime Expiry { get; set; }
        public string Token { get; set; }
    }
}
