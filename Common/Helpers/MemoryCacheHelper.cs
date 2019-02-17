using System;
using System.Runtime.Caching;

namespace Common.Helpers
{
    /// <summary>
    /// simple cache, you can use advance cache like Redis
    /// </summary>
    public static class MemoryCacheHelper
    {
        private static MemoryCache memoryCache = MemoryCache.Default;

        public static void Set(string key, object value)
        {
            if (!Exists(key) && value != null)
                memoryCache.Add(key, value, DateTime.Now.AddDays(7));
        }

        public static void Set(string key, object value, DateTime expires)
        {
            if (!Exists(key) && value != null)
                memoryCache.Add(key, value, expires);
        }

        public static T Get<T>(string key) where T : class, new()
        {
            if (Exists(key))
                return memoryCache.Get(key) as T;
            return null;
        }

        public static object Get(string key)
        {
            if (Exists(key))
                return memoryCache.Get(key);
            return null;
        }

        public static void Remove(string key)
        {
            if (Exists(key))
                memoryCache.Remove(key);
        }

        public static bool Exists(string key)
        {
            return memoryCache.Contains(key);
        }

        public static void Clear()
        {
            memoryCache.Dispose();
            memoryCache = MemoryCache.Default;
        }
    }
}
