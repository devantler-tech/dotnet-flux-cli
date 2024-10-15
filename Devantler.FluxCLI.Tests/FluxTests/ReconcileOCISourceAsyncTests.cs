namespace Devantler.FluxCLI.Tests.FluxTests;

/// <summary>
/// Tests for the <see cref="Flux.ReconcileOCISourceAsync(string, string, CancellationToken)"/> method.
/// </summary>
[Collection("Flux")]
public class ReconcileOCISourceAsyncTests
{
  /// <summary>
  /// Test to verify that the method throws an <see cref="InvalidOperationException" /> when no cluster is configured.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task ReconcileAsync_ThrowsInvalidOperationException_WhenNoClusterIsConfigured()
  {
    // Act
    var exception = await Record.ExceptionAsync(async () => await Flux.ReconcileOCISourceAsync("test").ConfigureAwait(false));

    // Assert
    _ = Assert.IsType<InvalidOperationException>(exception);
  }
}
