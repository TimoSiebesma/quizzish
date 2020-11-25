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

namespace Quizzish.Data.Repositories.Answers
{
    public class AnswersRepository : RepositoryWithCache<Answer>, IAnswersRepository
    {
        public AnswersRepository(QuizzishDbContext context, Cache<Answer> cache) : base(context, cache)
        {

        }
    }
}
