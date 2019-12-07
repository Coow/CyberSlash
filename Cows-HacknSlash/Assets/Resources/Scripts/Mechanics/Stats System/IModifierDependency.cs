using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics.StatsSystem
{
    public interface IModifierDependency : IBaseObject
    {
        IStatBase Stat { get; set; }
    }
}
