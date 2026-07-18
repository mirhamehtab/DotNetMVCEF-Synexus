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
