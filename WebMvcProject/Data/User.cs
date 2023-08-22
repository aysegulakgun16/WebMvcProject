namespace WebMvcProject.Data
{
    public class User
    {
        public string Id { get; init; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string? PasswordHash { get; set; }

        public string? PasswordSalt { get; set; }

        public List<Role> Roles { get; set; }


        public User()
        {
            Id= Guid.NewGuid().ToString();
        }


    }
}
