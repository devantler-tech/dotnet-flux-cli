namespace Devantler.FluxCLI.Tests.FluxTests;

/// <summary>
/// Tests for the <see cref="Flux.ReconcileHelmReleaseAsync(string, string, string, bool, bool, bool, CancellationToken)"/> method.
/// </summary>
[Collection("Flux")]
public class ReconcileHelmReleaseAsyncTests
{
  /// <summary>
  /// Test to verify that the method throws an <see cref="InvalidOperationException" /> when no cluster is configured.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task ReconcileHelmReleaseAsync_ThrowsInvalidOperationException_WhenNoClusterIsConfigured()
  {
    // Act
    var exception = await Record.ExceptionAsync(async () => await Flux.ReconcileHelmReleaseAsync("test").ConfigureAwait(false));

    // Assert
    _ = Assert.IsType<FluxException>(exception);
  }
}
