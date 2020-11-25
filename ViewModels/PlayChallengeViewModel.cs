using Quizzish.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.ViewModels
{
    public class PlayChallengeViewModel
    {
        public QuestionOutputDto Question { get; set; }
        public int ChallengeId { get; set; }
    }
}
