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

namespace Quizzish.Data.Repositories.Challenges
{
    public class ChallengesRepository : RepositoryWithCache<Challenge>, IChallengesRepository
    {
        public ChallengesRepository(QuizzishDbContext context, Cache<Challenge> cache) : base(context, cache)
        {

        }

        public override IEnumerable<Challenge> GetAll(bool willCache = true)
        {
            return (willCache)
                ? GetEntitiesFromCache("Challenges").ToList()
                : Entities.ToList();
        }

        public override Challenge GetById(int id, bool willCache = true)
        {
            return willCache
                ? GetAll().FirstOrDefault(x => x.Id == id)
                : Entities.FirstOrDefault(x => x.Id == id);
        }
    }
}
