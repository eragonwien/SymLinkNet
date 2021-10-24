using Microsoft.Extensions.DependencyInjection;
using SymLinkNet.Services;

namespace SymLinkNet.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSymLinkNetTransient(this IServiceCollection services)
            => services.AddTransient(SymLinkFactory.GetInstance);

        public static IServiceCollection AddSymLinkNetScoped(this IServiceCollection services)
            => services.AddScoped(SymLinkFactory.GetInstance);

        public static IServiceCollection AddSymLinkNetSingleton(this IServiceCollection services)
            => services.AddSingleton(SymLinkFactory.GetInstance);
    }
}
