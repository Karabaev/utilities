using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Object = UnityEngine.Object;

namespace com.karabaev.utilities
{
  [PublicAPI]
  public static class CommonUtils
  {
    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
      foreach(var element in enumerable)
        action.Invoke(element);
    }

    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<int, T> action)
    {
      var i = 0;
      foreach(var element in enumerable)
      {
        action.Invoke(i, element);
        i++;
      }
    }

    public static bool IsEmpty<T>(this IList<T> collection) => collection.Count == 0;

    public static T Require<T>(this IEnumerable<T> collection, Func<T, bool> predicate, string message)
    {
      try
      {
        return collection.First(predicate);
      }
      catch(InvalidOperationException)
      {
        throw new NullReferenceException(message);
      }
    }

    public static T? GetStruct<T>(this IEnumerable<T> collection, Func<T, bool> predicate) where T : struct
    {
      return collection
       .Where(predicate)
       .Cast<T?>()
       .FirstOrDefault();
    }

    public static bool ContainsStruct<T>(this IEnumerable<T> collection, Func<T, bool> predicate) where T : struct =>
      GetStruct(collection, predicate) != null;

    public static bool Contains<T>(this IEnumerable<T> collection, Func<T, bool> predicate) where T : class =>
      collection.FirstOrDefault(predicate) != null;

    public static object? Get(this IReadOnlyDictionary<string, object> dictionary, string key)
    {
      dictionary.TryGetValue(key, out var result);
      return result;
    }

    public static object Require(this IReadOnlyDictionary<string, object> dictionary, string key)
    {
      var result = Get(dictionary, key);

      if(result == null)
        throw new NullReferenceException($"Entry with key '{key}' not found in dictionary");

      return result;
    }

    public static T Require<T>(this IReadOnlyDictionary<string, T> dictionary, string key)
    {
      if(!dictionary.TryGetValue(key, out var result))
        throw new NullReferenceException($"Entry with key {key} not found in dictionary");

      return result;
    }

    public static TValue Require<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
    {
      if(!dictionary.TryGetValue(key, out var result))
        throw new NullReferenceException($"Entry with key {key} not found in dictionary");

      return result!;
    }

    public static T? AsNullable<T>(this T? reference) where T : class
    {
      if(reference == null || reference.Equals(null))
        return null;

      if(reference is Object unityObject)
        return unityObject ? reference : null;

      return reference;
    }

    public static string Capitalize(this string source) =>
      char.IsLower(source[0]) ? $"{char.ToUpper(source[0])}{source.Substring(1)}" : source;

    public static string Uncapitalize(this string source) =>
      char.IsUpper(source[0]) ? $"{char.ToLower(source[0])}{source.Substring(1)}" : source;

    public static T ParseEnum<T>(string text) where T : Enum => (T)ParseEnum(text, typeof(T));

    public static object ParseEnum(string text, Type enumType) => Enum.Parse(enumType, text, true);

    public static bool Contains(this string source, string value, StringComparison comparison)
    {
      var index = source.IndexOf(value, comparison);
      return index != -1;
    }

    public static TimeSpan ToSeconds(this float seconds) => TimeSpan.FromSeconds(seconds);
  }
}