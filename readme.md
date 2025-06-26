Anh vẽ cho chú cái **sơ đồ cấu trúc thư mục** chuẩn bài cho một project **.NET 9 Clean Architecture + DDD** — không lý thuyết lòng vòng, nhìn là hiểu flow, chia đúng tầng, đúng trách nhiệm.

---
Domain-Driven Design (DDD) trong ASP.NET Core thường được tổ chức theo cấu trúc phân lớp rõ ràng. Dưới đây là cách tổ chức một dự án ASP.NET Core theo DDD:

## Cấu trúc thư mục chính

```
Account/
├── src/
│   ├── Account.Domain/          # Lớp Domain
│   ├── Account.Application/     # Lớp Application
│   ├── Account.Infrastructure/  # Lớp Infrastructure
│   ├── Account.Presentation/    # Lớp Presentation (Web API)
│   └── Account.Shared/          # Shared Kernel
├── tests/
└── docs/
```

## Chi tiết từng lớp

### 1. Domain Layer (Account.Domain)
Đây là trung tâm của ứng dụng, chứa business logic thuần túy:

```
Domain/
├── Entities/
│   ├── User.cs
│   ├── Order.cs
│   └── Product.cs
├── ValueObjects/
│   ├── Email.cs
│   ├── Money.cs
│   └── Address.cs
├── Aggregates/
│   └── OrderAggregate/
├── DomainEvents/
│   ├── OrderCreatedEvent.cs
│   └── UserRegisteredEvent.cs
├── DomainServices/
│   └── PricingService.cs
├── Repositories/
│   ├── IUserRepository.cs
│   └── IOrderRepository.cs
├── Specifications/
└── Exceptions/
    └── DomainException.cs
```

### 2. Application Layer (Account.Application)
Điều phối các use cases và orchestrate domain objects:

```
Application/
├── Commands/
│   ├── CreateOrderCommand.cs
│   └── UpdateUserCommand.cs
├── Queries/
│   ├── GetOrderQuery.cs
│   └── GetUserQuery.cs
├── Handlers/
│   ├── CreateOrderHandler.cs
│   └── GetOrderHandler.cs
├── DTOs/
│   ├── OrderDto.cs
│   └── UserDto.cs
├── Services/
│   └── ApplicationService.cs
├── Interfaces/
│   └── IEmailService.cs
├── Validators/
│   └── CreateOrderValidator.cs
└── Mappings/
    └── MappingProfile.cs
```

### 3. Infrastructure Layer (Account.Infrastructure)
Triển khai các interface từ Domain và Application:

```
Infrastructure/
├── Data/
│   ├── ApplicationDbContext.cs
│   ├── Configurations/
│   │   ├── UserConfiguration.cs
│   │   └── OrderConfiguration.cs
│   └── Migrations/
├── Repositories/
│   ├── UserRepository.cs
│   └── OrderRepository.cs
├── Services/
│   ├── EmailService.cs
│   └── FileService.cs
├── External/
│   └── PaymentGateway.cs
└── Caching/
    └── CacheService.cs
```

### 4. Presentation Layer (Account.Presentation)
API Controllers và các endpoint:

```
Presentation/
├── Controllers/
│   ├── UsersController.cs
│   └── OrdersController.cs
├── Middlewares/
│   ├── ExceptionMiddleware.cs
│   └── AuthenticationMiddleware.cs
├── Filters/
│   └── ValidationFilter.cs
├── Models/
│   ├── Requests/
│   └── Responses/
└── Program.cs
```

## Dependency Injection Setup

Trong `Program.cs`:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Domain services
builder.Services.AddScoped<IPricingService, PricingService>();

// Application services
builder.Services.AddScoped<ICreateOrderHandler, CreateOrderHandler>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Infrastructure services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Presentation services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
```

## Nguyên tắc quan trọng

**Dependency Rule**: Các lớp bên trong không được phụ thuộc vào lớp bên ngoài
- Domain không phụ thuộc vào bất kỳ lớp nào khác
- Application chỉ phụ thuộc vào Domain
- Infrastructure có thể phụ thuộc vào Application và Domain
- Presentation phụ thuộc vào Application

**Aggregate Boundaries**: Mỗi aggregate có một root entity và được truy cập thông qua repository

**Domain Events**: Sử dụng để loose coupling giữa các bounded context

Cấu trúc này giúp tách biệt rõ ràng các concerns, dễ dàng testing và maintain code theo thời gian.

# MyAccount - User Account Management System

A .NET 8 Clean Architecture + Domain-Driven Design (DDD) implementation for managing user accounts.

## Project Structure

```
MyAccount/
├── src/
│   ├── MyAccount.Domain/          # Domain Layer
│   ├── MyAccount.Application/     # Application Layer
│   ├── MyAccount.Infrastructure/  # Infrastructure Layer
│   ├── MyAccount.Presentation/    # API Layer
│   └── MyAccount.Shared/          # Shared Kernel
├── tests/
└── docs/
```

## Architecture

This project follows the principles of Clean Architecture and Domain-Driven Design (DDD):

1. **Domain Layer**: Core business logic and entities
2. **Application Layer**: Use cases, commands, queries, and DTOs
3. **Infrastructure Layer**: External concerns like databases, files, and APIs
4. **Presentation Layer**: API endpoints and controllers

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- MySQL 8.0 or later
- Visual Studio 2022 or JetBrains Rider

### Setup

1. Clone the repository
2. Update connection string in `src/MyAccount.Presentation/appsettings.json`
3. Run database migrations:

```bash
cd src/MyAccount.Presentation
dotnet ef database update
```

4. Run the application:

```bash
dotnet run
```

## Features

- User account management
- Profile management
- Two-factor authentication
- Session management
- Activity logging
- Notification settings
- Privacy settings
- Payment methods
- Transaction history
- API key management

## Database Schema

The database schema includes tables for:

- Users
- User profiles
- Two-factor authentication
- Sessions
- Activity logs
- Notification settings
- Privacy settings
- Payment methods
- Transactions
- User tokens (JWT)
- API keys

## Dependencies

- Entity Framework Core
- MediatR for CQRS
- AutoMapper for object mapping
- FluentValidation for validation
- Pomelo.EntityFrameworkCore.MySql for MySQL support

## License

This project is licensed under the MIT License - see the LICENSE file for details.
