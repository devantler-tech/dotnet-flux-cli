using Devantler.ContainerEngineProvisioner.Docker;

namespace Devantler.FluxCLI.Tests.FluxTests;

/// <summary>
/// Tests for the <see cref="Flux.PushArtifactAsync(Uri, string, string, string, CancellationToken)"/> method.
/// </summary>
[Collection("Flux")]
public class PushArtifactAsyncTests
{
  /// <summary>
  /// Test to verify that the method pushes an artifact when the registry is configured.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task PushArtifactAsync_PushesArtifact_WhenRegistryIsConfigured()
  {
    // Arrange
    var cancellationToken = new CancellationToken();
    var dockerProvisioner = new DockerProvisioner();

    // Act
    await dockerProvisioner.CreateRegistryAsync("ksail-registry", 5555, cancellationToken: cancellationToken);
    string testFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
    await File.WriteAllTextAsync(testFile, "test");
    var exception = await Record.ExceptionAsync(async () => await Flux.PushArtifactAsync(new Uri("oci://localhost:5555/test-artifact"), testFile, cancellationToken: cancellationToken).ConfigureAwait(false));

    // Assert
    Assert.Null(exception);

    // Cleanup
    File.Delete(testFile);
    await dockerProvisioner.DeleteRegistryAsync("ksail-registry", cancellationToken);
  }

  /// <summary>
  /// Test to verify that the method throws an <see cref="InvalidOperationException" /> when no registry is configured.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task PushArtifactAsync_ThrowsInvalidOperationException_WhenNoRegistryIsConfigured()
  {
    // Act
    var exception = await Record.ExceptionAsync(async () => await Flux.PushArtifactAsync(new Uri("oci://localhost:5555/test-artifact"), "./Devantler.FluxCLI.Tests/assets", cancellationToken: new CancellationToken()).ConfigureAwait(false));

    // Assert
    _ = Assert.IsType<InvalidOperationException>(exception);
  }
}
