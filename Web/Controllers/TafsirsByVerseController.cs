﻿using QuranX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuranX.Controllers
{
    public class TafsirsByVerseController : Controller
    {
		public ActionResult Verse(int chapter, int verse)
		{
			ViewBag.Chapter = chapter;
			ViewBag.Verse = verse;
			var model = new Quran_VerseTafsirs(
				chapter: chapter,
				verse: verse
			);
			return View(model);
		}

		[HttpPost]
		public ActionResult RedirectToChapterVerse(int chapter, int verse)
		{
			return RedirectToAction("Verse", new
			{
				Chapter = chapter,
				Verse = verse
			});
		}


    }
}
