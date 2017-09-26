using System.Linq;
using System.Web.Http;
using TaskTracking.DbContext;
using TaskTracking.Models;
using System.Linq.Expressions;
using System;
using TaskTracking.DTOs;
using TaskTracking.Utils;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace TaskTracking.Controllers.api
{
    [Authorize]
    [RoutePrefix("api/Task")]
    public class TaskApiController : ApiController
    {
        //static readonly ITaskRepository repository = new TaskRepository();
        TrackingDbContext db = new TrackingDbContext();
        private static readonly Expression<Func<t_task, TaskViewModels>> AsModelViews = x => new TaskViewModels
        {
            TaskId = x.TaskId,
            Subject = x.Subject,
            Description = x.Description,
            Priority = x.Priority,
            Important = x.Important,
            Type = x.Type,
            GroupId = x.GroupId,
            Status = x.Status,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            Done = x.Done,
            Assign = x.Assign
        };

        [Route("GetAll")]
        public IQueryable<TaskViewModels> GetAllTaskTracking()
        {
            return db.t_task.Select(AsModelViews);
        }
        [Route("{taskId:int}/details")]
        public IHttpActionResult GetTaskDetail(int taskId)
        {
            var task = db.t_task.Where(n => n.TaskId == taskId)
                .Select(AsModelViews).FirstOrDefault();
            if(task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [Route("{condition:int}/GetSummary")]
        public IHttpActionResult GetSummaryTaskForDay(int condition)
        {
            List<SummaryDto> summarys = new List<SummaryDto>();
            switch (condition)
            {
                case Constants.INT_DAY:
                    summarys = (from n in db.t_task
                                where (n.StartDate.Day == DateTime.Now.Day || n.EndDate.Day == DateTime.Now.Day)
                                select new SummaryDto
                                {
                                    NotDone = (n.Done != 100 ? 1 : 0),
                                    Done = (n.Done == 100 ? 1 : 0)
                                }).ToList();
                    break;
                case Constants.INT_MONTH:
                    summarys = (from n in db.t_task
                                where (n.StartDate.Month == DateTime.Now.Month || n.EndDate.Month == DateTime.Now.Month)
                                select new SummaryDto
                                {
                                    NotDone = (n.Done != 100 ? 1 : 0),
                                    Done = (n.Done == 100 ? 1 : 0)
                                }).ToList();
                    break;
                case Constants.INT_QUARTER:
                    int mth = DateTime.Now.Month;
                    int startMonth =0;
                    int endMonth = 0;
                    if (mth <= 3)
                    {
                        startMonth = 1;
                        endMonth = 3;
                    }
                    else if (mth > 3 && mth <= 6)
                    {
                        startMonth = 4;
                        endMonth = 6;
                    }
                    else if (mth > 6 && mth <= 9)
                    {
                        startMonth = 7;
                        endMonth = 9;
                    }
                    else if (mth > 9 && mth <= 12)
                    {
                        startMonth = 10;
                        endMonth = 12;
                    }

                    summarys = (from n in db.t_task
                                where ( ( n.StartDate.Month >= startMonth && n.StartDate.Month <= endMonth)   || (n.EndDate.Month >= startMonth && n.EndDate.Month <= endMonth))
                                select new SummaryDto
                                {
                                    NotDone = (n.Done != 100 ? 1 : 0),
                                    Done = (n.Done == 100 ? 1 : 0)
                                }).ToList();

                    break;
                case Constants.INT_YEAR:
                    summarys = (from n in db.t_task
                                where (n.StartDate.Year == DateTime.Now.Year || n.EndDate.Year == DateTime.Now.Year)
                                select new SummaryDto
                                {
                                    NotDone = (n.Done != 100 ? 1 : 0),
                                    Done = (n.Done == 100 ? 1 : 0)
                                }).ToList();
                    break;
                default:break;
            }

            if (summarys.Count() == 0)
            {
                return NotFound();
            }
            SummaryTaskDto summary;
            int sumTaskDone = summarys.Select(n => n.Done).Sum();
            int sumTaskNotDone = summarys.Select(n => n.NotDone).Sum();
            summary = new SummaryTaskDto
            {
                TaskTotal = sumTaskDone + sumTaskNotDone,
                TaskDone = sumTaskDone,
                TaskNotDone = sumTaskNotDone,
                TaskRate = (sumTaskDone * 100) / (sumTaskDone + sumTaskNotDone)
            };
            
            return Ok(summary);
        }

        [Route("GetTaskGood")]
        public IQueryable<TaskViewModels> GetTaskGoodLst()
        {
            var today =DateTime.Today;
            var experienceTasks =  db.t_task.Where(n => (DateTime.Compare(n.StartDate, today)==0 || DateTime.Compare(n.EndDate, today) == 0) && (n.Done == 100))
                .Select(AsModelViews);
            return experienceTasks;
        }

        [Route("GetTaskImprove")]
        public IQueryable<TaskViewModels> GetTaskImproveLst()
        {
            var today = DateTime.Today;
            var experienceTasks = db.t_task.Where(n => (DateTime.Compare(n.StartDate, today) == 0 || DateTime.Compare(n.EndDate, today) == 0) && (n.Done != 100))
                .Select(AsModelViews);
            return experienceTasks;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
