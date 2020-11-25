using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quizzish.Data.Session;
using Quizzish.Data.UnitOfWork;
using Quizzish.DTO_s;
using Quizzish.ViewModels;

namespace Quizzish.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly Session _session;

        public PlayersController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, Session session)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _session = session;
        }

        public IActionResult Index()
        {
            var players = _unitOfWork.Players.GetAll();

            var vms = players.Where(p => p.PlayerSections != null && p.PlayerSections.Any()).Select(p => new HomeViewModel { Player = _mapper.Map<PlayerOutputDto>(p) });

            var users = vms.OrderByDescending(v => v.PlayedGames).Take(25).ToList();

            return View(users);
        }

        [HttpGet]
        public IActionResult Search(string username)
        {
            var players = _mapper.Map<List<PlayerOutputDto>>( _unitOfWork.Players.GetAll().Where(u => u.UserName.Contains(username)));

            return View(players);
        }

        public IActionResult Player(int id)
        {
            int ownId = _session.GetPlayerIdFromSession(HttpContext);
            ownId = ownId != -1 ? ownId : _unitOfWork.Players.GetAll().Where(p => p.IdentityId == _userManager.GetUserId(User)).FirstOrDefault().Id;

            if (id == ownId)
            {
                return RedirectToAction("Player", "Home");
            }

            PlayerViewModel vm = new PlayerViewModel 
            { 
                Player = _mapper.Map<PlayerOutputDto>(
                     _unitOfWork.Players.GetById(id)), 

                CommonGames = _mapper.Map<IEnumerable<ChallengeOutputDto>>(
                     _unitOfWork.Challenges.GetAll()
                        .Where(ch => !new int[] { ownId, id }
                            .Except(ch.PlayerSections.Select(ps => ps.Player.Id))
                            .Any()))
            };

            return View(vm);
        }

        
    }
}
