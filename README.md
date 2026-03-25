# Ecommerce API

A layered .NET 10 backend solution for e-commerce, organized into:

- `ECommerce.API` – REST API endpoints and application startup.
- `ECommerce.BLL` – business logic, DTOs, mappers, validators, and managers.
- `ECommerce.DAL` – Entity Framework Core data access, repositories, unit of work, migrations, and seed data.
- `ECommerce.Common` – shared result models, filtering, and pagination utilities.

> `ECommerce.MVC` is intentionally not part of this repository scope.

## Tech Stack

- .NET `net10.0`
- ASP.NET Core Web API
- Entity Framework Core (SQL Server)
- ASP.NET Core Identity
- FluentValidation
- Serilog
- OpenAPI + Scalar UI

## Project Structure

```text
ECommerce.API
ECommerce.BLL
ECommerce.DAL
ECommerce.Common
```

## API Modules

- `ProductsController`
  - `GET /api/products`
  - `GET /api/products/pagination`
  - `GET /api/products/{id}`
  - `GET /api/products/search?name=`
  - `POST /api/products`
  - `PUT /api/products/{id}`
  - `DELETE /api/products/{id}`
- `CategoriesController`
  - `GET /api/categories`
  - `GET /api/categories/{id}`
  - `POST /api/categories`
  - `PUT /api/categories/{id}`
  - `DELETE /api/categories/{id}`
- `ImageController`
  - `POST /api/image/upload`

## Configuration

Set connection string in `ECommerce.API/appsettings.Development.json`:

- `ConnectionStrings:DefaultConnection`

Current development sample uses local SQL Server Express.

## Run Locally

1. Restore packages:
   - `dotnet restore`
2. Build:
   - `dotnet build`
3. Run API:
   - `dotnet run --project ECommerce.API`

Default development URLs (from launch settings):

- `https://localhost:7183`
- `http://localhost:5121`

Open Scalar UI at:

- `https://localhost:7183/scalar`

## Database

Create/update database with EF Core migrations:

- `dotnet ef database update --project ECommerce.DAL --startup-project ECommerce.API`

## Notes

- Keep sensitive values out of source control. Prefer environment variables or secret managers for production settings.
- Static files for uploaded images are served from the `Files` folder.
