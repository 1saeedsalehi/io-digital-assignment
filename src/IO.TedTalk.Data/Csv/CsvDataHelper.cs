using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Io.TedTalk.Core.Entities;
using IO.TedTalk.Core.Exceptions;

namespace Io.TedTalk.Data.Csv;
internal class CsvDataHelper
{
    public List<Ted> ReadCsv(string csvPath)
    {
        try
        {
            using var reader = new StreamReader(csvPath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<TedMapConfiguration>();
            var records = csv.GetRecords<Ted>().ToList();
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
