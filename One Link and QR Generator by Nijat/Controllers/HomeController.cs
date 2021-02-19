using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using One_Link_and_QR_Generator_by_Nijat.Models;
using One_Link_and_QR_Generator_by_Nijat.Repository.IRepository;

namespace One_Link_and_QR_Generator_by_Nijat.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEvents eventsrepo;

        public HomeController(IEvents eventsrepo)
        {
            this.eventsrepo = eventsrepo;
        }
        public IActionResult Index()
        {
            ViewBag.BaseUrl = new Uri(HttpContext.Request.GetDisplayUrl()).GetLeftPart(UriPartial.Authority);
            if (HttpContext.Request.Cookies["code"] == null)
            {
                URLS uRLS = eventsrepo.createNewUrl();
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddMinutes(20);
                Response.Cookies.Append("code", uRLS.Code, option);

                return View(uRLS);
            }
            else
            {
                return View(eventsrepo.getUrls(HttpContext.Request.Cookies["code"]));
            }
        }

        [Route("{id}")]
        public IActionResult Redirecting(string id)
        {
            URLS uRLS = eventsrepo.getUrls(id);
            if (uRLS == null)
            {
                return RedirectToAction("Info");
            }
            else if (uRLS.AppStore == null && uRLS.GooglePlay == null && uRLS.Web == null)
            {
                return RedirectToAction("Info");
            }
            else
            {
                if (uRLS.Web != null)
                {
                    if (uRLS.AppStore == null)
                    {
                        uRLS.AppStore = uRLS.Web;
                    }
                    if (uRLS.GooglePlay == null)
                    {
                        uRLS.GooglePlay = uRLS.Web;
                    }
                }
                else if (uRLS.AppStore != null)
                {
                    if (uRLS.Web == null)
                    {
                        uRLS.Web = uRLS.AppStore;
                    }
                    if (uRLS.GooglePlay == null)
                    {
                        uRLS.GooglePlay = uRLS.AppStore;
                    }
                }
                else
                {
                    if (uRLS.Web == null)
                    {
                        uRLS.Web = uRLS.GooglePlay;
                    }
                    if (uRLS.AppStore == null)
                    {
                        uRLS.AppStore = uRLS.GooglePlay;
                    }
                }
            }

            return View(uRLS);
        }


        public IActionResult Info()
        {
            return View();
        }

        [HttpPost]
        public JsonResult updateUrls(string Appstore, string GooglePlay, string Web)
        {
            if (HttpContext.Request.Cookies["code"] != null)
            {
                return Json(eventsrepo.updateUrls(HttpContext.Request.Cookies["code"], Appstore, GooglePlay, Web));
            }
            return Json(false);
        }
    }
}
