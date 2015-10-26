using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    /// <summary>
    /// Main class where the execution starts
    /// </summary>
    class Program
    {
        public static int numberOfSteps = 0;
        public static int numberOfInits = 1;
        /// <summary>
        /// Main method from where the execution starts
        /// Accepts the input from user 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            int input;
            Console.Write("Enter the size of board: ");
            while(true)
            {
                if (Int32.TryParse(Console.ReadLine(), out input)) break;
                else Console.WriteLine("Enter a valid number: ");
            }
            
            NQueensProblem nqueens ;
            bool flag = true;

            //initialize the object until a solution is found for CSP
            do
            {
                nqueens = new NQueensProblem(input);
                nqueens.InitializeBoard();                
                if(flag)
                {
                    Console.WriteLine("Initial State");
                    nqueens.PrintState();
                    Console.WriteLine("======HILL CLIMBING======");
                    //Call the HillClimbing method
                    NQueensProblem result = HillClimbingProblem.HillClimbing(nqueens);
                    result.PrintState();
                    Console.WriteLine("Number of Random Initializations are {0}", numberOfInits);
                    Console.WriteLine("Number of State changes are {0}", numberOfSteps);
                    flag = false;
                    numberOfInits = 0;
                    Console.WriteLine("\n=========CSP===========");
                }
                numberOfInits++;
                numberOfSteps = 0;
                nqueens = CSP.MinConlicts(nqueens, 100);
            } while (nqueens == null);

            nqueens.PrintState();
            Console.WriteLine("Number of Random Initializations are {0}", numberOfInits);
            Console.WriteLine("Number of State changes are {0}", numberOfSteps);
            Console.Read();
            
        }
    }
}
