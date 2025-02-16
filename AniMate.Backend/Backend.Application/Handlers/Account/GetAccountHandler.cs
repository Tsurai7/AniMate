using Backend.Application.Models.Account;
using Backend.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Backend.Application.Handlers.Account;

public record GetAccountResponse
(
    string Username,
    string ProfileImage,
    string Email,
    List<string> WatchedTitles,
    List<string> LikedTitles
);

public class GetAccountHandler : IRequestHandler<GetAccountRequest, GetAccountResponse>
{
    private readonly AccountRepository _accountRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetAccountHandler(
        AccountRepository accountRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _accountRepository = accountRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GetAccountResponse> Handle(GetAccountRequest request, CancellationToken cancellationToken)
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

        return new GetAccountResponse(account.UserName, account.ImageUrl, account.Email, account.WatchedTitles, account.LikedTitles);
    }
}