using Quizzish.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.ViewModels
{
    public class HighscoresViewModel
    {
        public IEnumerable<HomeViewModel> MainScores { get; set; }

        public IEnumerable<IEnumerable<ScoreOutputDto>> CategoryScores { get; set; }
    }
}
