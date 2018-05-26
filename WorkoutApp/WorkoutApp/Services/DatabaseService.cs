using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using WorkoutApp.Models;
using WorkoutApp.Models.dbHelpers;
using XamarinToolkit.Storage;

namespace WorkoutApp.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string name, ISQLiteConnectionStringFactory connectionFactory)
        {
            _database = new SQLiteAsyncConnection(connectionFactory.Create(name));
        }

        #region init
        public async Task InitDatabase()
        {
            await _database.CreateTableAsync<WorkoutExcercises>();
            await _database.CreateTableAsync<WorkoutCategory>();
            await _database.CreateTableAsync<Excercise>();
            await _database.CreateTableAsync<WorkoutPlan>();
            await _database.CreateTableAsync<BodyPart>();

            
            //await PopulateDatabase();
        }

        private async Task PopulateDatabase()
        {
            #region BodyParts

            var absBodyPart = new BodyPart()
            {
                Title = "Abs",
            };
            await TryCreateBodyPartAsync(absBodyPart);

            var legsBodyPart = new BodyPart()
            {
                Title = "Legs",
            };
            await TryCreateBodyPartAsync(legsBodyPart);

            var chestBodyPart = new BodyPart()
            {
                Title = "Chest",
            };
            await TryCreateBodyPartAsync(chestBodyPart);

            var shouldersBodyPart = new BodyPart()
            {
                Title = "Shoulders",
            };
            await TryCreateBodyPartAsync(shouldersBodyPart);

            var armsBodyPart = new BodyPart()
            {
                Title = "Arms",
            };
            await TryCreateBodyPartAsync(armsBodyPart);

            var backBodyPart = new BodyPart()
            {
                Title = "Back",
            };
            await TryCreateBodyPartAsync(backBodyPart);

            #endregion

            #region WorkoutCategories

            var cardioCategory = new WorkoutCategory()
            {
                Title = "Cardio",
                Description = "Cardio workout is any workout that raises your heart rate.",
            };
            await TryCreateWorkoutCategoryAsync(cardioCategory);

            var strengthCategory = new WorkoutCategory()
            {
                Title = "Strength",
                Description = "Strength training sessions are designed to impose increasingly greater resistance, which in turn stimulates development of muscle strength to meet the added demand.",
            };
            await TryCreateWorkoutCategoryAsync(strengthCategory);

            var volumeCategory = new WorkoutCategory()
            {
                Title = "Volume",
                Description = "blalba",
            };
            await TryCreateWorkoutCategoryAsync(volumeCategory);
            #endregion

            #region excercises

            Excercise lunges = new Excercise()
            {
                Title = "Lunges",
                BodyPartId = legsBodyPart.Id,
                Description =
                    "The lunge is a classic fitness exercise for the lower body, which helps the flexibility of the hips and hamstrings, and the strength of the buttocks, hamstrings and hip flexors."
            };
            await TryCreateExcerciseAsync(lunges);

            Excercise squat = new Excercise()
            { 
                Title = "Squat",
                BodyPartId = legsBodyPart.Id,
                Description = "They’re sometimes referred to as the king of all exercises, and with good reason. Squats are a full-body fitness staple that work the hips, glutes, quads, and hamstrings, and sneakily strengthen the core. Squats may help improve balance and coordination, as well as bone density . Plus, they’re totally functional. Time to banish those sloppy squats and help perfect the go-to move.",
            };
            await TryCreateExcerciseAsync(squat);

            var crunches = new Excercise()
            {
                Title = "Crunches",
                BodyPartId = absBodyPart.Id,
                Description = "The crunch is one of the most popular abdominal exercises. It primarily works the rectus abdominis muscle and also works the obliques.",
            };
            await TryCreateExcerciseAsync(crunches);

            var running = new Excercise()
            {
                Title = "Running",
                BodyPartId = legsBodyPart.Id,
                Description = "Running is a method of terrestrial locomotion allowing humans and other animals to move rapidly on foot."
            };
            await TryCreateExcerciseAsync(running);
            #endregion

            #region workouts

            var hiit = new WorkoutPlan()
            {
                Description = "HIIT, or high-intensity interval training, is a training technique in which you give all-out, one hundred percent effort through quick, intense bursts of exercise, followed by short, sometimes active, recovery periods.",
                Title = "HIIT",
                WorkoutCategoryId = cardioCategory.Id,
            };
            await TryCreateWorkoutPlanAsync(hiit, new List<Excercise>() { running });

            var highRep = new WorkoutPlan()
            {
                Description = "You perform a burnout set per body part to completely fatigue the muscles. A burnout set involves lighter weights and very high repetitions -- from 15 to up to 100, to encourage maximal glycogen depletion.",
                Title = "High rep burn",
                WorkoutCategoryId = cardioCategory.Id,
            };
            await TryCreateWorkoutPlanAsync(highRep, new List<Excercise>()
            {
                lunges,
                squat,
                crunches
            });

            await TryCreateWorkoutPlanAsync(new WorkoutPlan()
            {
                Description = "On StrongLifts 5×5 you workout three times a week. Each workout you do three barbell exercises for sets of five reps. The five exercises you’ll do on StrongLifts 5×5 are the Squat, Bench Press, Deadlift, Overhead Press and Barbell Row. Together they work your whole body.",
                Title = "5 x 5",
                WorkoutCategoryId = strengthCategory.Id
            }, null);


            #endregion
        }

        private async Task CreateMapping(int workoutId, int excerciseId, int sets, int reps)
        {
                var mapping = await TryGetWorkoutMappingAsync(workoutId, excerciseId);
                mapping.Sets = sets;
                mapping.Reps = reps;
                await _database.UpdateAsync(mapping);
        }

        #endregion

        #region Read
        public async Task<IList<WorkoutCategory>> TryGetAllWorkoutCategoriesAsync()
        {
            try
            {
                return await _database.Table<WorkoutCategory>().ToListAsync();
            }
            catch
            {
                return new List<WorkoutCategory>();
            }
        }
        public async Task<IList<BodyPart>> TryGetAllBodyPartsAsync()
        {
            try
            {
                return await _database.Table<BodyPart>().ToListAsync();
            }
            catch
            {
                return new List<BodyPart>();
            }
        }
        public async Task<IList<WorkoutPlan>> TryGetAllWorkoutsInCategoryAsync(int categoryId)
        {
            try
            {
                return await _database.Table<WorkoutPlan>().Where(wp => wp.WorkoutCategoryId == categoryId).ToListAsync();
            }
            catch
            {
                return new List<WorkoutPlan>();
            }
        }

        public async Task<WorkoutPlan> TryGetWorkoutAsync(int workoutId)
        {
            try
            {
                return await _database.GetWithChildrenAsync<WorkoutPlan>(workoutId);
            }
            catch
            {
                return new WorkoutPlan();
            }
        }

        public async Task<WorkoutExcercises> TryGetWorkoutMappingAsync(int workoutId, int excerciseId)
        {
            try
            {
                return await _database.Table<WorkoutExcercises>()
                    .Where(we => we.ExcerciseId == excerciseId && we.WorkoutPlanId == workoutId).FirstOrDefaultAsync();
            }
            catch
            {
                return new WorkoutExcercises();
            }
        }

        public async Task<IList<Excercise>> TryGetAllExcercisesInBodyPartAsync(int bodyPartId)
        {
            try
            {
                return await _database.Table<Excercise>().Where(ex => ex.BodyPartId == bodyPartId).ToListAsync();
            }
            catch
            {
                return new List<Excercise>();
            }
        }

        public async Task<IList<Excercise>> TryGetAllExcercisesAsync()
        {
            try
            {
                return await _database.Table<Excercise>().ToListAsync();
            }
            catch
            {
                return new List<Excercise>();
            }
        }

        #endregion

        #region Create
        public async Task<bool> TryCreateBodyPartAsync(BodyPart part)
        {
            try
            {
                await _database.InsertAsync(part);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> TryCreateWorkoutCategoryAsync(WorkoutCategory category)
        {
            try
            {
                await _database.InsertAsync(category);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> TryCreateExcerciseAsync(Excercise excercise)
        {
            try
            {
                await _database.InsertAsync(excercise);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> TryAddExcerciseToWorkoutAsync(Excercise excercise, WorkoutPlan workout)
        {
            try
            {
                await _database.InsertAsync(excercise);
                excercise.Workouts = new List<WorkoutPlan>() { workout };

                await _database.UpdateWithChildrenAsync(excercise);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> TryCreateWorkoutPlanAsync(WorkoutPlan workout, List<Excercise> excercises)
        {
            try
            {
                await _database.InsertAsync(workout);
                workout.Excercises = excercises;
                await _database.UpdateWithChildrenAsync(workout);
                foreach (var ex in workout.Excercises)
                {
                    await CreateMapping(workout.Id, ex.Id, ex.Sets, ex.Reps);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Delete
        public async Task<bool> TryDeleteWorkoutPlanAsync(WorkoutPlan workout)
        {
            try
            {
                await _database.DeleteAsync(workout);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> TryDeleteExcerciseAsync(Excercise excercise)
        {
            try
            {
                await _database.DeleteAsync(excercise);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
