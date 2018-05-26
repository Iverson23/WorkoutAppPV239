using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WorkoutApp.Models;
using WorkoutApp.Services;
using WorkoutApp.Views;
using Xamarin.Forms;

namespace WorkoutApp.ViewModels
{
    public class WorkoutsViewModel : AppViewModelBase
    {
        private ObservableCollection<WorkoutPlan> _workoutPlans = new ObservableCollection<WorkoutPlan>();
        private ObservableCollection<WorkoutCategory> _categories = new ObservableCollection<WorkoutCategory>();
        private ObservableCollection<Excercise> _excercises = new ObservableCollection<Excercise>();
        private ObservableCollection<Excercise> _dynamicExcercises = new ObservableCollection<Excercise>();

        private Command<WorkoutCategory> _navigateToCategoryCommand;
        public Command<WorkoutCategory> NavigateToCategoryCommand => _navigateToCategoryCommand ?? (_navigateToCategoryCommand = new Command<WorkoutCategory>(NavigateToCategory));

        private Command<WorkoutPlan> _navigateToWorkoutCommand;
        public Command<WorkoutPlan> NavigateToWorkoutCommand => _navigateToWorkoutCommand ?? (_navigateToWorkoutCommand = new Command<WorkoutPlan>(NavigateToWorkout));

        private Command<Excercise> _navigateToExcerciseCommand;
        public Command<Excercise> NavigateToExcerciseCommand => _navigateToExcerciseCommand ?? (_navigateToExcerciseCommand = new Command<Excercise>(NavigateToExcercise));

        private Command _navigateToWorkoutCreationCommand;
        public Command NavigateToWorkoutCreationCommand => _navigateToWorkoutCreationCommand ?? (_navigateToWorkoutCreationCommand = new Command(NavigateToWorkoutCreation));
        private Command<WorkoutPlan> _deleteWorkoutCommand;

        public Command<WorkoutPlan> DeleteWorkoutCommand => _deleteWorkoutCommand ?? (_deleteWorkoutCommand = new Command<WorkoutPlan>(DeleteWorkout));

        private Command<Excercise> _deleteExcerciseFromDynamicsCommand;

        public Command<Excercise> DeleteExcerciseFromDynamicsCommand => _deleteExcerciseFromDynamicsCommand ?? (_deleteExcerciseFromDynamicsCommand = new Command<Excercise>(ex => DynamicExcercises.Remove(ex)));
        public WorkoutPlan Workout { get; set; } = new WorkoutPlan();

        public WorkoutCategory Category
        {
            get;
            set;
        }
        public ObservableCollection<WorkoutPlan> Workouts
        {
            get => _workoutPlans;
            set
            {
                _workoutPlans = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<WorkoutCategory> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Excercise> Excercises
        {
            get => _excercises;
            set
            {
                _excercises = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Excercise> DynamicExcercises
        {
            get => _dynamicExcercises;
            set
            {
                _dynamicExcercises = value;
                OnPropertyChanged();
            }
        }

        private async void NavigateToWorkout(WorkoutPlan workout)
        {
            this.Workout = await WorkoutDb.TryGetWorkoutAsync(workout.Id);
            foreach (var ex in Workout.Excercises)
            {
                var mapping = await WorkoutDb.TryGetWorkoutMappingAsync(workout.Id, ex.Id);
                ex.Sets = mapping.Sets;
                ex.Reps = mapping.Reps;
            }

            await Navigation.PushAsync(new WorkoutDetail()
            {
                Title = workout.Title,
                ViewModel = this
            });
        }
        private async void NavigateToExcercise(Excercise excercise)
        {
            await Navigation.PushAsync(new ExcerciseDetail()
            {
                Title = excercise.Title,
                ViewModel = new ExcercisesViewModel()
                {
                    ExcerciseProp = excercise
                }
            });
        }
        private async void NavigateToCategory(WorkoutCategory category)
        {
            this.Category = category;
            await Navigation.PushAsync(new Workouts()
            {
                Title = category.Title,
                ViewModel = this
            });
        }
        private async void NavigateToWorkoutCreation()
        {
            Workout = new WorkoutPlan();
            DynamicExcercises.Clear();

            await Navigation.PushAsync(new WorkoutCreation()
            {
                Title = "New " + Category.Title + " workout",
                ViewModel = this
            });
        }

        public async Task NavigateToWorkoutExcerciseAdding()
        {
            await Navigation.PushAsync(new WorkoutAddExcercise()
            {
                Title = "Add new excercise",
                ViewModel = this
            });
        }

        private async void DeleteWorkout(WorkoutPlan workout)
        {
            if (await WorkoutDb.TryDeleteWorkoutPlanAsync(workout))
            {
                Workouts.Remove(workout);
            }
        }
        public async Task LoadWorkouts()
        {
            Workouts = new ObservableCollection<WorkoutPlan>(
                await WorkoutDb.TryGetAllWorkoutsInCategoryAsync(Category.Id)
            );
        }
        public async Task LoadCategories()
        {
            if(Categories.Count == 0)
                Categories = new ObservableCollection<WorkoutCategory>(
                    await WorkoutDb.TryGetAllWorkoutCategoriesAsync()
                );
        }
        public async Task LoadExcercises()
        {
            Excercises = new ObservableCollection<Excercise>(
                await WorkoutDb.TryGetAllExcercisesAsync()
            );
        }

        public async Task AddWorkout()
        {
            await WorkoutDb.TryCreateWorkoutPlanAsync(Workout, DynamicExcercises.ToList());
            await Navigation.PopAsync();
        }
    }
}
