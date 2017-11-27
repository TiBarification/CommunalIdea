using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectBOSE.Model
{
    public class historyres
    {
        public decimal accountid { get; set; } // client property
        public string firstname { get; set; } // client property
        public string lastname { get; set; } // client property
        public long age { get; set; } // client property
        public string name { get; set; } // service name
        public double tariff { get; set; } // service property (i dunno know why)
        public string by_date { get; set; } // history property
        public double paid { get; set; } // history property
        public long receiver { get; set; } // history property
    }
}
