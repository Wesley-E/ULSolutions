using ULSolutions.Services.ExpressionStrategies;
using ULSolutions.Services.Interfaces;
using Xunit;

namespace UnitTests.Services.ExpressionStrategies;

public class AddOperatorStrategyTests
{
    private readonly IBinaryOperatorStrategy _sut;
    
    public AddOperatorStrategyTests()
    {
        _sut = new AddOperatorStrategy();
    }

    [Theory]
    [InlineData(5, 10, 15)]
    [InlineData(20, 20, 40)]
    [InlineData(375, 458, 833)]
    [InlineData(1046, 2648, 3694)]
    public void WhenTwoValuesAreAddedTogether_ThenTheCorrectResultIsReturned(int rightOperand, int leftOperand, int expected)
    {
        var actual = _sut.Apply(rightOperand, leftOperand);
        Assert.Equal(expected, actual);
    }
}