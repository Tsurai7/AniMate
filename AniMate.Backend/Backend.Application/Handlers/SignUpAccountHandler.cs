using Backend.Application.Services;
using Backend.Domain.Models;
using Backend.Infrastructure.Repositories;
using MediatR;

namespace Backend.Application.Handlers;

public class SignUpAccountCommand : IRequest<AuthToken>
{
    public string Username { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}

public class SignUpAccountHandler : IRequestHandler<SignUpAccountCommand, AuthToken>
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
    
    public async Task<AuthToken> Handle(SignUpAccountCommand request, CancellationToken cancellationToken)
    {
        var existingAccount = await _accountRepository.GetByEmailAsync(request.Email);

        if (existingAccount != null)
        {
            throw new InvalidOperationException("Account already exists. Try to sign in");
        }

        var newAccount = new Account
        {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password
        };
        
        await _accountRepository.AddAsync(newAccount);
        
        return _tokenService.GenerateToken(request.Email);
    }
}