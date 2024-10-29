using Backend.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Backend.Application.Handlers;

public class UpdateAccountCommand : IRequest<Unit>
{
    public string Email { get; set; }
    public JsonPatchDocument<UpdateAccountRequest> PatchDocument { get; set; }
}

public class UpdateAccountHandler : IRequestHandler<UpdateAccountCommand, Unit>
{
    private readonly AccountRepository _accountRepository;
    
    public UpdateAccountHandler(AccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public async Task<Unit> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByEmailAsync(request.Email);
        
        if (account == null)
        {
            throw new InvalidOperationException($"Account with email '{request.Email}' not found.");
        }

        var accountToUpdate = new UpdateAccountRequest
        {
            Email = account.Email,
            ImageUrl = account.ImageUrl
        };

        request.PatchDocument.ApplyTo(accountToUpdate);
        
        account.Email = accountToUpdate.Email;
        account.ImageUrl = accountToUpdate.ImageUrl;
        
        await _accountRepository.UpdateOneAsync(account);

        return Unit.Value;
    }
}