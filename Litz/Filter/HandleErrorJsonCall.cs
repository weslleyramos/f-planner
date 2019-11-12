using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Litz.Filter
{
    public class HandleErrorJsonCall : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

            filterContext.Result = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                ContentType = "application/json",
                Data = new
                {
                    Message = filterContext.Exception.Message,
                    ExceptionStack = filterContext.Exception.StackTrace,
                    ExceptionSource = filterContext.Exception.Source,
                    ExceptionHelpLink = filterContext.Exception.HelpLink,
                    ExceptionMsg = filterContext.Exception.ToString()
                }
            };
        }
    }
}