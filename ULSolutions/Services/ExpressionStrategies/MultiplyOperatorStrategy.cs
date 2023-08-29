using ULSolutions.Services.Interfaces;

namespace ULSolutions.Services.ExpressionStrategies;

public class MultiplyOperatorStrategy : IBinaryOperatorStrategy
{
    public double Apply(double leftOperand, double rightOperand)
    {
        return leftOperand * rightOperand;
    }
}