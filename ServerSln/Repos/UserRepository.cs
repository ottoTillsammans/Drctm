using System;
using Microsoft.EntityFrameworkCore;

namespace ServerSln.Repos
{
    public class UserRepository
    {
        private readonly DbSet<User> dbSet;
        private readonly DbContext context;
        private static UserRepository instance;

        private UserRepository(DbContext dbContext)
        {
            context = dbContext;
            dbSet = dbContext.Set<User>();
        }

        public static UserRepository GetInstance(DbContext dbContext)
        {
            if (instance == null)
                instance = new UserRepository(dbContext);
            return instance;
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

        public int Create(string name, string password, State state)
        {
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
