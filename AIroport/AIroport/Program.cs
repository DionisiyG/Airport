using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport
{
    class Program
    {
        enum FlightStatus : int
        { CheckIn, GateClosed, Arrived, DepartedAt, Unknown, Canceled, ExpectedAt, Delayed, InFlight }

        struct Flight
        {
            public int flightNumber;
            public DateTime flightDT;
            public string cityName;
            public string airLine;
            public byte terminalNumber;
            public FlightStatus flightStatus;

            public Flight(int flightNumber, DateTime flightDT, string cityName, string airLine, byte terminalNumber, FlightStatus flightStatus)
            {
                this.flightNumber = flightNumber;
                this.flightDT = flightDT;
                this.cityName = cityName;
                this.airLine = airLine;
                this.terminalNumber = terminalNumber;
                this.flightStatus = flightStatus;
            }
            public override string ToString()
            {
                return string.Format("| {0,-15}| {1,-20}| {2,-25}| {3,-25}| {4,-10}| {5,-15} |", this.flightNumber, this.flightDT, this.cityName, this.airLine, this.terminalNumber, this.flightStatus);
            }
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(150, 20);
            Flight?[,] FlightsInfo = new Flight?[2, 20];

            FillingFlightInfo(FlightsInfo);
            do
            {
                //Console.Clear();
                Console.WriteLine(@"Welcome to flight information service!
Please keep in touch with following features:
1. View the information about  all Flights
2. View the information about Arrivals
3. View the information about Depatures
4. Create new Flight information
5. Edit Flight information
6. Delete Flight information
7. Various search
8. Search to the nearest (1 hour) Flight");

                byte mode = 0;
                bool tryInput = byte.TryParse(Console.ReadLine(), out mode);
                if (!tryInput)
                {
                    Console.WriteLine("Please enter valid input.");
                }
                else
                {
                    switch (mode)
                    {
                        case 1:
                            ShowAllFlights(FlightsInfo, mode);
                            break;
                        case 2:
                            ShowAllFlights(FlightsInfo, mode);
                            break;
                        case 3:
                            ShowAllFlights(FlightsInfo, mode);
                            break;
                        case 4:
                            CreateNewFlight(FlightsInfo);
                            break;
                        case 5:
                            EditFlight(FlightsInfo);
                            break;
                        case 6:
                            DeleteFlight(FlightsInfo);
                            break;
                        case 7:
                            SearchingMenu(FlightsInfo);
                            break;
                        case 8:
                            Searching(FlightsInfo, mode);
                            break;
                        default:
                            Console.WriteLine("Incorrect input");
                            break;
                    }
                    Console.WriteLine(@"Press Esc to exit.
Enter to go back.");
                }
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        #region Creating information about the flights
        static void FillingFlightInfo(Flight?[,] FlightsInfo)
        {
            Console.Clear();
            Random random = new Random();
            DateTime dateTime = new DateTime();
            int month = 0;
            int amountToFill = 10;

            string[] cityName = new string[] { "Kiev", "Valencia", "Milan", "Krakow", "London", "Paris", "Cardiff" };
            string[] airLine = new string[] { "UIA", "British Airways", "Wizz Air", "United Flights", "British Airways", "Fly Dubai", "Motor Sich" };

            for (int i = 0; i < FlightsInfo.GetLength(0); i++)
            {
                for (int j = 0; j < amountToFill; j++)
                {
                    month = random.Next(1, 13);
                    if (month % 2 == 1)
                        dateTime = new DateTime(random.Next(2016, 2017), month, random.Next(1, 32), random.Next(0, 24), random.Next(0, 60), random.Next(0, 60));
                    else if ((month != 2) && (month % 2 == 0))
                        dateTime = new DateTime(random.Next(2016, 2017), month, random.Next(1, 31), random.Next(0, 24), random.Next(0, 60), random.Next(0, 60));
                    else if (month == 2)
                        dateTime = new DateTime(random.Next(2016, 2017), month, random.Next(1, 29), random.Next(0, 24), random.Next(0, 60), random.Next(0, 60));

                    FlightsInfo[i, j] = new Flight(random.Next(1, 50), dateTime, cityName[random.Next(0, 7)], airLine[random.Next(0, 7)], (byte)random.Next(1, 15), (FlightStatus)(random.Next(0, 9)));
                }
            }
        }
        #endregion

        #region Showing all flights
        static void ShowAllFlights(Flight?[,] FlightsInfo, byte mode)
        {
            //  Console.WriteLine("{0,50}", "All Flights info");
            int index = 0;
            switch (mode)
            {
                case 1:
                    CreateMainHeader(mode);
                    PrintArrivalHeader();
                    for (int i = 0; i < FlightsInfo.GetLength(0); i++)
                    {
                        for (int j = 0; j < FlightsInfo.GetLength(1); j++)
                        {
                            if (FlightsInfo?[i, j] != null)
                            {
                                index++;
                                Console.WriteLine("| {0, -4}{1}", i.ToString() + " " + j.ToString(), FlightsInfo[i, j].ToString());
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (index == 0)
                        {
                            Console.WriteLine("Sorry! There is no suitable flights");
                        }
                        index = 0;
                        if (i != 1)
                        {
                            PrintDepatureHeader();
                        }
                    }
                    break;

                case 2:
                    CreateMainHeader(mode);
                    for (int j = 0; j < FlightsInfo.GetLength(1); j++)
                    {
                        if (FlightsInfo[0, j] != null)
                        {
                            index++;
                            Console.WriteLine("| {0, -4}{1}", 0.ToString() + " " + j.ToString(), FlightsInfo[0, j].ToString());
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (index == 0)
                    {
                        Console.WriteLine("Sorry! There is no suitable flights");
                    }
                    break;

                case 3:
                    CreateMainHeader(mode);
                    for (int j = 0; j < FlightsInfo.GetLength(1); j++)
                    {
                        if (FlightsInfo[1, j] != null)
                        {
                            index++;
                            Console.WriteLine("| {0, -4}{1}", 1.ToString() + " " + j.ToString(), FlightsInfo[1, j].ToString());
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (index == 0)
                    {
                        Console.WriteLine("Sorry! There is no suitable flights");
                    }
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Create new Flight
        static void CreateNewFlight(Flight?[,] FlightsInfo)
        {
            Console.Clear();
            bool flag = true;
            int flightNumberInput = 0;
            string cityInput;
            string airLineInput;
            byte terminalNumberInput = 0;
            int intFlightStatus = 0;
            int add = 0;
            FlightStatus flightStatusInput = new FlightStatus();
            DateTime dt = new DateTime();
            bool isParse = false;

            while (flag)
            {
                Console.WriteLine(@"What element do you want to add?
1. Arrival
2. Depature");
                isParse = int.TryParse(Console.ReadLine(), out add);
                if (!isParse || (add != 1 && add != 2))
                {
                    Console.WriteLine("Invalid input detected! Please check your input and try again");
                }
                else
                {
                    Console.WriteLine(" ");
                    bool isCheck = true;
                    #region Flight Number
                    while (isCheck)
                    {
                        Console.WriteLine($"{ "Please, enter Flight number",-30}");
                        isParse = int.TryParse(Console.ReadLine(), out flightNumberInput);
                        if (!isParse)
                        {
                            Console.WriteLine("Invalid input detected!Please check your input and try again");
                            continue;
                        }
                        isCheck = false;
                    }


                    #endregion

                    #region City name & Airline name
                    Console.Write($"{"Please enter the City",-30}");

                    cityInput = Console.ReadLine();
                    if (string.Empty.Equals(cityInput))
                        Console.WriteLine("You don`t enter anything valid");

                    Console.Write($"{"Please enter the Airline",-30}");

                    airLineInput = Console.ReadLine();
                    if (string.Empty.Equals(airLineInput))
                        Console.WriteLine("You don`t enter anything valid");
                    #endregion
                    isCheck = true;

                    #region Terminal number
                    while (isCheck)
                    {
                        Console.Write($"{"Please, enter Terminal number",-30}");
                        isParse = byte.TryParse(Console.ReadLine(), out terminalNumberInput);
                        if (!isParse)
                        {
                            Console.WriteLine("Invalid input. Please try again");
                            continue;
                        }
                        isCheck = false;
                    }
                    #endregion
                    isCheck = true;

                    #region Flight status
                    while (isCheck)
                    {
                        Console.WriteLine($"{"Please, enter the Flight status",-30}" + "   ");
                        Console.WriteLine(@"Note:
1 is CheckIn, 
2 is GateClosed, 
3 is Arrived, 
4 is DepartedAt, 
5 is Unknown,
6 is Canceled,
7 is ExpectedAt, 
8 is Delayed, 
9 is InFlight");
                        isParse = int.TryParse(Console.ReadLine(), out intFlightStatus);
                        if (!isParse || (intFlightStatus < 0 || intFlightStatus > 8))
                        {
                            Console.WriteLine("Invalid input, try again");
                            continue;
                        }
                        else
                        {
                            flightStatusInput = (FlightStatus)intFlightStatus;
                        }
                        isCheck = false;
                    }
                    #endregion
                    isCheck = true;

                    Console.WriteLine("-------------------------");

                    #region DateTime
                    while (isCheck)
                    {
                        Console.WriteLine("Please, enter Date and Time");
                        DateAndTimeInput(out dt);
                        isCheck = false;
                    }
                    #endregion

                    #region Adding the element
                    int index = 0;
                    switch (add)
                    {
                        case 1:
                            for (int j = 0; j < FlightsInfo.GetLength(1); j++)
                            {
                                if (FlightsInfo[0, j] == null)
                                {
                                    FlightsInfo[0, j] = new Flight(flightNumberInput, dt, cityInput, airLineInput, terminalNumberInput, flightStatusInput);
                                    Console.WriteLine("Congratulations! Your item has been added!");
                                    break;
                                }
                                else
                                {
                                    index++;
                                    if (index == FlightsInfo.GetLength(1))
                                    {
                                        Console.WriteLine("Sorry, there is no empty places, please try next time!");
                                    }
                                }
                            }
                            break;

                        case 2:
                            for (int j = 0; j < FlightsInfo.GetLength(1); j++)
                            {
                                if (FlightsInfo[1, j] == null)
                                {
                                    FlightsInfo[1, j] = new Flight(flightNumberInput, dt, cityInput, airLineInput, terminalNumberInput, flightStatusInput);
                                    Console.WriteLine("Congratulations! Your item has been added!");
                                    break;
                                }
                                {
                                    index++;
                                    if (index == FlightsInfo.GetLength(1))
                                    {
                                        Console.WriteLine("Sorry, there is no empty places, please try next time!");
                                    }
                                }
                            }
                            break;

                        default:
                            break;
                    }
                    #endregion
                    flag = false;
                }
            }
        }
        #endregion

        #region Deleting the Flight
        static void DeleteFlight(Flight?[,] FlightInfo)
        {
            ShowAllFlights(FlightInfo, 1);
            Console.WriteLine();

            bool check = true;
            while (check)
            {
                Console.WriteLine("Deleting Flight information menu");
                Console.WriteLine("Input Flight index, to deleting(e.g if you want to delete the Flight with index 10 - input 1->Enter, 0->Enter)");
                int i = 0;
                int j = 0;
                bool TryByteI = int.TryParse(Console.ReadLine(), out i);
                bool TryByteJ = int.TryParse(Console.ReadLine(), out j);
                if (!TryByteI && !TryByteJ)
                {
                    Console.WriteLine("Invalid input, please, more attention and try again");
                }
                else
                {
                    if ((0 <= i && i < 2) && (0 <= j && j < 20))
                    {
                        if (FlightInfo[i, j] == null)
                        {
                            Console.WriteLine("Flight element does not exist, try again");
                        }
                        else
                        {
                            FlightInfo[i, j] = null;
                            Console.WriteLine("Flight element deleting...");
                            RebuildingFlightInfo(ref FlightInfo);
                            ShowAllFlights(FlightInfo, 1);
                            check = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input, try again");
                    }
                }
            }
        }
        #endregion

        #region Edit Flight
        static void EditFlight(Flight?[,] FlightsInfo)
        {
            int i, j;
            bool check = true;
            bool validationCheck = true;
            int flightNumberInput = 0;
            string cityInput;
            string airLineInput;
            byte terminalInput = 0;
            FlightStatus flightStatusInput = new FlightStatus();
            int flightStatusInt = 0;
            string readLine = "";
            DateTime dt = new DateTime();
            bool isParse = false;
            ShowAllFlights(FlightsInfo, 1);
            Console.WriteLine();

            while (check)
            {
                Console.WriteLine("Input Flight index, to editing (e.g if you want to select the Flight with index 10 - input 1->Enter, 0->Enter)");
                isParse = int.TryParse(Console.ReadLine(), out i);
                isParse &= int.TryParse(Console.ReadLine(), out j);
                if (!isParse)
                {
                    Console.WriteLine("Invalid input, please, try again!");
                }
                else if (FlightsInfo[i, j] == null)
                {
                    Console.WriteLine("This element does not exist");
                }
                else
                {
                    while (validationCheck)
                    {
                        Console.WriteLine("Selected item");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("| {0, -4}{1}", i.ToString() + " " + j.ToString(), FlightsInfo[i, j].ToString());
                        Console.ForegroundColor = ConsoleColor.Gray;

                        bool isCheck = true;

                        #region  Flight Number

                        while (isCheck)
                        {
                            Console.Write($"{"Please enter the Flight number",-30}" + "  ");
                            readLine = Console.ReadLine();
                            if (!string.Empty.Equals(readLine))
                            {
                                isParse = int.TryParse(readLine, out flightNumberInput);
                                if (!isParse)
                                {
                                    Console.WriteLine("Invalid input, please, try again!");
                                    continue;
                                }
                            }
                            else
                            {
                                flightNumberInput = FlightsInfo[i, j].Value.flightNumber;
                            }
                            isCheck = false;
                        }
                        #endregion
                        isCheck = true;

                        #region City name & Airline
                        Console.Write($"{"Please, enter the City name",-30}" + "  ");
                        cityInput = Console.ReadLine();
                        if (string.Empty.Equals(cityInput))
                        {
                            cityInput = FlightsInfo[i, j].Value.cityName;
                        }

                        Console.Write($"{"Please, enter the Airline",-30}");
                        airLineInput = Console.ReadLine();
                        if (string.Empty.Equals(airLineInput))
                        {
                            airLineInput = FlightsInfo[i, j].Value.airLine;
                        }
                        #endregion

                        #region Terminal Number
                        while (isCheck)
                        {
                            Console.Write($"{"Please, enter, Terminal number",-30}" + "  ");
                            readLine = Console.ReadLine();
                            if (!string.Empty.Equals(readLine))
                            {
                                isParse = byte.TryParse(readLine, out terminalInput);
                                if (!isParse)
                                {
                                    Console.WriteLine("Invalid input, try again");
                                    continue;
                                }
                            }
                            else
                            {
                                terminalInput = FlightsInfo[i, j].Value.terminalNumber;
                            }
                            isCheck = false;
                        }
                        #endregion
                        isCheck = true;

                        #region flight status
                        while (isCheck)
                        {
                            Console.Write($"{"Please, enter the Flight status",-30}" + "  ");
                            Console.WriteLine(@"Note:
1 is CheckIn, 
2 is GateClosed, 
3 is Arrived, 
4 is DepartedAt, 
5 is Unknown,
6 is Canceled,
7 is ExpectedAt, 
8 is Delayed, 
9 is InFlight");
                            readLine = Console.ReadLine();
                            if (!string.Empty.Equals(readLine))
                            {
                                isParse = int.TryParse(readLine, out flightStatusInt);
                                if (!isParse || (flightStatusInt < 0 && flightStatusInt > 8))
                                {
                                    Console.WriteLine("Invalid input, try again");
                                    continue;
                                }
                                else
                                {
                                    flightStatusInput = (FlightStatus)flightStatusInt;
                                }
                            }
                            else
                            {
                                flightStatusInput = FlightsInfo[i, j].Value.flightStatus;
                            }
                            isCheck = false;
                        }
                        #endregion
                        isCheck = true;

                        #region DateTime
                        while (isCheck)
                        {
                            int changeDateTime = 0;
                            Console.WriteLine("Do you want to change date and time?");
                            Console.WriteLine("1. Change\n2. Save");
                            readLine = Console.ReadLine();
                            isParse = int.TryParse(readLine, out changeDateTime);
                            if (isParse && changeDateTime == 1)
                            {
                                DateAndTimeInput(out dt);
                                isCheck = false;
                            }
                            else if (isParse && changeDateTime == 2)
                            {
                                dt = FlightsInfo[i, j].Value.flightDT;
                                isCheck = false;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input, try again");
                            }
                        }
                        #endregion

                        FlightsInfo[i, j] = new Flight(flightNumberInput, dt, cityInput, airLineInput, terminalInput, flightStatusInput);
                        Console.WriteLine("Your Flight has been modified\\saved");
                        validationCheck = false;
                    }
                }
                check = false;
            }
        }

        #endregion //

        #region Searching
        static void Searching(Flight?[,] FlightInfo, byte mode)
        {
            int flightNumber = 0;
            int index = 0;
            string cityInput;
            bool IsParse = false;
            DateTime dt = new DateTime();

            switch (mode)
            {
                case 1:
                    #region Search to Flight number

                    Console.WriteLine("Please, enter the Flight number");
                    IsParse = int.TryParse(Console.ReadLine(), out flightNumber);
                    if (!IsParse)
                    {
                        Console.WriteLine("Invalid input");
                    }
                    else
                    {
                        CreateMainHeader(mode);
                        PrintArrivalHeader();
                        for (int i = 0; i < FlightInfo.GetLength(0); i++)
                        {
                            for (int j = 0; j < FlightInfo.GetLength(1); j++)
                            {
                                if (FlightInfo[i, j] != null)
                                {
                                    if (flightNumber == FlightInfo[i, j].Value.flightNumber)
                                    {
                                        index++;
                                        Console.WriteLine("| {0, -4}{1}", i.ToString() + " " + j.ToString(), FlightInfo[i, j].ToString());
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            if (index == 0)
                            {
                                Console.WriteLine("Sorry, there is no Flights now");
                            }
                            index = 0;
                            if (i != 1)
                            {
                                PrintDepatureHeader();
                            }
                        }
                        PrintFooter();
                    }
                    #endregion
                    break;

                case 2:
                    #region Search to flight of time of arrival\\departures
                    Console.WriteLine("Please, enter date and time");
                    DateAndTimeInput(out dt);

                    CreateMainHeader(mode);
                    PrintArrivalHeader();
                    for (int i = 0; i < FlightInfo.GetLength(0); i++)
                    {
                        for (int j = 0; j < FlightInfo.GetLength(1); j++)
                        {
                            if (FlightInfo[i, j] != null)
                            {
                                if (DateTime.Equals(dt, FlightInfo[i, j].Value.flightDT))
                                {
                                    index++;
                                    Console.WriteLine("| {0, -4}{1}", i.ToString() + " " + j.ToString(), FlightInfo[i, j].ToString());
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        if (index == 0)
                        {
                            Console.WriteLine("There is no flights");
                        }
                        index = 0;
                        if (i != 1)
                        {
                            PrintDepatureHeader();
                        }
                    }
                    PrintFooter();

                    #endregion
                    break;

                case 3:
                    #region Search to flight City of arrival\\departures
                    Console.WriteLine("Please, enter City name");
                    cityInput = Console.ReadLine();

                    if (string.Empty.Equals(cityInput))
                    {
                        Console.WriteLine("You dont specify anything");
                    }
                    else
                    {
                        CreateMainHeader(mode);
                        PrintArrivalHeader();
                        for (int i = 0; i < FlightInfo.GetLength(0); i++)
                        {
                            for (int j = 0; j < FlightInfo.GetLength(1); j++)
                            {
                                if (FlightInfo[i, j] != null)
                                {
                                    if (string.Equals(cityInput, FlightInfo[i, j].Value.cityName))// (DateTime.Equals(dt, FlightInfo[i, j].Value.flightDT))
                                    {
                                        index++;
                                        Console.WriteLine("| {0, -4}{1}", i.ToString() + " " + j.ToString(), FlightInfo[i, j].ToString());
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            if (index == 0)
                            {
                                Console.WriteLine("Sorry, there is no flights");
                            }
                            index = 0;
                            if (i != 1)
                            {
                                PrintDepatureHeader();
                            }
                        }
                        PrintFooter();
                    }


                    #endregion
                    break;

                case 8:
                    #region Search to nearest (1 hour) flight from city

                    Console.WriteLine("Input date and time");
                    DateAndTimeInput(out dt);

                    DateTime minDT = dt;
                    minDT = minDT.AddHours(-1.0);
                    DateTime maxDT = dt;
                    maxDT = maxDT.AddHours(1.0);


                    Console.Write($"{"Please, enter the City name",-35}");
                    cityInput = Console.ReadLine();
                    if (string.Empty.Equals(cityInput))
                    {
                        Console.WriteLine("You dont specify anything");
                    }
                    else
                    {

                        CreateMainHeader(1);
                        PrintArrivalHeader();
                        for (int i = 0; i < FlightInfo.GetLength(0); i++)
                        {

                            for (int j = 0; j < FlightInfo.GetLength(1); j++)
                            {
                                if (FlightInfo[i, j] != null)
                                {
                                    if ((string.Equals(cityInput, FlightInfo[i, j].Value.cityName)) && ((FlightInfo[i, j].Value.flightDT > minDT) && (FlightInfo[i, j].Value.flightDT < maxDT)))// (DateTime.Equals(dt, FlightInfo[i, j].Value.flightDT))
                                    {
                                        index++;
                                        Console.WriteLine("| {0, -4}{1}", i.ToString() + " " + j.ToString(), FlightInfo[i, j].ToString());
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            if (index == 0)
                            {
                                Console.WriteLine("No flights");
                            }
                            index = 0;
                            if (i != 1)
                            {
                                PrintDepatureHeader();
                            }
                        }
                        PrintFooter();
                    }

                    #endregion
                    break;

                default:
                    Console.WriteLine("Incorrect input");
                    break;
            }
        }

        static void SearchingMenu(Flight?[,] FlightInfo)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Searching menu");
                Console.WriteLine("1. View flight to Flight number");
                Console.WriteLine("2. View flight to Time of arrival\\departures");
                Console.WriteLine("3. View flight to City of arrival\\departures");
                byte mode = 0; ;
                bool TryByte = byte.TryParse(Console.ReadLine(), out mode);
                if (!TryByte)
                {
                    Console.WriteLine("Invalid input");
                }
                else
                    switch (mode)
                    {
                        case 1:
                            Searching(FlightInfo, mode);
                            break;
                        case 2:
                            Searching(FlightInfo, mode);
                            break;
                        case 3:
                            Searching(FlightInfo, mode);
                            break;

                        default:
                            Console.WriteLine("Incorrect input");
                            break;
                    }

                Console.WriteLine("Press Enter to go back");
            }
            while (Console.ReadKey().Key != ConsoleKey.Enter);
        }
        #endregion

        #region Date and Time Input
        static void DateAndTimeInput(out DateTime dt)
        {
            bool isParse = false;
            int year = 0;
            int month = 0;
            int day = 0;
            int hour = 0;
            int minute = 0;
            int second = 0;
            bool isDTCheck = true;

            #region Input Year
            while (isDTCheck)
            {
                Console.Write($"{"Please, enter the year (from 2000 to 2020)",-35}" + "   ");
                isParse = int.TryParse(Console.ReadLine(), out year);
                if (!isParse || year < 2000 || year > 2020)
                {
                    Console.WriteLine("Invalid input, try again");
                    continue;
                }
                isDTCheck = false;
            }
            #endregion
            isDTCheck = true;

            #region Input Month
            while (isDTCheck)
            {
                Console.Write($"{"Please, enter the month (from 1 to 12)",-35}" + "   ");
                isParse = int.TryParse(Console.ReadLine(), out month);
                if (!isParse || month < 1 || month > 12)
                {
                    Console.WriteLine("Invalid input, try again");
                    continue;
                }
                isDTCheck = false;
            }
            #endregion
            isDTCheck = true;

            #region Input Day
            while (isDTCheck)
            {
                Console.Write($"{"Please, enter the day(from 1 to 28 or from 30 or 31)",-35}" + "   ");
                isParse = int.TryParse(Console.ReadLine(), out day);
                if (!isParse)
                {
                    Console.WriteLine("Invalid input, try again");
                    continue;
                }
                else if ((month == 2) && (day < 1 || day > 28))
                {
                    Console.WriteLine("Incorrect input, try again");
                    continue;
                }
                else if ((month % 2 == 1) && (day < 1 || day > 31))
                {
                    Console.WriteLine("Incorrect input, try again");
                    continue;
                }
                else if ((month % 2 == 0) && (day < 1 || day > 30))
                {
                    Console.WriteLine("Incorrect input, try again");
                    continue;
                }
                isDTCheck = false;
            }
            #endregion
            isDTCheck = true;

            #region Input Hour
            while (isDTCheck)
            {
                Console.Write($"{"Please, enter the hour (0 to 23)",-35}" + "   ");
                isParse = int.TryParse(Console.ReadLine(), out hour);
                if (!isParse || hour < 0 || hour > 23)
                {
                    Console.WriteLine("Invalid input, try again");
                    continue;
                }
                isDTCheck = false;
            }
            #endregion
            isDTCheck = true;

            #region Input Minutes
            while (isDTCheck)
            {
                Console.Write($"{"Please, enter the minutes (0 to 59)",-35}" + "   ");
                isParse = int.TryParse(Console.ReadLine(), out minute);
                if (!isParse || minute < 0 || minute > 59)
                {
                    Console.WriteLine("Invalid input, try again");
                    continue;
                }
                isDTCheck = false;
            }
            #endregion
            isDTCheck = true;

            #region Input Seconds
            while (isDTCheck)
            {
                Console.Write($"{"Please, enter the seconds (0 to 59)",-35}" + "   ");
                isParse = int.TryParse(Console.ReadLine(), out second);
                if (!isParse || second < 0 || second > 59)
                {
                    Console.WriteLine("Invalid input, try again");
                    continue;
                }
                isDTCheck = false;
            }
            #endregion
            isDTCheck = true;

            dt = new DateTime(year, month, day, hour, minute, second);
        }
        #endregion

        #region Rebuild Flight info
        static void RebuildingFlightInfo(ref Flight?[,] FlightsInfo)
        {
            Flight? tmp = new Flight();

            for (int i = 0; i < FlightsInfo.GetLength(0); i++)
            {
                tmp = FlightsInfo[i, 0];
                for (int j = 0; j < FlightsInfo.GetLength(1); j++)
                {
                    if (tmp == null)
                    {
                        if (FlightsInfo[i, j] != null)
                        {
                            FlightsInfo[i, j - 1] = FlightsInfo[i, j];
                            FlightsInfo[i, j] = null;
                            tmp = null;
                        }
                        else if (FlightsInfo[i, j] == null && j != 0)
                        {
                            break;
                        }
                    }
                    else if (tmp != null)
                    {
                        tmp = FlightsInfo[i, j];
                    }
                }
            }
        }
        #endregion

        #region Printing Airport table
        static void CreateMainHeader(byte mode)
        {
            mode = (byte)(mode - 1);
            Console.WriteLine("Flight information");

            string[] headTable = new string[]
            {
                string.Format("| {0, -4}| {1,-15}| {2,-20}| {3,-25}| {4,-25}| {5,-10}| {6,-15} |", "ID", "Flight #", "Date", "City of Arriv./Depar.", "Airline", "Terminal #", "Flight status"),
                string.Format("| {0, -4}| {1,-15}| {2,-20}| {3,-25}| {4,-25}| {5,-10}| {6,-15} |", "ID", "Flight #", "Date", "City of arrival", "Airline", "Terminal #", "Flight status"),
                string.Format("| {0, -4}| {1,-15}| {2,-20}| {3,-25}| {4,-25}| {5,-10}| {6,-15} |", "ID", "Flight #", "Date", "City of departure", "Airline", "Terminal #", "Flight status")
            };
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < headTable[mode].Length; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();

            Console.WriteLine(headTable[mode]);

            for (int i = 0; i < headTable[mode].Length; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void PrintArrivalHeader()
        {
            string arrivalHeader = string.Format("| {0, -50}| {1,-15}| {2,-57} |", "", "Arrivals", "");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(arrivalHeader);
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = 0; i < arrivalHeader.Length; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }

        static void PrintDepatureHeader()
        {
            string departuresHeader = string.Format("| {0, -50}| {1,-15}| {2,-57} |", "", "Departures", "");
            for (int i = 0; i < departuresHeader.Length; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(departuresHeader);
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = 0; i < departuresHeader.Length; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }

        static void PrintFooter()
        {
            int stringLenght = 130;
            for (int i = 0; i < stringLenght; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }
        #endregion

        #region DateTime Equals
        static bool DateTimeEquals(DateTime dt1, DateTime dt2)
        {
            if ((dt1.Year == dt2.Year) && (dt1.Month == dt2.Month) && (dt1.Day == dt2.Day) && (dt1.Hour == dt2.Hour))
            {
                return true;
            }
            return false;

        }
        #endregion
    }
}
