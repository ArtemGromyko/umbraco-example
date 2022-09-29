using LicensePlate.Core.Abstraction;
using Microsoft.AspNetCore.Http;

namespace LicensePlate.Core.Infrastructure.Exeptions;

public class ExistedUsernameExeption : HttpExeptionAbstract
{
    public ExistedUsernameExeption(string username) : base($"User with username: \"{username}\" alredy exist")
    {

    }
    public override int StatusCode => StatusCodes.Status200OK;
}

