using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaskTracking.Utils;

namespace TaskTracking.Models
{
    public class TaskViewModels
    {
        public int TaskId { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string Priority { get; set; }

        public string Important { get; set; }

        public string Type { get; set; }

        public int GroupId { get; set; }
        
        public string Status { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public int Done { get; set; }

        public string Assign { get; set; }

        public Dictionary<string, string> PriorityDic = Constants.DIC_PRIORITY;

        public Dictionary<string, string> ImportantDic = Constants.DIC_IMPORTANT;

        public Dictionary<string, string> StatusDic = Constants.DIC_STATUS;

        public Dictionary<string, string> TypeDic = Constants.DIC_TYPE;
    }
}