using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public struct Clients
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public TypesAccount TypeAccount { get; set; }
        public int NumberAccount { get; set; }
        public float SumAccount { get; set; }
        public DateTime DateOfLastChange { get; set; }
    }
}
