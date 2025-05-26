using Application.Interfaces;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RecoverPassword : IRecoverPassword
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;

        public RecoverPassword(IUserRepository userRepository, IEmailService emailService, IConfiguration config)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _config = config;
        }

        public async Task SendResetPasswordEmailAsync(string email)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null) return;

            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            user.ResetPasswordToken = token;
            user.ResetPasswordTokenExpiry = DateTime.UtcNow.AddHours(1);

             _userRepository.Update(user);

            var frontendUrl = _config["Frontend:ResetPasswordUrl"];
            var resetLink = $"{frontendUrl}?token={Uri.EscapeDataString(token)}";

            var subject = "Restablecer contraseña";
            var body = $"<p>Hola,</p><p>Para restablecer tu contraseña hacé clic en este enlace:</p><a href='{resetLink}'>Restablecer</a>";

            await _emailService.SendEmailAsync(user.Email, subject, body);
        }

        public async Task<bool> ResetPasswordAsync(string token, string newPassword)
        {
            var user = await _userRepository.GetByResetTokenAsync(token);
            if (user == null || user.ResetPasswordTokenExpiry < DateTime.UtcNow)
                return false;

            user.Password = newPassword;
            user.ResetPasswordToken = null;
            user.ResetPasswordTokenExpiry = null;

             _userRepository.Update(user);
            return true;
        }
    }
}
