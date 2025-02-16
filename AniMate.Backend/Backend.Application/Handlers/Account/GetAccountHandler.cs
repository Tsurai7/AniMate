using Backend.Application.Models.Account;
using Backend.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Backend.Application.Handlers.Account;

public class GetAccountResponse
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? ProfileImageUrl { get; set; }
    public List<string> LikedTitles { get; set; } = [];
    public List<string> WatchedTitles { get; set; } = [];
}

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

        return new GetAccountResponse
        {
            UserName = account.UserName,
            Email = account.Email,
            FirstName = account.FirstName,
            LastName = account.LastName,
            ProfileImageUrl = account.ImageUrl,
            LikedTitles = account.LikedTitles,
            WatchedTitles = account.WatchedTitles
        };
    }
}