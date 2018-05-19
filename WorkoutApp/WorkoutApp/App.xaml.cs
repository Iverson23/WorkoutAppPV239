using WorkoutApp.Services;
using Xamarin.Forms;
using XamarinToolkit.Storage;

namespace WorkoutApp
{
	public partial class App : Application
	{
	    public static DatabaseService Database { get; private set; }
	        = new DatabaseService("WorkoutApp.sqlite", DependencyService.Get<ISQLiteConnectionStringFactory>());

        public App ()
		{
			InitializeComponent();
		    MainPage = new NavigationPage(new Views.MainPage());
		}

	    protected async override void OnStart()
	    {
	        await Database.InitDatabase();
	    }

        protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
