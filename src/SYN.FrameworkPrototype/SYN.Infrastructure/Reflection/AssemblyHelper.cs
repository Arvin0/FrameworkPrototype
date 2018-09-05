using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SYN.Infrastructure.Reflection
{
    public static class AssemblyHelper
    {
        public static Assembly[] GetAssemblies(string searchPattern)
        {
            Assembly[] assemblies = Directory
                .GetFiles(PlatformServices.Default.Application.ApplicationBasePath, searchPattern, SearchOption.AllDirectories)
                .Select(Assembly.LoadFrom).ToArray();

            return assemblies;
        }
    }
}
