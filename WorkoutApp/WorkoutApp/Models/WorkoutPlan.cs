using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;
using WorkoutApp.Models.dbHelpers;

namespace WorkoutApp.Models
{
    [Table("WorkoutPlans")]
    public class WorkoutPlan
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }

        [ManyToMany(typeof(WorkoutExcercises))]
        public List<Excercise> Excercises { get; set; }
        public int WorkoutCategoryId { get; set; }
    }
}
