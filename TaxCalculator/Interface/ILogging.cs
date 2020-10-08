using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Interface
{
    public interface ILogging
    {
   
        /// <summary>
        /// Write error message
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);
    }
}
