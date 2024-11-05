# Playwright Automation Framework

This repository contains an automated testing framework using [Microsoft Playwright](https://playwright.dev/dotnet/) with [NUnit](https://nunit.org/) for automated web testing. This framework is designed to handle web-based test cases, including functional and accessibility tests.

## Prerequisites

Before you begin, ensure you have the following installed:

- **.NET SDK**: Download and install the [.NET SDK](https://dotnet.microsoft.com/download).
- **Node.js and npm**: Required for accessibility tests. Download from [Node.js](https://nodejs.org/).

### Required NuGet Packages

Install the following NuGet packages via the Package Manager Console:

```sh
Install-Package Microsoft.Playwright
Install-Package NUnit
Install-Package NUnit3TestAdapter
```

## Project Setup
1. Clone the Repository
2. Install Playwright Browsers:
```sh 
dotnet tool install --global Microsoft.Playwright.CLI
playwright install
```
3. Install Node.js Dependencies 
```sh 
npm install axe-core
```

## Project Structure
1. appsettings.json: usercreditials, browser and headless mode configurations
2. TestBase: Browser intializtion
3. Utilities: Contains helper classes and reusable utilities.
4. Pages: Contains Page Object classes representing UI elements and actions.
5. Tests: Contains all test classes

## Running Test
1. Run with Visual Studio Test Explorer: Open the project in Visual Studio and run tests using the Test Explorer. NUnit will automatically handle test execution.


