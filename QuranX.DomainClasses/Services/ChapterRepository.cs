using QuranX.DomainClasses.Model;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace QuranX.DomainClasses.Services
{
    public interface IChapterRepository
    {
        Chapter Get(int number);
        Chapter[] All();
    }

    public class ChapterRepository : IChapterRepository
    {
        private readonly ObjectSpace ObjectSpace;
        private static readonly object SyncRoot = new object();
        private static ConcurrentDictionary<int, Chapter> Chapters;

        public ChapterRepository(ObjectSpace objectSpace)
        {
            this.ObjectSpace = objectSpace;
        }

        public Chapter[] All()
        {
            return GetChapters().Values.OrderBy(x => x.Number).ToArray();
        }

        public Chapter Get(int number)
        {
            return GetChapters()[number];
        }

        private ConcurrentDictionary<int, Chapter> GetChapters()
        {
            if (Chapters == null)
            {
                lock(SyncRoot)
                {
                    if (Chapters == null)
                    {
                        Chapters = new ConcurrentDictionary<int, Chapter>(
                            ObjectSpace.Chapters
                            .ToList()
                            .Select(
                                x => new KeyValuePair<int, Chapter>(x.Number, x)));
                    }
                }
            }
            return Chapters;
        }
    }
}
