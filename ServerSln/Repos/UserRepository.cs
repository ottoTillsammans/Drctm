﻿using System;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ServerSln.Repos
{
    public class UserRepository
    {
        private readonly DbSet<User> dbSet;
        private readonly DbContext context;

        public UserRepository(DbContext dbContext)
        {
            context = dbContext;
            dbSet = dbContext.Set<User>();
        }

        public User GetById(int id)
        {
            return dbSet == null ? null : dbSet.FirstOrDefault(u => u.Id.Value == id);
        }

        public User GetByName(string name)
        {
            return dbSet == null ? null : dbSet.FirstOrDefault(u => u.Name == name);
        }

        public IQueryable<User> GetAllByName(string name)
        {
            return dbSet.Where(u => u.Name == name);
        }

        public int Create(int id, string name, string password, State state)
        {
            int? maxId = dbSet.Select(u => u.Id).Max();
            int currId = 1;

            if (maxId.HasValue)
                currId = maxId.Value + 1;

            User user = new User(name, password, state);

            var info = dbSet.Add(user);
            var userId = info.Entity.Id;

            int numberOfWritten = context.SaveChanges();

            return numberOfWritten == 0 || userId == null ? 0 : userId.Value;
        }

        public bool Update(int id, string name, string password, State state)
        {
            User user = dbSet.FirstOrDefault(u => u.Id == id);
            int numberOfUpdated = 0;

            if (user != null)
            {
                user.Name = name;
                user.Password = password;
                user.State = state;

                numberOfUpdated = context.SaveChanges();
            }
            return numberOfUpdated == 0 ? false : true;
        }
    }
}
