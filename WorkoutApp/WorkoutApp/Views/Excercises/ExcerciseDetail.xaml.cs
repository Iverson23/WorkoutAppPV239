using WorkoutApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExcerciseDetail : ContentPage
	{
	    public ExcercisesViewModel ViewModel
	    {
            get => BindingContext as ExcercisesViewModel;
	        set => BindingContext = value;
	    }

	    public ExcerciseDetail ()
		{
			InitializeComponent ();
		}
    }
}