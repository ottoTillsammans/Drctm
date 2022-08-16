using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ServerSln
{
    public class MessageRepository
    {
        private readonly DbSet<Message> dbSet;
        private readonly DbContext dbContext;
        private static MessageRepository instance;

        private MessageRepository(DbContext externalContext)
        {
            dbContext = externalContext;
            dbSet = externalContext.Set<Message>();
        }

        public static MessageRepository GetInstance(DbContext dbContext)
        {
            if (instance == null)
                instance = new MessageRepository(dbContext);
            return instance;
        }

        public IQueryable<Message> GetAllByUserId(int id)
        {
            return dbSet == null ? null : dbSet.Where(m => m.UserId == id);
        }

        public Message GetBySubstring(string substring)
        {
            return dbSet == null ? null : dbSet
                .Where(m => m.Content != null && m.Content.Contains(substring))
                .FirstOrDefault();
        }

        public bool Create(int? userId, int? contactId, DateTime? sendTime, DateTime? deliveryTime, string? content)
        {
            Message message = new Message(userId, contactId, sendTime, deliveryTime, content);

            dbSet.Add(message);

            int numberOfWritten = dbContext.SaveChanges();

            return numberOfWritten > 0;
        }
    }
}
