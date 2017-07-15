using QuranX.DomainClasses.Builders;
using QuranX.DomainClasses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QuranX.Website.Controllers
{
    public class VerseBrowseController : Controller
    {
        private IVerseViewBuilder VerseViewBuilder;

        public VerseBrowseController(IVerseViewBuilder verseViewBuilder)
        {
            VerseViewBuilder = verseViewBuilder;
        }

        public async Task<ActionResult> Index(int chapter, int verse)
        {
            var verseRange = new VerseRange(
                chapter: chapter,
                firstVerse: verse,
                lastVerse: verse);
            Verse[] viewModel = await VerseViewBuilder.Build(verseRange);

            return View(viewModel);
        }
    }
}