# SpellOutNumber API

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/1187a31303f94b959654b5ec7e57e1af)](https://app.codacy.com?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)

A simple Azure Functions API that converts numbers into their spelled-out text representation in multiple languages.

## Features

- Convert numbers to written text (e.g., `42` → `"Forty two"`)
- Support for multiple cultures (English, Italian)
- RESTful API built with Azure Functions
- Lightweight and fast

## Quick Start

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Azure Functions Core Tools](https://docs.microsoft.com/azure/azure-functions/functions-run-local) (for local development)

### Running Locally

1. Clone the repository:
```bash
git clone <repository-url>
cd SpellOutNumberAPI
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Run the function locally:
```bash
cd SpellOutNumberAPI
func start
```

The API will be available at `http://localhost:7071`

### Running Tests

```bash
dotnet test
```

## API Usage

### Endpoint

```
GET /api/spellout/{number}?culture={culture}
```

### Parameters

- `number` (required, route parameter): The integer number to spell out
- `culture` (optional, query parameter): The culture/language to use. Defaults to `en-US`

### Supported Cultures

- English: `en`, `en-US`, `en-GB`, `en-UK`, `english`
- Italian: `it`, `it-IT`, `italian`, `italiano`

## Examples

### Localhost

**Basic request (English, default):**
```bash
curl http://localhost:7071/api/spellout/42
```
Response: `Forty two`

**With culture parameter:**
```bash
curl http://localhost:7071/api/spellout/123?culture=en-US
```
Response: `One hundred twenty three`

**Italian culture:**
```bash
curl http://localhost:7071/api/spellout/13?culture=it
```
Response: `Tredici`

**Large number:**
```bash
curl http://localhost:7071/api/spellout/5555?culture=english
```
Response: `Five thousand five hundred fifty five`

### Public Hosted Function

**Basic request (English, default):**
```bash
curl https://spelloutnumber.azurewebsites.net/api/spellout/42
```
Response: `Forty two`

**With culture parameter:**
```bash
curl https://spelloutnumber.azurewebsites.net/api/spellout/123?culture=en-US
```
Response: `One hundred twenty three`

**Italian culture:**
```bash
curl https://spelloutnumber.azurewebsites.net/api/spellout/13?culture=it
```
Response: `Tredici`

**Browser examples:**
- https://spelloutnumber.azurewebsites.net/api/spellout/42
- https://spelloutnumber.azurewebsites.net/api/spellout/100?culture=english
- https://spelloutnumber.azurewebsites.net/api/spellout/999?culture=it

### Error Handling

**Negative numbers:**
```bash
curl http://localhost:7071/api/spellout/-1
```
Response: `400 Bad Request - "Number must be positive!"`

**Unsupported culture:**
```bash
curl http://localhost:7071/api/spellout/42?culture=es
```
Response: `400 Bad Request - "Culture 'es' is not supported"`

## Project Structure

```
SpellOutNumberAPI/
├── Business/
│   ├── Culture/           # Localization services
│   ├── Spelling/          # Spelling logic
│   ├── ISpellOutService.cs
│   └── SpellOutService.cs
├── Repo/                  # Language-specific repositories
├── SpellOut.cs            # Azure Function endpoint
└── Program.cs             # DI configuration

SpellOutNumberAPI.Tests/   # NUnit tests
```

## License

See LICENSE file for details.
