namespace MessengerApplication.Models
{
    public class User 
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime Registered {  get; set; } = DateTime.Now;
        public bool Active { get; set; } = true;
        public string? Password { get; set; } = null;        
    }
}
