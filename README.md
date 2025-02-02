# Public API - HNG12 Stage 0 Task

## Description

An API that takes a number and returns interesting mathematical properties about it, along with a fun fact.

## Technology Stack

ASP.NET Core

## API Documentation

### Endpoint GET /api/classify-number?number=371
- Sample Response (200 OK)

```
{
  "number": 371,
  "is_prime": false,
  "is_perfect": false,
  "properties": ["armstrong", "odd"],
  "digit_sum": 11,  // sum of its digits
  "fun_fact": "371 is an Armstrong number because 3^3 + 7^3 + 1^3 = 371"
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

