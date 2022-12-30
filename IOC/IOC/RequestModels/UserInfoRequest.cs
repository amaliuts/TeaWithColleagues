namespace IOC.RequestModels
{
    public class UserInfoRequest
    {
        public UserInfoRequest(string name, string surname, string phNumber)
        {
            Name = name;
            Surname = surname;
            PhoneNumber = phNumber;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
    }
}
