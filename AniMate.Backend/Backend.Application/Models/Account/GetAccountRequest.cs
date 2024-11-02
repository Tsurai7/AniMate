using Backend.Application.Handlers.Account;
using MediatR;

namespace Backend.Application.Models.Account;

public class GetAccountRequest : IRequest<GetAccountResponse>;