using Microsoft.AspNetCore.Mvc;
using ULSolutions.Models.Requests;
using ULSolutions.Services;

namespace ULSolutions.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpressionCalculationController : ControllerBase
{
    private readonly IExpressionCalculationService _expressionCalculationService;
    private readonly ILogger<ExpressionCalculationController> _logger;

    public ExpressionCalculationController(
        IExpressionCalculationService expressionCalculationService,
        ILogger<ExpressionCalculationController> logger)
    {
        _expressionCalculationService = expressionCalculationService;
        _logger = logger;
    }

    [HttpPost(Name = "GetFormulaCalculation")]
    public IActionResult CalculateExpression([FromBody] ExpressionRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Expression))
        {
            return BadRequest("Expression is missing or empty.");
        }

        try
        {
            var result = _expressionCalculationService.Calculate(request.Expression);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Failed to calculate expression: {ex}");
        }
    }
}