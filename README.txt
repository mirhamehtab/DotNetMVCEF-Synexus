DotNetMVCEF — Week 2: Full-Stack Authentication & Protected Interfaces

ASP.NET Core MVC + EF Core Code-First app with a complete login/register/logout flow
built on ASP.NET Core Identity.

Setup


Open DotNetMVCEF.sln in Visual Studio 2022.
Restore NuGet packages (auto on build, or right-click project > Restore Packages).
Confirm the connection string in appsettings.json matches your local SQL Server instance.
Open Package Manager Console and run:


   Update-Database

This applies all migrations, including the Identity tables (AspNetUsers, AspNetRoles, etc).
5. Press F5 to run.

Sample test account:

test@synexus.com
test123