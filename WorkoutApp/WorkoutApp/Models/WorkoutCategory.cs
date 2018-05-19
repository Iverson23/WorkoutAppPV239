using SQLite;

namespace WorkoutApp.Models
{
    [Table("WorkoutCategories")]
    public class WorkoutCategory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
