using FluentValidation.Results;

namespace SuperbReads.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationException()
    {
    }

    public ValidationException(string? message)
        : base(message)
    {
    }

    public ValidationException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this("One or more validation failures have occurred.")
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray())
            .AsReadOnly();
    }

    public IReadOnlyDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>().AsReadOnly();
}
