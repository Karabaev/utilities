using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace com.karabaev.utilities
{
  public static class ReflectionUtils
  {
    public static IEnumerable<(Type, TAttribute)> FindAllTypesWithAttribute<TAttribute>(AssembliesCollection assemblies)
      where TAttribute : Attribute
    {
      return GetTypesFromAssemblies(assemblies).Select(t => (t, t.GetCustomAttribute<TAttribute>())).Where(t => t.Item2 != null);
    }

    public static IEnumerable<Type> FindAllTypesWithInterface<TInterface>(AssembliesCollection assemblies)
    {
      return GetTypesFromAssemblies(assemblies).Where(t => t.GetInterfaces().Contains(typeof(TInterface)));
    }

    public static IEnumerable<(Type, TAttribute)> FindAllTypesWithAttributeAndInterface<TAttribute, TInterface>(AssembliesCollection assemblies)
      where TAttribute : Attribute
    {
      return GetTypesFromAssemblies(assemblies)
       .Where(t => t.GetInterfaces().Contains(typeof(TInterface)))
       .Select(t => (t, t.GetCustomAttribute<TAttribute>()))
       .Where(t => t.Item2 != null);
    }

    public static IEnumerable<(Type, TAttribute)> FindAllTypesWithAttributeAndBaseType<TAttribute, TBase>(AssembliesCollection assemblies)
      where TAttribute : Attribute
    {
      return GetTypesFromAssemblies(assemblies)
       .Where(t =>
        {
          var baseType = t.BaseType;
          while(baseType != null)
          {
            if(baseType == typeof(TBase))
            {
              return true;
            }
            baseType = baseType.BaseType;
          }
          return false;
        })
       .Select(t => (t, t.GetCustomAttribute<TAttribute>()))
       .Where(t => t.Item2 != null);
    }

    private static IEnumerable<Type> GetTypesFromAssemblies(AssembliesCollection assemblies)
    {
      return assemblies.Assemblies.Select(Assembly.Load).SelectMany(a => a.GetTypes());
    }

    public static TAttribute RequireAttribute<TAttribute>(Type sourceType)
      where TAttribute : Attribute
    {
      var attribute = sourceType.GetCustomAttribute<TAttribute>();
      return attribute ??
        throw new
          NullReferenceException($"There is no attribute on specified type. SourceType={sourceType.Name}. AttributeType={typeof(TAttribute).Name}");
    }

    public static bool IsInterfaceImplemented<TInterface>(this Type type)
    {
      var interfaceType = typeof(TInterface);

      if(!interfaceType.IsInterface)
      {
        throw new ArgumentException($"Generic argument have to be interface type. Type={interfaceType.Name}");
      }

      if(interfaceType == type)
      {
        return true;
      }

      foreach(var @interface in type.GetInterfaces())
      {
        if(@interface == interfaceType)
        {
          return true;
        }
      }

      return false;
    }
    
    public class AssembliesCollection
    {
      public IReadOnlyList<string> Assemblies { get; }

      public AssembliesCollection(IReadOnlyList<string> assemblies) => Assemblies = assemblies;
      
      public AssembliesCollection(params string[] assemblies) => Assemblies = assemblies;
    }
  }
}