using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using NETCore.MailKit.Core;
using Quizzish.Data;
using Quizzish.Data.Session;
using Quizzish.Data.UnitOfWork;
using Quizzish.DTO_s;
using Quizzish.MailServices;
using Quizzish.Models;
using Quizzish.Profiles;
using Quizzish.ViewModels;
using static Quizzish.Data.ApiCaller;

namespace Quizzish.Controllers
{
    [Route("Challenge")]
    public class ChallengeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly MailService _mailService;
        private readonly CustomMapper _customMapper;
        private readonly Session _session;

        public ChallengeController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, MailService mailService, CustomMapper customMapper, Session session)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _mailService = mailService;
            _customMapper = customMapper;
            _session = session;

        }

        [Authorize]
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var userId = (await _userManager.GetUserAsync(User)).Id;
            int ownId = _session.GetPlayerIdFromSession(HttpContext);
            ownId = ownId != -1 ? ownId : _unitOfWork.Players.GetAll().Where(p => p.IdentityId == userId).FirstOrDefault().Id;
            var challenges =  _unitOfWork.Challenges.GetAll(true).Where(ch => ch.PlayerSections.Select(ps => ps.Player.Id).Contains(ownId)).Reverse();

            var vm = new ChallengesViewModel
            {
                OpenChallenges = _mapper.Map<IEnumerable<ChallengeOutputDto>>(
                    challenges.Where(ch => ch.PlayerSections.Any(ps => ps.Counter < 10))),
                ClosedChallenges = _mapper.Map<IEnumerable<ChallengeOutputDto>>(
                    challenges.Where(ch => ch.PlayerSections.All(ps => ps.Counter >= 10))),
                OwnId = ownId
            };

            return View(vm);
        }

        [Authorize]
        [HttpGet("Create/{id}")]
        public IActionResult Create(int id)
        {
            return View(new CreateChallengeViewModel(id));
        }

        [Authorize]
        [HttpPost("Create/{id}")]
        public async Task<IActionResult> Create(int id, CreateChallengeViewModel challengeInput)
        {

            int ownId = _session.GetPlayerIdFromSession(HttpContext);
            ownId = ownId != -1 ? ownId : _unitOfWork.Players.GetAll().Where(p => p.IdentityId == _userManager.GetUserId(User)).FirstOrDefault().Id;

            var challenge = _mapper.Map<Challenge>(challengeInput.Challenge);

            challenge.PlayerSections.FirstOrDefault().Player = _unitOfWork.Players.Detach(_unitOfWork.Players.GetById(ownId));
            challenge.PlayerSections.LastOrDefault().Player = _unitOfWork.Players.Detach(_unitOfWork.Players.GetById(id));

            IEnumerable<Result> results = new ApiCaller(challenge.Category, challenge.Difficulty).Fetch();
            IEnumerable<Question> questions = _customMapper.MapApiResultsIntoQuestions(results);

            challenge.Questions = questions.ToList();

            _unitOfWork.Challenges.Add(challenge);

            _unitOfWork.Complete();

            var opponent = await _userManager.FindByIdAsync(challenge.PlayerSections.LastOrDefault().Player.IdentityId);
            //await _mailService.SendChallengeEmail(challenge, opponent, Request, Url);

            return RedirectToAction("Play", new { id = challenge.Id });
        }

        [Authorize]
        [HttpGet("Play/{id}", Name ="Play")]
        public IActionResult Play(int id)
        {
            int ownId = _session.GetPlayerIdFromSession(HttpContext);
            ownId = ownId != -1 ? ownId : _unitOfWork.Players.GetAll(false).Where(p => p.IdentityId == _userManager.GetUserId(User)).First().Id;
            var challenge =  _unitOfWork.Challenges.GetById(id, false);

            if (challenge != null && challenge.PlayerSections.Select(ps => ps.Player.Id).Contains(ownId))
            {
                if(challenge.PlayerSections.FirstOrDefault(ps => ps.Player.Id == ownId).Counter >= 10)
                {
                    return RedirectToAction("Results", new { id });
                }

                PlayChallengeViewModel vm = new PlayChallengeViewModel { 
                    Question = _mapper.Map<QuestionOutputDto>(
                        challenge.Questions.ElementAtOrDefault(challenge.PlayerSections.FirstOrDefault(ps => ps.Player.Id == ownId).Counter)), 
                    ChallengeId = id 
                };

                return View(vm);
            }

            return NotFound();

        }

        [Authorize]
        [HttpPost("Answer")]
        public IActionResult TakeInQuestionResult(int answerId, int challengeId)
        {
            var challenge = _unitOfWork.Challenges.GetById(challengeId, false);
            var answer = _unitOfWork.Answers.GetById(answerId, false);

            if (challenge != null && answer != null)
            {
                var currUser =  _unitOfWork.Players.GetAll(false).FirstOrDefault(p => p.IdentityId == _userManager.GetUserId(User));
                var playerSection = challenge.PlayerSections.FirstOrDefault(
                    ps => ps.Player.Id == currUser.Id);

                if (challenge.GameOverOnCurrentUserSide(playerSection.Counter, answer.Id))
                { 
                    return RedirectToAction("Results", new { id = challengeId });
                }

                challenge.ProcessUserAnswer(playerSection, answer);
                _unitOfWork.Complete();

                return RedirectToAction("Play", new { id = challengeId });
            }

            return NotFound();

        }

        [Authorize]
        [HttpGet("Results/{id}")]
        public IActionResult Results(int id, bool cache = true)
        {
            var challenge = _unitOfWork.Challenges.GetById(id, cache);

            if (challenge != null)
            {
                return View(new ResultsViewModel
                {
                    Answers = challenge.PlayerSections.Select(ps => ps.Answers
                        .Split('-')
                        .Where(x => x.Length > 0)
                        .Select(a => int.Parse(a))),
                    PlayerSections = _mapper.Map<IEnumerable<PlayerSectionOutputDto>>(challenge.PlayerSections),
                    Questions = _mapper.Map<IEnumerable<QuestionOutputDto>>(challenge.Questions)
                });
            }

            return NotFound();
        }
    }
}
