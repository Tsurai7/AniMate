using Backend.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Backend.Application.Handlers;

public class GetProfileRequest : IRequest<GetProfileResponse>;

public record GetProfileResponse
(
    string Username,
    string ProfileImage,
    string Email,
    List<string> WatchedTitles,
    List<string> LikedTitles
);

public class GetProfileHandler : IRequestHandler<GetProfileRequest, GetProfileResponse>
{
    private readonly AccountRepository _accountRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetProfileHandler(AccountRepository accountRepository, IHttpContextAccessor httpContextAccessor)
    {
        _accountRepository = accountRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GetProfileResponse> Handle(GetProfileRequest request, CancellationToken cancellationToken)
    {
        var email = _httpContextAccessor.HttpContext?.User.Identity?.Name;

        if (string.IsNullOrEmpty(email))
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        var account = await _accountRepository.GetByEmailAsync(email);

        if (account is null)
        {
            throw new InvalidOperationException("Account not found.");
        }

        return new GetProfileResponse(account.Username, account.ImageUrl, account.Email, account.WatchedTitles, account.LikedTitles);
    }
}