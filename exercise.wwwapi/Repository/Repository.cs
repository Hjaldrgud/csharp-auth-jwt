﻿using exercise.wwwapi.Data;
using exercise.wwwapi.DataModels;
using exercise.wwwapi.DataModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DataContext _db;
        private DbSet<T> _table = null;

        public Repository(DataContext dataContext)
        {
            _db = dataContext;
            _table = _db.Set<T>();
        }


        public async Task<IEnumerable<T>> SelectAll()
        {
            return await _table.ToListAsync();
        }

        public async Task<T> Insert(T entity)
        {
            _table.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }


        public async Task<T> SelectById(object id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<T> Update(T entity)
        {
            _table.Update(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return entity;

        }

        public async Task<T> Delete(T entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}

