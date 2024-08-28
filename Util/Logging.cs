using Serilog;

namespace Util
{
    public  class Logging
    {
     
        Serilog.Core.Logger _logger;
        public Logging(string fileName)
        {
            _logger = new LoggerConfiguration().WriteTo.File(fileName, rollingInterval: RollingInterval.Day)
           .CreateLogger();
        }
        public void WriteInfo(string message)        {

            _logger.Information(message);
        }
    }
}
