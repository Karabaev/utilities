using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace com.karabaev.utilities
{
  [PublicAPI]
  public static class CollectionUtils
  {
    public static T[] With<T>(this T[] collection, params T[] items)
    {
      var result = new T[collection.Length + items.Length];
      Array.Copy(collection, result, collection.Length);
      Array.Copy(items, 0, result, collection.Length, items.Length);
      return result;
    }

    public static int IndexOf<T>(this IReadOnlyList<T> list, T item)
    {
      for (var i = 0; i < list.Count; i++)
      {
        if (EqualityComparer<T>.Default.Equals(list[i], item)) return i;
      }
      
      return -1;
    }

    public static IEnumerable<TItem> Where<TItem, TState>(
      this IReadOnlyList<TItem> list,
      TState state,
      Func<TItem, TState, bool> predicate)
    {
      for (var i = 0; i < list.Count; i++)
      {
        var item = list[i];
        if (predicate.Invoke(item, state))
        {
          yield return item;
        }
      }
    }
    
    public static int IndexOf<TItem, TState>(
      this IReadOnlyList<TItem> list,
      TState state,
      Func<TItem, TState, bool> predicate)
    {
      for (var i = 0; i < list.Count; i++)
      {
        if (predicate.Invoke(list[i], state)) return i;
      }
      
      return -1;
    }
    
    public static float Sum<TSource>(this TSource[] source, Func<TSource, float> selector)
    {
      var sum = 0.0f;
      foreach (var item in source) sum += selector.Invoke(item);
      return sum;
    }
    
    public static double Sum<TSource>(this TSource[] source, Func<TSource, double> selector)
    {
      var sum = 0.0;
      foreach (var item in source) sum += selector.Invoke(item);
      return sum;
    }
    
    public static float Sum<TSource>(this TSource[] source, Func<TSource, int> selector)
    {
      var sum = 0;
      foreach (var item in source) sum += selector.Invoke(item);
      return sum;
    }
    
    public static float Sum<TSource>(this TSource[] source, Func<TSource, long> selector)
    {
      long sum = 0;
      foreach (var item in source) sum += selector.Invoke(item);
      return sum;
    }
    
    public static float Sum<TSource>(this List<TSource> source, Func<TSource, float> selector)
    {
      var sum = 0.0f;
      foreach (var item in source) sum += selector.Invoke(item);
      return sum;
    }
    
    public static double Sum<TSource>(this List<TSource> source, Func<TSource, double> selector)
    {
      var sum = 0.0;
      foreach (var item in source) sum += selector.Invoke(item);
      return sum;
    }
    
    public static float Sum<TSource>(this List<TSource> source, Func<TSource, int> selector)
    {
      var sum = 0;
      foreach (var item in source) sum += selector.Invoke(item);
      return sum;
    }
    
    public static float Sum<TSource>(this List<TSource> source, Func<TSource, long> selector)
    {
      long sum = 0;
      foreach (var item in source) sum += selector.Invoke(item);
      return sum;
    }
  }
}