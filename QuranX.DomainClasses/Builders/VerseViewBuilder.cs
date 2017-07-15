
using QuranX.DomainClasses.Models;
using QuranX.DomainClasses.Services;
using System;
using System.Collections.Generic;
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
                    && (
                        verseRange.LastVerse == -1 
                        || verseText.Verse <= verseRange.LastVerse
                    )
                orderby
                    translator.DisplayOrder,
                    verseText.Verse
                group verseText by new
                {
                    ChapterNumber = verseText.Chapter,
                    VerseNumber = verseText.Verse
                } 
                into verse
                select verse
                ).ToListAsync();

            Verse[] result = (
                from verse in verses
                select new Verse(
                    chapter: ChapterRepository.Get(verse.Key.ChapterNumber),
                    verseNumber: verse.Key.VerseNumber,
                    translations: verse.Select(x => 
                        new VerseTranslation(
                            translator: translators[x.TranslatorCode],
                            translation: x)),
                    tafsirCount: 0,
                    hadithCount: 0,
                    rootWordCount: 0)
                ).ToArray();

            return result;
        }
    }
}
