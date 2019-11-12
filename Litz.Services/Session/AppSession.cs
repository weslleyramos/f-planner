using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Litz.Services.Session
{
    [Serializable]
    public sealed class AppSession
    {
        public static AppSession Current
        {
            get
            {
                if (HttpContext.Current.Session["Singleton_502E69E5-668B-E011-951F-00155DF26207"] == null)
                    HttpContext.Current.Session["Singleton_502E69E5-668B-E011-951F-00155DF26207"] = new AppSession();
                return HttpContext.Current.Session["Singleton_502E69E5-668B-E011-951F-00155DF26207"] as AppSession;
            }
        }

        public User User { get; set; }
        public bool Demo { get; set; }
    }
}