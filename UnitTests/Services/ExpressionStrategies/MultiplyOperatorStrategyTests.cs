using ULSolutions.Services.ExpressionStrategies;
using ULSolutions.Services.Interfaces;
using Xunit;

namespace UnitTests.Services.ExpressionStrategies;

public class MultiplyOperatorStrategyTests
{
    private readonly IBinaryOperatorStrategy _sut;
    
    public MultiplyOperatorStrategyTests()
    {
        _sut = new MultiplyOperatorStrategy();
    }

    [Theory]
    [InlineData(5, 10, 50)]
    [InlineData(20, 20, 400)]
    [InlineData(375, 458, 171750)]
    [InlineData(1046, 2648, 2769808)]
    public void WhenTwoValuesAreMultiplied_ThenTheCorrectResultIsReturned(int rightOperand, int leftOperand, int expected)
    {
        var actual = _sut.Apply(rightOperand, leftOperand);
        Assert.Equal(expected, actual);
    }
}