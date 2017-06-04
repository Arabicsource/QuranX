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
        readonly ObjectSpace ObjectSpace;
        private object XDocment;

        public static void Execute(ObjectSpace objectSpace, string dataFolder)
        {
            var instance = new TafsirImporter(objectSpace, dataFolder);
            instance.Execute();
        }

        private TafsirImporter(ObjectSpace objectSpace, string dataFolder)
        {
            this.ObjectSpace = objectSpace;
            this.DataFolder = dataFolder;
        }

        private void Execute()
        {
            ClearData();
            string folderPath = Path.Combine(DataFolder, "Tafsirs");
            foreach(string filePath in Directory.GetFiles(folderPath, "*.xml"))
            {
                Console.WriteLine("Importing tafsir: " + Path.GetFileNameWithoutExtension(filePath));
                var xml = XDocument.Load(File.OpenText(filePath));
                string commentatorCode = xml.Document.Root.Element("code").Value;
                string commentatorName = xml.Document.Root.Element("mufassir").Value;
                ObjectSpace.Commentators.Add(
                    new Commentator(
                        code: commentatorCode,
                        name: commentatorName));

                var commentaries =
                    from chapter in xml.Document.Root.Elements("chapter")
                    from commentary in chapter.Elements("commentary")
                    select new Commentary(
                        chapter: int.Parse(chapter.Attribute("index").Value),
                        firstVerse: int.Parse(commentary.Element("firstVerse").Value),
                        lastVerse: int.Parse(commentary.Element("lastVerse").Value),
                        commentatorCode: commentatorCode,
                        text: string.Join("\r\n", commentary.Elements("text").Select(x => x.Value)));
                ObjectSpace.Commentaries.AddRange(commentaries);
                ObjectSpace.SaveChanges();
            }
        }

        private void ClearData()
        {
            Console.WriteLine("Clearing Commentators");
            ObjectSpace.Commentators.RemoveRange(ObjectSpace.Commentators);
            Console.WriteLine("Clearing Commentaries");
            ObjectSpace.Commentaries.RemoveRange(ObjectSpace.Commentaries);
            ObjectSpace.SaveChanges();
        }
    }
}
