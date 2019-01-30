using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;
using System.Web.SessionState;

namespace TAhub.Models
{
    public static class Cache
    {
        private static string UserKey = "User";//HttpContext.Current.Request.UserHostAddress;

        public static bool Set<T>(string Key, T Value) where T : class
        {
            //HttpSessionState
            //HttpSessionState state = new HttpSessionState();
             
            if (Value != null)
            {
                try
                {
                    T value = HttpContext.Current.Session[Key] as T;
                    if(value == null)
                    {
                        throw new Exception($"Cache Key '{Key}' has no assigned value.");
                    }
                    HttpContext.Current.Session[Key] = Value;
                }
                catch (Exception)
                {
                    HttpContext.Current.Session.Add(Key, Value);
                        //key: cacheKey,
                        //value: cacheValue,
                        //dependencies: null,
                        //absoluteExpiration: System.Web.Caching.Cache.NoAbsoluteExpiration,
                        //slidingExpiration: TimeSpan.FromHours(1));
                }
                return true;
            }
            return false;
        }

        public static bool Set(TAModel Value)
        {
            if (Value != null)
            {
                try
                {
                    TAModel value = HttpContext.Current.Session[UserKey] as TAModel;
                    if (value == null)
                    {
                        throw new Exception($"Cache Key 'User' has no assigned value.");
                    }
                    HttpContext.Current.Session[UserKey] = Value;
                }
                catch (Exception)
                {
                    HttpContext.Current.Session.Add(UserKey, Value);
                }
                return true;
            }
            return false;
        }

        public static bool Set(ProfessorModel Value)
        {
            if (Value != null)
            {
                try
                {
                    ProfessorModel value = HttpContext.Current.Session[UserKey] as ProfessorModel;
                    if (value == null)
                    {
                        throw new Exception($"Cache Key 'User' has no assigned value.");
                    }
                    HttpContext.Current.Session[UserKey] = Value;
                }
                catch (Exception)
                {
                    HttpContext.Current.Session.Add(UserKey, Value);
                }
                return true;
            }
            return false;
        }

        public static T Get<T>(string cacheKey) where T : class
        {
            try
            {
                return HttpContext.Current.Session[cacheKey] as T;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static T GetUser<T>() where T : class
        {
            try
            {
                return HttpContext.Current.Session[UserKey] as T;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void RemoveUser()
        {
            HttpContext.Current.Session.Remove(UserKey);
        }
    }
}