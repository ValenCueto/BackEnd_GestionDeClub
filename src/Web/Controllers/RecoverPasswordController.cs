using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RecoverPasswordController : ControllerBase
{
    private readonly IRecoverPassword _recoverPasswordService;

    public RecoverPasswordController(IRecoverPassword recoverPassword)
    {
        _recoverPasswordService = recoverPassword;
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] EmailRequest rq)
    {
        await _recoverPasswordService.SendResetPasswordEmailAsync(rq.Email);
        return Ok("Si el email existe, se envió un enlace.");
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] RecoverPasswordRequest rq)
    {
        var result = await _recoverPasswordService.ResetPasswordAsync(rq.Token, rq.NewPassword);
        return result ? Ok("Contraseña restablecida") : BadRequest("Token inválido o expirado");
    }
}
