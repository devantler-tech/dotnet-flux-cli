namespace Devantler.FluxCLI.Tests.FluxTests;

/// <summary>
/// Tests for the <see cref="Flux.CreateOCISourceAsync(string, Uri, string, string, string, string, CancellationToken)"/> method.
/// </summary>
[Collection("Flux")]
public class CreateOCISourceAsyncTests
{

  /// <summary>
  /// Test to verify that the method throws an <see cref="InvalidOperationException" /> when no cluster is configured.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task CreateOCISourceAsync_ThrowsInvalidOperationException_WhenNoClusterIsConfigured()
  {
    // Act
    var exception = await Record.ExceptionAsync(async () => await Flux.CreateOCISourceAsync("test", new Uri("http://test")).ConfigureAwait(false));

    // Assert
    _ = Assert.IsType<FluxException>(exception);
  }
}
