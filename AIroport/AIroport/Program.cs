using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIroport
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"Welcome to flight information service.
Keep in mind next ReadMe about using our service:
1. If you want to EDIT information please type 'edit' (without qoutes) and follow the next rules.
2. If you want to DISPLAY information about ALL FLIGHTS please type 'print' (without qoutes)
3. If you want to SEARCH for some information please type 'search' (without qoutes) and follow the next rules.");
            
            Console.WriteLine("Please, choose the option...");
           Label: string userEnter = Console.ReadLine();
            
            switch(userEnter)
            {
                case "edit":
                    {
                        Console.WriteLine("edit smth");
                        break;
                    }
                case "print":
                    {
                        Console.WriteLine("display all flights");
                        Flight allFlights = new Flight();
                        allFlights.DisplayAllFlights() + allFlights.DisplayAllFlights();
                            break;
                    }
                case "search":
                    {
                        Console.WriteLine("search smth");
                        break;
                    }
                default:
                    {
                        Console.Write("wrong option\n");
                        goto Label;
                        
                    }
            }
            


                Console.ReadLine();
           
        }
    }
}
