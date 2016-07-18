using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIroport
{
    public class Flight
    {
        //public int flightNumber;
        //public string cityName;
        //public string airlineName;
        //public int terminalNumber;
        Random random = new Random();

        DateTime now = DateTime.Now;

        public string[] cities = new string[] { "Kiev(UA)", "Valencia(ES)", "Milan(ITA)", "Krakow(PL)",
            "London(GB)", "Paris(FR)", "Cardiff(WLS)", "Edinburgh(SC)", "Vienne(AUT)", "Glasgow(SCO)", "Abu-Dabu(UAE)",
            "Miami(USA)", "Karakas(VE)", "Odessa(UA)", "Moscow(RU)", "Tbilisi(GE)", "Amsterdam(NED)" };

        public string[] airLines = new string[] { "British Airways", "UIA", "Fly Dubai", "Wizz Air", "United Flights" };

        public int[] terminalNumbers = new int[1000];   
        
        public string DisplayAllFlights()
        {
            for (int i = 0; i < cities.Length; i++)
            {
                Console.WriteLine(cities[i] + now);
            }
            return cities[i];
        }
        public void DisplayTerminals()
        {
            for (int i=0; i < terminalNumbers.Length; i++ )
            {
                terminalNumbers[i] = random.Next();
            }
        }
    }
}
