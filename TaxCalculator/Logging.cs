using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Interface;

namespace TaxCalculator
{
    public class Logging : ILogging
    {
        public Logging()
        {

        }
        public void Error(string message)
        {
            var _logger = LogManager.GetCurrentClassLogger();
            _logger.Log(LogLevel.Error,message);
        }
    }
}
