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

namespace Quizzish.Data.Repositories.Players
{
    public class PlayersRepository : RepositoryWithCache<Player>, IPlayersRepository
    {
        public PlayersRepository(QuizzishDbContext context, Cache<Player> cache) : base(context, cache)
        {

        }

        public override IEnumerable<Player> GetAll(bool willCache = true)
        {
            return (willCache)
                ? GetEntitiesFromCache("Players").ToList()
                : Entities.ToList();
        }

        public override Player GetById(int id, bool willCache = true)
        {
            return willCache
                ? GetAll().FirstOrDefault(x => x.Id == id)
                : Entities.FirstOrDefault(x => x.Id == id);
        }
    }
}
