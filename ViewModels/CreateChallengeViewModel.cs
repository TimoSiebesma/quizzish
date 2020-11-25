using Quizzish.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.ViewModels
{
    public class CreateChallengeViewModel
    {
        public ChallengeInputDto Challenge { get; set; }
        public int OpponentId { get; set; }

        public CreateChallengeViewModel(int opponentId)
        {
            OpponentId = opponentId;
        }

        public CreateChallengeViewModel()
        {

        }
    }
   
}
