using LicensePlate.Core.Abstraction;
using Microsoft.AspNetCore.Http;

namespace LicensePlate.Core.Infrastructure.Exeptions;

public class RefreshTokenIsNotFoundExeption : HttpExeptionAbstract
{
    public RefreshTokenIsNotFoundExeption(string massage) : base($"RefreshToke \"{massage}\" is not found")
    {
    }

    public override int StatusCode => StatusCodes.Status401Unauthorized;

}
