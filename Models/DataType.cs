using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kazidocs2.Models
{
    public class DataTypes
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IsActive { get; set; }
        public string IsArchieved { get; set; }
    }
}
