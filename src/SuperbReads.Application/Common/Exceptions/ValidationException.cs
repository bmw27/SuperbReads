namespace SuperbReads.Application.Common.Exceptions;

public class ValidationException() : Exception("One or more validation failures have occurred.")
{
    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray())
            .AsReadOnly();
    }

    public IReadOnlyDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>().AsReadOnly();
}
