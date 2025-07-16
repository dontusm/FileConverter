using Microsoft.Extensions.DependencyInjection;

namespace FileConverter.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(Inject).Assembly));
        
        return services;
    }
}