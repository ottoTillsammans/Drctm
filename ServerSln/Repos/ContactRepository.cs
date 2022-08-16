using System;
using System.Linq;
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

        public static ContactRepository GetInstance(DbContext dbContext)
        {
            if (instance == null)
                instance = new ContactRepository(dbContext);
            return instance;
        }

        public Contact GetContactOwner(int id)
        {
            return dbSet == null ? null : dbSet.FirstOrDefault(c => c.ContactId == id);
        }

        public IQueryable<Contact> GetUserContasts(int id)
        {
            return dbSet == null ? null : dbSet.Where(c => c.UserId == id);
        }

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

        public bool AddInUserContacts(int userId, int contactId)
        {
            User user = UserRepository.GetInstance(dbContext).GetById(userId);
            Contact contact = new Contact(userId, contactId, DateTime.MinValue);

            dbSet.Add(contact);
            
            int numberOfWritten = dbContext.SaveChanges();

            return numberOfWritten > 0;
        }

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
