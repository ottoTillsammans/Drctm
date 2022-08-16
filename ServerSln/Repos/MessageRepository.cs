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

        /// <summary>
        /// Get singleton instance of Message repository.
        /// </summary>
        /// <param name="dbContext">Database context to connect DB.</param>
        /// <returns>Instance of the repository.</returns>
        public static MessageRepository GetInstance(DbContext dbContext)
        {
            if (instance == null)
                instance = new MessageRepository(dbContext);
            return instance;
        }

        /// <summary>
        /// Get all the user's messages
        /// or null if there is no connection to DB.
        /// </summary>
        /// <param name="id">Users's id.</param>
        /// <returns>Messages.</returns>
        public IQueryable<Message> GetAllByUserId(int id)
        {
            return dbSet == null ? null : dbSet.Where(m => m.UserId == id);
        }

        /// <summary>
        /// Get message by it's substring.
        /// or null if there is no connection to DB.
        /// </summary>
        /// <param name="substring">The substring of a message.</param>
        /// <returns>Message.</returns>
        public Message GetBySubstring(string substring)
        {
            return dbSet == null ? null : dbSet
                .Where(m => m.Content != null && m.Content.Contains(substring))
                .FirstOrDefault();
        }

        /// <summary>
        /// Create a new message.
        /// </summary>
        /// <param name="userId">Sender's id.</param>
        /// <param name="contactId">Recipient's id.</param>
        /// <param name="sendTime">Date of sending.</param>
        /// <param name="deliveryTime">Date of delivering.</param>
        /// <param name="content">Text of message.</param>
        /// <returns>True if created successfully, otherwise false.</returns>
        public bool Create(int userId, int contactId, DateTime sendTime, DateTime deliveryTime, string content)
        {
            Message message = new Message(userId, contactId, sendTime, deliveryTime, content);

            dbSet.Add(message);

            int numberOfWritten = dbContext.SaveChanges();

            return numberOfWritten > 0;
        }
    }
}
