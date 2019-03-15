using Mechanics.FunctionsSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics.StatsSystem
{
    public interface IStatBaseModifier : IBaseObject
    {
        IFunctionBase Function { get; set; }
        ObservableDictionary<string, IModifierDependency> Map { get; }
    }
}
