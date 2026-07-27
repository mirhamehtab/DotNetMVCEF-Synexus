## Week 2 — Authentication

Added a full login/register/logout flow using ASP.NET Core Identity.

- Register + Login screens with validation
- Passwords hashed via `UserManager` (never stored in plain text)
- Cookie-based session, persists across refresh (14 days, sliding expiration)
- `[Authorize]` on `ProductController` and `APIController` — anonymous users get redirected (MVC) or a 401 (API)
- Logout clears the session

### Test account
Email:    test@synexus.com
Password: Test@123

### Why cookies, not JWT

This is server-rendered MVC, not a separate SPA + API, so ASP.NET Identity's cookie auth was used instead of JWT — the browser handles the cookie automatically, and it can be marked HttpOnly so JS can't read it (avoids the XSS risk of storing a JWT in localStorage). JWT makes more sense when the frontend is a separate SPA with no shared session.


## Week 3 — Filtering, Sorting & Pagination

Added a Category to products (basic one-to-many — a category can have many products), and turned the product list into something that filters/sorts/paginates without reloading the page.

### What I built

- New `Category` entity, linked to `Product` via a nullable `CategoryId` (nullable so existing products don't break when the column gets added)
- A new API endpoint — `GET /api/api/products?page=&pageSize=&search=&sortBy=&sortDir=&categoryId=` — that builds the query with EF Core's `IQueryable`, so filtering/sorting/pagination all happen in a single SQL query, not in memory after fetching everything
- The Product Index page now has a search box (debounced — waits 400ms after you stop typing before calling the API, so it's not firing on every keystroke) and a sort dropdown
- The URL updates as you search/sort/page (`?search=phone&sortBy=Price&sortDir=desc&page=2`) using `history.pushState`, so refreshing the page keeps your filters instead of resetting them
- Table rows are rendered client-side from the API response — no full page reload when you search, sort, or change pages

### Query examples

GET /api/api/products?page=1&pageSize=5
GET /api/api/products?search=phone
GET /api/api/products?sortBy=Price&sortDir=desc
GET /api/api/products?page=2&pageSize=5&search=pan&sortBy=Name&sortDir=asc

