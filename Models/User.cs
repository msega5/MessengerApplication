namespace MessengerApplication.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Email { get; set; } = null!;
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
        public RoleId RoleId { get; set; }
        public virtual Role Role { get; set; }
        //public string? FirstName { get; set; }
        //public string? LastName { get; set; }
        //public DateTime Registered { get; set; } = DateTime.Now;
        //public bool Active { get; set; } = true;
    }
}
