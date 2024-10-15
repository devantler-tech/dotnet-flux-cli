namespace Devantler.FluxCLI.Tests.FluxTests;

/// <summary>
/// Tests for the <see cref="Flux.CreateKustomizationAsync(string, string, string, string, string, string, string[], bool, bool, CancellationToken)" /> method.
/// </summary>
[Collection("Flux")]
public class CreateKustomizationAsyncTests
{
  /// <summary>
  /// Test to verify that the method throws an <see cref="InvalidOperationException" /> when no cluster is configured.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task CreateKustomizationAsync_ThrowsInvalidOperationException_WhenNoClusterIsConfigured()
  {
    // Act
    var exception = await Record.ExceptionAsync(async () => await Flux.CreateKustomizationAsync("test", "test", "test").ConfigureAwait(false));

    // Assert
    _ = Assert.IsType<InvalidOperationException>(exception);
  }
}
