using Backend.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Backend.Application.Handlers.Auth;

public class GetRefreshTokenRequest : IRequest<GetRefreshTokenResponse>;

public class GetRefreshTokenResponse
{
    public string Token { get; set; } = string.Empty;
    
    public DateTime Expiration { get; set; }
}

public class GetRefreshTokenCommandHandler : 
    IRequestHandler<GetRefreshTokenRequest,
    GetRefreshTokenResponse>
{
    private readonly AccountRepository _accountRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetRefreshTokenCommandHandler(
        AccountRepository accountRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _accountRepository = accountRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GetRefreshTokenResponse> Handle(
        GetRefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

}