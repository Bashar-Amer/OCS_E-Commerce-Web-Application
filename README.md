# Rahhala — Outdoor & Travel Gear E‑Commerce

Rahhala is a full‑stack e‑commerce web application for camping, trekking and wilderness
equipment. It provides a public storefront (catalog, cart, wishlist, checkout, order
history) and a role‑protected admin portal for managing the catalog, orders and
user‑generated content.

**Live site:** https://rahhala.runasp.net/

> The project is also referred to internally by its earlier working names
> *Barrameru* / *CampTravelGear* — these still appear in the assembly name, some CSS/JS
> file prefixes and seed data.

---

## Tech stack

| Layer | Technology |
|---|---|
| Runtime / framework | ASP.NET Core **MVC** on **.NET 10** |
| Data access | Entity Framework Core 10 (`Microsoft.EntityFrameworkCore.SqlServer`) |
| Database | SQL Server (LocalDB / Express locally, MonsterASP.NET‑hosted SQL Server in production) |
| Authentication | ASP.NET Core Identity (cookie auth, roles) with the default Identity UI (Razor Pages) |
| Views | Razor (`.cshtml`), Bootstrap 5, Bootstrap Icons, Font Awesome 6 |
| Client scripting | Vanilla JavaScript (no SPA framework), SweetAlert2, jQuery (validation only) |
| Caching / HTTP | `IMemoryCache`, `IHttpClientFactory` |

---

## Features

### Storefront

- **Home** – newest products, store statistics (product / category / customer counts) and
  approved customer testimonials.
- **Shop catalog** – category filter, keyword search, sorting (price asc/desc, rating,
  newest) and server‑side pagination (`PaginatedList<T>`).
- **Product details** – multi‑image gallery, category‑related products, customer reviews
  and a "write a review" form (rating 1–5 + comment, queued for admin approval).
- **Cart**
  - Guest cart persisted in `localStorage` (`cart-store.js`).
  - Authenticated cart persisted in the database.
  - Guest cart is **merged** into the user's database cart after login
    (`/Identity/Account/PostLoginSync` → `CartController.MergeGuestCart`).
  - Mini off‑canvas cart in the site header.
- **Wishlist** – same guest / user / merge model as the cart, with heart‑icon sync across
  listing pages.
