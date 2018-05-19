using WorkoutApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExcerciseBodyParts : ContentPage
	{
	    public ExcercisesViewModel ViewModel => BindingContext as ExcercisesViewModel;

        public ExcerciseBodyParts ()
		{
			InitializeComponent ();
		}

	    protected async override void OnAppearing()
	    {
	        await ViewModel.LoadBodyParts();
	        base.OnAppearing();
	    }
    }
}