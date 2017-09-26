using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using TaskTracking.Models;
using System.Security.Claims;
using TaskTracking.DbContext;
using System.Net;
using TaskTracking.DTOs;
using TaskTracking.Utils;
using System.Net.Http.Headers;

namespace TaskTracking.Controllers
{
    //[Authorize]
    //[RoutePrefix("Task")]
    public class TaskManageController : Controller
    {
        // GET: Task
        //[Route("GetAll")]
        public ActionResult Index()
        {
            string token = "";
            if (Session["accessToken"] != null)
            {
                token = (string)Session["accessToken"];
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
            IEnumerable<TaskViewModels> tasks = null;
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("http://localhost:65076/api");
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HTTP GET
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseTask = client.GetAsync("http://localhost:65076/api/Task/GetAll");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<TaskViewModels>>();
                    readTask.Wait();

                    tasks = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    tasks = Enumerable.Empty<TaskViewModels>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            ViewBag.TaskForDay = GetSummaryTask(Constants.INT_DAY, token);
            ViewBag.TaskForMonth = GetSummaryTask(Constants.INT_MONTH, token);
            ViewBag.TaskForQuarter = GetSummaryTask(Constants.INT_QUARTER, token);
            ViewBag.TaskForYear = GetSummaryTask(Constants.INT_YEAR, token);
            return View(tasks);
        }
        

        public ActionResult Details(int? taskId)
        {
            if(taskId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TaskViewModels task = null;
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("http://localhost:65076/api");
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string token = "";
                if (Session["accessToken"] != null)
                {
                    token = (string)Session["accessToken"];
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string url_api = "http://localhost:65076/api/Task/" + taskId + "/details";
                //HTTP GET
                var responseTask = client.GetAsync(url_api);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<TaskViewModels>();
                    readTask.Wait();

                    task = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    task = new TaskViewModels();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(task);
        }

        public ActionResult Improvement()
        {
            string token = "";
            if (Session["accessToken"] != null)
            {
                token = (string)Session["accessToken"];
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
            
            IEnumerable<TaskViewModels> taskGoodLst = GetTaskGoodLst(token);
            ViewBag.taskImproveLst = GetTaskImproveLst(token);
            return View(taskGoodLst);
        }

        private SummaryTaskDto GetSummaryTask(int condition, string token)
        {
            SummaryTaskDto taskForDay = null;
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("http://localhost:65076/api");
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //HTTP GET
                string url_api = "http://localhost:65076/api/Task/" + condition + "/GetSummary";
                var responseTask = client.GetAsync(url_api);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<SummaryTaskDto>();
                    readTask.Wait();

                    taskForDay = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    taskForDay = new SummaryTaskDto
                    {
                        TaskTotal = 0,
                        TaskDone = 0,
                        TaskNotDone = 0,
                        TaskRate = 0
                    };
                }
            }
            return taskForDay;
        }

        private IEnumerable<TaskViewModels> GetTaskGoodLst(string token)
        {
            IEnumerable<TaskViewModels> experiences = null;
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("http://localhost:65076/api");
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HTTP GET
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseTask = client.GetAsync("http://localhost:65076/api/Task/GetTaskGood");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<TaskViewModels>>();
                    readTask.Wait();

                    experiences = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    experiences = Enumerable.Empty<TaskViewModels>();
                }
            }
            return experiences;
        }

        private IEnumerable<TaskViewModels> GetTaskImproveLst(string token)
        {
            IEnumerable<TaskViewModels> improvementLst = null;
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("http://localhost:65076/api");
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HTTP GET
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseTask = client.GetAsync("http://localhost:65076/api/Task/GetTaskImprove");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<TaskViewModels>>();
                    readTask.Wait();

                    improvementLst = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    improvementLst = Enumerable.Empty<TaskViewModels>();
                }
            }
            return improvementLst;
        }
    }
}