using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSubstitute;
using ULSolutions.Controllers;
using ULSolutions.Models.Requests;
using ULSolutions.Services;
using Xunit;

namespace UnitTests.Controllers;

public class ExpressionCalculationControllerTests : IClassFixture<WebApplicationFactory<Program>>

{
    private readonly ExpressionCalculationController _sut;
    private readonly IExpressionCalculationService _expressionCalculationService;
    private readonly ILogger<ExpressionCalculationController> _logger;
    private readonly HttpClient _client;
    
    public ExpressionCalculationControllerTests(WebApplicationFactory<Program> factory)
    {
        _expressionCalculationService = Substitute.For<IExpressionCalculationService>();
        _logger = Substitute.For<ILogger<ExpressionCalculationController>>();
        _sut = new ExpressionCalculationController(_expressionCalculationService, _logger);
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CalculateExpression_ReturnsOkResponse_WhenValidExpressionGiven()
    {
        var expressionRequest = new ExpressionRequest { Expression = "7+5" };
        var content = new StringContent(JsonConvert.SerializeObject(expressionRequest), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("http://localhost:5099/ExpressionCalculation", content);

        Assert.Equal("12", await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task CalculateExpression_ReturnsBadResponse_WhenInValidExpressionGiven()
    {
        var expressionRequest = new ExpressionRequest { Expression = "7&5" };
        var content = new StringContent(JsonConvert.SerializeObject(expressionRequest), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("http://localhost:5099/ExpressionCalculation", content);
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}