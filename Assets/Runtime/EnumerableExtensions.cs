using System;
using System.Collections.Generic;

namespace com.karabaev.utilities
{
  public static class EnumerableExtensions
  {
    public static TItem First<TItem, TState>(
      this IEnumerable<TItem> enumerable,
      TState state,
      Func<TState, TItem, bool> predicate)
    {
      foreach (var item in enumerable)
      {
        if (predicate.Invoke(state, item))
        {
          return item;
        }
      }

      throw new InvalidOperationException("Sequence contains no matching element");
    }
    
    public static TItem? FirstOrDefault<TItem, TState>(
      this IEnumerable<TItem> enumerable,
      TState state,
      Func<TState, TItem, bool> predicate)
    {
      foreach (var item in enumerable)
      {
        if (predicate.Invoke(state, item))
        {
          return item;
        }
      }

      return default;
    }
  }
}