﻿namespace IO.TedTalk.Core.Extensions;

public static class CollectionExtensions
{
    /// <summary>
    /// Checks whatever given collection object is null or has no item.
    /// </summary>
    public static bool IsNullOrEmpty<T>(this ICollection<T> source)
    {
        return source == null || source.Count <= 0;
    }

    /// <summary>
    /// Builds a string representation of the dictionary.
    /// Sample usages:
    /// Consider this dictionary
    /// <code>new Dictionary&lt;string, string&gt;
    /// {
    ///     { "a", "1" },
    ///     { "b", "2" }
    /// };</code>
    /// dict.ToFormattedString() ==> output: a='1' b='2'
    /// dict.ToFormattedString("{0}={1}", ", ") ==> output: a=1, b=2
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dict"></param>
    /// <param name="format">If null, the space character will be used</param>
    /// <param name="separator">If null, the following format will be used {0}='{1}'</param>
    /// <returns></returns>
    /// <see cref="https://stackoverflow.com/questions/3639094/most-efficient-dictionaryk-v-tostring-with-formatting" />
    public static string ToFormattedString<TKey, TValue>(this IDictionary<TKey, TValue> dict, string format = null,
        string separator = null)
    {
        if (dict == null)
            return "";
        separator = !string.IsNullOrEmpty(separator) ? separator : " ";
        format = !string.IsNullOrEmpty(format) ? format : "{0}=\'{1}\'";
        return string.Join(separator, dict.Select(p => string.Format(format, p.Key, p.Value)));
    }
}
