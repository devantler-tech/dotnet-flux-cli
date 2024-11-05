namespace Devantler.FluxCLI.Tests.FluxTests;

/// <summary>
/// Tests for the <see cref="Flux.ReconcileKustomizationAsync(string, string, string, bool, CancellationToken)"/> method.
/// </summary>
[Collection("Flux")]
public class ReconcileKustomizationAsyncTests
{
  /// <summary>
  /// Test to verify that the method throws an <see cref="InvalidOperationException" /> when no cluster is configured.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task ReconcileKustomizationAsync_ThrowsInvalidOperationException_WhenNoClusterIsConfigured()
  {
    // Act
    var exception = await Record.ExceptionAsync(async () => await Flux.ReconcileKustomizationAsync("test").ConfigureAwait(false));

    // Assert
    _ = Assert.IsType<FluxException>(exception);
  }
}
