using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mechanics.FunctionsSystem;

namespace Mechanics.StatsSystem
{
    public class StatBaseModifier : BaseObject<IStatBaseModifier>, IStatBaseModifier
    {
        private IFunctionBase _function;
        private ObservableDictionary<string, IModifierDependency> _map = new ObservableDictionary<string, IModifierDependency>();

        public IFunctionBase Function
        {
            get
            {
                return _function;
            }

            set
            {
                var previous = _function;

                if (SetField(ref _function, value))
                {
                    if (previous != null)
                    {
                        previous.PropertyChanged -= Function_PropertyChanged;
                    }

                    _function.PropertyChanged += Function_PropertyChanged;
                    Update();
                }
            }
        }

        private void Function_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IFunctionBase.Expression))
            {
                Update();
            }
        }

        public ObservableDictionary<string, IModifierDependency> Map => _map;

        public override bool Equals(IStatBaseModifier other)
        {
            return other != null && other.Id == Id;
        }

        private void Update()
        {
            _map.Clear();

            if (Function == null)
            {
                return;
            }

            var args = FunctionTools.GetArguments(Function);

            if (args.Count() > 0)
            {
                foreach (var arg in args)
                {
                    _map.Add(arg, new ModifierDependency { Name = arg });
                }
            }
        }
    }
}
