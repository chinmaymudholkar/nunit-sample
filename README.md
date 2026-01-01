# Swag Labs Test Harness

A robust test automation framework for [Swag Labs](https://www.saucedemo.com/) using **Playwright**, **NUnit**, and **FluentAssertion**.

## Features
- **Standard Architecture**: Uses the Page Object Model (POM) pattern.
- **Cross-Browser**: Supports Chrome, Firefox, and Edge via Playwright.
- **Parallel Execution**: Tests run in parallel using `[Parallelizable(ParallelScope.All)]` and `[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]`.
- **Environment Configuration**: Credentials and settings managed via `.env` file.
- **Fluent Assertions**: Expressive and readable test assertions.

## Prerequisites
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
- PowerShell (for Playwright installation script) or Playwright CLI (if using Linux or macOS)
- [Node.js](https://nodejs.org/en/) (for Allure Reporting).

## Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/chinmaymudholkar/nunit-sample.git
   cd nunit-sample
   ```
2. **Restore dependencies**
   ```bash
   dotnet restore
   ```
3. **Install Playwright Browsers**
   ```bash
   # Build the project first to copy playwright scripts
   dotnet build
   
   # Install browsers
   playwright install
   # OR if using the build output script:
   pwsh bin/Debug/net10.0/playwright.ps1 install
   ```

## Configuration

The project uses a `.env` file for configuration.

1. Copy the example file:
   ```bash
   cp .env.example .env
   ```
2. Edit `.env` to configure your run:
   ```ini
   SWAG_LABS_USERNAME=standard_user
   SWAG_LABS_PASSWORD=secret_sauce
   BROWSER=firefox          # chrome, firefox, edge, webkit
   HEADLESS=true            # true or false
   SLOW_MO=0                # ms delay between actions
   ```

## Running Tests

Run all tests:
```bash
dotnet test
```

Run tests for a specific browser (overriding .env):
```bash
export BROWSER=chrome
dotnet test
```

Run specific tests:
```bash
dotnet test --filter Name~Login_ValidCredentials
```

## Reporting

The project uses [Allure Reporting](https://allurereport.org/) for beautiful test execution reports.

### Viewing Reports

After running tests (`dotnet test`), results are generated in `bin/Debug/net10.0/allure-results`.

To serve the report immediately:

Required: `allure` CLI or `npm`.

**Option 1: Using Allure CLI (if installed)**
```bash
allure serve bin/Debug/net10.0/allure-results
```

**Option 2: Using npx (no installation required)**
```bash
npx -y allure-commandline serve bin/Debug/net10.0/allure-results
```

### Features
- **Screenshots**: Automatically captures and attaches screenshots to the report when a test fails.
- **History**: Keeps track of test execution history (if report is generated).

Reports are also published to this repo's [Github Actions](https://chinmaymudholkar.github.io/nunit-sample/gh-pages/):


## Project Structure

- **Libraries/**: Core infrastructure.
  - `BrowserFactory.cs`: Handles Playwright interactions.
  - `TestConfig.cs`: Configuration management.
- **Pages/**: Page Object Models.
  - `LoginPage.cs`, `InventoryPage.cs`.
- **Tests/**: NUnit Test classes.
  - `BaseTest.cs`: Setup/Teardown lifecycle.
  - `LoginTests.cs`: Test scenarios.
