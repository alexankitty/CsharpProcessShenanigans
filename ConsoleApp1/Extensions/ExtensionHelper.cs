using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

public static class ExtensionHelper
{
    public static MethodInfo? FindExtensionMethod(Type targetType, string methodName , params object[] args)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var asm in assemblies)
        {
            foreach (var type in asm.GetTypes())
            {
                if (!type.IsSealed || !type.IsAbstract) // static classes
                    continue;

                foreach (var method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    if (!method.IsDefined(typeof(ExtensionAttribute), false))
                        continue;

                    if (method.Name != methodName)
                        continue;

                    var parameters = method.GetParameters();
                    if (parameters.Length == 0 && parameters.Length != args.Length) // +1 for the "this" parameter
                        continue;

                    if (parameters[0].ParameterType.IsAssignableFrom(targetType))
                        return method;
                }
            }
        }

        return null;
    }
}