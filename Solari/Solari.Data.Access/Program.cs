using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Solari.Data.Access.Models;

namespace Solari.Data.Access
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using var db = new SolariContext();

            var airlines = db.Airlines.Include(a => a.Flights).ToList();

            foreach(Airline a in airlines)
            {
                Console.WriteLine(a.Name);

                foreach(Flight f in a.Flights)
                {
                    Console.WriteLine(f.FlightNumber);
                }
            }
        }
    }
}
