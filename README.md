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
Project Structure

src/
├── Ecom.API
│   ├── Controllers
│   ├── Helper
│   ├── Mapping
│   ├── Middleware
│   └── Program.cs
│   (Handles HTTP requests, controllers, API mappings, and middleware)
│
├── Ecom.Core
│   ├── Entities
│   ├── DTO
│   ├── Interfaces
│   ├── Services
│   └── Sharing
│   (Contains business entities, DTOs, service interfaces, and domain logic)
│
└── Ecom.Infrastructure
    ├── Data
    ├── Repositories
    ├── Configurations
    └── InfrastructureRegistration.cs
    (Implements repositories, EF Core configurations, and external service integrations)

