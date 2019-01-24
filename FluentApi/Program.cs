using System;

namespace FluentApi
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
