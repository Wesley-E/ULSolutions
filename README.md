# ULSolutions
#### Written and Architected by: Dominic Eccles
## Project Structure
```
.
├── Controllers
│   └── ExpressionCalculationController.cs
├── Factories
│   ├── Interfaces
│   │   └── IOperatorStrategyFactory.cs
│   └── OperatorStrategyFactory.cs
├── Models
│   └── Requests
│       └── ExpressionRequest.cs
├── Program.cs
├── Properties
│   └── launchSettings.json
├── Services
│   ├── ExpressionCalculationService.cs
│   ├── ExpressionStrategies
│   │   ├── AddOperatorStrategy.cs
│   │   ├── DivideOperatorStrategy.cs
│   │   ├── MultiplyOperatorStrategy.cs
│   │   └── SubtractOperatorStrategy.cs
│   └── Interfaces
│       ├── IBinaryOperatorStrategy.cs
│       └── IExpressionCalculationService.cs
├── ULSolutions.csproj
├── appsettings.Development.json
├── appsettings.json
└── read
```

The calculation of expressions occurs within the expression calculation service.

