# Public API - HNG12 Stage 0 Task

## Description

This is a simple public API developed as part of the HNG12 Stage 0 Backend task. The API returns basic information in JSON format, including:

- The registered email address used for HNG12 Slack workspace.

- The current datetime as an ISO 8601 formatted timestamp.

- The GitHub repository URL of this project.

## Technology Stack

ASP.NET Core

## API Documentation

### Endpoint GET /
- Sample Response (200 OK)

```
{
  "email": "my_email@gmail.com",
  "current_datetime": "2025-01-31T09:30:00Z",
  "github_url": "https://github.com/heba-webdev/HNG12-Task1"
}
```

## Prerequisites

- .NET SDK installed (Download Here)

- Git 

## Steps to Run Locally

- Clone the repository:
```
git clone https://github.com/heba-webdev/HNG12-Task1.git
cd HNG12-Task1
```
- Build the project:
```
dotnet build
```
- Run the application:
```
dotnet run
```

* The API will be available at http://localhost:5093/

## CORS Handling

The API is configured to accept requests from any origin. If needed, modify the Program.cs file to restrict allowed origins.

## License

This project is open-source and available under the MIT License.

