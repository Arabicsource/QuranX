using QuranX.DomainClasses.Services;
using System.Web.Mvc;

namespace QuranX.Website.Controllers
{
    public class QuranController : Controller
    {
        private readonly ChapterRepository ChapterRepository;

        public QuranController(ChapterRepository chapterRepository)
        {
            ChapterRepository = chapterRepository;
        }

        // GET: Quran
        public ActionResult Chapters()
        {
            return View(ChapterRepository.All());
        }
    }
}