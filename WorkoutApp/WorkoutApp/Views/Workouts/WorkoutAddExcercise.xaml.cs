using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutApp.Models;
using WorkoutApp.Models.dbHelpers;
using WorkoutApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorkoutAddExcercise : ContentPage
	{
	    public WorkoutsViewModel ViewModel
	    {
	        get => BindingContext as WorkoutsViewModel;
	        set => BindingContext = value;
	    }
        public WorkoutAddExcercise ()
		{
			InitializeComponent ();
		}
	    protected override async void OnAppearing()
	    {
	        await ViewModel.LoadExcercises();
	        base.OnAppearing();
	    }

	    private async void AddExcerciseButton_OnClicked(object sender, EventArgs e)
	    {
            Excercise excercise = excercisesView.SelectedItem as Excercise;
	        excercise.Reps = Int32.Parse(Reps.Text);
	        excercise.Sets = Int32.Parse(Sets.Text);
            ViewModel.DynamicExcercises.Add(excercise);
	        await Navigation.PopAsync();
        }
	}
}