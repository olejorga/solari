using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solari.Data.Access.Models;
using Solari.Data.Access.Repositories;

namespace Solari.Data.Access
{
    class Program
    {
        public static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            Console.WriteLine("Hello World!");
        }
    }
}
