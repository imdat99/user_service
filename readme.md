# Command Tạo
```shell
dotnet new sln && dotnet sln add src
dotnet new webapi -n User.API -o src/User.API && dotnet sln add src/User.API/User.API.csproj
dotnet new classlib -n User.Application -o src/User.Application && dotnet sln add src/User.Application/User.Application.csproj
dotnet new classlib -n User.Domain -o src/User.Domain && dotnet sln add src/User.Domain/User.Domain.csproj
dotnet new classlib -n User.Infrastructure -o src/User.Infrastructure && dotnet sln add src/User.Infrastructure/User.Infrastructure.csproj
dotnet new classlib -n User.Persistence -o src/User.Persistence && dotnet sln add src/User.Persistence/User.Persistence.csproj
```
# Command Xoá
```shell
dotnet sln remove src/User.API/User.API.csproj && rm -rf src/User.API
```
Anh vẽ cho chú cái **sơ đồ cấu trúc thư mục** chuẩn bài cho một project **.NET 8 Clean Architecture + DDD** — không lý thuyết lòng vòng, nhìn là hiểu flow, chia đúng tầng, đúng trách nhiệm.

---

### 🗂️ **Cấu trúc thư mục tổng thể**

```plaintext
/MyApp (solution)
│
├── MyApp.API                  ← 🧾 Web API đầu vào, controller, DI
│   └── Controllers/
│   └── Program.cs
│
├── MyApp.Application          ← 🧠 Use Cases, DTOs, interface repo, CQRS
│   └── Orders/
│       ├── Commands/
│       │   ├── CreateOrderCommand.cs
│       │   └── CreateOrderHandler.cs
│       ├── Queries/
│       └── Dtos/
│   └── Common/
│       ├── Interfaces/
│       └── Behaviors/         ← (Validation, Logging, etc.)
│
├── MyApp.Domain               ← 📦 Core domain logic: Entity, VO, Events
│   └── Entities/
│       └── Order.cs
│   └── ValueObjects/
│       └── OrderItem.cs
│   └── Events/
│   └── Enums/
│
├── MyApp.Persistence          ← 💾 EF Core DbContext, migrations, repo impl
│   ├── Repositories/
│   │   └── OrderRepository.cs
│   └── AppDbContext.cs
│
├── MyApp.Infrastructure       ← 🌐 Implementation infra: Email, File, Identity
│   └── Email/
│   └── FileStorage/
│   └── DependencyInjection.cs
│
├── MyApp.SharedKernel         ← 🔧 BaseEntity, Result<T>, Errors, Utils (tuỳ chọn)
│   └── Base/
│   └── ValueObjects/
│
└── MyApp.sln
```

---

### Sơ đồ đơn giản hóa

```plaintext
📁 MyApp (solution)
├── 🧾 API                → Giao tiếp với client (Controller, DI)
├── 🧠 Application        → Nơi xử lý UseCase (CQRS)
├── 📦 Domain            → Core nghiệp vụ (Entity, VO, Event)
├── 💾 Persistence       → EF Core, Repository Impl
├── 🌐 Infrastructure     → Email, Logger, 3rd Party
├── 🔧 SharedKernel       → BaseEntity, helpers, Result<T>
```

---

### Flow xử lý request:

```
API Controller
   ↓
Application Command (CreateOrderCommand)
   ↓
Handler xử lý UseCase
   ↓
Gọi Domain để khởi tạo Order
   ↓
Order.AddItem() → Domain logic
   ↓
Lưu Order qua Repository (interface)
   ↓
Repository Impl (EF Core) ở Persistence
```
