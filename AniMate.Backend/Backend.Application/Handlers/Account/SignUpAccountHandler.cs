using Backend.Application.Models.Account;
using Backend.Application.Services;
using Backend.Domain.Models;
using Backend.Infrastructure.Repositories;
using MediatR;

namespace Backend.Application.Handlers.Account;

public class SignUpAccountHandler : IRequestHandler<SignUpRequest, AuthToken>
{
    private readonly AccountRepository _accountRepository;
    private readonly TokenService _tokenService;
    
    public SignUpAccountHandler(
        AccountRepository accountRepository,
        TokenService tokenService)
    {
        _accountRepository = accountRepository;
        _tokenService = tokenService;
    }
    
    public async Task<AuthToken> Handle(SignUpRequest request, CancellationToken cancellationToken)
    {
        var existingAccount = await _accountRepository.GetByEmailAsync(request.Email);

        if (existingAccount != null)
        {
            throw new InvalidOperationException("Account already exists. Try to sign in");
        }

        var newAccount = new Domain.Models.Account
        {
            UserName = request.UserName,
            Email = request.Email,
            PasswordHash = request.Password
        };
        
        await _accountRepository.AddAsync(newAccount);
        
        return _tokenService.GenerateToken(request.Email);
    }
}