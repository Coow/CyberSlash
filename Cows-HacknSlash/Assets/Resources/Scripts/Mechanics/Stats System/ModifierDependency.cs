using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics.StatsSystem
{
    public class ModifierDependency : BaseObject<IModifierDependency>, IModifierDependency
    {
        private IStatBase _stat;

        public IStatBase Stat
        {
            get
            {
                return _stat;
            }

            set
            {
                SetField(ref _stat, value);
            }
        }

        public override bool Equals(IModifierDependency other)
        {
            return other != null && other.Id == Id;
        }
    }
}
