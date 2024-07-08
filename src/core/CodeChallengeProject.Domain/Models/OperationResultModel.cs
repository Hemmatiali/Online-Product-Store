namespace CodeChallengeProject.Domain.Models;

/// <summary>
///     Represents the result of an operation.
/// </summary>
public sealed class OperationResultModel
{
    /// <summary>
    ///     Indicates whether the operation was successful.
    /// </summary>
    public bool WasSuccess { get; }

    /// <summary>
    ///     Gets the error message if the operation was not successful.
    /// </summary>
    public string ErrorMessage { get; private set; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="OperationResultModel"/> class.
    /// </summary>
    /// <param name="wasSuccess">Indicates whether the operation was successful.</param>
    /// <param name="errorMessage">The error message if the operation was not successful.</param>
    private OperationResultModel(bool wasSuccess, string errorMessage = "")
    {
        WasSuccess = wasSuccess;
        ErrorMessage = errorMessage;
    }

    /// <summary>
    ///     Creates a failed operation result with the specified error message.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <returns>The failed operation result.</returns>
    public static OperationResultModel Fail(string errorMessage) => new(wasSuccess: false, errorMessage: errorMessage);

    /// <summary>
    ///     Creates a successful operation result.
    /// </summary>
    /// <returns>The successful operation result.</returns>
    public static OperationResultModel Success() => new(wasSuccess: true);
}

/// <summary>
///     Represents the result of an operation with additional data.
/// </summary>
/// <typeparam name="T">The type of additional data.</typeparam>
public sealed class OperationResultModel<T>
{
    /// <summary>
    ///     Indicates whether the operation was successful.
    /// </summary>
    public bool WasSuccess { get; }

    /// <summary>
    ///     Gets the error message if the operation was not successful.
    /// </summary>
    public string ErrorMessage { get; private set; }

    /// <summary>
    ///     Gets the additional data associated with the operation result.
    /// </summary>
    public T? Data { get; private set; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="OperationResultModel{T}"/> class.
    /// </summary>
    /// <param name="wasSuccess">Indicates whether the operation was successful.</param>
    /// <param name="data">The additional data associated with the operation result.</param>
    /// <param name="errorMessage">The error message if the operation was not successful.</param>
    private OperationResultModel(bool wasSuccess, T? data = default, string errorMessage = "")
    {
        WasSuccess = wasSuccess;
        Data = data;
        ErrorMessage = errorMessage;
    }

    /// <summary>
    ///     Creates a failed operation result with the specified error message.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <returns>The failed operation result.</returns>
    public static OperationResultModel<T> Fail(string errorMessage) => new(wasSuccess: false, data: default, errorMessage: errorMessage);

    /// <summary>
    ///     Creates a successful operation result with optional additional data.
    /// </summary>
    /// <param name="data">The additional data associated with the operation result.</param>
    /// <returns>The successful operation result.</returns>
    public static OperationResultModel<T> Success(T? data = default) => new(wasSuccess: true, data: data);
}