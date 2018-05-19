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

	    private async Task Button_OnClicked(object sender, EventArgs e)
	    {
	        WorkoutPlan workout = new WorkoutPlan()
	        {
	            WorkoutCategoryId = ViewModel.Category.Id,
	            Description = this.DescriptionEditor.Text,
	            Title = this.TitleEntry.Text
	        };
	        await ViewModel.AddWorkout(workout);
	    }
    }
}