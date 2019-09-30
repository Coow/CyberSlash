namespace Mechanics.FunctionsSystem
{
    public class FunctionBase : BaseObject<IFunctionBase>, IFunctionBase
    {
        private string _expression;

        public string Expression
        {
            get
            {
                return _expression;
            }

            set
            {
                SetField(ref _expression, value);
            }
        }

        public override bool Equals(IFunctionBase other)
        {
            return other != null && other.Id == Id;
        }

        public override bool FilterHits(string filter)
        {
            return base.FilterHits(filter)
            || Expression.ContainsString(filter);
        }
    }
}
