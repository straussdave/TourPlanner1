using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner1.Model
{
    public interface IConfig
    {
        string ConnectionString { get; }
        string MapHandlerKey { get; }
        string MapHandlerWidth { get; }
        string MapHandlerHeight { get; }
        string Mode { get; }
    }
}
