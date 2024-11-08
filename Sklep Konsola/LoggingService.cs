using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_Konsola
{
    public class LoggingService
    {
        private static LoggingService _instance;
        public LoggingRepository Accounts;
        private LoggingService()
        {
            Accounts = new LoggingRepository();
        }
    }
}
