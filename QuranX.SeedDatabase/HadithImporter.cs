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

        private void ClearData()
        {
            Console.WriteLine("Clearing HadithCollectors");
            ObjectSpace.HadithCollectors.RemoveRange(ObjectSpace.HadithCollectors);
            ObjectSpace.HadithReferenceDefinitions.RemoveRange(ObjectSpace.HadithReferenceDefinitions);
            ObjectSpace.SaveChanges();
        }

    }
}
