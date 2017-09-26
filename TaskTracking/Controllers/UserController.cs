using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TaskTracking.DTOs;
using TaskTracking.Models;

namespace TaskTracking.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterBindingModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    //client.BaseAddress = new Uri("http://localhost:65076/api/Account/Register");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<RegisterBindingModel>("http://localhost:65076/api/Account/Register", model);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Login");
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginBindingModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    //client.BaseAddress = new Uri("http://localhost:65076/api/Account/Register");

                    //HTTP POST
                    //var postTask = client.PostAsJsonAsync<LoginBindingModel>("http://localhost:65076/token", model);
                    //postTask.Wait();

                    var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>( "grant_type", "password" ),
                        new KeyValuePair<string, string>( "username", model.Email ),
                        new KeyValuePair<string, string> ( "Password", model.Password )
                    };
                    var content = new FormUrlEncodedContent(pairs);
                    HttpResponseMessage response = client.PostAsync("http://localhost:65076/token", content).Result;
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (response.IsSuccessStatusCode)
                    {
                        Dictionary<string, string> tokenDictionary =
                            JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                        Session["accessToken"] = tokenDictionary["access_token"];
                        Session["userName"] = tokenDictionary["userName"];
                        return RedirectToAction("Index", "TaskManage");
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult LoginBySocial(string accessToken, string userName)
        {
            Session["accessToken"] = accessToken;
            Session["userName"] = userName;

            return this.Json(new { success = true });
        }

        public ActionResult Logout()
        {
            if (Session["accessToken"] != null)
            {
                Session.Remove("accessToken");
            }
            return RedirectToAction("Login");
        }

    }
}