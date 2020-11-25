using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quizzish.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Quizzish.Data.UnitOfWork;
using Quizzish.MailServices;
using Quizzish.ViewModels;

namespace Quizzish.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MailService _mailService;

        public UserController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, MailService mailService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _mailService = mailService;
        }
        
        

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVm)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVm);
            }

            var user = new IdentityUser { UserName = registerVm.Username, Email = registerVm.Email };
            var result = await _userManager.CreateAsync(user, registerVm.Password);

            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                await _mailService.SendConfirmMationEmail(user, code, Request, Url);  

                return RedirectToAction("EmailVerification");
            }

            return View();
        }

        [HttpGet]
        public IActionResult EmailVerification() => View();

        [HttpGet(Name="VerifyEmail")]
        public async Task<IActionResult> VerifyEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest();
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                var player = new Player { IdentityId = user.Id, UserName = user.UserName, PlayerSections = new List<PlayerSection>() };
                List<Score> scores = new List<Score>();

                for (int i = 9; i <= 32; i++)
                {
                    scores.Add(new Score { Category = (Category)i, Player = player, Amount = 1500 });
                }

                player.Scores = scores;

                _unitOfWork.Players.Add(player);

                _unitOfWork.Complete();
                //_unitOfWork.RemoveCache();
                return View();
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");

        }
    }
}
