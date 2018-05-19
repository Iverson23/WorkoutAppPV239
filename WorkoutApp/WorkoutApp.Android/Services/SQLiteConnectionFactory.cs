using System.IO;
using WorkoutApp.Droid.Services;
using XamarinToolkit.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteConnectionFactory))]

namespace WorkoutApp.Droid.Services
{
    public class SQLiteConnectionFactory : ISQLiteConnectionStringFactory
    {
        public string Create(string name)
        {
            var directory = System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Personal);

            return Path.Combine(directory, name);
        }
    }
}