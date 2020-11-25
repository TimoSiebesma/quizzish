using Quizzish.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.ViewModels
{
    public class ChallengesViewModel
    {
        public IEnumerable<ChallengeOutputDto> OpenChallenges { get; set; }
        public IEnumerable<ChallengeOutputDto> ClosedChallenges { get; set; }

        public int OwnId { get; set; }

        public bool AnyOpenChallenges => OpenChallenges.Any();
        public bool DidOpponentFinish(ChallengeOutputDto ch, PlayerOutputDto opponent) => ch.PlayerSections.FirstOrDefault(ps => ps.Player.Id == opponent.Id).Counter >= 10;
        public bool DidCurrentUserFinish(ChallengeOutputDto ch, PlayerOutputDto opponent) => ch.PlayerSections.FirstOrDefault(ps => ps.Player.Id != opponent.Id).Counter >= 10;
        public bool DidCurrentUserPlayBefore(ChallengeOutputDto ch, PlayerOutputDto opponent) => ch.PlayerSections.FirstOrDefault(ps => ps.Player.Id != opponent.Id).Counter == 0;
        public PlayerOutputDto FindWinner(ChallengeOutputDto ch)
            => ch.PlayerSections.FirstOrDefault().Points == ch.PlayerSections.LastOrDefault().Points
                ? null
                : ch.PlayerSections.OrderByDescending(ps => ps.Points).FirstOrDefault().Player;

        public PlayerOutputDto FindOpponent(ChallengeOutputDto ch) 
        {
            var playerSection = ch.PlayerSections.FirstOrDefault(ps => ps.Player.Id != OwnId);

            return playerSection?.Player;
        }
    }
}
