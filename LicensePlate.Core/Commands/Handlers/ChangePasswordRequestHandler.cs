using LicensePlate.Core.Commands.Requests;
using LicensePlate.Core.Infrastructure.Exeptions;
using LicensePlate.Core.Interfaces;
using MediatR;

namespace LicensePlate.Core.Commands.Handlers;

public class ChangePasswordRequestHandler : IRequestHandler<ChangePasswordRequest>
{
    private readonly IUserService _userService;

    public ChangePasswordRequestHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Unit> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        bool isValidUser = await _userService.ValidateUser(request.UserName, request.OldPassword);
        if (isValidUser)
        {
            await _userService.ChangePasswordAsync(request.UserName, request.OldPassword, request.NewPassword);
            return Unit.Value;
        }

        throw new PasswordChangingExeption();
    }
}