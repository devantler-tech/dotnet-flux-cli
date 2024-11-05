namespace Devantler.FluxCLI.Tests.FluxTests;

/// <summary>
/// Tests for the <see cref="Flux.InstallAsync(string, CancellationToken)"/> method.
/// </summary>
[Collection("Flux")]
public class InstallAsyncTests
{
  /// <summary>
  /// Test to verify that the method throws an <see cref="InvalidOperationException" /> when no cluster is configured.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task InstallAsync_ThrowsInvalidOperationException_WhenNoClusterIsConfigured()
  {
    // Act
    var exception = await Record.ExceptionAsync(async () => await Flux.InstallAsync().ConfigureAwait(false));

    // Assert
    _ = Assert.IsType<FluxException>(exception);
  }
}
