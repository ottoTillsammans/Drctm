namespace ServerSln  
{
    public class Contact
    {
        public int? UserId { get; set; }

        public int? ContactId { get; set; }

        public DateTime? LastUpdateTime { get; set; }

        public Contact(int? userId, int? contactId, DateTime? lastUpdateTime)
        {
            UserId = userId;
            ContactId = contactId;
            LastUpdateTime = lastUpdateTime;
        }
    }
}
