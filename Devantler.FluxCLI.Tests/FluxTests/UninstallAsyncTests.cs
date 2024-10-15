namespace Devantler.FluxCLI.Tests.FluxTests;

/// <summary>
/// Tests for the <see cref="Flux.UninstallAsync(string, CancellationToken)"/> method.
/// </summary>
[Collection("Flux")]
public class UninstallAsyncTests
{
  /// <summary>
  /// Test to verify that the method throws an <see cref="InvalidOperationException" /> when no cluster is configured.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task UninstallAsync_ThrowsInvalidOperationException_WhenNoClusterIsConfigured()
  {
    // Act
    var exception = await Record.ExceptionAsync(async () => await Flux.UninstallAsync().ConfigureAwait(false));

    // Assert
    _ = Assert.IsType<InvalidOperationException>(exception);
  }
}
