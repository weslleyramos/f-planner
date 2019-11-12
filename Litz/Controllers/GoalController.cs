using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Litz.Models;
using Litz.Services;
using Litz.Services.Extensions;
using Litz.Services.Session;

namespace Litz.Controllers
{
    public class GoalController : Controller
    {
        private readonly IAccountService _context;

        public GoalController()
        {
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

        [HttpGet]
        public ActionResult Index(DateTime? month)
        {
            var period = month ?? DateTime.Now;

            return View(new GoalIndexViewModel
            {
                CurrentPeriod = period,
                PreviousMonth = period.AddMonths(-1),
                NextMonth = period.AddMonths(1)
            });
        }

        [HttpGet]
        public JsonResult GetChartsJson(DateTime? month)
        {
            var period = month ?? DateTime.Now;

            var goals = _context.GetGoalsWithMetrics(AppSession.Current.User.Id, period.FirstDayOfMonth(), period.LastDayOfMonth());

            return Json(goals, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}