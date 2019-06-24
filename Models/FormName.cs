using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kazidocs2.Models
{
    public class FormNames
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public string AccountId { get; set; }
        public string IsActive { get; set; }
        public string IsAchieved { get; set; }
        public string IsFormClosed { get; set; }
        public string DateClosed { get; set; }
        
    }
}
