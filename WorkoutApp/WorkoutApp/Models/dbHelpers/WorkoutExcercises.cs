using SQLite;
using SQLiteNetExtensions.Attributes;

namespace WorkoutApp.Models.dbHelpers
{
    public class WorkoutExcercises
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [ForeignKey(typeof(Excercise))]
        public int ExcerciseId { get; set; }

        [ForeignKey(typeof(WorkoutPlan))]
        public int WorkoutPlanId { get; set; }

        public int Reps { get; set; }
        public int Sets { get; set; }
    }
}
