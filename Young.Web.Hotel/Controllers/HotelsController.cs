using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Young.Core.Cqrs;
using Young.App.CQRS.Aggregate;
using Young.App.CQRS.Query;
using Young.App.CQRS.QueryResult;
using Young.Util;

using Newtonsoft.Json;
using System.IO;
using System.Data;

namespace Young.Web.Hotel.Controllers
{
    [HandleError()]
    public class HotelsController : Controller
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        //Dependency Injection
        public HotelsController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }
        
        // GET: Hotels
        [HttpGet]
        public ActionResult Index()
        {           
            return View();
        }
        
        [HttpGet]
        [Route("Hotels/InitHotelList")]
        public JsonResult InitHotelList()
        {
            var result = _queryDispatcher.Dispatch<HotelsByIDDateQuery, HotelsByIDDateQueryResult>(null);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]        
        public ActionResult HotelsList(HotelsByIDDateQuery query)
        {
            var result = _queryDispatcher.Dispatch<HotelsByIDDateQuery, HotelsByIDDateQueryResult>(query);

            switch (query.ReportMode)
            {
                case 1://HTMLVIEW
                    return PartialView("HotelsListHTMLView", result);
                case 2://JSONVIEW
                    return PartialView("HotelsListJSONView", result);
                case 3://EXCEL DOWNLOAD
                    {
                        string json = JsonConvert.SerializeObject(result);                       
                      
                        string generatedPath = Tools.GenerateExcel2007(Server.MapPath("~/App_Data/"), json);

                        if (String.IsNullOrEmpty(generatedPath)) return PartialView("HotelsListHTMLView", result);

                        this.TempData["DownloadFileName"] = generatedPath;

                        this.TempData["DownloadContentType"] = System.Net.Mime.MediaTypeNames.Application.Octet;
                        
                        this.TempData.Keep("DownloadFileName");

                        this.TempData.Keep("DownloadContentType");

                        return new JavaScriptResult() { Script = "document.location = \"" + this.Url.Action("Download") + "\";" };
                    }                
                default:
                    return PartialView("HotelsListHTMLView", result);                    
            };            
        }              

        [HttpGet]
        [Route("api/Ratings/{hotelid:int}/{date:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        [Route("api/Ratings/{hotelid:int}/{*date:datetime:regex(\\d{4}/\\d{2}/\\d{2})}")]
        public JsonResult Ratings(int hotelid,DateTime date)
        {
            HotelsByIDArrivalQuery query = new HotelsByIDArrivalQuery()
            {
                HotelID = hotelid,
                ArrivalDate = date.ToString()
            };

            var result = _queryDispatcher.Dispatch<HotelsByIDArrivalQuery, HotelsByIDDateQueryResult>(query);
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Download()
        {
            return File((string)this.TempData["DownloadFileName"], (string)this.TempData["DownloadContentType"], Path.GetFileName((string)this.TempData["DownloadFileName"]));
        }

        private bool IsAjax(ExceptionContext filterContext)
        {
            return filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
        protected override void OnException(ExceptionContext filterContext)
        {            

            // if the request is AJAX return JSON else view.
            if (IsAjax(filterContext))
            {
                //Because its a exception raised after ajax invocation
                //Lets return Json
                filterContext.Result = new JsonResult()
                {
                    Data = filterContext.Exception.Message,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
            }
            else
            {
                filterContext.ExceptionHandled = true;

                // Redirect on error:
                filterContext.Result = RedirectToAction("Index", "Error");

                // OR set the result without redirection:
                filterContext.Result = new ViewResult
                {
                    ViewName = "Error"

                };
            }          
        }
    }
}