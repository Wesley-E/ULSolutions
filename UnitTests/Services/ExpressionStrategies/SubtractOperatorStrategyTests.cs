using ULSolutions.Services.ExpressionStrategies;
using ULSolutions.Services.Interfaces;
using Xunit;

namespace UnitTests.Services.ExpressionStrategies;

public class SubtractOperatorStrategyTests
{
    private readonly IBinaryOperatorStrategy _sut;
    
    public SubtractOperatorStrategyTests()
    {
        _sut = new SubtractOperatorStrategy();
    }

    [Theory]
    [InlineData(10, 5, 5)]
    [InlineData(20, 20, 0)]
    [InlineData(458, 375, 83)]
    [InlineData(1046, 2648, -1602)]
    public void WhenTwoValuesAreSubtracted_ThenTheCorrectResultIsReturned(int rightOperand, int leftOperand, int expected)
    {
        var actual = _sut.Apply(rightOperand, leftOperand);
        Assert.Equal(expected, actual);
    }
}