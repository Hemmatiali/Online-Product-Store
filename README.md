# An OnlineProductStore, C#.NET 8 API with Clean architecture, CQRS pattern including unit tests.

## Description

A clean architecture, CQRS patterned online product store with REST APIs for product management and purchase, including unit tests. Built with C# and EF Core.

## Table of Contents

- [Project Structure](#project-structure)
- [Entities](#entities)
- [Technologies Used](#technologies-used)
- [Documentation and Comments](#documentation-and-comments)
- [License](#license)

## Project Structure
```bash
CodeChallengeProject/
│
├── CodeChallengeProject.src/
    ├── CodeChallengeProject.core/
        ├── CodeChallengeProject.Domain/
        │ ├── Entities/
        │ ├── Models/
        │ └── Shared/
        │ └── ValueObjects/
        │
        ├── CodeChallengeProject.Application/
        │ ├── Features/
        │ ├── Mapping/
        │ └── ViewModels/
        │
    ├── CodeChallengeProject.infrastructure/
        ├── CodeChallengeProject.Infrastructure/

        ├── CodeChallengeProject.Persistence/
        │ ├── Data/
        │ ├── Repositories/
          │ └── Contexts/
          │ └── EntityConfigurations/
          │ └── Repositories/
        │
    ├── CodeChallengeProject.presentation/
        ├── CodeChallengeProject.WebApi/
        │ ├── Controllers/
        │ ├── Program.cs/
        │
    ├── CodeChallengeProject.tests/
        ├── CodeChallenge.UnitTests/
        │ ├── Features/
        │
└── OnlineProductStore.sln
```
## Entities

### Product

- Id
- Title
- InventoryCount
- Price
- Discount (percentage)

### User

- Id
- Name
- Orders

### Order

- Id
- Product
- CreationDate
- Buyer

### Technologies Used
- C#
- .NET 8
- EF Core
- CQRS
- Clean Architecture
- In-Memory Caching
- Unit Testing (xUnit)

## Documentation and Comments
The code is thoroughly documented with XML comments explaining the functionality of each component and method. Clear instructions are provided on how to build, run, and use the application.

## License
This project is licensed under the MIT License - see the [LICENSE](license.txt) file for details.
