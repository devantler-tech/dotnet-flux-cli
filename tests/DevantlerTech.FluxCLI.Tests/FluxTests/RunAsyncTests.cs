using CliWrap;

namespace DevantlerTech.FluxCLI.Tests.FluxTests;

/// <summary>
/// Tests for the <see cref="Flux.RunAsync(string[], CommandResultValidation, bool, bool, CancellationToken)" /> method.
/// </summary>
public class RunAsyncTests
{
  /// <summary>
  /// Tests that the binary can return the version of the flux CLI command.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task RunAsync_Version_ReturnsVersion()
  {
    // Act
    var (exitCode, output) = await Flux.RunAsync(["--version"]);

    // Assert
    Assert.Equal(0, exitCode);
    Assert.Matches(@"^flux version \d+\.\d+\.\d+$", output.Trim());
  }
}
