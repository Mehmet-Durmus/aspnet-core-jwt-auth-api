# ASP.NET Core JWT Authentication API
This project is a JWT-based authentication API built with ASP.NET Core Web API, providing core authentication features such as user registration, login, token management, email verification, and password reset.

The project follows a layered architecture, clearly separating API, business logic, and data access responsibilities.

## Features
* User registration
* Login with JWT access token
* Refresh token based authentication
* Email verification flow
* Input validation with FluentValidation
* Centralized exception handling

## Tech Stack
* ASP.NET Core Web API
* Entity Framework Core
* JWT (Access Token)
* FluentValidation
* SQL Server
* Layered Architecture

## Project Structure

The solution is organized into three main layers:

**API**

Responsible for handling HTTP requests and responses.
* Controllers
* Filters
* Global exception handling
* Application configuration

**BusinessLogic**

Contains the core business rules and application logic.
* Service abstractions and implementations
* Data transfer objects
* Validation rules
* Security utilities (password hashing, token handling)
* Custom application exceptions

**DataAccess**

Handles database interactions.
* Entity definitions
* Repository implementations
* Entity Framework Core context

This separation ensures better maintainability, testability, and clarity of responsibilities.

## Getting Started

### Prerequisites
* .NET SDK
* SQL Server

### Appsettings Configuration

Before running the application, make sure to configure the required values in `appsettings.json`.

#### **Connection String**
Update the database connection string according to your local or preferred database setup.

```json
"ConnectionStrings": {
  "SqlServer": "sql-server-connection-string"
}
```

#### **JWT Settings**
Issuer and Audience fields are preconfigured for development purposes. However, you must provide a valid `SecurityKey` that is at least 32 characters long and sufficiently random.

```json
"TokenSettings": {
    "JwtSettings": {
        "SecurityKey": "your_secret_key"
    }
}
```

#### **Mail Settings**
To enable email-related features, update the mail configuration with your own SMTP credentials.

```json
"MailSettings": {
  "EmailVerificationSettings": {
    "EmailAddress": "your_email_address",
    "DisplayName": "display_name",
    "Subject": "Email Verification",
    "MailBody": "mail_message",
    "BaseUrl": "https://www.appdomain.com/verify-email",
    "Port": {your_port_number},
    "EnableSsl": true,
    "Host": "smtp_host",
    "Password": "your_password",
    "ResendCooldownSeconds": 180
  },
  "ResetPasswordSettings": {
    "EmailAddress": "your-email-address",
    "DisplayName": "display-name",
    "Subject": "Reset Password",
    "MailBody": "mail_message",
    "BaseUrl": "https://www.appdomain.com/reset-password",
    "Port": {your_port_number},
    "EnableSsl": true,
    "Host": "your_host",
    "Password": "your_password",
    "ResendCooldownSeconds": 180
  }
}
```

### Database Setup
After configuring `appsettings.json`, the next step is to create the database using Entity Framework Core migrations.

The project already contains the required migrations in the **DataAccess** layer.

#### Using Visual Studio (Package Manager Console)

1. Open the solution in Visual Studio
2. Set the **API** project as the Startup Project
3. Open **Package Manager Console**
4. Select **DataAccess** as the Default Project
5. Run the following command
```powershell
update-database
```
This will apply the existing migrations and create the database using the connection string defined in `appsettings.json`.

#### Using .NET CLI (VS Code / Terminal)
Make sure you are in the solution root directory (where the `.sln` file is located), then run:
```powershell
dotnet ef database update --project LogInSignUp.DataAccess --startup-project LogInSignUp.API
```
This command:
* Uses the migrations from the **DataAccess** project.
* Uses the configuration (`appsettings.json`) from the **API** project.

**EF Core CLI Requirement** 

If the `dotnet ef` command is not available, install it using the following command:
```powershell
dotnet tool install --global dotnet-ef
```

### Run the Application

After completing the database setup, the application can be built and run.

#### Using Visual Studio
1. Open the solution in Visual Studio
2. Ensure that the API project is configured as the startup project.
3. Run the application.

#### Using .NET CLI
The application can be run using the .NET CLI by targeting the API project.

From the solution root directory:
```powershell
dotnet run --project LogInSignUp.API
```
Or by navigating directly to the API project directory:
```powershell
cd LogInSignUp.API
dotnet run
```
When the application starts successfully, the listening URL will be displayed in the output window. The Swagger UI can be accessed at `http://localhost:{PORT}/swagger`.

Swagger provides a complete list of available endpoints and allows you to test the API directly from the browser.