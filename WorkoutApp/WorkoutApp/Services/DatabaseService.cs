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

            await PopulateDatabase();
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
                    "The lunge is a classic fitness exercise for the lower body, which helps the flexibility of the hips and hamstrings, and the strength of the buttocks, hamstrings and hip flexors.",
                Sets = 3,
                Reps = 20
            };
            await TryCreateExcerciseAsync(lunges);

            Excercise squat = new Excercise()
            {
                Title = "Squat",
                BodyPartId = legsBodyPart.Id,
                Description = "They’re sometimes referred to as the king of all exercises, and with good reason. Squats are a full-body fitness staple that work the hips, glutes, quads, and hamstrings, and sneakily strengthen the core. Squats may help improve balance and coordination, as well as bone density . Plus, they’re totally functional. Time to banish those sloppy squats and help perfect the go-to move.",
                Sets = 5,
                Reps = 5
            };
            await TryCreateExcerciseAsync(squat);

            var running = new Excercise()
            {
                Title = "Running",
                BodyPartId = legsBodyPart.Id,
                Description = "Running is a method of terrestrial locomotion allowing humans and other animals to move rapidly on foot.",
                Sets = 10,
                Reps = 20
            };
            await TryCreateExcerciseAsync(running);

            var crunches = new Excercise()
            {
                Title = "Crunches",
                BodyPartId = absBodyPart.Id,
                Description = "The crunch is one of the most popular abdominal exercises. It primarily works the rectus abdominis muscle and also works the obliques.",
                Sets = 3,
                Reps = 30
            };
            await TryCreateExcerciseAsync(crunches);

            var abWheel = new Excercise()
            {
                Title = "Ab wheel rollout",
                BodyPartId = absBodyPart.Id,
                Description = "Classic abdominal exercise to build strength and muscular density in the abs using an ab wheel."
            };
            await TryCreateExcerciseAsync(abWheel);

            var pullup = new Excercise()
            {
                Title = "Pull up",
                BodyPartId = backBodyPart.Id,
                Description = "The pull up is perhaps the best exercise you can do if you want to build a strong, lean upper body. All you need is a bar that will support your weight.",
                Sets = 3,
                Reps = 10
            };
            await TryCreateExcerciseAsync(pullup);

            var chinup = new Excercise()
            {
                Title = "Chin up",
                BodyPartId = backBodyPart.Id,
                Description = "The chin-up is a strength training exercise. People frequently do this exercise with the intention of strengthening muscles such as the latissimus dorsi and biceps, which extend the shoulder and flex the elbow, respectively."
            };
            await TryCreateExcerciseAsync(chinup);

            var dl = new Excercise()
            {
                Title = "Deadlift",
                BodyPartId = backBodyPart.Id,
                Description = "The deadlift is a weight training exercise in which a loaded barbell or bar is lifted off the ground to the level of the hips, then lowered to the ground.[1] It is one of the three powerlifting exercises, along with the squat and bench press.",
                Sets = 5,
                Reps = 5
            };
            await TryCreateExcerciseAsync(dl);

            var bp = new Excercise()
            {
                Title = "Bench press",
                BodyPartId = chestBodyPart.Id,
                Description = "The bench press is an upper body strength training exercise that consists of pressing a weight upwards from a supine position.The exercise works the pectoralis major as well as supporting chest, arm, and shoulder muscles such as the anterior deltoids, serratus anterior, coracobrachialis, scapulae fixers, trapezii, and the triceps.",
                Sets = 5,
                Reps = 5
            };
            await TryCreateExcerciseAsync(bp);

            var pushup = new Excercise()
            {
                Title = "Push up",
                BodyPartId = chestBodyPart.Id,
                Description = "A push-up (or press-up) is a common calisthenics exercise performed in a prone position by raising and lowering the body using the arms. Push-ups exercise the pectoral muscles, triceps, and anterior deltoids, with ancillary benefits to the rest of the deltoids, serratus anterior, coracobrachialis and the midsection as a whole.",
                Sets = 3,
                Reps = 25
            };
            await TryCreateExcerciseAsync(pushup);

            var barbelPress = new Excercise()
            {
                Title = "Overhead press",
                BodyPartId = shouldersBodyPart.Id,
                Description = "The press, overhead press or shoulder press is a weight training exercise, typically performed while standing, in which a weight is pressed straight upwards from racking position until the arms are locked out overhead.",
                Sets = 5,
                Reps = 5
            };
            await TryCreateExcerciseAsync(barbelPress);

            var raise = new Excercise()
            {
                Title = "Lateral raise",
                BodyPartId = shouldersBodyPart.Id,
                Description = "The dumbbell lateral raise is an isolation exercise that strengthens the entire shoulder.",
                Sets = 3,
                Reps = 15
            };
            await TryCreateExcerciseAsync(raise);

            var curl = new Excercise()
            {
                Title = "Biceps curl",
                BodyPartId = armsBodyPart.Id,
                Description = "Bicep Curl is an upper body strength move that strengthens and sculpts your arms and make your biceps look and feel stronger than ever."
            };
            await TryCreateExcerciseAsync(curl);

            var tri = new Excercise()
            {
                Title = "Skull crusher",
                BodyPartId = armsBodyPart.Id,
                Description = "Lying triceps extensions, also known as skull crushers and French extensions or French presses, are a strength exercise used in many different forms of strength training.Lying triceps extensions are one of the most stimulating exercises to the entire triceps muscle group in the upper arm.",
                Sets = 3,
                Reps = 15
            };
            await TryCreateExcerciseAsync(curl);
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
                crunches,
                pullup,
                pushup,
                raise,
                tri
            });

            await TryCreateWorkoutPlanAsync(new WorkoutPlan()
            {
                Description = "On StrongLifts 5×5 you workout three times a week. Each workout you do three barbell exercises for sets of five reps. The five exercises you’ll do on StrongLifts 5×5 are the Squat, Bench Press, Deadlift, Overhead Press and Barbell Row. Together they work your whole body.",
                Title = "5 x 5",
                WorkoutCategoryId = strengthCategory.Id
            }, new List<Excercise>()
            {
                squat,
                dl,
                bp
            });


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