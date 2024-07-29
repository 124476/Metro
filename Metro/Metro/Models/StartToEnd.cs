using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro.Models
{
    public class StartToEnd
    {
        public int Id { get; set; }
        public int? StationStartId { get; set; }
        public int? StationEndId { get; set; }
        public int? BranchId { get; set; }
        public Station StationStart { get; set; }
        public Station StationEnd { get; set; }
        public Branch Branch { get; set; }
    }
}
