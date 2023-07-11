using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner1.Model
{
    public class Config : IConfig
    {
        public string ConnectionString => System.Configuration.ConfigurationManager.AppSettings["cs"];
        public string MapHandlerKey => System.Configuration.ConfigurationManager.AppSettings["mapHandlerKey"];
        public string MapHandlerWidth => System.Configuration.ConfigurationManager.AppSettings["mapHandlerWidth"];
        public string MapHandlerHeight => System.Configuration.ConfigurationManager.AppSettings["mapHandlerHeight"];
        public string Mode => System.Configuration.ConfigurationManager.AppSettings["mode"];
    }
}
