# Ecom Clean Architecture (.NET 8)

Ecom API is a fully functional E-Commerce Web API built using **.NET 8**.  
It strictly follows the **Clean Architecture** principles, separating concerns into distinct layers for maintainability and scalability.  
The system includes secure user authentication, payment processing through **Stripe**, and automatic email notifications via **SMTP** when orders are placed.

---

## Tech Stack

- **.NET 8 Web API**  
- **Entity Framework Core 8**  
- **ASP.NET Core Identity**  
- **JWT Authentication**  
- **Stripe.NET**  
- **AutoMapper**  
- **SQL Server**  
- **SMTP**  
- **Repository + Unit of Work Pattern**  

---

## Features

- Clean Architecture (Core / Infrastructure / API layers)  
- Authentication & Authorization with ASP.NET Identity + JWT  
- Stripe Integration for secure payments  
- SMTP Email Service for order confirmations  
- Complete Order Management System (Orders, Items, Delivery Methods, Addresses)  
- Product Rating System  
- Entity Framework Core 8 with Migrations and SQL Server  
- AutoMapper and Repository Pattern  
- Global Exception Handling and Custom Middleware  

---
## Project Structure

```
src/
├── Ecom.API
│   ├── Controllers    # API endpoints
│   ├── Helper         # Utilities
│   ├── Mapping        # AutoMapper profiles
│   ├── Middleware     # Auth & error handling
│   └── Program.cs     # App entry point
│
├── Ecom.Core
│   ├── Entities       # Domain models
│   ├── DTO            # Request/response objects
│   ├── Interfaces     # Service & repository contracts
│   ├── Services       # Business logic
│   └── Sharing        # Shared utilities/enums
│
└── Ecom.Infrastructure
    ├── Data           # DbContext & seeding
    ├── Repositories   # Repository implementations
    ├── Configurations # EF Core configs
    └── InfrastructureRegistration.cs # DI setup
```


