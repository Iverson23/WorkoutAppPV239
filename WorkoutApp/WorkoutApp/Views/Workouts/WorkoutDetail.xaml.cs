using WorkoutApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorkoutDetail : ContentPage
	{
	    public WorkoutsViewModel ViewModel
	    {
	        get => BindingContext as WorkoutsViewModel;
	        set => BindingContext = value;
	    }
        public WorkoutDetail ()
		{
			InitializeComponent ();
		}
    }
}