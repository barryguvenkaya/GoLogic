using System.Reflection;
using AutoMapper;
using VendingMachine.Application.Common.Interfaces;

namespace VendingMachine.Application.Common.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(ConsumingApplicationAssemblies);
        }

        public static Assembly ConsumingApplicationAssembly
        {
            get => ConsumingApplicationAssemblies?.SingleOrDefault();
            set => ConsumingApplicationAssemblies = new Assembly[1] { value };
        }

        public static IEnumerable<Assembly> ConsumingApplicationAssemblies { get; set; }

        private void ApplyMappingsFromAssembly(IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetExportedTypes()
                    .Where(t => t.GetInterfaces().Any(i =>
                        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                    .ToList();

                foreach (var type in types)
                {
                    var instance = Activator.CreateInstance(type);

                    var methodInfo = type.GetMethod("Mapping")
                        ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                    methodInfo?.Invoke(instance, new object[] { this });
                }
            }
        }
    }
}
