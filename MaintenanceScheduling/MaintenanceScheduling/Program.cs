using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MaintenanceScheduling
{
    class Program
    {
        // Declaring global variables for tracking the number of steps 
        // and number of initializations in the Hill Climbing approach
        public static int numberOfSteps = 0;
        public static int numberOfInits = 1;

        /// <summary>
        /// Main method where the execution starts
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Get the input from the user
            int[] capacity=null, maxPerInterval = null, intervalsLeft = null;

            Console.Write("Enter 1 to provide input from console or Enter any other key to take input from file:");
            int display;
            string input = Console.ReadLine();
            if (Int32.TryParse(input, out display) && display == 1)
                Program.GetInputFromConsole(ref capacity, ref maxPerInterval, ref intervalsLeft);
            else
              Program.GetInputFromFile(ref capacity, ref maxPerInterval, ref intervalsLeft);

            //Call the FindSolution method if input is OK
            if (capacity != null && maxPerInterval != null && intervalsLeft != null)
            {
                ProblemState ps = Program.FindSolution(capacity, maxPerInterval, intervalsLeft);
                Program.DisplayResult(ps);
                Application.Run(new Visualization(ps));
            }
            
        }

        /// <summary>
        /// Gets the input from the file and stores it in capacity, maxPerInterval and intervalsLeft
        /// </summary>
        /// <param name="capacity">reference to the array containing capacity of the power units</param>
        /// <param name="maxPerInterval">reference to the array containing the maximum power needed in each interval</param>
        /// <param name="intervalsLeft">reference to the array containing the intervals needed for each unit to compplete the maintenance</param>
        public static void GetInputFromConsole(ref int[] capacity, ref  int[] maxPerInterval, ref int[] intervalsLeft)
        {
            int units,intervals;
            Console.Write("Enter the number of units:");            
            Program.CheckInput(out units,1);

            Console.Write("Enter the number of intervals:");
            Program.CheckInput(out intervals,1);

            while (true)
            {
                if (intervals >= units)
                {
                    Console.WriteLine("Number of intervals should be less than number of Units.");
                    Console.Write("Enter a new value:");
                    Program.CheckInput(out intervals, 1);
                }
                else
                    break;
            }
            capacity = new int[units];
            maxPerInterval = new int[intervals ];
            intervalsLeft = new int[units];

            //Get the capacities
            Console.WriteLine("Enter the Capacity of units");
            int total=0;
            for(int i=0;i<units;i++)
            {
                Console.Write("Unit {0}:", i+1);
                Program.CheckInput(out capacity[i],1);
                total += capacity[i];
            }

            //Get the Max loads
            Console.WriteLine("Enter the maximum loads expected during each interval");
            for (int i = 0; i < intervals ; i++)
            {
                Console.Write("Interval {0}:", i+1);
                Program.CheckInput(out maxPerInterval [i]);
                while (true)
                {
                    if (maxPerInterval[i] > total)
                    {
                        Console.Write("Maximum loads expected during each interval cannot exceed total capacity. Enter new value:");
                        Program.CheckInput(out maxPerInterval[i]);
                    }
                    else
                        break;
                }
            }

            //Get the intervals needed
            Console.WriteLine("Enter the number of intervals required for each unit maintenance during a year:");
            for (int i = 0; i < units; i++)
            {
                Console.Write("Unit {0}:", i+1);
                Program.CheckInput(out intervalsLeft [i]);
                while (true)
                {
                    if (intervalsLeft[i] > intervals)
                    {
                        Console.Write("Number of intervals cannot exceed total Intervals. Enter new value:");
                        Program.CheckInput(out intervalsLeft[i]);
                    }
                    else
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the input from the file and stores it in capacity, maxPerInterval and intervalsLeft
        /// </summary>
        /// <param name="capacity">reference to the array containing capacity of the power units</param>
        /// <param name="maxPerInterval">reference to the array containing the maximum power needed in each interval</param>
        /// <param name="intervalsLeft">reference to the array containing the intervals needed for each unit to compplete the maintenance</param>
        public static void GetInputFromFile(ref int[] capacity,ref  int[] maxPerInterval, ref int[] intervalsLeft)
        {
            try
            {
                //Path of the input file
                string filePath = AppDomain.CurrentDomain.BaseDirectory;
                                
                StreamReader sr = File.OpenText(filePath + "\\input.txt");

                StringBuilder data = new StringBuilder();
                while (sr.Peek() >= 0)
                    data.Append(sr.ReadLine() + "+");

                string[] strArr = data.ToString().Split('+', ':');

                for (int i = 0; i < strArr.Length - 1; i = i + 2)
                {
                    string[] input = strArr[i + 1].Trim().Split(',');
                    switch (strArr[i].Trim().ToLower())
                    {

                        case "capacity":
                            capacity = new int[input.Length];
                            for (int j = 0; j < input.Length; j++)
                                Int32.TryParse(input[j].Trim(), out capacity[j]);
                            break;
                        case "maxperinterval":
                            maxPerInterval = new int[input.Length];
                            for (int j = 0; j < input.Length; j++)
                                Int32.TryParse(input[j].Trim(), out maxPerInterval[j]);
                            break;
                        case "intervalsleft":
                            intervalsLeft = new int[input.Length];
                            for (int j = 0; j < input.Length; j++)
                                Int32.TryParse(input[j].Trim(), out intervalsLeft[j]);
                            break;
                        case "default":
                            break;
                    }
                }

                //Check if data is valid
                int total = 0;
                if (maxPerInterval.Length >= capacity.Length)
                {
                    Console.WriteLine("max load array size should be less than capacity array size. Please correct it.");
                    Console.Read();
                    System.Environment.Exit(-1);
                }

                if (intervalsLeft.Length > capacity.Length)
                {
                    Console.WriteLine("Intervals array size is more than capacity array size. Please correct it.");
                    Console.Read();
                    System.Environment.Exit(-1);
                }

                for (int i = 0; i < capacity.Length; i++)
                {
                    
                    if (intervalsLeft [i]<0 || intervalsLeft[i] > maxPerInterval.Length )
                    {
                        Console.WriteLine("Number of intervals cannot be negative or exceed total Intervals.");
                        Console.WriteLine("Check the value of Unit {0} in your file", i + 1);
                        Console.Read();
                        System.Environment.Exit(-1);
                    }
                    if(capacity [i]<=0)
                    {
                        Console.WriteLine("Capacity should be greater than 0.");
                        Console.WriteLine("Check the value of Unit {0} in your file", i + 1);
                        Console.Read();
                        System.Environment.Exit(-1);
                    }
                    total += capacity[i];
                }

                for (int i = 0; i < maxPerInterval.Length; i++)
                {
                    if (maxPerInterval[i]<0 || maxPerInterval[i] > total)
                    {
                        Console.WriteLine("Maximum loads expected during each interval cannot be negative or cannot exceed total capacity.");
                        Console.WriteLine("Check the value of interval {0} in your file", i + 1);
                        Console.Read();
                        System.Environment.Exit(-1);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Content in the file is not in expected format. Please correct it.");
                Console.Read();
                System.Environment.Exit(-1);
            }
        }

        /// <summary>
        /// Creates an instance of ProblemState, initializes it and calls HillClimbing method to get the solution
        /// </summary>
        /// <param name="capacity"> array containing capacity of the power units  </param>
        /// <param name="maxPerInterval">array containing the maximum power needed in each interval</param>
        /// <param name="intervalsLeft">array containing the intervals needed for each unit to compplete the maintenance</param>
        /// <returns> the state found by the HillClimbing technique</returns>
        public static ProblemState FindSolution(int[] capacity, int[] maxPerInterval, int[] intervalsLeft)
        {
            ProblemState ps = new ProblemState(capacity, maxPerInterval, intervalsLeft);
            ps.InitializeState();

            return HillClimbingProblem.HillClimbing(ps);
        }

        /// <summary>
        /// Function to display the solution of the problem found by the Hill Climbing approach and SelectMax approach
        /// </summary>
        /// <param name="ps"> an instance of the ProblemState</param>
        public static void DisplayResult(ProblemState ps)
        {
            //Hill Climbing
            Console.WriteLine("=====Output of the Hill Climbing approach=====");

            int [] netReservePerInterval = new int[ps.NumberOfIntervals ];
            for (int i = 0; i < ps.NumberOfIntervals; i++)
            {
                Console.WriteLine("Interval {0}", i+1);
                Console.Write("Units Scheduled : ");
                for (int j = 0; j < ps.Length; j++)
                    if(ps.Solution[j, i] == 1)
                    {
                        Console.Write(" {0} ", j+1);
                        netReservePerInterval[i] += ps.Solution[j, i] * ps.Capacity[j];
                    }

                netReservePerInterval[i] = ps.Total - ps.MaxPerInterval[i] - netReservePerInterval[i];
                Console.WriteLine();
                Console.WriteLine("Net Reserve     : {0}", netReservePerInterval[i]);
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine("Number of steps to reach the solution = {0}", numberOfSteps);
            Console.WriteLine("Number of Initializations             = {0}", numberOfInits);
            Console.WriteLine();
        }

        /// <summary>
        /// Checks the input from command prompt and validate if it is number or not
        /// </summary>
        /// <param name="a"></param>
        public static void CheckInput(out int a, params int[] flag)
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (Int32.TryParse(input, out a)) 
                {
                    if(a<0)
                        Console.Write("Enter a positive number:");
                    else if(flag .Length >0 && a== 0)
                        Console.Write("Enter a number greater than 0:");
                    else
                        break;
                }
                else 
                    Console.Write("Enter a valid number:");
            }
        }
       
    }
}
