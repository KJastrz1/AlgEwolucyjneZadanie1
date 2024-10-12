using MathNet.Symbolics;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace AlgEwolucyjneZadanie1.Algorytm;

public static class FunctionProcessor
{
    public static double EvaluateFunction(string function, double x)
    {
        var expr = Expr.Parse(function);


        var variables = new Dictionary<string, FloatingPoint>
        {
            { "x", x }
        };

        var result = expr.Evaluate(variables);

        return (double)result.RealValue;
    }

    public static double EvaluateFunction(string function, int x)
    {
        return EvaluateFunction(function, (double)x);
    }
}