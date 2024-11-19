# SharpEcho - Coding Assessment

## Overview
This project is intended to track matches (or games) played for any arbitrary sport but is incomplete.

## Project Components

### Framework Level
- **SharpEcho.CodeChallenge.Api**: Implementation of a generic REST API that maps HTTP actions to CRUD operations.
- **SharpEcho.CodeChallenge.Data**: Implements a generic repository and CRUD operations on tables following the convention that there is a primary key called `Id`.

### Application Level
- **SharpEcho.CodeChallenge.Api.Team**: Provides functionality to create, update, delete, and find a team. The project is configured to use Swagger for API browsing.
- **SharpEchoCodeChallenge**: The database solution. The only table currently in place is the `Team` table.

## Implementation Tasks
1. **Functionality to Record a Match Between Two Teams**
   - Track the result of a match (e.g., Dallas Cowboys played the Atlanta Falcons on 9/20 and the Cowboys won).
   - Assume no ties are allowed.

2. **Capability to Get the Overall Win-Loss for Matches Between Two Teams**
   - Example: Cowboys and Falcons have played each other 28 times, and the Cowboys have won 17 times.

3. **Create Unit Tests**
   - Cover the new features and existing functionality provided automatically at the framework level.

4. **Create a Client**
   - Add two teams: Dallas Cowboys and Atlanta Falcons.
   - Record 28 matches between the two teams, with the Cowboys winning 17 of the matches.
   - Return the overall win-loss for games between the Cowboys and Falcons.

## Developer Notes

- Unzipped the archive and loaded the solution in VS 2022.
- Converted the solution from .NET 4.5 to .NET 4.8.
- Created a GitHub repo: [SharpEcho.CodeChallenge](https://github.com/wblackmon/SharpEcho.CodeChallenge.git).

### Task: Get the SharpEcho.CodeChallenge.Api Working
- Set `SharpEcho.CodeChallenge.Api` as the startup project.
- Fixed the cert error by adding the localhost certificate to Trusted Root Certification certificates.
- Fixed the "HTTP Error 500.31 - Failed to load ASP.NET Core runtime" by installing the .NET Core 3.1 SDK.
- The API project now runs and loads a Swagger interface showing all API methods.

### Task: Get the SharpEcho.CodeChallenge.Api.Team.Tests Project Working
- Fixed database connectivity issues by creating and seeding the `SharpEchoCodeChallenge` database.
- Moved the database to SQL Server 2022 Developer Edition for performance reasons.
- Changed connection string keyword "Trust Server Certificate" to "TrustServerCertificate" to fix a connection error.
- Created `Game` entity for recording games or matches.
- Refactored and simplified `Game` entity and database.

### Goals and Best Practices
1. **Code Quality**: Clean, readable, and well-organized code.
2. **Separation of Concerns**: Clear separation between different layers and use of dependency injection.
3. **Testability**: Easy to unit test with demonstration of unit tests for key components.
4. **Use of Best Practices**: Proper error handling, logging, and secure handling of data.
5. **Simplicity and Efficiency**: Avoid unnecessary complexity and ensure efficient use of resources.
6. **Documentation and Comments**: Clear comments and documentation for public methods and classes.

### Simplified Approach
1. **Use Data Transfer Objects (DTO) for Decoupling**: Continue using DTOS to decouple the web app from the API entities.
2. **Simplify Services**: Keep the services focused on their core responsibilities.
3. **Ensure Testability**: Write unit tests for the services and controllers.
