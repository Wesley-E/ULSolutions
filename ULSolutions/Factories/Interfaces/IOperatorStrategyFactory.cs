using ULSolutions.Services.Interfaces;

namespace ULSolutions.Factories;

public interface IOperatorStrategyFactory
{
    Dictionary<char, IBinaryOperatorStrategy> CreateOperatorStrategies();
}