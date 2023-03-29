namespace RPG_Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];

        /* ONE TO MANY*/
        /* USER will have access to MANY(ALL) CHARACTERS(MODEL)*/
        public List<Character>? Characters { get; set; }
    }
}
