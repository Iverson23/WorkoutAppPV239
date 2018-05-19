using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using WorkoutApp.Models;
using WorkoutApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExcerciseCreation : ContentPage
	{
	    public ExcercisesViewModel ViewModel
	    {
	        get => BindingContext as ExcercisesViewModel;
	        set => BindingContext = value;
	    }

        public ExcerciseCreation ()
		{
			InitializeComponent ();
		}

	    private async Task Button_OnClicked(object sender, EventArgs e)
	    {
	        Excercise excercise = new Excercise() {BodyPartId = ViewModel.BodyPartProp.Id, Description = this.DescriptionEditor.Text, Title = this.TitleEntry.Text};
	        await ViewModel.AddExcercise(excercise);
	    }
	}
}