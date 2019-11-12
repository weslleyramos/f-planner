using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Litz.Filter;
using Litz.Services;
using Litz.Services.Session;

namespace Litz.Controllers
{
    public class GroupController : Controller
    {
        private readonly IAccountService _context;

        public GroupController()
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

        public ActionResult Index()
        {
            var group = _context.GetGroups(AppSession.Current.User.Id);

            return View(group.ToList());
        }

        #endregion

        #region Create

        [HttpGet]
        public PartialViewResult Create()
        {
            return PartialView();
        }


        [HttpPost]
        [HandleErrorJsonCall]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "Name,Color")] Group group)
        {
            group.UserId = AppSession.Current.User.Id;
            var result = _context.Create(group);

            return Json(result);
        }

        #endregion

        #region Edit

        [HttpGet]
        [HandleErrorJsonCall]
        public ActionResult Edit(int id)
        {
            var group = _context.GetGroup(AppSession.Current.User.Id, id);

            return PartialView(group);
        }

 
        [HttpPost]
        [HandleErrorJsonCall]
        [ValidateAntiForgeryToken]
        public JsonResult Edit([Bind(Include = "Id,Name,Color")] Group group)
        {
            var result = _context.Update(group);
           
            return Json(result);
        }

        #endregion
    }
}
