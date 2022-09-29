using LicensePlate.Core.Abstraction;
using Microsoft.AspNetCore.Http;

namespace LicensePlate.Core.Infrastructure.Exeptions;

public sealed class InternalExeption : HttpExeptionAbstract
{
    public InternalExeption(string message) : base($"Internal Exeption: {message}")
    {
    }

    public override int StatusCode => StatusCodes.Status500InternalServerError;
}
