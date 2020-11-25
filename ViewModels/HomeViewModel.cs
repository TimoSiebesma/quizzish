using Quizzish.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quizzish.ViewModels
{
    public class HomeViewModel
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

    }
}
