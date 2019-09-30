using org.mariuszgromada.math.mxparser;
using System.Collections.Generic;

namespace Mechanics.FunctionsSystem
{
    public static class FunctionTools
    {
        public class OperationResult
        {
            public bool Result { get; set; }
            public string ErrorMessage { get; set; }
            public double CalculationResult { get; set; }
        }

        static FunctionTools() { }

        public static OperationResult CheckSyntax(this IFunctionBase function)
        {
            return CheckSyntax(new Expression(function.Expression));  
        }

        private static OperationResult CheckSyntax(this Expression expression)
        {
            OperationResult result = new OperationResult();

            var args = expression.getMissingUserDefinedArguments();

            if (args.Length > 0)
            {
                foreach (var arg in args)
                {
                    expression.defineArgument(arg, 1);
                }
            }

            var hasErrors = !expression.checkSyntax();
            if (hasErrors)
            {
                result.ErrorMessage = expression.getErrorMessage();
                result.Result = false;
            }
            else
            {
                result.Result = true;
            }

            return result;
        }

        public static OperationResult Calculate(this IFunctionBase function, params double[] vars)
        {
            Expression expression = new Expression(function.Expression);
            OperationResult result = expression.CheckSyntax();

            var args = expression.getArgumentsNumber();
            if (result.Result && vars.Length >= args)
            {
                Argument arg;
                for (int i = 0; i < args; i++)
                {
                    arg = expression.getArgument(i);
                    arg.setArgumentValue(vars[i]);
                }

                result.CalculationResult = expression.calculate();
            }

            return result;
        }

        public static IEnumerable<string> GetArguments(this IFunctionBase function)
        {
            return new Expression(function.Expression).getMissingUserDefinedArguments();
        }
    }
}