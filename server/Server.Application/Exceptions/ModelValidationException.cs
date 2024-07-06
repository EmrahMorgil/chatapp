using FluentValidation.Results;

namespace Server.Application.Exceptions;

public class ModelValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }
    public ModelValidationException() : base("One or more validation failures have occured.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ModelValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
        .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
        .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}
