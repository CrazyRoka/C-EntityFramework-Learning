using System;

namespace MenusSample
{
    class Program
    {
        static void Main(string[] args)
        {
            MenusContext context = new MenusContext();
            bool created = context.Database.EnsureCreated();
            Console.WriteLine(created);
        }
    }
}
