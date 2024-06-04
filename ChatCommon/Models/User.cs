namespace MessengerApplication.ChatCommon.Models
{
    public class User
    {
        public virtual List<Message>? MessageTo { get; set; } = new();
        public virtual List<Message>? MessageFrom { get; set; } = new();
        public int Id { get; set; }
        public string? FullName { get; set; }
    }
}
