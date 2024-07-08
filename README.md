# An OnlineProductStore, C#.NET 8 API with Clean architecture, CQRS pattern including unit tests.

## Description

A clean architecture, CQRS patterned online product store with REST APIs for product management and purchase, including unit tests. Built with C# and EF Core.

## Table of Contents

- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Entities](#entities)
- [API Endpoints](#api-endpoints)
- [Technologies Used](#technologies-used)
- [Setup Instructions](#setup-instructions)
- [Running Tests](#running-tests)

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

## Project Structure

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
