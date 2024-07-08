# An OnlineProductStore, C#.NET 8 API with Clean architecture, CQRS pattern including unit tests.

## Description

A clean architecture, CQRS patterned online product store with REST APIs for product management and purchase, including unit tests. Built with C# and EF Core.

## Table of Contents

- [Project Structure](#project-structure)
- [Technologies Used](#technologies-used)
- [Code Challenge Requirements](#Code-Challenge-Requirements)
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

## Technologies Used
- C#
- .NET 8
- EF Core
- CQRS
- Clean Architecture
- In-Memory Caching
- Unit Testing (xUnit)

## Code Challenge Requirements
Consider a simple online store in which users can buy only one product in each order. This
system has the following entities:
### Entities:
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
### Requirement
### We need some "Rest API"s as follows:
- Add a product with a predefined `InventoryCount`. `product`'s title must be validated to be less than
40 characters. Also, `product`'s name must be unique.
- Increase `InventoryCount` of a product and update the product.
- Get a `product` by `id` with proper `price` considering current `discount` value.
- "Buy" endpoint that changes the inventory count of a product and adds an order to user orders.
- Write unit tests for two use cases of the application.
### Sidenotes
- You have to use `ef core` as your ORM (code first).
- For the third API you have to apply a caching procedure (any simple caching solution like in memory, ...) to
reduce database hits.
- There is no need to use an auth system.
- Both simplicity and cleanliness of your code matters.
- Add some sample users without any orders

## Documentation and Comments
The code is thoroughly documented with XML comments explaining the functionality of each component and method. Clear instructions are provided on how to build, run, and use the application.

## License
This project is licensed under the MIT License - see the [LICENSE](license.txt) file for details.
