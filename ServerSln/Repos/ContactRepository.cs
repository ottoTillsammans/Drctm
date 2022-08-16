using Microsoft.EntityFrameworkCore;

namespace ServerSln
{
    public class ContactRepository
    {
        private readonly DbSet<Contact> dbSet;
        private readonly DbContext dbContext;
        private static ContactRepository instance;

        private ContactRepository(DbContext externalContext)
        {
            dbContext = externalContext;
            dbSet = externalContext.Set<Contact>();
        }

        /// <summary>
        /// Get singleton instance of Contact repository.
        /// </summary>
        /// <param name="dbContext">Database context to connect DB.</param>
        /// <returns>Instance of the repository.</returns>
        public static ContactRepository GetInstance(DbContext dbContext)
        {
            if (instance == null)
                instance = new ContactRepository(dbContext);
            return instance;
        }

        /// <summary>
        /// Get the contact of the user
        /// or null if there is no connection to DB.
        /// </summary>
        /// <param name="id">User's id.</param>
        /// <returns>Contact.</returns>
        public Contact GetContact(int id)
        {
            return dbSet == null ? null : dbSet.FirstOrDefault(c => c.ContactId == id);
        }

        /// <summary>
        /// Get the user's list of contacts
        /// or null if there is no connection to DB.
        /// </summary>
        /// <param name="id">User's id.</param>
        /// <returns>List of user's contacts.</returns>
        public IQueryable<Contact> GetUserContasts(int id)
        {
            return dbSet == null ? null : dbSet.Where(c => c.UserId == id);
        }

        /// <summary>
        /// Get the contact by name in the list of user's contacts
        /// or null if there is no connection to DB or there is no contact.
        /// </summary>
        /// <param name="id">User's id.</param>
        /// <param name="name">Name of wanted contact.</param>
        /// <returns></returns>
        public Contact GetContactByName(int id, string name)
        {
            if (dbSet == null)
                return null;

            User user = dbSet
                .Where(c => c.UserId == id)
                .Select(c => UserRepository.GetInstance(dbContext).GetById(c.ContactId.Value))
                .Where(u => u.Name == name)
                .FirstOrDefault();

            return dbSet
                .Where(c => c.ContactId == user.Id)
                .FirstOrDefault();
        }

        /// <summary>
        /// Add an contact in the list of user's contacts.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <param name="contactId">Id of the contact.</param>
        /// <returns>True if successfully added, otherwise false.</returns>
        public bool AddInUserContacts(int userId, int contactId)
        {
            User user = UserRepository.GetInstance(dbContext).GetById(userId);
            Contact contact = new Contact(userId, contactId, DateTime.MinValue);

            dbSet.Add(contact);
            
            int numberOfWritten = dbContext.SaveChanges();

            return numberOfWritten > 0;
        }

        /// <summary>
        /// Update contact's data.
        /// </summary>
        /// <param name="userId">Id of contact's owner.</param>
        /// <param name="contactId">Id of contact.</param>
        /// <param name="lastUpdateTime">Date of the last messaging.</param>
        /// <returns>True if successfully updated, otherwise false.</returns>
        public bool Update(int userId, int contactId, DateTime lastUpdateTime)
        {
            Contact contact = dbSet.Where(c => c.ContactId.Value == contactId).FirstOrDefault();
            int numberOfUpdated = 0;

            if (contact != null)
            {
                contact.UserId = userId;
                contact.LastUpdateTime = lastUpdateTime;

                numberOfUpdated = dbContext.SaveChanges();
            }
            return numberOfUpdated > 0;
        }

        /// <summary>
        /// Delete the contact.
        /// </summary>
        /// <param name="id">Id of deleting contact.</param>
        public void Delete(int id)
        {
            if (dbSet == null)
                return;
            
            Contact contact = dbSet
                .Where(c => c.ContactId == id)
                .FirstOrDefault();

            dbSet.Remove(contact);
        }
    }
}
