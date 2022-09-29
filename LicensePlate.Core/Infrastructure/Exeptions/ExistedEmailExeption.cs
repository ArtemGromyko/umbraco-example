using LicensePlate.Core.Abstraction;
using Microsoft.AspNetCore.Http;

namespace LicensePlate.Core.Infrastructure.Exeptions;

public class ExistedEmailExeption : HttpExeptionAbstract
{
    public ExistedEmailExeption(string email) : base($"User with email: \"{email}\" alredy exist")
    {

    }
    public override int StatusCode => StatusCodes.Status200OK;
}
