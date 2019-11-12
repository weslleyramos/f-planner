using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Litz.Services;
using Litz.Services.Extensions;
using Litz.Services.Session;
using Litz.Services.Models;

namespace Litz.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountService _context;

        public HomeController()
        {
            AppSession.Current.User = new User { Id = 1 };
            AppSession.Current.Demo = true;

            if (!AppSession.Current.Demo)
            {
                _context = new AccountService();
            }
            else
            {
                _context = new AccountServiceMock();
            }
        }

        #region Index

        public ActionResult Index()
        {
            return View(_context.GetAccountInfo(AppSession.Current.User.Id));
        }

        public JsonResult GetSummaryPerGroup(DateTime periodStart, DateTime periodEnd)
        {
            var data = _context.GetSummaryPerGroup(AppSession.Current.User.Id, periodStart, periodEnd);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}