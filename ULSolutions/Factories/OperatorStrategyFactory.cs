using ULSolutions.Services.ExpressionStrategies;
using ULSolutions.Services.Interfaces;

namespace ULSolutions.Factories;

public class OperatorStrategyFactory : IOperatorStrategyFactory
{
    public Dictionary<char, IBinaryOperatorStrategy> CreateOperatorStrategies()
    {
        return new Dictionary<char, IBinaryOperatorStrategy>
        {
            { '/', new DivideOperatorStrategy() },
            { '*', new MultiplyOperatorStrategy() },
            { '+', new AddOperatorStrategy() },
            { '-', new SubtractOperatorStrategy() }
        };
    }
}