using System;
using System.Linq;
using System.Reflection;
using SeaInk.Utility.Attributes;

namespace SeaInk.Utility.Tools
{
    public static class AssemblyScanner
    {
        public static Type[] ScanAssignableTo<T>(params Type[] markers)
            => ScanAssignableTo(typeof(T), markers);

        public static Type[] ScanAssignableTo(Type type, params Type[] markers)
            => ScanAssignableTo(type, markers.Select(m => m.Assembly).ToArray());

        public static Type[] ScanAssignableTo<T>(params Assembly[] assemblies)
            => ScanAssignableTo(typeof(T), assemblies);

        public static Type[] ScanAssignableTo(Type type, params Assembly[] assemblies)
        {
            return assemblies
                .Distinct()
                .SelectMany(a => a.DefinedTypes)
                .Where(t => t.IsAssignableTo(type))
                .Where(t => t.AsType().GetCustomAttribute<AssemblyScannerIgnoreAttribute>() is null)
                .Select(t => t.AsType())
                .ToArray();
        }
    }
}