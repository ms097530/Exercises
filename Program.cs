using System;
using System.Linq;
using static System.Console;
using System.Collections.Generic;
using Schultz.Ch12;
using System.Text;

namespace Exercises
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            using (var db = new Northwind())
            {
                HashSet<string> cities = new HashSet<string>(); // used so that cities aren't repeated
                var customersDescByCity = db.Customers
                    //.AsEnumerable()
                    .OrderBy(p => p.City);
                foreach (Customer cust in customersDescByCity)
                {
                    cities.Add(cust.City);
                }
                WriteLine("Enter the name of one of these cities: ");
                var em = cities.GetEnumerator();    // used to iterate through HashSet
                int i = 0;
                StringBuilder sb = new StringBuilder();
                while (em.MoveNext())       // build string of possible cities
                {
                    if (i % 8 != 0 || i == 0)
                    {
                        sb.Append(em.Current + ", ");
                    }
                    else
                    {
                        sb.Append(em.Current + ",\n");
                    }   
                    ++i;                 
                }

                // remove trailing comma
                int j = sb.Length-1;
                while(true)     
                {
                    if (sb[j] == ',')
                    {
                        sb.Remove(j, 1);
                        break;
                    }
                    --j;
                }

                WriteLine(sb);
                WriteLine();
                
                Write("Enter city name here: ");
                string city = ReadLine();

                var matches = db.Customers      // find customer companies in the city that was input
                    .AsEnumerable()
                    .Where(p => p.City.ToUpper() == city.ToUpper());

                sb.Clear();     // clear StringBuilder to reuse for matching companies
                foreach(Customer match in matches)
                {
                    sb.Append(match.CompanyName + "\n");
                }

                WriteLine($"There are {matches.Count()} matches:");
                WriteLine(sb);
            }

        }
    }
}
