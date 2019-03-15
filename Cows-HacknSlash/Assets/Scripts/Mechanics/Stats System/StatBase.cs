using System;
using System.Collections.Generic;
namespace Mechanics.StatsSystem
{
    public class StatBase : BaseObject<IStatBase>, IStatBase
    {
        #region Overrides

        public override bool Equals(object obj)
        {
            return Equals(obj as IStatBase);
        }

        public override int GetHashCode()
        {
            return 1969571243 + EqualityComparer<Guid>.Default.GetHashCode(Id);
        }

        public override bool FilterHits(string filter)
        {
            return Name.ContainsString(filter)
                || Id.ToString().ContainsString(filter);
        }

        public override bool Equals(IStatBase other)
        {
            return other != null && other.Id == Id;
        }

        #endregion
    }
}