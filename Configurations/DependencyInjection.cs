using Gestor.API.Services;

namespace Gestor.API.Configurations
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Registro de servicios de aplicación
            services.AddScoped<ProductoService>();
            services.AddScoped<CategoriaServices>();


        }
    }
}
