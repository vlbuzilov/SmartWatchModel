using System.Runtime.CompilerServices;

namespace OOP_Lab6.logger
{
    public class Logger
    {
        public static log4net.ILog GetLogger([CallerFilePath] string filename = "")
        {
            return log4net.LogManager.GetLogger(filename);
        }
    }
}