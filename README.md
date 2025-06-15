
# 📦 ReqRes Integration Service (.NET 8)

This project demonstrates a modular and testable service component in **.NET 8**, designed to fetch and cache user data from the [ReqRes public API](https://reqres.in/). The solution simulates interaction with external systems while applying clean coding principles and resilience strategies.

---

## 🛠️ Technologies

- [.NET 8](https://dotnet.microsoft.com/en-us/)
- `HttpClientFactory`
- `IOptions<T>` for configuration
- `Moq` & `xUnit` for unit testing


---

## 📁 Project Structure

```
/SolutionRoot
│
├── ConsoleApp/                → Main app that calls the service
├── ServiceLibrary/           → Class library with the core logic
│   ├── Clients/              → HttpClient wrapper (ReqResClient)
│   ├── Models/               → POCOs for user data
│   ├── Services/             → ExternalUserService logic
│   └── Config/               → AppSettings.cs
│
├── ServiceLibrary.Test/      → xUnit test project with Moq
└── README.md                 → This file
```

---

## 🧪 Running the Tests

1. **Restore packages**:
   ```bash
   dotnet restore
   ```

2. **Build the solution**:
   ```bash
   dotnet build
   ```

3. **Run all unit tests**:
   ```bash
   dotnet test
   ```

---

## ▶️ Running the Console App

1. Navigate to the `ConsoleApp` project directory.

2. Run the app:
   ```bash
   dotnet run
   ```

---

## 🔧 Configuration (`AppSettings.cs`)

```csharp
public class AppSettings
{
    public string BaseUrl { get; set; }
    public int PageSize { get; set; }
}
```

The `AppSettings` are injected into `ExternalUserService` using the `IOptions<T>` pattern and can be customized easily.

---

## ✅ Features & Design Decisions

Implemented in `Program.cs` during `HttpClient` registration:


### 🧱 Clean Architecture
- Interfaces and POCOs live in the shared library.
- No business logic in the console app.
- All dependencies injected via DI for testability.

### 🧪 Unit Testing
- `xUnit` and `Moq` used for testing the `ExternalUserService`.
- `IOptions<AppSettings>` are mocked appropriately.

---

## 🚫 Error Handling

The service handles:
- `HttpRequestException` for network/API failures
- `JsonException` for invalid responses
- Custom `ApiRequestException` and `UserNotFoundException`

These are logged and either bubbled or wrapped depending on severity.

---

## 📝 Future Enhancements

- Add paging support in the console UI.
- Add integration tests against the real API.
