using Quizzish.DTO_s;
using Quizzish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.ViewModels
{
    public class PlayerViewModel
    {
        public PlayerOutputDto Player { get; set; }
        public int TotalScore => PlayedScores.Count() == 0 ? 1500 : (int)Math.Round(PlayedScores.Select(sc => sc.Amount).Average());
        public IEnumerable<ScoreOutputDto> PlayedScores => Player.Scores.Where(sc =>
        {
            var sections = Player.PlayerSections;
            return sections.Select(ps => ps.Category).Contains(sc.Category) && sections.FirstOrDefault(ps => ps.Category == sc.Category).Counter > 0;
        });
        public ScoreOutputDto BestCategory => Player.Scores.OrderBy(sc => sc.Amount).Last();
        public int PlayedGames => Player.PlayerSections.Count();
        public bool HasPlayedGames => PlayedGames > 0;



        public IEnumerable<ChallengeOutputDto> CommonGames { get; set; }

        public string FindWinner(ChallengeOutputDto challenge) => challenge.PlayerSections.Select(ps => ps.Points).Distinct().Count() > 1
                            ? challenge.PlayerSections.OrderBy(ps => ps.Points).LastOrDefault().Player.UserName
                            : "Tie";


    }
}
