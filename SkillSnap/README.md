# SkillSnap

SkillSnap is a .NET solution that consists of a Web API project and a Blazor WebAssembly client application. This solution is designed to demonstrate the capabilities of building a modern web application using .NET technologies.

## Projects Overview

### SkillSnap.Api
- A Web API project that serves as the backend for the application.
- Handles HTTP requests and provides data to the client.
- Contains controllers that manage the application's endpoints.

### SkillSnap.Client
- A Blazor WebAssembly project that serves as the frontend for the application.
- Provides a rich interactive user interface using C# and Razor components.
- Communicates with the API to fetch and display data.

### SkillSnap.Shared
- A Class Library project that contains shared Data Transfer Objects (DTOs) and reusable logic.
- Ensures that both the API and client projects can utilize common data structures.

## Getting Started

### Prerequisites
- .NET SDK (version 6.0 or later)
- A code editor (e.g., Visual Studio Code)

### Setup Instructions
1. Clone the repository:
   ```
   git clone <repository-url>
   ```
2. Navigate to the project directory:
   ```
   cd SkillSnap
   ```
3. Restore the project dependencies:
   ```
   dotnet restore
   ```
4. Build the solution:
   ```
   dotnet build
   ```
5. Run the API project:
   ```
   cd SkillSnap.Api
   dotnet run
   ```
6. In a new terminal, run the Blazor client project:
   ```
   cd SkillSnap.Client
   dotnet run
   ```

## Contributing
Contributions are welcome! Please feel free to submit a pull request or open an issue for any suggestions or improvements.

## License
This project is licensed under the MIT License. See the LICENSE file for more details.