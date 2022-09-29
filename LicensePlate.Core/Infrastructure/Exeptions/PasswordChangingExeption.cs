using LicensePlate.Core.Abstraction;
using Microsoft.AspNetCore.Http;

namespace LicensePlate.Core.Infrastructure.Exeptions;

public class PasswordChangingExeption : HttpExeptionAbstract
{
    public PasswordChangingExeption(string message = "Old password is not valid") : base(message)
    {
    }

    public override int StatusCode => StatusCodes.Status403Forbidden;
}