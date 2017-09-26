using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaskTracking.Utils;

namespace TaskTracking.Models
{
    public class GroupViewModels
    {
        public int GroupId { get; set; }

        [Display(Name ="Group Name")]
        public string GroupName { get; set; }

        [Display(Name = "Group Type")]
        public string GroupType { get; set; }

        public string Description { get; set; }

        [Display(Name = "Number Members")]
        public int Members { get; set; }

        public string Tags { get; set; }

        public List<string> TagList = new List<string>();

        public List<MemberViewModels> MemberLst = new List<MemberViewModels>();

        public Dictionary<string, string> GroupTypeDic = Constants.DIC_GROUP_TYPE;

    }
}