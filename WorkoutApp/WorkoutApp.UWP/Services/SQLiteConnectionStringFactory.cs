using System.IO;
using Windows.Storage;
using WorkoutApp.UWP.Services;
using XamarinToolkit.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteConnectionStringFactory))]

namespace WorkoutApp.UWP.Services
{
    public class SQLiteConnectionStringFactory : ISQLiteConnectionStringFactory
    {
        public string Create(string name) => Path.Combine(ApplicationData.Current.LocalFolder.Path, name);
    }
}