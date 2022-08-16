using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ServerSln
{
    public class UserRepository
    {
        private readonly DbSet<User> dbSet;
        private readonly DbContext dbContext;
        private static UserRepository instance;

        private UserRepository(DbContext externalContext)
        {
            dbContext = externalContext;
            dbSet = externalContext.Set<User>();
        }

        /// <summary>
        /// Get singleton instance of User repository.
        /// </summary>
        /// <param name="dbContext">Database context to connect DB.</param>
        /// <returns>Instance of the repository.</returns>
        public static UserRepository GetInstance(DbContext dbContext)
        {
            if (instance == null)
                instance = new UserRepository(dbContext);
            return instance;
        }

        /// <summary>
        /// Get user by id
        /// or null if there is no connection to DB.
        /// </summary>
        /// <param name="id">User's id.</param>
        /// <returns>User.</returns>
        public User GetById(int id)
        {
            return dbSet == null ? null : dbSet.FirstOrDefault(u => u.Id.Value == id);
        }

        /// <summary>
        /// Get user by name
        /// or null if there is no connection to DB.
        /// </summary>
        /// <param name="name">User's name.</param>
        /// <returns>User.</returns>
        public User GetByName(string name)
        {
            return dbSet == null ? null : dbSet.FirstOrDefault(u => u.Name == name);
        }

        /// <summary>
        /// Get all users by name
        /// or null if there is no connection to DB.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<User> GetAllByName(string name)
        {
            return dbSet == null ? null : dbSet.Where(u => u.Name == name);
        }

        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="name">User's name.</param>
        /// <param name="password">User's password.</param>
        /// <param name="state">Current user's state.</param>
        /// <returns>New user's id if created successfully, otherwise 0.</returns>
        public int Create(string name, string password, State state)
        {
            User user = new User(name, password, state);

            var info = dbSet.Add(user);
            var userId = info.Entity.Id;

            int numberOfWritten = dbContext.SaveChanges();

            return numberOfWritten == 0 || userId == null ? 0 : userId.Value;
        }

        /// <summary>
        /// Update user's data.
        /// </summary>
        /// <param name="id">User's id.</param>
        /// <param name="name">Usser's name.</param>
        /// <param name="password">Users's password.</param>
        /// <param name="state">Users's state.</param>
        /// <returns>True if updated successfully, otherwise false.</returns>
        public bool Update(int id, string name, string password, State state)
        {
            User user = dbSet.FirstOrDefault(u => u.Id == id);
            int numberOfUpdated = 0;

            if (user != null)
            {
                user.Name = name;
                user.Password = password;
                user.State = state;

                numberOfUpdated = dbContext.SaveChanges();
            }
            return numberOfUpdated > 0;
        }
    }
}
