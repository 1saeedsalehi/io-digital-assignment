using System.Globalization;
using CsvHelper;
using Io.TedTalk.Core.Entities;

namespace Io.TedTalk.Data.Csv;
public static class CsvDataHelper
{
    public static IEnumerable<Ted> ReadCsv(string filePath)
    {
        try
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<TedMapConfiguration>();
            var records = csv.GetRecords<Ted>().ToList();

            //Workaround to fill Id's in SQLite :)
            var id = 1;
            records.ForEach(record => record.Id = id++);


            return records;
        }
        catch (CsvHelperException e)
        {
            throw new IO.TedTalk.Core.Exceptions.IOException("an error occured during reading csv file", e);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
