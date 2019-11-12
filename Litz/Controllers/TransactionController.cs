using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Litz.Filter;
using Litz.Models;
using Litz.Services;
using Litz.Services.Extensions;
using Litz.Services.Session;

namespace Litz.Controllers
{
    public class TransactionController : Controller
    {
        private readonly AccountService _context = new AccountService();

        #region Index

        public ActionResult Index(DateTime? month)
        {
            var period = month ?? DateTime.Now;
            var transactions = _context.GetTransactions(AppSession.Current.User.Id, period.FirstDayOfMonth(), period.LastDayOfMonth());

            return View(new TransactionIndexViewModel
            {
                CurrentPeriod = period,
                PreviousMonth = period.AddMonths(-1),
                NextMonth = period.AddMonths(1),
                Transactions = transactions
            });
        }

        #endregion

        #region Create

        [HttpGet]
        public PartialViewResult Create(string type)
        {
            ViewBag.GroupId = _context.GetGroups(AppSession.Current.User.Id);
            ViewBag.TransactionType = type;

            return PartialView();
        }


        [HttpPost]
        [HandleErrorJsonCall]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "Type,Date,Description,GroupId,Amount")] Transaction transaction)
        {
            transaction.UserId = AppSession.Current.User.Id;
            var result = _context.Create(transaction);

            return Json(result);
        }

        #endregion

        #region Delete

        [HttpPost]
        [HandleErrorJsonCall]
        [ValidateAntiForgeryToken]
        public JsonResult Delete(int id)
        {
            var result = _context.Delete(new Transaction
            {
                Id = id
            });

            return Json(result);
        }

        #endregion
    }
}