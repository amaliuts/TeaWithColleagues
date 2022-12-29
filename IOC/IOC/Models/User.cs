namespace IOC.Models
{
    public class User
    {
        int idUser { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Availability> Availabilities { get; } = new List<Availability>();
    }
}
