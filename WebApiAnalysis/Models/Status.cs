using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAnalysis.Models
{

    public class Status
    {
        public string Result { set; get; }
        public List<string> Errors { set; get; }
    }
}
