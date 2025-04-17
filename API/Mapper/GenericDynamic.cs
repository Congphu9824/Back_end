using AutoMapper;
using System.Reflection;

namespace Back_end.Mapper
{
    public class GenericDynamic : Profile
    {
        public GenericDynamic()
        {
            Assembly assembly = Assembly.Load("DATA");
            var entityTypes = assembly.GetTypes().Where(t => t.IsClass && t.Namespace == "DATA.Entities").ToList();
            foreach (var entityType in entityTypes)
            {
                CreateDynamicMap(entityType);
            }
        }

        private void CreateDynamicMap(Type entityType)
        {
            var method = typeof(Profile).GetMethods()
                .FirstOrDefault(m => m.Name == nameof(CreateMap) && m.IsGenericMethod);
            var genericMethod = method?.MakeGenericMethod(entityType, entityType);
            genericMethod?.Invoke(this, null);
        }
    }
}
