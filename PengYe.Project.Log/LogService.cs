using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using PengYe.Project.Infrastructure;

namespace PengYe.Project.Log
{
    public interface ILogService : IDependency
    {
        void Debug(string msg);
        void Error(string msg);
        void Info(string msg);

        void Warn(string msg);
    }

    public class LogService : ILogService
    {
        private readonly ILog _log = LogManager.GetLogger("AppLog.Logging");
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
}
