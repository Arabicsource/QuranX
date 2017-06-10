using QuranX.DomainClasses.Model;
using QuranX.DomainClasses.ServicesImpl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuranX.SeedDatabase
{
    public class TafsirImporter
    {
        readonly string DataFolder;

        public static void Execute(string dataFolder)
        {
            var instance = new TafsirImporter(dataFolder);
            instance.Execute();
        }

        private TafsirImporter(string dataFolder)
        {
            this.DataFolder = dataFolder;
        }

        private void Execute()
        {
            ClearData();
            string folderPath = Path.Combine(DataFolder, "Tafsirs");
            foreach(string filePath in Directory.GetFiles(folderPath, "*.xml"))
            {
                Console.WriteLine("Importing tafsir: " + Path.GetFileNameWithoutExtension(filePath));
                var xml = System.Xml.Linq.XDocument.Load(File.OpenText(filePath));
                string commentatorCode = ImportCommentator(xml);

                var commentaries =
                    from chapter in xml.Document.Root.Elements("chapter")
                    from commentary in chapter.Elements("commentary")
                    select new Commentary(
                        chapter: int.Parse(chapter.Attribute("index").Value),
                        firstVerse: int.Parse(commentary.Element("firstVerse").Value),
                        lastVerse: int.Parse(commentary.Element("lastVerse").Value),
                        commentatorCode: commentatorCode,
                        text: string.Join("\r\n", commentary.Elements("text").Select(x => x.Value)));

                int total = commentaries.Count();
                int current = 0;
                var nextUpdateTime = DateTime.UtcNow.AddSeconds(1);
                foreach(var commentary in commentaries)
                {
                    current++;
                    if (DateTime.UtcNow > nextUpdateTime)
                    {
                        double percent = Math.Ceiling(current * 100f / total);
                        Console.WriteLine($"{commentatorCode} {current} ({percent}%)");
                        nextUpdateTime = DateTime.UtcNow.AddSeconds(1);
                    }
                    using (var objectSpace = new ObjectSpace())
                    {
                        objectSpace.Commentaries.Add(commentary);
                        objectSpace.SaveChanges();
                    }
                }
            }
        }

        private static string ImportCommentator(XDocument xml)
        {
            using (var objectSpace = new ObjectSpace())
            {
                string commentatorCode = xml.Document.Root.Element("code").Value;
                string commentatorName = xml.Document.Root.Element("mufassir").Value;
                objectSpace.Commentators.Add(
                    new Commentator(
                        code: commentatorCode,
                        name: commentatorName));
                return commentatorCode;
            }
        }

        private void ClearData()
        {
            using (var objectSpace = new ObjectSpace())
            {
                Console.WriteLine("Clearing Commentators");
                objectSpace.Commentators.RemoveRange(objectSpace.Commentators);
                Console.WriteLine("Clearing Commentaries");
                objectSpace.Commentaries.RemoveRange(objectSpace.Commentaries);
                objectSpace.SaveChanges();
            }
        }
    }
}
