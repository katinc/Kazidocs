using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kazidocs2.Models
{
    public class FormValues
    {
        public long Id { get; set; }
        public string FormNameId { get; set; }
        public string FormFieldId { get; set; }
        public string Value { get; set; }
        public DateTime DateCreated { get; set; }
        public string AccountId { get; set; }
    }
}
