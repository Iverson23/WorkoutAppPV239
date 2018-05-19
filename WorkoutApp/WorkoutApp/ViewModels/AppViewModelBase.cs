using WorkoutApp.Services;
using XamarinToolkit.MVVM;

namespace WorkoutApp.ViewModels
{
    public class AppViewModelBase : ViewModelBase
    {
        public DatabaseService WorkoutDb => App.Database;
    }
}
