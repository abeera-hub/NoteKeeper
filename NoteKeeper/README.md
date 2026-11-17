# NoteKeeper (.NET 9)

A simple ASP.NET Core MVC application with three main pages/features:

- Home
- Sign up
- Login and Logout

The application uses ASP.NET Core Identity for account creation, login, logout,
password hashing, and authentication cookies. Entity Framework Core stores Identity
data in SQL Server LocalDB.

## Requirements

- .NET 9 SDK
- Visual Studio 2022 (17.12+) or VS Code
- SQL Server LocalDB

## Run the project

1. Open the `NoteKeeper` folder.
2. Restore packages:

   ```bash
   dotnet restore
   ```

3. Configure the database connection in `appsettings.json` if required.
4. Create and apply the Identity database migration if this is a fresh database:

   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

   If the database already has an existing migration history, inspect it before
   creating another migration. Do not create a duplicate `InitialCreate` migration.

5. Run the application:

   ```bash
   dotnet run
   ```

6. Open the localhost URL shown in the terminal and create an account.

## Project structure

```text
NoteKeeper/
├── Controllers/
│   ├── AccountController.cs
│   └── HomeController.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Models/
│   └── ApplicationUser.cs
├── ViewModels/
│   ├── LoginViewModel.cs
│   └── RegisterViewModel.cs
├── Views/
│   ├── Account/
│   ├── Home/
│   └── Shared/
├── wwwroot/
├── Program.cs
├── appsettings.json
└── NoteKeeper.csproj
```
