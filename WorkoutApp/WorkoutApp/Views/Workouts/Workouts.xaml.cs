using WorkoutApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Workouts : ContentPage
	{
	    public WorkoutsViewModel ViewModel
	    {
	        get => BindingContext as WorkoutsViewModel;
	        set => BindingContext = value;
	    }
        public Workouts ()
		{
			InitializeComponent ();
		}

	    protected async override void OnAppearing()
	    {
	        await ViewModel.LoadWorkouts();
	        base.OnAppearing();
	    }
    }
}