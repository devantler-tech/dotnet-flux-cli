﻿using System.Runtime.InteropServices;
using CliWrap;

using Devantler.CLIRunner;

namespace Devantler.FluxCLI;

/// <summary>
/// A class to run flux CLI commands.
/// </summary>
public static class Flux
{
  /// <summary>
  /// The flux CLI command.
  /// </summary>
  static Command Command => GetCommand();
  internal static Command GetCommand(PlatformID? platformID = default, Architecture? architecture = default, string? runtimeIdentifier = default)
  {
    platformID ??= Environment.OSVersion.Platform;
    architecture ??= RuntimeInformation.ProcessArchitecture;
    runtimeIdentifier ??= RuntimeInformation.RuntimeIdentifier;

    string binary = (platformID, architecture, runtimeIdentifier) switch
    {
      (PlatformID.Unix, Architecture.X64, "osx-x64") => "flux-osx-x64",
      (PlatformID.Unix, Architecture.Arm64, "osx-arm64") => "flux-osx-arm64",
      (PlatformID.Unix, Architecture.X64, "linux-x64") => "flux-linux-x64",
      (PlatformID.Unix, Architecture.Arm64, "linux-arm64") => "flux-linux-arm64",
      (PlatformID.Win32NT, Architecture.X64, "win-x64") => "flux-win-x64.exe",
      (PlatformID.Win32NT, Architecture.Arm64, "win-arm64") => "flux-win-arm64.exe",
      _ => throw new PlatformNotSupportedException($"Unsupported platform: {Environment.OSVersion.Platform} {RuntimeInformation.ProcessArchitecture}"),
    };
    string binaryPath = Path.Combine(AppContext.BaseDirectory, binary);
    return !File.Exists(binaryPath) ?
      throw new FileNotFoundException($"{binaryPath} not found.") :
      Cli.Wrap(binaryPath);
  }

  /// <summary>
  /// Runs the flux CLI command with the given arguments.
  /// </summary>
  /// <param name="arguments"></param>
  /// <param name="validation"></param>
  /// <param name="silent"></param>
  /// <param name="includeStdErr"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  public static async Task<(int ExitCode, string Message)> RunAsync(
    string[] arguments,
    CommandResultValidation validation = CommandResultValidation.ZeroExitCode,
    bool silent = false,
    bool includeStdErr = true,
    CancellationToken cancellationToken = default)
  {
    return await CLI.RunAsync(
      Command.WithArguments(arguments),
      validation: validation,
      silent: silent,
      includeStdErr: includeStdErr,
      cancellationToken: cancellationToken).ConfigureAwait(false);
  }
}
