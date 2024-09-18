using AccountService.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.API.Controllers;

[Authorize]
[Controller]
[Route("account")]
public class AccountController : ControllerBase
{
    private readonly Application.Services.AccountService _accountService;

    public AccountController(Application.Services.AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        try
        {
            var profileInfo = await _accountService.GetProfile(HttpContext);
            return Ok(profileInfo);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Внутренняя ошибка сервера: {ex.Message}");
        }
    }
}