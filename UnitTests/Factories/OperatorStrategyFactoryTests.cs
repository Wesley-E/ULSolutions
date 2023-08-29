using ULSolutions.Factories;
using ULSolutions.Services.ExpressionStrategies;
using ULSolutions.Services.Interfaces;
using Xunit;

namespace UnitTests.Factories;

public class OperatorStrategyFactoryTests
{
    private readonly IOperatorStrategyFactory _sut;
    
    public OperatorStrategyFactoryTests()
    {
        _sut = new OperatorStrategyFactory();
    }

    [Fact]
    public void CreateOperatorStrategies_ReturnsStrategyDictionary_WithCorrectStrategies()
    {
        var expected = 
            new Dictionary<char, IBinaryOperatorStrategy>
            {
                { '/', new DivideOperatorStrategy() },
                { '*', new MultiplyOperatorStrategy() },
                { '+', new AddOperatorStrategy() },
                { '-', new SubtractOperatorStrategy() }
            };
        
        var actual = _sut.CreateOperatorStrategies();
        
        Assert.Equal(expected.Keys, actual.Keys);
        foreach (var expression in expected)
        {
            Assert.True(actual.ContainsKey(expression.Key));
            Assert.Equal(expression.Value.GetType(), actual[expression.Key].GetType());
        }
    }
}