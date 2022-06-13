namespace IO.TedTalk.Core;

public static class AppConsts
{
    public const string AppName = "IO.TedTalk.Api";

    public const string ApiTitle = "IO Digital TedTalk API";
    public const string ApiVersion = "v1";
    public const string ApiURL = "api/v1/swagger.json";

    public static class Database
    {
        static Database()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "io.tedtalk.db");

            CsvFilePath = @"Csv\File\data.csv";
        }

        public static string CsvFilePath { get;  private set; }
        public static string DbPath { get; }
    }
}
