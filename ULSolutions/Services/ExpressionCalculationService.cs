using System.Text.RegularExpressions;
using ULSolutions.Factories;
using ULSolutions.Services.Interfaces;

namespace ULSolutions.Services;

public class ExpressionCalculationService : IExpressionCalculationService
{
    private readonly Dictionary<char, IBinaryOperatorStrategy> _operatorStrategies;

    public ExpressionCalculationService(IOperatorStrategyFactory operatorStrategyFactory)
    {
        _operatorStrategies = operatorStrategyFactory.CreateOperatorStrategies();
    }

    public double Calculate(string expression)
    {
        ValidateExpressionOperators(expression);
        var parsedExpression = ParseExpression(expression);
        return CalculateExpression(parsedExpression);
    }

    private double CalculateExpression(List<string> operandsAndOperators)
    {
        var operandStack = new Stack<double>();
        var operatorStack = new Stack<char>();

        foreach (var item in operandsAndOperators)
        {
            if (char.IsDigit(item[0]))
            {
                operandStack.Push(double.Parse(item));
            }
            else
            {
                while (operatorStack.Count > 0 && Precedence(operatorStack.Peek()) >= Precedence(item[0]))
                {
                    ProcessTopOperator(operandStack, operatorStack);
                }
                operatorStack.Push(item[0]);
            }
        }

        while (operatorStack.Count > 0)
        {
            ProcessTopOperator(operandStack, operatorStack);
        }

        return operandStack.Pop();
    }
    
    private static int Precedence(char op)
    {
        switch (op)
        {
            case '+':
            case '-':
                return 1;
            case '*':
            case '/':
                return 2;
            default:
                return 0;
        }
    }

    private void ProcessTopOperator(Stack<double> operandStack, Stack<char> operatorStack)
    {
        var op = operatorStack.Pop();
        var rightOperand = operandStack.Pop();
        var leftOperand = operandStack.Pop();
    
        var strategy = _operatorStrategies[op];
        var result = strategy.Apply(leftOperand, rightOperand);
        operandStack.Push(result);
    }


    private void ValidateExpressionOperators(string expression)
    {
        var operators = expression.Where(c => !char.IsDigit(c)).ToList();
        if (_operatorStrategies is null)
            throw new ArgumentException("No strategies declared for usage");
        var invalidOperators = operators.Where(o => !_operatorStrategies.ContainsKey(o)).ToList();
        if (invalidOperators.Any())
            throw new ArgumentException($"Invalid operator(s) given {string.Join(", ", invalidOperators)}");
    }
    
    private List<string> ParseExpression(string expression)
    {
        var pattern = $"({string.Join("|", 
            _operatorStrategies.Keys.Select(key => Regex.Escape(key.ToString())))})";
        return Regex.Split(expression, pattern).ToList();
    }
}