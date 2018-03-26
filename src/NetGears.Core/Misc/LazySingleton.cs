using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace NetGears.Core.Misc
{
    public class LazySingleton<T> where T : class, new()
    {
        private static T _instance;

        private static readonly object _bolt = new object();

        public static T Instance
        {
            get
            {
                lock (_bolt)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }
    }
}
