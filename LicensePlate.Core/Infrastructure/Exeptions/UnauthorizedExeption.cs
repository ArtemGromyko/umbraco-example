using LicensePlate.Core.Abstraction;
using Microsoft.AspNetCore.Http;

namespace LicensePlate.Core.Infrastructure.Exeptions;

public sealed class UnauthorizedExeption : HttpExeptionAbstract
{
    public UnauthorizedExeption(string massage) : base(massage)
    {
    }

    public override int StatusCode => StatusCodes.Status401Unauthorized;
}
