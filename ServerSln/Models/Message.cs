namespace ServerSln
{
    public class Message
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public int? ContactId { get; set; }

        public DateTime? SendTime { get; set; }

        public DateTime? DeliveryTime { get; set; }

        public string? Content { get; set; }

        public Message(int? userId, int? contactId, DateTime? sendTime, DateTime? deliveryTime, string? content)
        {
            UserId = userId;
            ContactId = contactId;
            SendTime = sendTime;
            DeliveryTime = deliveryTime;
            Content = content;
        }
    }
}
