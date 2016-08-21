using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementationPatterns
{
    class Program
    {
        static void Main(string[] args)
        {          
            LogFileSource logFs = new LogFileSource("1.txt");              
            ////Strategy Pattern
            LogProccesor lp = new LogProccesor(new LogFileReader().Read);
            lp.ProcessLogs(logFs);                   
        } 
    }
}
