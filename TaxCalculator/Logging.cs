using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Interface;

namespace TaxCalculator
{
    // Note: Only error log is capture, but idea is we can use different login type (Audit,Info,Debug,etc.)
    public class Logging : ILogging
    {
        public Logging() //Todo : Make LogManger instance injectable
        {

        }
        public void Error(string message)
        {
            var _logger = LogManager.GetCurrentClassLogger();
            _logger.Log(LogLevel.Error,message);
        }
    }
}
