using System.Collections.ObjectModel;
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

        private Command<WorkoutCategory> _navigateToCategoryCommand;
        public Command<WorkoutCategory> NavigateToCategoryCommand => _navigateToCategoryCommand ?? (_navigateToCategoryCommand = new Command<WorkoutCategory>(NavigateToCategory));

        private Command<WorkoutPlan> _navigateToWorkoutCommand;
        public Command<WorkoutPlan> NavigateToWorkoutCommand => _navigateToWorkoutCommand ?? (_navigateToWorkoutCommand = new Command<WorkoutPlan>(NavigateToWorkout));

        private Command<Excercise> _navigateToExcerciseCommand;
        public Command<Excercise> NavigateToExcerciseCommand => _navigateToExcerciseCommand ?? (_navigateToExcerciseCommand = new Command<Excercise>(NavigateToExcercise));

        public WorkoutPlan Workout
        {
            get;
            set;
        }

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

        public async Task AddWorkout(WorkoutPlan workout)
        {
            await WorkoutDb.TryCreateWorkoutPlanAsync(workout);
            await Navigation.PopAsync();
        }
    }
}
