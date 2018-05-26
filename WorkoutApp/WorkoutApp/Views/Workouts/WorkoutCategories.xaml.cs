using WorkoutApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorkoutCategories : ContentPage
	{
	    public WorkoutsViewModel ViewModel => BindingContext as WorkoutsViewModel;

	    public WorkoutCategories ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
	    {
            await ViewModel.LoadCategories();
	        base.OnAppearing();
	    }
    }
}