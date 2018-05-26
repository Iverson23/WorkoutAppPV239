using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutApp.Models;
using WorkoutApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorkoutCreation : ContentPage
	{
	    public WorkoutsViewModel ViewModel
	    {
	        get => BindingContext as WorkoutsViewModel;
	        set => BindingContext = value;
	    }

        public WorkoutCreation ()
		{
			InitializeComponent ();
		}

	    private async Task AddPlanButton_OnClicked(object sender, EventArgs e)
	    {
	        ViewModel.Workout.WorkoutCategoryId = ViewModel.Category.Id;
	        ViewModel.Workout.Description = this.DescriptionEditor.Text;
            ViewModel.Workout.Title = this.TitleEntry.Text;
	        await ViewModel.AddWorkout();
	    }

	    private async Task AddExcerciseButton_OnClicked(object sender, EventArgs e)
	    {
	        await ViewModel.NavigateToWorkoutExcerciseAdding();
        }
    }
}