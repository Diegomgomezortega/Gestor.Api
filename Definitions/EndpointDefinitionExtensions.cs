using System.Reflection;

namespace Gestor.API.Definitions
{
    public static class EndpointDefinitionExtensions
    {
        public static void RegisterEndpointDefinitions(this WebApplication app)
        {
            var endpointDefinitions = Assembly.GetExecutingAssembly()
                .ExportedTypes
                .Where(t => typeof(IEndpointDefinition).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IEndpointDefinition>();

            foreach (var endpointDefinition in endpointDefinitions)
            {
                endpointDefinition.RegisterEndpoints(app);
            }
        }
    }
}
