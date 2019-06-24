using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kazidocs2.Models
{
    public class FormFields
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DateTypeId { get; set; }
        public string FormNameId { get; set; }
        public DateTime DateCreated { get; set; }
        public string AccountId { get; set; }
    }
}
