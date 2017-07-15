using QuranX.DomainClasses.Entities;

namespace QuranX.DomainClasses.Models
{
    public class VerseTranslation
    {
        public Translator Translator { get; private set; }
        public VerseText Translation { get; private set; }

        public VerseTranslation(Translator translator, VerseText translation)
        {
            Translator = translator;
            Translation = translation;
        }

    }
}
