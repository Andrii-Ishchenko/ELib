using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace ELib.Common
{
    public class ELogger
    {
        private Logger logger;
        public ELogger(String classname)
        {
            logger = LogManager.GetLogger(classname);
        }

        public void Trace(String msg)
        {
            logger.Log(LogLevel.Trace, msg);           
        }

        public void Trace(String msg, Exception ex)
        {
            logger.Trace(ex, msg);
        }

        public void Debug(String msg)
        {
            logger.Log(LogLevel.Debug, msg);
        }

        public void Debug(String msg, Exception ex)
        {
            logger.Debug(ex, msg);
        }

        public void Info(String msg)
        {
            logger.Log(LogLevel.Info, msg);
        }

        public void Info(String msg, Exception ex)
        {
            logger.Info(ex, msg);
        }

        public void Warn(String msg)
        {
            logger.Log(LogLevel.Warn, msg);
        }

        public void Warn(String msg, Exception ex)
        {
            logger.Warn(ex, msg);
        }

        public void Error(String msg)
        {
            logger.Log(LogLevel.Error, msg);
        }

        public void Error(String msg, Exception ex)
        {
            //logger.Log(LogLevel.Error, msg, ex, null);
            logger.Error(ex,msg);
        
        }

        public void Fatal(String msg)
        {
            logger.Log(LogLevel.Fatal, msg);
        }

        public void Fatal(String msg, Exception ex)
        {
            logger.Fatal(ex, msg);
        }
    }
}
