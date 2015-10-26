using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoProject1
{
    /// <summary>
    /// This class implements the methods that are used by all classes
    /// </summary>
    class CommonClass
    {
        /// <summary>
        /// Counts the number of key comparisons
        /// </summary>
        public static int numberOfAssignments;
        public static int resultSize;

        /// <summary>
        /// Increments the key comparisons by one
        /// </summary>
        public static void IncreaseCount()
        {
            ++numberOfAssignments;
        }

        /// <summary>
        /// Exchanges two values
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void Swap(ref int a, ref int b)
        {
            int temp = a; 
            a = b; 
            b = temp; 
            return;
        }

        /// <summary>
        /// Checks the input from command prompt and validates if it is number or not
        /// </summary>
        /// <param name="a"></param>
        public static void CheckInput(out int a)
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (Int32.TryParse(input, out a)) { break; }
                else Console.Write("Enter a valid number:");
            }
        }

        /// <summary>
        /// Selects and returns first i elements in an array
        /// </summary>
        /// <param name="searchSample"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static int[] largestIElements(int[] searchSample, int size)
        {
            int[] result = new int[size];
            for (int i = 0; i < size; i++)
            {
                result[i] = searchSample[searchSample.Length - i - 1];
            }
            return result;
        }

        /// <summary>
        /// Displays the array
        /// </summary>
        /// <param name="searchSample"></param>
        public static void DisplayArray(int[] searchSample)
        {
            Console.WriteLine(); Console.WriteLine();
            for (int i = 0; i < searchSample.Length; i++) Console.Write(searchSample[i] + " ");
            Console.WriteLine(); Console.WriteLine();
        }

        /// <summary>
        /// Displays the number of operations
        /// </summary>
        public static void DisplayNoOfOperartions()
        {

            Console.WriteLine("Number of key operations :" + CommonClass.numberOfAssignments);
            Console.WriteLine(); Console.WriteLine();
        }
    }
}
