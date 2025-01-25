# 🔁 .NET Flux CLI

[![License](https://img.shields.io/badge/License-Apache_2.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
[![Test](https://github.com/devantler/dotnet-flux-cli/actions/workflows/test.yaml/badge.svg)](https://github.com/devantler/dotnet-flux-cli/actions/workflows/test.yaml)
[![codecov](https://codecov.io/gh/devantler/dotnet-flux-cli/graph/badge.svg?token=RhQPb4fE7z)](https://codecov.io/gh/devantler/dotnet-flux-cli)

<details>
  <summary>Show/hide folder structure</summary>

<!-- readme-tree start -->
```
.
├── .github
│   ├── scripts
│   └── workflows
├── Devantler.FluxCLI
│   └── runtimes
│       ├── linux-arm64
│       │   └── native
│       ├── linux-x64
│       │   └── native
│       ├── osx-arm64
│       │   └── native
│       ├── osx-x64
│       │   └── native
│       ├── win-arm64
│       │   └── native
│       └── win-x64
│           └── native
└── Devantler.FluxCLI.Tests
    └── FluxTests

20 directories
```
<!-- readme-tree end -->

</details>

A simple .NET library that embeds the Flux CLI.

## 🚀 Getting Started

To get started, you can install the package from NuGet.

```bash
dotnet add package Devantler.FluxCLI
```

## 📝 Usage

You can execute the Flux CLI commands using the `Flux` class.

```csharp
using Devantler.FluxCLi;

var (exitCode, output) = await Flux.RunAsync(["arg1", "arg2"]);
```
