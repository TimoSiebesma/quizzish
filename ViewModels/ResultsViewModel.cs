using Quizzish.DTO_s;
using Quizzish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.ViewModels
{
    public class ResultsViewModel
    {
        public IEnumerable<IEnumerable<int>> Answers { get; set; }
        public IEnumerable<PlayerSectionOutputDto> PlayerSections { get; set; }
        public IEnumerable<QuestionOutputDto> Questions { get; set; }
        public bool IsGameOver => PlayerSections.All(ps => ps.Counter >= 10);

        public bool IsTie => PlayerSections.FirstOrDefault().Points == PlayerSections.LastOrDefault().Points;

        public PlayerSectionOutputDto Winner => PlayerSections.OrderByDescending(ps => ps.Points).FirstOrDefault();
    }
}
