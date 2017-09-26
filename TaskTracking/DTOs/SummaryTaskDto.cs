using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskTracking.DTOs
{
    public class SummaryTaskDto
    {
        public int TaskTotal { get; set; }
        public int TaskDone { get; set; }
        public int TaskNotDone { get; set; }
        public int TaskRate { get; set; }
    }

    public class SummaryDto
    {
        public int Done { get; set; }
        public int NotDone { get; set; }
    }
}