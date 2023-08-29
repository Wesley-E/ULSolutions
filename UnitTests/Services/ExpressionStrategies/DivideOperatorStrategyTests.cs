using ULSolutions.Services.ExpressionStrategies;
using ULSolutions.Services.Interfaces;
using Xunit;

namespace UnitTests.Services.ExpressionStrategies;

public class DivideOperatorStrategyTests
{
    private readonly IBinaryOperatorStrategy _sut;
    
    public DivideOperatorStrategyTests()
    {
        _sut = new DivideOperatorStrategy();
    }

    [Theory]
    [InlineData(5, 10, 0.5)]
    [InlineData(20, 20, 1)]
    [InlineData(375, 458, 0.81877729257641918)]
    [InlineData(1046, 2648, 0.39501510574018128)]
    public void WhenTwoValuesAreMultiplied_ThenTheCorrectResultIsReturned(int rightOperand, int leftOperand, double expected)
    {
        var actual = _sut.Apply(rightOperand, leftOperand);
        Assert.Equal(expected, actual);
    }
}