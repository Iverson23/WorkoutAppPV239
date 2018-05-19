using WorkoutApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Excercises : ContentPage
	{
	    public ExcercisesViewModel ViewModel
	    {
	        get => BindingContext as ExcercisesViewModel;
	        set => BindingContext = value;
	    }
        public Excercises ()
		{
			InitializeComponent();
		}

	    protected async override void OnAppearing()
	    {
	        await ViewModel.LoadExcercises();
	        base.OnAppearing();
	    }
    }
}