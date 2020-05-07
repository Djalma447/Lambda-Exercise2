using System;
using Course.Entities;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace Course
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();

            using (StreamReader sr = File.OpenText(path))
            {
                List<Product> list = new List<Product>();
                while (!sr.EndOfStream)
                {
                    string[] values = sr.ReadLine().Split(',');
                    string name = values[0];
                    double price = double.Parse(values[1], CultureInfo.InvariantCulture);
                    list.Add(new Product(name, price));
                }
                var average = list.Select(p => p.Price).DefaultIfEmpty(0.0).Average();
                Console.WriteLine("Average price: " + average.ToString("F2", CultureInfo.InvariantCulture));
                var names = list.Where(p => p.Price < average).OrderByDescending(p => p.Name).Select(p => p.Name);
                foreach(string s in names)
                {
                    Console.WriteLine(s);
                }
            }
        }
    }
}
