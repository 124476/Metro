using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro.Models
{
    public class Train
    {
        public int Id { get; set; }
        public int? StationId { get; set; }
        public bool IsUp { get; set; }
        public bool IsRun { get; set; }
        public int? BranchId { get; set; }
        public Station Station { get; set; }
        public Branch Branch { get; set; }
    }
}
