using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using Quizzish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Quizzish.MailServices
{
    
    public class MailService
    {
        private readonly IEmailService _emailService;
        public MailService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendChallengeEmail(Challenge challenge, IdentityUser opponent, HttpRequest request, IUrlHelper url)
        {
            var link = url.Action("Index", "Home", new { id = challenge.Id }, request.Scheme, request.Host.ToString());

            await _emailService.SendAsync(opponent.Email, "Quizzish - New Challenge",
                "<!DOCTYPE html>" +
                "<html>" +
                "<head>" +
                "</head>" +
                "<body>" +
                $"<h3>{challenge.PlayerSections.FirstOrDefault().Player.UserName} has challenged you for a Quizzish challenge!</h1>" +
                 "<p>Click on the link below to visit our website.</p>" +
                $"<a href=\"{link}\">Go To Quizzish</a>" +
                "<p>Happy quizing,</p>" +
                "<p>Quizzish!</p>" +
                "</body>" +
                "</html>"
                , true);
        }

        public async Task SendConfirmMationEmail(IdentityUser user, string code, HttpRequest request, IUrlHelper url)
        {
            var link = url.Action("VerifyEmail", "User", new { userId = user.Id, code }, request.Scheme, request.Host.ToString());

            await _emailService.SendAsync(user.Email, "Quizzish - Acount Verification",
                "<!DOCTYPE html>" +
                "<html>" +
                "<head>" +
                "</head>" +
                "<body>" +
                "<h3>Thank you for joining <strong>Quizzish</strong></h1>" +
                "<p>You have successfully created an account. Please click the link below to verify your email:</p>" +
                $"<a href=\"{link}\">Verify Email</a>" +
                "<p>Happy quizing,</p>" +
                "<p>Quizzish!</p>" +
                "</body>" +
                "</html>"
                , true);
        }
    }
}
