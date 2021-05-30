using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CV19Console
{
    class Program
    {
        private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(data_url, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

        private static IEnumerable<string> GetDataLines()
        {
            using Stream data_stream = GetDataStream().Result;
            using StreamReader data_reader = new StreamReader(data_stream);

            while (!data_reader.EndOfStream)
            {
                string line = data_reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                yield return line
                    .Replace("Korea,", "Korea - ")
                    .Replace("Bonaire,", "Bonaire - ")
                    .Replace("Helena,", "Helena - ");
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();

        private static IEnumerable<(string Country, string Province, int[] Counts)> GetData()
        {
            IEnumerable<string[]> lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));

            foreach (string[] row in lines)
            {
                string province = row[0].Trim();
                string country_name = row[1].Trim(' ', '"');
                int[] counts = row.Skip(4).Select(int.Parse).ToArray();

                yield return (country_name, province, counts);
            }
        }

        static void Main(string[] args)
        {
            //WebClient client = new WebClient();

            //var client = new HttpClient();

            //HttpResponseMessage response = client.GetAsync(data_url).Result;
            //string csv_str = response.Content.ReadAsStringAsync().Result;

            //foreach (string data_line in GetDataLines())
            //{
            //    Console.WriteLine(data_line);
            //}

            //DateTime[] dates = GetDates();

            //Console.WriteLine(string.Join("\r\n", dates));

            var ukraine_data = GetData()
                .First(v => v.Country.Equals("Ukraine", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine(string.Join("\r\n", GetDates().Zip(ukraine_data.Counts, (date, count) => $"{date:dd.MM.yyyy} - {count}" )));
        }
    }
}
