using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Quizzish.Data.UnitOfWork;
using Quizzish.DTO_s;
using Quizzish.Models;
using Quizzish.ViewModels;

namespace Quizzish.Controllers
{
    public class HighscoresController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HighscoresController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var scores = _unitOfWork.Scores.GetAll();

            return View(new HighscoresViewModel
            {
                MainScores =  _unitOfWork.Players.GetAll()
                    .Select(p => new HomeViewModel { Player = _mapper.Map<PlayerOutputDto>(p) })
                    .Where(p => p.HasPlayedGames)
                    .OrderByDescending(v => v.TotalScore)
                    .Take(10),

                CategoryScores = _mapper.Map<IEnumerable<IEnumerable<ScoreOutputDto>>>(
                    Enumerable.Range(9, 24)
                        .Select(n =>
                             scores.Where(sc => sc.Category == (Category)n)
                            .Where(sc => sc.Player.PlayerSections.Any(ps => ps.Category == (Category)n))
                            .OrderByDescending(sc => sc.Amount)
                            .Take(10))
                        .Where(sc => sc.Any()))
            });
        }
    }
}
