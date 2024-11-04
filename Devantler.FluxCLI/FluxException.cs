namespace Devantler.FluxCLI;

/// <summary>
/// An exception thrown by the Flux CLI.
/// </summary>
[Serializable]
public class FluxException : Exception
{
  /// <summary>
  /// Initializes a new instance of the <see cref="FluxException"/> class.
  /// </summary>
  public FluxException()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="FluxException"/> class with a specified error message.
  /// </summary>
  /// <param name="message"></param>
  public FluxException(string? message) : base(message)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="FluxException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
  /// </summary>
  /// <param name="message"></param>
  /// <param name="innerException"></param>
  public FluxException(string? message, Exception? innerException) : base(message, innerException)
  {
  }
}
