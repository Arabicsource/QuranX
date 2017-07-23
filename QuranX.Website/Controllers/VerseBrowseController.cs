using QuranX.DomainClasses.Builders;
using QuranX.DomainClasses.Models;
using QuranX.Website.ViewModels;
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
		private readonly IVerseViewBuilder VerseViewBuilder;
		private readonly IVerseNavigatorBuilder VerseNavigatorBuilder;
		private readonly IVerseRangesBuilder VerseRangesBuilder;

		public VerseBrowseController(
			IVerseViewBuilder verseViewBuilder,
			IVerseNavigatorBuilder verseNavigatorBuilder,
			IVerseRangesBuilder verseRangesBuilder
			)
		{
			VerseViewBuilder = verseViewBuilder;
			VerseNavigatorBuilder = verseNavigatorBuilder;
			VerseRangesBuilder = verseRangesBuilder;
		}

		public async Task<ActionResult> Index(int chapter, int verse)
		{
			var verseRange = new VerseRange(
					chapter: chapter,
					firstVerse: verse,
					lastVerse: verse);
			VerseRange[] quranVerseRanges = VerseRangesBuilder.BuildForQuran();
			Verse[] verses = await VerseViewBuilder.Build(verseRange);
			VerseNavigator verseNavigator = VerseNavigatorBuilder.Build(chapter, verse, quranVerseRanges);

			var viewModel = new VerseBrowseViewModel(
				verses: verses,
				verseNavigator: verseNavigator);

			return View(viewModel);
		}

		[ChildActionOnly]
		public ActionResult VerseNavigator(VerseNavigator verseNavigator)
		{
			return PartialView("VerseNavigator", verseNavigator);
		}
	}
}