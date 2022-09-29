using LicensePlate.Core.Interfaces;
using LicensePlate.Core.Commands.Requests;
using LicensePlate.Core.Commands.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using LicensePlate.Core.Contracts.V1.Requests;
using LicensePlate.Core.Contracts.V1.Responses;
using LicensePlate.Core.Contracts.V1;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LicensePlate.Core.Controllers;

[ApiController]
public class OAuthController : ControllerBase
{
    private readonly IOAuthOptions _oauthOptions;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public OAuthController(IOAuthOptions oauthOptions,
                           IMediator mediator,
                           IMapper mapper)
    {
        _oauthOptions = oauthOptions ?? throw new ArgumentNullException(nameof(oauthOptions));
        _mediator = mediator;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.OAuth.Login)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
    {
        var request = _mapper.Map<LoginRequest>(loginRequest);
        LoginResponse response = await _mediator.Send(request);
        SetTokenCookie(response.RefreshToken);

        var loginResponse = _mapper.Map<LoginResponseDTO>(response);
        return Ok(loginResponse);
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.OAuth.Refresh)]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequestDTO refreshRequest)
    {
        var request = _mapper.Map<RefreshRequest>(refreshRequest);
        request.RefreshToken = Request.Cookies["refreshToken"];
        RefreshResponse response = await _mediator.Send(request);
        SetTokenCookie(response.RefreshToken);

        var refreshResponse = _mapper.Map<RefreshResponseDTO>(response);
        return Ok(refreshResponse);
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.OAuth.Registration)]
    public async Task<IActionResult> Registration([FromBody] RegistrationRequestDTO registrationRequest)
    {
        var request = _mapper.Map<RegistrationRequest>(registrationRequest);
        RegistrationResponse response = await _mediator.Send(request);
        SetTokenCookie(response.RefreshToken);

        var registrationResponse = _mapper.Map<RegistrationResponseDTO>(response);
        return Ok(registrationResponse);
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.OAuth.Revoke)]
    public async Task<IActionResult> Revoke([FromBody] RevokeRequestDTO revokeRequest)
    {
        var request = _mapper.Map<RevokeRequest>(revokeRequest);
        request.RefreshToken = Request.Cookies["refreshToken"];
        await _mediator.Send(request);
        return Ok();
    }

    [Authorize]
    [HttpPut(ApiRoutes.OAuth.Password)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDTO changePasswordRequest)
    {
        var request = _mapper.Map<ChangePasswordRequest>(changePasswordRequest);
        request.UserName =  User.Identity.GetUserName();
        await _mediator.Send(request);
        return Ok();
    }

    private void SetTokenCookie(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddMinutes(_oauthOptions.RefreshTokenLifeTime)
        };
        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }

    // private string IpAddress()
    // {
    //     // get source ip address for the current request
    //     if (Request.Headers.ContainsKey("X-Forwarded-For"))
    //         return Request.Headers["X-Forwarded-For"];
    //     else
    //         return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    // }
}