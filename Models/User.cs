namespace MessengerApplication.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Email { get; set; } = null!;
        public string? FullName { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
        public RoleId RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual List<Message>? MessageTo { get; set; } = new();
        public virtual List<Message>? MessageFrom { get; set; } = new();
    }
}
