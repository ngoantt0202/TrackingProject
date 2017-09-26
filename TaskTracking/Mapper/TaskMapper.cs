using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskTracking.DbContext;
using TaskTracking.Models;

namespace TaskTracking.Mapper
{
    public class TaskMapper
    {
        public static TaskViewModels ConvertTableToView(t_task model)
        {
            TaskViewModels view = new TaskViewModels();
           
            return view;
        }
    }
}