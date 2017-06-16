namespace QuranX.DomainClasses.Services
{
    public interface IServiceRegistrationCallback
    {
        void RegisterScoped<TService, TImplementation>() where TImplementation : TService;
        void RegisterSingleton<TService, TImplementation>() where TImplementation : TService;
        void RegisterTransient<TService, TImplementation>() where TImplementation : TService;
    }
    public static class ServiceRegistration
    {
        public static void Register(IServiceRegistrationCallback registration)
        {
            registration.RegisterScoped<ObjectSpace, ObjectSpace>();
            registration.RegisterScoped<IChapterRepository, ChapterRepository>();
        }
    }

}
