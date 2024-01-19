namespace ClientDashboardAPI.DTOs
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Archive { get; set; }
    }
}