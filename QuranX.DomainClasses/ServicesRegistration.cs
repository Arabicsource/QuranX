using QuranX.DomainClasses.Builders;
using QuranX.DomainClasses.Services;

namespace QuranX.DomainClasses
{
    public interface IServicesRegistrationCallback
    {
        void RegisterScoped<TService, TImplementation>() where TImplementation : TService;
        void RegisterSingleton<TService, TImplementation>() where TImplementation : TService;
        void RegisterTransient<TService, TImplementation>() where TImplementation : TService;
    }
    public static class ServicesRegistration
    {
        public static void Register(IServicesRegistrationCallback registration)
        {
            registration.RegisterScoped<ObjectSpace, ObjectSpace>();
            registration.RegisterScoped<IChapterRepository, ChapterRepository>();
            registration.RegisterScoped<IVerseViewBuilder, VerseViewBuilder>();
        }
    }

}
