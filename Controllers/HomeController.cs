using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NETCore.MailKit.Core;
using Quizzish.Data.Session;
using Quizzish.Data.UnitOfWork;
using Quizzish.DTO_s;
using Quizzish.Models;
using Quizzish.Profiles;
using Quizzish.ViewModels;

namespace Quizzish.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly Session _session;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMemoryCache memoryCache, Session session)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _session = session;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.LoggedIn = false;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = await _userManager.FindByNameAsync(login.Username);

            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);

                if (signInResult.Succeeded)
                {
                    ViewBag.LoggedIn = true;
                    return RedirectToAction("Player", "Home");
                }
            }

            return View(login);
        }

        [Authorize]
        public IActionResult Player()
        {
            var player = _unitOfWork.Players.GetAll()
                        .Where(p => p.IdentityId == _userManager.GetUserId(User))
                        .FirstOrDefault();

            _session.SavePlayerIdToSession(player.Id, HttpContext);

            if (player != null)
            {
                return View(new HomeViewModel { Player = _mapper.Map<PlayerOutputDto>(player) });
            }

            return NotFound();
        }


    }
}
