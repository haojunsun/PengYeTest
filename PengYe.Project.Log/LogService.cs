using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using PengYe.Project.Infrastructure;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = @"Configuration\Log4Net.config", Watch = true)]
namespace PengYe.Project.Log
{
    public class LogService : ILogService
    {
        private readonly ILog _log = LogManager.GetLogger("Log.Logging");
        
        public void Debug(string msg)
        {
            _log.Debug(msg);
        }

        public void Error(string msg)
        {
            _log.Error(msg);
        }

        public void Info(string msg)
        {
            _log.Info(msg);
        }

        public void Warn(string msg)
        {
            _log.Warn(msg);
        }
    }

    public interface ILogService : IDependency
    {
        void Debug(string msg);
        void Error(string msg);
        void Info(string msg);
        void Warn(string msg);
    }
}
