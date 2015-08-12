using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace ELib.Common
{
    public class ELoggerFactory
    {
        private static ELoggerFactory _instance;

        private Dictionary<string, ELogger> _dict;

        private ELoggerFactory() {
            _dict = new Dictionary<string, ELogger>();
        }

        public static ELoggerFactory GetInstance()
        {
            if (_instance == null)
                _instance = new ELoggerFactory();
            return new ELoggerFactory();
        }

        public ELogger GetLogger(String name)
        {
            if (!_dict.Keys.Contains(name))
            {
                _dict.Add(name, new ELogger(name));
            }
            return _dict[name];
        }
    }
}
