namespace Devantler.FluxCLI;

/// <summary>
/// A Flux resource.
/// </summary>
public enum FluxResource
{
  /// <summary>
  /// A HelmRelease resource.
  /// </summary>
  HelmRelease,
  /// <summary>
  /// An image resource.
  /// </summary>
  Image,
  /// <summary>
  /// A Flux Kustomization resource.
  /// </summary>
  Kustomization,
  /// <summary>
  /// A receiver resource.
  /// </summary>
  Receiver,
  /// <summary>
  /// A Source resource.
  /// </summary>
  Source
}
