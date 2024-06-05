﻿namespace MessengerApplication.Models
{
    public class Message
    {
        public int? MessageId { get; set; }
        public string? Text { get; set; }
        public DateTime DateSend { get; set; }
        public bool IsSent { get; set; }
        public Guid UserToId { get; set; }
        public Guid UserFromId { get; set; }
        public virtual User? UserTo { get; set; }
        public virtual User? UserFrom { get; set; }
    }
}
