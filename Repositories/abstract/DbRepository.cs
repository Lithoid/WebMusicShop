﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;

namespace Repositories
{
    public class DbRepository<T> : IDbRepository<T>
        where T : class, IDbEntity
    {
        private DbContext _context;

        public DbRepository(DbContext context)
        {
            _context = context;
        }

        public IQueryable<T> AllItems => _context.Set<T>();

        public async Task<bool> AddItemAsync(T entity, bool saving = true)
        {
            _context.Set<T>().Add(entity);
            if (saving) return await SaveChangesAsync() > 0;
            else return false;
        }

        public async Task<int> AddItemsAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return await SaveChangesAsync();
        }

        public async Task<bool> ChangeItemAsync(T entity)
        {
           

            _context.Set<T>().Attach(entity).State = EntityState.Modified;
            return await SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            T candidate = await AllItems.FirstOrDefaultAsync(e => e.Id == id);
            _context.Set<T>().Remove(candidate);
            return await SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteItemsAsync(IEnumerable<T> entities)
        {
            foreach (var item in entities)
            {
                var h = _context.Set<T>().Find(item.Id);
                _context.Set<T>().Remove(h);
            }
            return await SaveChangesAsync() > 0;
        }

        public async Task<T> GetItemAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true, string includeProps = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProps != null)
            {
                foreach (var item in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync()
        {

            

            try
            {
                return await _context.SaveChangesAsync();
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return -1;
            }
        }

        public async Task<List<T>> GetAllItemsAsync(Expression<Func<T, bool>> filter = null, string includeProps = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProps != null)
            {
                foreach (var item in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return await query.ToListAsync();
        }
    }
}