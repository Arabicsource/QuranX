using QuranX.DomainClasses.Services;
using System.Linq;
using System.Web.Mvc;

namespace QuranX.Website.Controllers
{
    public class ChapterController : Controller
    {
        private readonly ChapterRepository ChapterRepository;

        public ChapterController(ChapterRepository chapterRepository)
        {
            ChapterRepository = chapterRepository;
        }

        public ActionResult Index()
        {
            return View(ChapterRepository.All().OrderBy(x => x.Number));
        }
    }
}