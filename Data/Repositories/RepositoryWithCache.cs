using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Quizzish.Data.Repositories.CacheData;
using Quizzish.Data.DbContexts;
using Quizzish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Quizzish.DTO_s;
using Quizzish.Profiles;

namespace Quizzish.Data.Repositories
{
    public class RepositoryWithCache<T> : IRepository<T> where T : class, IHaveId 
    {
        private readonly QuizzishDbContext _context;
        protected DbSet<T> Entities { get; set; }
        protected readonly Cache<T> _cache;

        public RepositoryWithCache(QuizzishDbContext context, Cache<T> cache)
        {
            _context = context;
            Entities = context.Set<T>();
            _cache = cache;
        }

        public T Detach(T replaceT)
        {
            var originalT = Entities.FirstOrDefault(entry => entry.Id.Equals(replaceT.Id));

            if (originalT != null)
            {
                _context.Entry(originalT).State = EntityState.Detached;
            }

            _context.Entry(replaceT).State = EntityState.Modified;

            return replaceT;
        }

        public void Add(T entity) => Entities.Add(entity);

        public virtual T GetById(int id, bool willCache = true)
        {
            return Entities.FirstOrDefault(x => x.Id == id);
        }

        public virtual IEnumerable<T> GetAll(bool willCache = true)
        {
            return Entities.ToList();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Entities.RemoveRange(entities);
        }

        protected IEnumerable<T> GetEntitiesFromCache(string t) => _cache.GetEntitiesFromCache(t);
    }
}
