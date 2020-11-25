using Quizzish.Data.Repositories.Answers;
using Quizzish.Data.Repositories.Challenges;
using Quizzish.Data.Repositories.Players;
using Quizzish.Data.Repositories.PlayerSections;
using Quizzish.Data.Repositories.Questions;
using Quizzish.Data.Repositories.Scores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAnswersRepository Answers { get; }
        IQuestionsRepository Questions { get; }
        IChallengesRepository Challenges { get; }
        IPlayersRepository Players { get; }
        IPlayerSectionsRepository PlayerSections { get; }
        IScoresRepository Scores { get; }
        void RemoveCache();
        int Complete();
    }
}
