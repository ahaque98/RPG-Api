using RPG_Api.Models.Enum;

namespace RPG_Api.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int Hitpoint { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defence { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RPG_Class Charaters { get; set; } = RPG_Class.Knight;
        public User? User { get; set; }

    }
}
