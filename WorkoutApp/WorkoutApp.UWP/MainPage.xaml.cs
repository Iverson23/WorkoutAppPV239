namespace WorkoutApp.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new WorkoutApp.App());
        }
    }
}