- **Checkout** – address entry with a **country → city** picker sourced from the
  [countriesnow.space](https://countriesnow.space) API (cached for 24h). Stock is
  re‑validated and adjusted before payment, and the order is written inside a database
  transaction with atomic stock decrement.
- **Orders** – per‑user order history and a printable **invoice** view.
- **Testimonials** – submitted from the Contact page (login required), queued for approval.

### Admin portal (`/Admin`, requires the `Admin` role)

- **Dashboard** – aggregate counts and recent activity.
- **Products** – create / edit / soft‑delete, multi‑image upload with "main image"
  selection, per‑image delete (removes the physical file from `wwwroot`).
- **Categories** – create / edit / soft‑delete; deleting a category soft‑deletes its
  products.
- **Orders** – list, detail and status changes (`Processing` / `Completed` / `Cancelled`).
  Cancelling an order **restocks** its items; un‑cancelling deducts them again.
- **Reviews & testimonials** – moderation queue (`Pending` / `Accepted` / `Rejected`).
- **Users** – paginated user listing.

### Cross‑cutting

- Soft deletes (`IsDeleted`) on `Product` and `Category`; storefront queries always
  filter them out.
- Custom `404` handling (`/Home/Error404`) via `UseStatusCodePagesWithReExecute` and
  `UseExceptionHandler`.
- Antiforgery tokens on all state‑changing requests (`RequestVerificationToken` header
  for AJAX calls).
- Session state enabled (used for pending‑testimonial hand‑off across login).

---

## Domain model

```
ApplicationUser (Identity, + FullName)
Category 1───* Product 1───* ProductImage
Product  *───* Cart      (via CartItem)      — one Cart per user
Product  *───* Wishlist  (via WishlistItem)  — one Wishlist per user
Product  1───* Review        (moderated)
Order    1───* OrderItem
Order    1───* Payment
Order    *───1 Address
User     1───* Address / Order / Review / Testimonial
```

Enums (`Models/Enums.cs`): `OrderStatus { Processing, Completed, Cancelled }`,
`AdminResponse { Pending, Accepted, Rejected }`.

---

## Project structure

```
CampTravelGear.csproj          Project file (net10.0, assembly "CampTravelGear")
Program.cs                     Composition root & middleware pipeline
appsettings.json               Base config (local dev connection string placeholder)
appsettings.Production.json    Production connection string — GIT-IGNORED, not in the repo

Controllers/                   Storefront MVC controllers
  HomeController, ShopController, CartController, WishlistController,
  OrderController, CountryController (API: /api/Country/GetCountries)

Areas/
  Admin/                       Admin portal (Area "Admin", [Authorize(Roles="Admin")])
    Controllers/               BaseAdminController + Dashboard/Products/Categories/
                               Orders/Reviews/Users
    Views/
  Identity/                    Scaffolded ASP.NET Core Identity UI (Razor Pages)
    Pages/Account/             Login, Register, Logout, PostLoginSync (guest-cart merge)…

Models/                        EF entities + ViewModels/
DTOs/                          Cart / Checkout / external-API DTOs
Data/
  ApplicationDbContext.cs      DbContext + Fluent config + full data seed
  ApplicationUser.cs
  Migrations/                  Single migration: 20260827072103_Seed
Helpers/PaginatedList.cs       Generic pagination helper
Views/                         Storefront Razor views + _Layout / _AuthLayout
wwwroot/
  css/  js/                    Global assets + per-page files (css/pages/*, js/pages/*)
  images/                      Static imagery + images/Products/<id>/ upload folders
  lib/                         Bootstrap, jQuery, jQuery-validation (client libs)
```

**Asset convention:** each view opts into its own stylesheet/script via the `Styles` /
`Scripts` Razor sections, e.g. `~/css/pages/shop.css` and `~/js/pages/shop.js`. Shared
behaviour lives in `wwwroot/js/barrameru-global.js` and `wwwroot/js/cart-store.js`.

---

## Running locally

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server — LocalDB, SQL Server Express, or a full instance
- EF Core CLI tools: `dotnet tool install --global dotnet-ef`

### 1. Configure the database connection

The app reads `ConnectionStrings:DefaultConnection`. Prefer **user secrets** (the project
already declares a `UserSecretsId`) so credentials never land in source control:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" \
  "Server=(localdb)\\MSSQLLocalDB;Database=RahhalaDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
```

Alternatively edit `appsettings.json` (do **not** commit real credentials).

### 2. Create the database

Migrations are **not** applied automatically at startup:

```bash
dotnet ef database update
```

This creates the schema and seeds categories, products + images, roles, users, addresses
and a large set of historical orders.

### 3. Run

```bash
dotnet run
```

Default URLs (`Properties/launchSettings.json`): `https://localhost:7043` and
`http://localhost:5298`.

### Seeded accounts

| Role | Email | Password |
|---|---|---|
| Admin | `admin@barrameru.com` | `Admin@123!` |
| Customer | `john.doe@example.com` | `Customer@123!` |
| Customer | `sara.helvey@example.com` | `Customer@123!` |
| Customer | `alex.h@example.com` | `Customer@123!` |
| Customer | `omar.khalil@example.com` | `Customer@123!` |

> Identity is configured for convenience in this project: `RequireConfirmedAccount = false`
> and relaxed password rules (min length 6, no digit/uppercase/symbol requirement). Tighten
> these in `Program.cs` for any real deployment.

---

## Configuration reference

| Key | Purpose |
|---|---|
| `ConnectionStrings:DefaultConnection` | SQL Server connection used by `ApplicationDbContext` |
| `ASPNETCORE_ENVIRONMENT` | `Development` enables the EF migrations endpoint & developer exception page; anything else uses the custom error handler + HSTS |

`appsettings.Production.json` carries the production connection string and is **git‑ignored**
— it must be supplied on the host (it is present in the deployed environment only).

---

## Deployment

The live site runs on **MonsterASP.NET** (`*.runasp.net`) against a hosted SQL Server
database. Publishing is done with the Visual Studio publish profiles under
`Properties/PublishProfiles/` (git‑ignored). A typical CLI publish:

```bash
dotnet publish -c Release
```

After deploying new model changes, run `dotnet ef database update` against the production
connection (or generate and apply an idempotent SQL script:
`dotnet ef migrations script --idempotent`).

---

## Notes & known gaps

- `Payment` is modelled and included in queries, but no payment gateway is integrated —
  checkout creates the order without a `Payment` record.
- `Review`/`Testimonial` submission does not currently enforce "verified purchase".
- Product‑image uploads are written directly to `wwwroot/images/Products/<id>/`; on a
  multi‑instance host use shared/object storage instead.
- Some legacy brand assets (`Logo_Barrameru*.png`) are still referenced by the layout
  despite the rename to *Rahhala*.
