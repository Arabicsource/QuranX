using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuranX.Website.Controllers
{
    public class VerseBrowseController : Controller
    {
        public ActionResult Index(int chapter, int firstVerse, int lastVerse)
        {
            return View();
        }
    }
}