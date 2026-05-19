# MiniAppAPI

MiniAppAPI tədbir (event), təşkilatçı (organizer) və bilet (ticket) idarəetməsi üçün qurulmuş ASP.NET Core **.NET 10** Web API layihəsidir.

## Xüsusiyyətlər

- .NET 10 (ASP.NET Core Web API)
- Entity Framework Core 10 + PostgreSQL provider (`Npgsql`)
- FluentValidation ilə request validation
- AutoMapper ilə DTO mapping
- Global exception handling middleware
- Standard API response strukturu (`ApiResponse<T>`)
- Pagination, sıralama və istiqamət parametrləri
- `wwwroot` üzərindən statik fayl (şəkil) servisi
- OpenAPI + Scalar API UI

## Texnologiyalar

- .NET SDK 10.0+
- ASP.NET Core
- Entity Framework Core 10
- PostgreSQL
- FluentValidation
- AutoMapper
- Scalar.AspNetCore

## Layihə strukturu

```text
MiniAppAPI/
├── MiniAppApi/
│   ├── Controllers/          # API endpoint-ləri
│   ├── Data/                 # DbContext, konfiqurasiya, migrations
│   ├── Dtos/                 # Request/Response DTO-lar
│   ├── Middleware/           # Exception handling middleware
│   ├── Models/               # Domain modelləri
│   ├── Profiles/             # AutoMapper profilləri
│   ├── Services/             # Biznes məntiqi
│   ├── Utils/                # Köməkçi utility-lər
│   ├── Program.cs
│   └── ServiceRegistration.cs
└── MiniAppApi.slnx
```

## Tələblər

Aşağıdakılar sisteminizdə qurulu olmalıdır:

- .NET SDK 10 (məs: `10.0.201`)
- PostgreSQL server
- (Opsional) EF Core CLI:
  ```bash
  dotnet tool install --global dotnet-ef
  ```

## Konfiqurasiya

Layihə `AddAppSettingsMultiPlatformJson(builder, "Mac")` istifadə edir və `.gitignore`-a görə `appsettings.Mac.json` faylı local saxlanılır.

`MiniAppApi/appsettings.Mac.json` faylını yaradın:

```json
{
  "ConnectionStrings": {
    "PostgreSqlConnection": "Host=localhost;Port=5432;Database=miniappdb;Username=postgres;Password=your_password"
  }
}
```

> Qeyd: `ServiceRegistration.cs` daxilində `UseNpgsql(configuration.GetConnectionString("PostgreSqlConnection"))` istifadə olunur; buna görə connection string adı **PostgreSqlConnection** olmalıdır.

## Quraşdırma və işə salma

Repo kökündə:

```bash
cd <repo-root>
```

Restore və build:

```bash
dotnet restore
dotnet build
```

Miqrasiyanı DB-yə tətbiq et:

```bash
dotnet ef database update --project MiniAppApi --startup-project MiniAppApi
```

API-ni işə sal:

```bash
dotnet run --project MiniAppApi
```

Default development URL (`launchSettings.json`):

- `http://localhost:5075`

## API sənədləşməsi

App ayağa qalxdıqdan sonra:

- Scalar UI: `http://localhost:5075/scalar`
- OpenAPI JSON (default): `http://localhost:5075/openapi/v1.json`

## Əsas endpoint-lər

Base route: `/api`

### Organizers

- `GET /api/Organizers` — təşkilatçı siyahısı (pagination)
- `POST /api/Organizers` — yeni təşkilatçı yarat
- `POST /api/Organizers/{id}/logo` — təşkilatçı logo upload (`multipart/form-data`)
- `GET /api/Organizers/{organizerId}/events` — təşkilatçının event-ləri

### Events

- `GET /api/Events` — event siyahısı (pagination)
- `POST /api/Events` — yeni event yarat
- `POST /api/Events/{id}/banner` — event banner upload (`multipart/form-data`)
- `GET /api/Events/{eventId}/tickets` — event-in biletləri
- `GET /api/Events/{eventId}/organizer` — event-in təşkilatçısı
- `POST /api/Events/{eventId}/tickets` — event üçün bilet yarat

### Tickets

- `GET /api/Tickets` — bilet siyahısı (pagination)
- `POST /api/Tickets` — yeni bilet yarat

## Pagination və sıralama

Pagination query parametrləri (`PaginationParams`):

- `PageNumber` (default: `1`)
- `PageSize` (default: `10`, max: `100`)
- `SortBy` (default: `Id`)
- `Ascending` (default: `true`)

Nümunə:

```http
GET /api/Events?PageNumber=1&PageSize=10&SortBy=Date&Ascending=false
```

## Request nümunələri

### Organizer yarat

```http
POST /api/Organizers
Content-Type: application/json
```

```json
{
  "name": "Tech Conf Org",
  "email": "contact@techconf.az",
  "phone": "+994501112233"
}
```

### Ticket yarat

```http
POST /api/Tickets
Content-Type: application/json
```

```json
{
  "type": "VIP",
  "price": 99.99,
  "eventId": 1
}
```

## Response formatı

Bütün cavablar standart `ApiResponse<T>` formatındadır:

```json
{
  "success": true,
  "message": "Operation successful",
  "data": {},
  "timestamp": "2026-01-01T12:00:00Z"
}
```

Pagination list endpoint-ləri üçün `data` hissəsi `PaginatedResponse<T>` qaytarır:

- `items`
- `totalCount`
- `pageNumber`
- `pageSize`
- `totalPages`
- `hasNextPage`
- `hasPreviousPage`

## Error handling

Global middleware istisnaları aşağıdakı kimi map edir:

- `EntityNotFoundException` → `404 Not Found`
- `ApplicationException` (custom) → `400 Bad Request`
- digər istisnalar → `500 Internal Server Error`

Error response-lar da `ApiResponse<object>` formatında qaytarılır (`success: false`).

## Fayl upload qeydləri

- Organizer logo: `wwwroot/uploads/organizers/{organizerId}/...`
- Event banner: `wwwroot/uploads/events/{eventId}/...`
- Statik fayllar `UseStaticFiles()` vasitəsilə servis olunur.

## Mümkün problemlər və həllər

- **Connection string xətası**: `appsettings.Mac.json` faylının mövcud olduğunu və `PostgreSqlConnection` key adını yoxlayın.
- **DB update xətası**: PostgreSQL server-in işlək olduğunu və credential-ların doğru olduğunu yoxlayın.
- **Port fərqliliyi**: `launchSettings.json` içində `applicationUrl` dəyərinə baxın.

## Qısa development flow

```bash
# 1) Build
dotnet build

# 2) Migration tətbiqi
dotnet ef database update --project MiniAppApi --startup-project MiniAppApi

# 3) Run
dotnet run --project MiniAppApi
```
