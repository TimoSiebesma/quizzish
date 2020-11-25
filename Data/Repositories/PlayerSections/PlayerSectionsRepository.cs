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

namespace Quizzish.Data.Repositories.PlayerSections
{
    public class PlayerSectionsRepository : RepositoryWithCache<PlayerSection>, IPlayerSectionsRepository
    {
        public PlayerSectionsRepository(QuizzishDbContext context, Cache<PlayerSection> cache) : base(context, cache)
        {

        }
    }
}
