using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WorkoutApp.Models;
using WorkoutApp.Views;
using Xamarin.Forms;

namespace WorkoutApp.ViewModels
{
    public class ExcercisesViewModel : AppViewModelBase
    {
        private ObservableCollection<Excercise> _excercises = new ObservableCollection<Excercise>();
        private ObservableCollection<BodyPart> _bodyParts = new ObservableCollection<BodyPart>();

        private Command<BodyPart> _navigateToCategoryCommand;
        public Command<BodyPart> NavigateToCategoryCommand => _navigateToCategoryCommand ?? (_navigateToCategoryCommand = new Command<BodyPart>(NavigateToBodyPart));

        private Command<Excercise> _navigateToExcerciseCommand;
        public Command<Excercise> NavigateToExcerciseCommand => _navigateToExcerciseCommand ?? (_navigateToExcerciseCommand = new Command<Excercise>(NavigateToExcercise));
        private Command _navigateToExcerciseCreationCommand;
        public Command NavigateToExcerciseCreationCommand => _navigateToExcerciseCreationCommand ?? (_navigateToExcerciseCreationCommand = new Command(NavigateToExcerciseCreation));

        public Excercise ExcerciseProp
        {
            get;
            set;
        }

        public BodyPart BodyPartProp
        {
            get;
            set;
        }

        public ObservableCollection<BodyPart> BodyParts
        {
            get => _bodyParts;
            set
            {
                _bodyParts = value;
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

        private async void NavigateToExcercise(Excercise excercise)
        {
            this.ExcerciseProp = excercise;
            await Navigation.PushAsync(new ExcerciseDetail()
            {
                Title = excercise.Title,
                ViewModel = this
            });
        }

        private async void NavigateToBodyPart(BodyPart part)
        {
            this.BodyPartProp = part;
            await Navigation.PushAsync(new Excercises()
            {
                Title = part.Title,
                ViewModel = this
            });
        }


        private async void NavigateToExcerciseCreation()
        {
            await Navigation.PushAsync(new ExcerciseCreation()
            {
                Title = "New " + BodyPartProp.ToString() + " excercise",
                ViewModel = this
            });
        }

        public async Task AddExcercise(Excercise excercise)
        {
            await WorkoutDb.TryCreateExcerciseAsync(excercise);
            await Navigation.PopAsync();
        }

        public async Task LoadExcercises()
        {
            Excercises = new ObservableCollection<Excercise>(
                await WorkoutDb.TryGetAllExcercisesInBodyPartAsync(BodyPartProp.Id)
            );
        }
        public async Task LoadBodyParts()
        {
            if(BodyParts.Count == 0)
                BodyParts = new ObservableCollection<BodyPart>(
                    await WorkoutDb.TryGetAllBodyPartsAsync()
                );
        }
    }
}
