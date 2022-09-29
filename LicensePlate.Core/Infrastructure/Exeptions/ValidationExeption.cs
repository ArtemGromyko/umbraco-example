using System.Text;
using LicensePlate.Core.Abstraction;
using Microsoft.AspNetCore.Http;

namespace LicensePlate.Core.Infrastructure.Exeptions;

public sealed class ValidationException : HttpExeptionAbstract
{
    public ValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary) : base()
    {
        ErrorsDictionary = errorsDictionary;
    }

    private IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; init; }

    public override string Message
    {
        get
        {
            var message = new StringBuilder();

            foreach (KeyValuePair<string, string[]> error in ErrorsDictionary)
            {
                message.Append(error.Key);
                message.Append(": ");
                message.AppendJoin("; ", error.Value);
            }

            return message.ToString();
        }
    }

    public override int StatusCode => StatusCodes.Status422UnprocessableEntity;
}
