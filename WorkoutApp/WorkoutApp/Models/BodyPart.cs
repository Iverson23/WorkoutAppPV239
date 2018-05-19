using SQLite;

namespace WorkoutApp.Models
{
    [Table("BodyParts")]
    public class BodyPart
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
