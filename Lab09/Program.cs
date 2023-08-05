using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace Lab09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string json = File.ReadAllText("../../../data.json");
            Console.WriteLine(json);

            FeatureCollection featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json);
            Console.WriteLine("Deserialized the Json data.");

            Location[] locations = featureCollection.features;
            Console.WriteLine(locations);
            Part1WithLINQ(locations);
            Part2(locations);
            Part3(locations);

        }


        public static void Part1WithLINQ(Location[] items)
        {
            var neighborHoodQuery = from item in items
                                    group item by item.properties.neighborhood into grouped
                                    select new { Key = grouped.Key, Value = grouped.Count() };

            foreach (var location in neighborHoodQuery)
            {
                Console.WriteLine($"{location.Key}: {location.Value}");
            }
        }

        public static void Part2(Location[] items)
        {
            var query = from item in items
                        where item.properties.neighborhood != ""
                        // group item by item.properties,neighborhood into grouped
                        select item;
        }


        public static void Part3(Location[] items)
        {
           // var neighborHoodQuery = from item in items
                                  //  where !string.IsNullOrEmpty(item.properties.neighborhood)
                                  //  group item by item.properties.neighborhood into grouped
                                   // select item;

            var distinctQuery = (from item in items
                                 select item.properties.neighborhood).Distinct();
            var distinctMethod = items.Select(item => item.properties.neighborhood).Distinct();



            foreach (string n in distinctMethod)
            {
                Console.WriteLine(n);
            }

        }

        public static void Part4(Location[] items) 
        {
            var neighborHoodQuery = items
                .Where(item => !string.IsNullOrEmpty(item.properties.neighborhood))
                .Select(item  => item.properties.neighborhood)
                .Distinct();

            foreach(var neighborhood in neighborHoodQuery)
            {
                Console.WriteLine(neighborhood);
            }


        }

        public static void Part5(Location[] items)
        {
            var neighborHoodQuery = items
                .Where(item => !string.IsNullOrEmpty(item.properties.neighborhood))
                .GroupBy(item => item.properties)
        }

    }
}