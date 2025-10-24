Ecom Clean Architecture (.NET 8)

Ecom API is a fully functional E-Commerce Web API built with .NET 8 following the Clean Architecture pattern.
It includes secure authentication, payment processing via Stripe, and SMTP email notifications for orders.

Tech Stack

.NET 8 Web API

Entity Framework Core 8

ASP.NET Core Identity

JWT Authentication

Stripe.NET

AutoMapper

SQL Server

SMTP

Repository + Unit of Work Pattern

Features

Clean Architecture (Core / Infrastructure / API layers)

Authentication & Authorization with ASP.NET Identity + JWT


Stripe Integration for secure payments

SMTP Email Service for order confirmations

Complete Order Management System (Orders, Items, Delivery Methods, Addresses)

Product Rating System

Entity Framework Core 8 with Migrations and SQL Server

AutoMapper and Repository Pattern

Global Exception Handling and Custom Middleware

Project Structure
src/
├── Ecom.API                 → Presentation Layer (Controllers, Middleware, Mapping)
│   ├── Controllers
│   ├── Helper
│   ├── Mapping
│   ├── Middleware
│   └── Program.cs
│
├── Ecom.Core                → Domain & Application Logic
│   ├── Entities
│   ├── DTO
│   ├── Interfaces
│   ├── Services
│   └── Sharing
│
└── Ecom.Infrastructure      → Data Access & External Integrations
    ├── Data
    ├── Repositories
    ├── Configurations
    └── InfrastructureRegistration.cs

