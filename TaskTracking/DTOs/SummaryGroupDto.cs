using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskTracking.DTOs
{
    public class SummaryGroupDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int TotalMembers { get; set; }
        public int RemainMembers { get; set; }
        public int LeaveMembers { get; set; }
    }
}