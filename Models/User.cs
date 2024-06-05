namespace MessengerApplication.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; } = null!;
        public byte[] Salt { get; set; } = null!;
        public RoleId RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;
        public virtual List<Message>? MessageTo { get; set; } = new();
        public virtual List<Message>? MessageFrom { get; set; } = new();
    }
}
