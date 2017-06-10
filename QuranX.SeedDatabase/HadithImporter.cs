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
    public class HadithImporter
    {
        private readonly ObjectSpace ObjectSpace;
        private readonly string DataFolder;

        public static void Execute(ObjectSpace objectSpace, string dataFolder)
        {
            var instance = new HadithImporter(objectSpace, dataFolder);
            instance.Execute();
        }

        private HadithImporter(ObjectSpace objectSpace, string dataFolder)
        {
            this.ObjectSpace = objectSpace;
            this.DataFolder = dataFolder;
        }

        private void Execute()
        {
            ClearData();
            string folderPath = Path.Combine(DataFolder, "Hadiths");
            foreach (string filePath in Directory.GetFiles(folderPath, "*.xml"))
            {
                Console.WriteLine("Importing hadith: " + Path.GetFileNameWithoutExtension(filePath));
                var xml = XDocument.Load(File.OpenText(filePath));
                var collector = new HadithCollector(
                    code: xml.Document.Root.Element("code").Value,
                    name: xml.Document.Root.Element("name").Value);
                ObjectSpace.HadithCollectors.Add(collector);
                ImportReferenceDefinitions(xml, collector.Code);
                ImportHadiths(xml, collector.Code);
                ObjectSpace.SaveChanges();
            }
        }

        private void ImportReferenceDefinitions(XDocument xml, string collectorCode)
        {
            foreach(XElement definitionElement in xml.Document.Root.Descendants("referenceDefinition"))
            {
                string code = definitionElement.Element("code").Value;
                string name = definitionElement.Element("name").Value;
                string valuePrefix = definitionElement.Element("valuePrefix")?.Value;
                bool isPrimary = bool.Parse(definitionElement.Element("isPrimary").Value);
                string[] keyParts = definitionElement.Descendants("part").Select(x => x.Value).ToArray();
                var definition = new HadithReferenceDefinition(
                    collectorCode: collectorCode,
                    code: code,
                    name: name,
                    isPrimary: isPrimary,
                    keyPart1Name: keyParts[0],
                    keyPart2Name: keyParts.Length > 1 ? keyParts[1] : null,
                    keyPart3Name: keyParts.Length > 2 ? keyParts[2] : null,
                    valuePrefix: valuePrefix);
                ObjectSpace.HadithReferenceDefinitions.Add(definition);
            }
        }

        private void ImportHadiths(XDocument xml, string collectorCode)
        {
            IEnumerable<XElement> hadiths = xml.Document.Root.Descendants("hadith");
            int counter = 0;
            float total = hadiths.Count() / 1f;
            var nextDisplayTime = DateTime.UtcNow.AddSeconds(1);
            foreach(XElement hadithElement in hadiths)
            {
                counter++;
                if (DateTime.UtcNow > nextDisplayTime)
                {
                    double percent = Math.Ceiling(counter * 100 / total);
                    Console.WriteLine($"{collectorCode} {counter} of {total} ({percent}%)");
                    nextDisplayTime = DateTime.UtcNow.AddSeconds(1);
                }
                var references = new List<HadithReference>();
                foreach(XElement referenceElement in hadithElement.Element("references").Elements("reference"))
                {
                    var parts = referenceElement.Descendants("part").Select(x => x.Value).ToArray();
                    string code = referenceElement.Element("code").Value;
                    string suffix = referenceElement.Element("suffix")?.Value;
                    string part1 = parts[0];
                    string part2 = parts.Length > 1 ? parts[1] : null;
                    string part3 = parts.Length > 2 ? parts[2] : null;
                    references.Add(new HadithReference(
                        code: code,
                        part1: part1,
                        part2: part2,
                        part3: part3,
                        suffix: suffix));
                }
                string arabic =
                    string.Join("\r\n", hadithElement.Element("arabic").Elements("text").Select(x => x.Value));
                string english =
                    string.Join("\r\n", hadithElement.Element("english").Elements("text").Select(x => x.Value));
                var hadith = new Hadith(
                    collectorCode: collectorCode, 
                    arabic: arabic, 
                    english: english, 
                    references: references);
                ObjectSpace.Hadiths.Add(hadith);
                ObjectSpace.SaveChanges();
            }
        }


        private void ClearData()
        {
            Console.WriteLine("Clearing HadithCollectors");
            ObjectSpace.HadithCollectors.RemoveRange(ObjectSpace.HadithCollectors);
            ObjectSpace.HadithReferenceDefinitions.RemoveRange(ObjectSpace.HadithReferenceDefinitions);
            Console.WriteLine("Clearing Hadiths");
            ObjectSpace.Hadiths.RemoveRange(ObjectSpace.Hadiths);
            ObjectSpace.SaveChanges();
        }

    }
}
