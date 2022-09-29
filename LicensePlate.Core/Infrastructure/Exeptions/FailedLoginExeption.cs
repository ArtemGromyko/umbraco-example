using LicensePlate.Core.Abstraction;
using Microsoft.AspNetCore.Http;

namespace LicensePlate.Core.Infrastructure.Exeptions;

public class FailedLoginExeption : HttpExeptionAbstract
{
    public FailedLoginExeption() : base("Password or Login is not valid")
    {
    }

    public override int StatusCode => StatusCodes.Status200OK;
}
