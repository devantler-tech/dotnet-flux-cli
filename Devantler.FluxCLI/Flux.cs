using System.Globalization;
using System.Runtime.InteropServices;
using IdentityModel;
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
  /// Installs flux.
  /// </summary>
  /// <param name="context"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="FluxException"></exception>
  public static async Task InstallAsync(string? context = default, CancellationToken cancellationToken = default)
  {
    var command = string.IsNullOrEmpty(context) ? Command.WithArguments(["install"]) :
      Command.WithArguments(["install", "--context", context]);
    var (exitCode, _) = await CLI.RunAsync(command, cancellationToken: cancellationToken).ConfigureAwait(false);
    if (exitCode != 0)
    {
      throw new FluxException($"Failed to install flux");
    }
  }

  /// <summary>
  /// Uninstalls flux.
  /// </summary>
  /// <param name="context"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="FluxException"></exception>
  public static async Task UninstallAsync(string? context = default, CancellationToken cancellationToken = default)
  {
    var command = string.IsNullOrEmpty(context) ?
      Command.WithArguments(["uninstall", "--silent"]) :
      Command.WithArguments(["uninstall", "--silent", "--context", context]);
    var (exitCode, message) = await CLI.RunAsync(command, cancellationToken: cancellationToken).ConfigureAwait(false);
    if (exitCode != 0 || message.Contains("connection refused", StringComparison.OrdinalIgnoreCase))
    {
      throw new FluxException($"Failed to uninstall flux");
    }
  }

  /// <summary>
  /// Creates a OCIRepository source.
  /// </summary>
  /// <param name="context"></param>
  /// <param name="name"></param>
  /// <param name="url"></param>
  /// <param name="insecure"></param>
  /// <param name="namespace"></param>
  /// <param name="tag"></param>
  /// <param name="interval"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="FluxException"></exception>
  public static async Task CreateOCISourceAsync(string name, Uri url, bool insecure = false, string? context = default, string @namespace = "flux-system", string tag = "latest", string interval = "10m", CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(url, nameof(url));
    var command = string.IsNullOrEmpty(context) ?
      Command.WithArguments(
        [
          "create", "source", "oci", name,
          "--url", url.ToString(),
          "--insecure", insecure.ToString(),
          "--tag", tag,
          "--interval", interval,
          "--namespace", @namespace
        ]
      ) :
      Command.WithArguments(
        [
          "create", "source", "oci", name,
          "--url", url.ToString(),
          "--insecure", insecure.ToString(),
          "--tag", tag,
          "--interval", interval,
          "--namespace", @namespace,
          "--context", context]
      );
    var (exitCode, _) = await CLI.RunAsync(command, cancellationToken: cancellationToken).ConfigureAwait(false);
    if (exitCode != 0)
    {
      throw new FluxException($"Failed to create OCI source");
    }
  }

  /// <summary>
  /// Creates a Kustomization.
  /// </summary>
  /// <param name="context"></param>
  /// <param name="name"></param>
  /// <param name="source"></param>
  /// <param name="path"></param>
  /// <param name="namespace"></param>
  /// <param name="interval"></param>
  /// <param name="dependsOn"></param>
  /// <param name="prune"></param>
  /// <param name="wait"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  public static async Task CreateKustomizationAsync(string name, string source, string path, string? context = default, string @namespace = "flux-system", string interval = "5m", string[]? dependsOn = default, bool prune = true, bool wait = true, CancellationToken cancellationToken = default)
  {
    var command = string.IsNullOrEmpty(context) ?
      Command.WithArguments(
        ["create", "kustomization", name, "--source", source, "--path", path, "--namespace", @namespace, "--target-namespace", @namespace, "--interval", interval, "--prune", prune.ToString(), "--wait", wait.ToString(), "--depends-on", dependsOn != null ? string.Join(",", dependsOn) : ""]
      ) :
      Command.WithArguments(
        ["create", "kustomization", name, "--source", source, "--path", path, "--namespace", @namespace, "--target-namespace", @namespace, "--interval", interval, "--prune", prune.ToString(), "--wait", wait.ToString(), "--depends-on", dependsOn != null ? string.Join(",", dependsOn) : "", "--context", context]
      );
    var (exitCode, _) = await CLI.RunAsync(command, cancellationToken: cancellationToken).ConfigureAwait(false);
    if (exitCode != 0)
    {
      throw new FluxException($"Failed to create Kustomization");
    }
  }

  /// <summary>
  /// Reconcile sources.
  /// </summary>
  /// <param name="name"></param>
  /// <param name="context"></param>
  /// <param name="namespace"></param>
  /// <param name="cancellationToken"></param>
  public static async Task ReconcileOCISourceAsync(string name, string? context = default, string @namespace = "flux-system", CancellationToken cancellationToken = default)
  {
    var command = string.IsNullOrEmpty(context) ?
      Command.WithArguments(
        ["reconcile", "source", "oci", name, "--namespace", @namespace]
      ) :
      Command.WithArguments(
        ["reconcile", "source", "oci", name, "--namespace", @namespace, "--context", context]
      );
    var (exitCode, _) = await CLI.RunAsync(command, cancellationToken: cancellationToken).ConfigureAwait(false);
    if (exitCode != 0)
    {
      throw new FluxException($"Failed to reconcile OCI source");
    }
  }

  /// <summary>
  /// Reconcile Kustomization.
  /// </summary>
  /// <param name="name"></param>
  /// <param name="context"></param>
  /// <param name="namespace"></param>
  /// <param name="withSource"></param>
  /// <param name="cancellationToken"></param>
  public static async Task ReconcileKustomizationAsync(string name, string? context = default, string @namespace = "flux-system", bool withSource = false, CancellationToken cancellationToken = default)
  {
    var command = string.IsNullOrEmpty(context) ?
      Command.WithArguments(
        ["reconcile", "kustomization", name, "--namespace", @namespace, "--with-source", withSource.ToString()]
      ) :
      Command.WithArguments(
        ["reconcile", "kustomization", name, "--namespace", @namespace, "--with-source", withSource.ToString(), "--context", context]
      );
    var (exitCode, _) = await CLI.RunAsync(command, cancellationToken: cancellationToken).ConfigureAwait(false);
    if (exitCode != 0)
    {
      throw new FluxException($"Failed to reconcile Kustomization");
    }
  }

  /// <summary>
  /// Reconcile HelmRelease.
  /// </summary>
  /// <param name="name"></param>
  /// <param name="context"></param>
  /// <param name="namespace"></param>
  /// <param name="withSource"></param>
  /// <param name="force"></param>
  /// <param name="reset"></param>
  /// <param name="cancellationToken"></param>
  public static async Task ReconcileHelmReleaseAsync(string name, string? context = default, string @namespace = "flux-system", bool withSource = false, bool force = false, bool reset = false, CancellationToken cancellationToken = default)
  {
    var command = string.IsNullOrEmpty(context) ?
      Command.WithArguments(
        ["reconcile", "helmrelease", name, "--namespace", @namespace, "--with-source", withSource.ToString(), "--force", force.ToString(), "--reset", reset.ToString()]
      ) :
      Command.WithArguments(
        ["reconcile", "helmrelease", name, "--namespace", @namespace, "--with-source", withSource.ToString(), "--force", force.ToString(), "--reset", reset.ToString(), "--context", context]
      );
    var (exitCode, _) = await CLI.RunAsync(command, cancellationToken: cancellationToken).ConfigureAwait(false);
    if (exitCode != 0)
    {
      throw new FluxException($"Failed to reconcile HelmRelease");
    }
  }

  /// <summary>
  /// Push an artifact to an OCI registry.
  /// </summary>
  /// <param name="registry"></param>
  /// <param name="path"></param>
  /// <param name="source"></param>
  /// <param name="revision"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  public static async Task PushArtifactAsync(Uri registry, string path, string? source = default, string? revision = default, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(registry, nameof(registry));

    long currentTimeEpoch = DateTime.Now.ToEpochTime();

    if (string.IsNullOrEmpty(source))
    {
      source = registry.ToString();
    }

    if (string.IsNullOrEmpty(revision))
    {
      revision = currentTimeEpoch.ToString(CultureInfo.InvariantCulture);
    }

    var pushCommand = Command.WithArguments(
      [
        "push",
        "artifact",
        $"{registry}:{revision}",
        "--path", path,
        "--source", source,
        "--revision", revision
      ]
    );
    var (exitCode, _) = await CLI.RunAsync(pushCommand, cancellationToken: cancellationToken).ConfigureAwait(false);
    if (exitCode != 0)
    {
      throw new FluxException($"Failed to push artifact");
    }

    var tagCommand = Command.WithArguments(
      [
        "tag",
        "artifact",
        $"{registry}:{revision}",
        "--tag", "latest"]
    );
    (exitCode, _) = await CLI.RunAsync(tagCommand, cancellationToken: cancellationToken).ConfigureAwait(false);
    if (exitCode != 0)
    {
      throw new FluxException($"Failed to tag artifact");
    }
  }
}
