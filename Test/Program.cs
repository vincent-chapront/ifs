using System;
using System.IO;
using October.Service;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists("simple.db")) File.Delete("simple.db");

            ProjetService.AddProjet("test", 5, 3.6M, "A", DateTime.Now, 100M);
            ProjetService.AddProjet("test2", 5, 3.6M, "A", DateTime.Now, -1M);

            var p1 = ProjetService.get("test");
            var p2 = ProjetService.get("test2");
            var p3 = ProjetService.get("test3");


            Console.WriteLine("Hello World!");
        }
    }
}
