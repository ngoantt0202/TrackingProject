using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using TaskTracking.DTOs;
using TaskTracking.Models;
using TaskTracking.Utils;

namespace TaskTracking.Controllers
{
    public class GroupManageController : Controller
    {

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
            IEnumerable<GroupViewModels> viewModels = GetAll(token);
            ViewBag.SummaryGroup = GetSummaryGroupForDay(token);
            return View(viewModels);
        }
        // GET: GroupManage
        public ActionResult Create()
        {
            GroupViewModels viewModel = new GroupViewModels();
            //viewModel.GroupTypeDic = Constants.DIC_GROUP_TYPE;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(GroupViewModels model)
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
            if (ModelState.IsValid)
            {
                string[] stringSeparators = new string[] { "," };
                if (model.Tags != null)
                {
                    string[] tags = model.Tags.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                    model.Members = tags.Count();
                    if (tags.Count() > 0)
                    {
                        model.TagList = new List<string>(tags);
                    }
                }

                // call api add new
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    client.BaseAddress = new Uri("http://localhost:65076/api/Group/");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<GroupViewModels>("CreateGroup", model);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        private IEnumerable<GroupViewModels> GetAll(string token)
        {
            IEnumerable<GroupViewModels> viewModels = null;
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("http://localhost:65076/api");
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HTTP GET
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseTask = client.GetAsync("http://localhost:65076/api/Group/GetAll");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<GroupViewModels>>();
                    readTask.Wait();

                    viewModels = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    viewModels = Enumerable.Empty<GroupViewModels>();
                }
            }

            return viewModels;
        }

        public ActionResult Details(int? groupId)
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
            if (groupId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GroupViewModels group = null;
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("http://localhost:65076/api");
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string url_api = "http://localhost:65076/api/Group/" + groupId + "/details";
                //HTTP GET
                var responseTask = client.GetAsync(url_api);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<GroupViewModels>();
                    readTask.Wait();

                    group = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    group = new GroupViewModels();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(group);
        }

        private IEnumerable<SummaryGroupDto> GetSummaryGroupForDay(string token)
        {
            IEnumerable<SummaryGroupDto> viewModels = null;
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("http://localhost:65076/api");
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HTTP GET
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseTask = client.GetAsync("http://localhost:65076/api/Group/GetSummaryGroupForDay");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<SummaryGroupDto>>();
                    readTask.Wait();

                    viewModels = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    viewModels = Enumerable.Empty<SummaryGroupDto>();
                }
            }

            return viewModels;
        }
    }
}