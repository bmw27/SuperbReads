namespace SuperbReads.Application.Common.Models;

public class Result
{
    protected internal Result(bool isSuccess, ResultError resultError)
    {
        if (isSuccess && resultError != ResultError.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && resultError == ResultError.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        ResultError = resultError;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public ResultError ResultError { get; }

    public static Result Success()
    {
        return new Result(true, ResultError.None);
    }

    public static Result<TValue> Success<TValue>(TValue value)
    {
        return new Result<TValue>(value, true, ResultError.None);
    }

    public static Result Failure(ResultError resultError)
    {
        return new Result(false, resultError);
    }

    public static Result<TValue> Failure<TValue>(ResultError resultError)
    {
        return new Result<TValue>(default, false, resultError);
    }

    public static Result Create(bool condition)
    {
        return condition ? Success() : Failure(ResultError.ConditionNotMet);
    }

    public static Result<TValue> Create<TValue>(TValue? value)
    {
        return value is not null ? Success(value) : Failure<TValue>(ResultError.NullValue);
    }
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, ResultError resultError)
        : base(isSuccess, resultError)
    {
        _value = value;
    }

    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public static implicit operator Result<TValue>(TValue? value)
    {
        return Create(value);
    }

    public Result<TValue> ToResult()
    {
        throw new NotImplementedException();
    }
}

public record ResultError(string Code, string Message)
{
    public static readonly ResultError None = new(string.Empty, string.Empty);

    public static readonly ResultError NullValue = new("Error.NullValue", "The specified result value is null.");

    public static readonly ResultError ConditionNotMet =
        new("Error.ConditionNotMet", "The specified condition was not met.");
}
