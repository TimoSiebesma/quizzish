using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Quizzish.Data.DbContexts;
using Quizzish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.Data.Repositories.CacheData
{
    public class Cache<T> where T : class, IHaveId
    {
        private readonly IMemoryCache _memoryCache;
        private readonly QuizzishDbContext _context;

        public Cache(IMemoryCache memoryCache, QuizzishDbContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        public IEnumerable<T> GetEntitiesFromCache(string pluralType)
        {
            if (!_memoryCache.TryGetValue(pluralType, out IEnumerable<T> entities))
            {
                LoadEntities();
            }

            return entities ?? _memoryCache.Get(pluralType) as IEnumerable<T>;
        }

        private void LoadEntities()
        {
            LoadPlayerSections();
            LoadScores();
            LoadAnswers();
            LoadQuestions();
            LoadPlayers();
            LoadChallenges();
        }

        private void LoadAnswers()
        {
            _context.Set<Answer>().Load();
        }

        private void LoadQuestions()
        {
            _context.Set<Question>().Load();
        }

        private void LoadPlayerSections()
        {
            _context.Set<PlayerSection>().Load();
        }

        private void LoadScores()
        {
            _context.Set<Score>().Load();

        }

        private void LoadChallenges()
        {
            var shEntities = _context.Set<Challenge>();

            shEntities.Load();

            _memoryCache.Set("Challenges", shEntities.ToList());
        }

        private void LoadPlayers()
        {
            var shEntities = _context.Set<Player>();
            shEntities.Load();
            _memoryCache.Set("Players", shEntities.ToList());
        }

        public void Remove(string item) => _memoryCache.Remove(item);
    }
}
