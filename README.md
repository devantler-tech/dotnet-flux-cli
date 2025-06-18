# 🔁 .NET Flux CLI

[![License](https://img.shields.io/badge/License-Apache_2.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
[![Test](https://github.com/devantler-tech/dotnet-flux-cli/actions/workflows/test.yaml/badge.svg)](https://github.com/devantler-tech/dotnet-flux-cli/actions/workflows/test.yaml)
[![codecov](https://codecov.io/gh/devantler-tech/dotnet-flux-cli/graph/badge.svg?token=RhQPb4fE7z)](https://codecov.io/gh/devantler-tech/dotnet-flux-cli)

A simple .NET library that embeds the Flux CLI.

## 🚀 Getting Started

### Prerequisites

- .NET 9.0 or later
- [Flux CLI](https://fluxcd.io/docs/installation/) installed and available in your system's PATH

### Installation

To get started, you can install the package from NuGet.

```bash
dotnet add package DevantlerTech.FluxCLI
```

## 📝 Usage

You can execute the Flux CLI commands using the `Flux` class.

```csharp
using DevantlerTech.FluxCLi;

var (exitCode, output) = await Flux.RunAsync(["arg1", "arg2"]);
```
