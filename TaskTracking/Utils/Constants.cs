using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskTracking.Utils
{
    public class Constants
    {
        //define api
        public const string ROOT_URL_API = "http://localhost:65076/api/";

        // define const
        public const int INT_DAY = 1;
        public const int INT_MONTH = 2;
        public const int INT_QUARTER = 3;
        public const int INT_YEAR = 4;

        //
        public const string FORMAT_YYYY_MM_DD = "yyyy/MM/dd";

        // dictionary
        public static readonly Dictionary<string, string> DIC_PRIORITY = new Dictionary<string, string>()
        {
            {"1","Low" },
            {"2","Normal" },
            {"3","High" },
            {"4","Urgent" },
            {"5","Immediate" }
        };
        public static readonly Dictionary<string, string> DIC_IMPORTANT = new Dictionary<string, string>()
        {
            {"1","Low" },
            {"2","Normal" },
            {"3","High" },
            {"4","Urgent" },
            {"5","Immediate" }
        };

        public static readonly Dictionary<string, string> DIC_STATUS = new Dictionary<string, string>()
        {
            {"1","New" },
            {"2","In Progress" },
            {"3","Resolved" },
            {"4","Feedback" },
            {"5","Closed" },
            {"6","Rejected" },
            {"7","Canceled" },
            {"8","Pending" }
        };
        public static readonly Dictionary<string, string> DIC_TYPE = new Dictionary<string, string>()
        {
            {"1","Once" },
            {"2","Repeat" }
        };

        public static readonly Dictionary<string, string> DIC_GROUP_TYPE = new Dictionary<string, string>()
        {
            {"1","Family" },
            {"2","Business" },
            {"3","Education" },
            {"4","Other" }
        };
    }
}