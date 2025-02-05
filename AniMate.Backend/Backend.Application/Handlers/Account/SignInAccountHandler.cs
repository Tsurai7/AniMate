using Backend.Application.Models.Account;
using Backend.Application.Services;
using Backend.Domain.Models;
using Backend.Infrastructure.Repositories;
using MediatR;

namespace Backend.Application.Handlers.Account;

public class SignInAccountHandler : IRequestHandler<SignInRequest, AuthToken>
{
    private readonly AccountRepository _accountRepository;
    private readonly TokenService _tokenService;
    
    public SignInAccountHandler(
        AccountRepository accountRepository,
        TokenService tokenService)
    {
        _accountRepository = accountRepository;
        _tokenService = tokenService;
    }
    
    public async Task<AuthToken> Handle(SignInRequest request, CancellationToken cancellationToken)
    {
        var existingAccount = await _accountRepository.GetByEmailAsync(request.Email);

        if (existingAccount is null)
        {
            throw new InvalidOperationException("No account found. Try to sign up.");
        }
        
        return _tokenService.GenerateToken(request.Email);
    }
}