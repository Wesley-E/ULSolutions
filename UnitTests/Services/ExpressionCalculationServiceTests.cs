using ULSolutions.Factories;
using ULSolutions.Services;
using ULSolutions.Services.ExpressionStrategies;
using ULSolutions.Services.Interfaces;
using Xunit;
using NSubstitute;

namespace UnitTests.Services
{
    public class ExpressionCalculationServiceTests
    {
        private const string Expression = "10/2*3";
        private readonly IOperatorStrategyFactory _operatorStrategyFactory;
        private readonly IExpressionCalculationService _sut;

        public ExpressionCalculationServiceTests()
        {
            _operatorStrategyFactory = Substitute.For<IOperatorStrategyFactory>();
            _operatorStrategyFactory.CreateOperatorStrategies()
                .Returns(new Dictionary<char, IBinaryOperatorStrategy>
                {
                    {'/', new DivideOperatorStrategy()},
                    {'*', new MultiplyOperatorStrategy()},
                    {'+', new AddOperatorStrategy()},
                    {'-', new SubtractOperatorStrategy()}
                });
            _sut = new ExpressionCalculationService(_operatorStrategyFactory);
        }

        [Fact]
        public void WhenNoStrategiesSpecified_ThenArgumentExceptionThrown()
        {
            var sut = new ExpressionCalculationService(Substitute.For<IOperatorStrategyFactory>());
            var ex = Assert.Throws<ArgumentException>(() => sut.Calculate(Expression));
            Assert.Equal("No strategies declared for usage", ex.Message);
        }

        [Fact]
        public void WhenStrategySpecified_AndInvalidOperatorGiven_ThenArgumentExceptionThrown()
        {
            var ex = Assert.Throws<ArgumentException>(() => _sut.Calculate("2£5"));
            Assert.Equal("Invalid operator(s) given £", ex.Message);
        }

        [Fact]
        public void WhenStrategySpecified_AndInvalidOperatorsGiven_ThenArgumentExceptionThrown()
        {
            var ex = Assert.Throws<ArgumentException>(() => _sut.Calculate("2$5@7"));
            Assert.Equal("Invalid operator(s) given $, @", ex.Message);
        }

        [Theory]
        [InlineData("2+6/7", 2.8571428571428572d)]
        [InlineData("2+6/7+8*2/5", 6.0571428571428569)]
        [InlineData("2+6/7-8*2/5+6/3/2*4*7", 27.657142857142858)]
        public void WhenStrategySpecified_AndValidOperatorGiven_ThenExpressionIsParsed_AndCorrectValueReturned(string expression, double result)
        {
            var actual = _sut.Calculate(expression);
            Assert.Equal(result, actual);
        }
    }
}
