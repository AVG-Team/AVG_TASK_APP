using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVG_TASK_APP.Models
{
    internal class Person
    {
        public string Name { get; set; }
        private static Random Randomizer = new Random();
        public static List<Person> Generate(int qty)
        {
            var items = new List<Person>();
            for (int i = 0; i < qty; i++)
            {
                items.Add(new Person()
                {
                    Name = Names[Randomizer.Next(Names.Length)] + " " + Surnames[Randomizer.Next(Surnames.Length)],
                });
            }
            return items;
        }
        private static string[] Names = new string[]
        {
            "Michael",
            "John",
            "Bernardo",
            "Alvaro",
            "Max",
            "Leonard",
            "Noela",
            "Gabriel",
            "Bruno",
            "Rodrigo",
            "Sheela",
            "Chris",
            "Martin",
            "Ben",
            "Alex",
            "Irina",
            "Dave",
            "Patric",
        };
        private static string[] Surnames = new string[]
         {
            "Johnson",
            "Alvarez",
            "Maxtor",
            "Johansen",
            "Vera",
            "García",
            "Days",
            "Castle",
            "Varela",
            "Smith",
            "Gates",
            "Meredith",
            "Drexler",
            "Beckham",
            "Jobs",
         };
    }
}
