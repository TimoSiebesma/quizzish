using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Quizzish.Data.DbContexts;
using Quizzish.Data.Repositories.CacheData;
using Quizzish.DTO_s;
using Quizzish.Models;
using Quizzish.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzish.Data.Repositories.Scores
{
    public class ScoresRepository : RepositoryWithCache<Score>, IScoresRepository
    {
        public ScoresRepository(QuizzishDbContext context, Cache<Score> cache) : base(context, cache)
        {

        }
    }
}
