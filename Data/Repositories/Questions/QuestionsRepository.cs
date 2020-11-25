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

namespace Quizzish.Data.Repositories.Questions
{
    public class QuestionsRepository : RepositoryWithCache<Question>, IQuestionsRepository
    {
        public QuestionsRepository(QuizzishDbContext context, Cache<Question> cache) : base(context, cache)
        {

        }
    }
}
