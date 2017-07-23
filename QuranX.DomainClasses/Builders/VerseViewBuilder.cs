using QuranX.DomainClasses.Models;
using QuranX.DomainClasses.Services;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace QuranX.DomainClasses.Builders
{
    public interface IVerseViewBuilder
    {
        Task<Verse[]> Build(VerseRange verseRange);
    }

    public class VerseViewBuilder : IVerseViewBuilder
    {
        private readonly ObjectSpace ObjectSpace;
        private readonly IChapterRepository ChapterRepository;

        public VerseViewBuilder(
            ObjectSpace objectSpace,
            IChapterRepository chapterRepository)
        {
            ObjectSpace = objectSpace;
            ChapterRepository = chapterRepository;
        }

        public async Task<Verse[]> Build(VerseRange verseRange)
        {
            if (verseRange == null)
                throw new ArgumentNullException(nameof(verseRange));

            int lastVerse =
                verseRange.LastVerse != -1
                ? verseRange.LastVerse
                : 999;

            var translators = 
                (await ObjectSpace.Translators.ToListAsync())
                .ToDictionary(x => x.Code, StringComparer.InvariantCultureIgnoreCase);

            var verses = await (
                from translator in ObjectSpace.Translators
                join verseText in ObjectSpace.VerseTexts
                    on translator.Code equals verseText.TranslatorCode
                where
                    verseText.Chapter == verseRange.Chapter
                    && verseText.Verse >= verseRange.FirstVerse
                    && verseText.Verse <= lastVerse
                orderby
                    translator.DisplayOrder,
                    verseText.Verse
                group verseText by new
                {
                    ChapterNumber = verseText.Chapter,
                    VerseNumber = verseText.Verse
                }
                into verse
                select new
                {
                    ChapterNumber = verse.Key.ChapterNumber,
                    VerseNumber = verse.Key.VerseNumber,
                    VerseTexts = verse.Select(x => x),
                    TafsirCount = (
                        from commentary in ObjectSpace.Commentaries
                        where
                            commentary.Chapter == verse.Key.ChapterNumber
                            && commentary.FirstVerse >= verse.Key.VerseNumber
                            && commentary.LastVerse <= verse.Key.VerseNumber
                        select 1
                      ).Count(),
                    HadithCount = (
                        from hadith in ObjectSpace.Hadiths
                        where
                            hadith.VerseReferences.Any(x => 
                                x.Chapter == verse.Key.ChapterNumber
                                && x.FirstVerse >= verse.Key.VerseNumber
                                && x.LastVerse <= verse.Key.VerseNumber)
                        select 1
                      ).Count(),
                   RootWordCount = (
                        from rootWord in ObjectSpace.VerseAnalysisWords
                        where
                            rootWord.Chapter == verse.Key.ChapterNumber
                            && rootWord.Verse == verse.Key.VerseNumber
                        select 1
                     ).Count()
                }).ToListAsync();

            Verse[] result = (
                from verse in verses
                select new Verse(
                    chapter: ChapterRepository.Get(verse.ChapterNumber),
                    verseNumber: verse.VerseNumber,
                    translations: verse.VerseTexts.Select(x => 
                        new VerseTranslation(
                            translator: translators[x.TranslatorCode],
                            translation: x)),
                    tafsirCount: verse.TafsirCount,
                    hadithCount: verse.HadithCount,
                    rootWordCount: verse.RootWordCount)
                ).ToArray();

            return result;
        }
    }
}
