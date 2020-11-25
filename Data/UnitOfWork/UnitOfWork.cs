using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Quizzish.Data.DbContexts;
using Quizzish.Data.Repositories.Answers;
using Quizzish.Data.Repositories.CacheData;
using Quizzish.Data.Repositories.Challenges;
using Quizzish.Data.Repositories.Players;
using Quizzish.Data.Repositories.PlayerSections;
using Quizzish.Data.Repositories.Questions;
using Quizzish.Data.Repositories.Scores;
using Quizzish.Models;
using Quizzish.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QuizzishDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public IAnswersRepository Answers { get; }
        public IQuestionsRepository Questions { get; }
        public IChallengesRepository Challenges { get; }
        public IPlayersRepository Players { get; }
        public IPlayerSectionsRepository PlayerSections { get; }
        public IScoresRepository Scores { get; }


        public UnitOfWork(QuizzishDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;

            Answers = new AnswersRepository(_context, new Cache<Answer>(_memoryCache, _context));
            Questions = new QuestionsRepository(_context, new Cache<Question>(_memoryCache, _context));
            Challenges = new ChallengesRepository(_context, new Cache<Challenge>(_memoryCache, _context));
            Players = new PlayersRepository(_context, new Cache<Player>(_memoryCache, _context));
            PlayerSections = new PlayerSectionsRepository(_context, new Cache<PlayerSection>(_memoryCache, _context));
            Scores = new ScoresRepository(_context, new Cache<Score>(_memoryCache, _context));
        }

        public int Complete()
        {
            RemoveCache();
            return _context.SaveChanges();
        }
        public void RemoveCache() 
        {
            _memoryCache.Remove("Players");
            _memoryCache.Remove("Challenges");
        }

        public void Dispose() => _context.Dispose();
    }
}