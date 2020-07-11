using System;
using System.Collections.Generic;
using System.Text;

namespace BANKEST2.Core.Entities
{
    public class TestsHistory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public DateTime DateTime { get; set; }
        public string Responsible { get; set; }
    }
}
