using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;
using WorkoutApp.Models.dbHelpers;

namespace WorkoutApp.Models
{
    [Table("Excercises")]
    public class Excercise
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int Sets { get; set; }
        public int Reps { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int BodyPartId { get; set; }

        [ManyToMany(typeof(WorkoutExcercises))]
        public List<WorkoutPlan> Workouts { get; set; }

    }
}
