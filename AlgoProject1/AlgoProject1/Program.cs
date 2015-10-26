using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoProject1
{
    class Program
    {
        /// <summary>
        /// Main method which takes the input and invoke methods in other classes to find the i largest elements in an array
        /// Also accepts the i size from command prompt
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] sample = createArray();
            int length = sample.Length;
            int[] copy = new int[length];
            Array.Copy(sample, copy, length);

            int output;
            Console.Write("Enter the size of the output:");
            CommonClass.CheckInput(out output );

            int display;
            Console.Write("Click Enter to continue\n" + "or Enter 1 to display the detailed report of Input and Original sorted arry for all the programs:");
            string input = Console.ReadLine();
            if (Int32.TryParse(input, out display)) {}
            else display = 0;            

            int resLength = output;
            if (length < resLength)
                resLength = length;
            int[] result = new int[resLength];

            CommonClass.resultSize = resLength;


            LinearSearchProgram linearSearchObject = new LinearSearchProgram();

            Console.WriteLine("==========LINEAR SEARCH==========");
            Console.WriteLine(); Console.WriteLine();
            Console.WriteLine("Random Array with {0} elements", length);
            CommonClass.DisplayArray(sample);

            CommonClass.numberOfAssignments = 0;

            result = linearSearchObject.LinearSearch(sample, resLength);
            
            //Console.WriteLine("==========LINEAR SEARCH OUTPUT==========");
            Console.WriteLine("Select Top {0} elements", resLength);
            CommonClass.DisplayArray(result);
            CommonClass.DisplayNoOfOperartions();

            //Display the sorted output of Linear search
            if (display == 1)
            {
                Console.WriteLine("Original Sorted Array", length);
                sample = linearSearchObject.LinearSearch(sample, length);
                CommonClass.DisplayArray(sample);
            }

            Array.Copy(copy, sample, length);

            MergeSortProgram mergeSortObject = new MergeSortProgram();

            Console.WriteLine("==========MERGE SORT==========");
            Console.WriteLine(); Console.WriteLine();
            

            //Display the input array for Merge sort
            if (display == 1)
            {
                Console.WriteLine("Random Array with {0} elements", length);
                CommonClass.DisplayArray(sample);
            }

            CommonClass.numberOfAssignments = 0;

            mergeSortObject.MergeSort(sample,0, length);
            result = CommonClass.largestIElements(sample, resLength);

            Console.WriteLine("Select Top {0} elements", resLength);
            CommonClass.DisplayArray(result);
            CommonClass.DisplayNoOfOperartions();

            //Display the sorted output of Merge Sort
            if (display == 1)
            {
                Console.WriteLine("Original Sorted Array", length);
                CommonClass.DisplayArray(sample);
            }

            Array.Copy(copy, sample, length);

            HeapSortProgram heapSortObject = new HeapSortProgram();

            Console.WriteLine("==========HEAP SORT==========");
            Console.WriteLine(); Console.WriteLine();             

            //Display the input array for heap sort
            if (display == 1)
            {
                Console.WriteLine("Random Array with {0} elements", length);
                CommonClass.DisplayArray(sample);
            }

            CommonClass.numberOfAssignments = 0;
            result = heapSortObject.largestIElements(sample, resLength);
            

            Console.WriteLine("Select Top {0} elements", resLength);
            CommonClass.DisplayArray(result);
            CommonClass.DisplayNoOfOperartions();

            //Display the sorted output of Heap Sort
            if (display == 1)
            {
                sample = heapSortObject.largestIElements(sample, length);
                Console.WriteLine("Original Sorted Array", length);
                CommonClass.DisplayArray(sample);
            }

            Array.Copy(copy, sample, length);

            QuickSortProgram quickSortObject = new QuickSortProgram();

            Console.WriteLine("==========QUICK SORT==========");
            Console.WriteLine(); Console.WriteLine();
                        
            //Display the input array for quicksort
            if (display == 1)
            {
                Console.WriteLine("Random Array with {0} elements", length);
                CommonClass.DisplayArray(sample);
            }

            CommonClass.numberOfAssignments = 0;
            quickSortObject.QuickSort(sample,0,length-1);
            result = CommonClass.largestIElements(sample, resLength);

            Console.WriteLine("Select Top {0} elements", resLength);
            CommonClass.DisplayArray(result);
            CommonClass.DisplayNoOfOperartions();

            //Display the sorted output of Quick Sort
            if (display == 1)
            {
                Console.WriteLine("Original Sorted Array", length);
                CommonClass.DisplayArray(sample);
            }


            Console.Read();

        }


        /// <summary>
        /// Accepts the input from command prompt and creates array of that size and loads it with random numbers
        /// </summary>
        /// <returns></returns>
        public static int[] createArray()
        {
            Console.WriteLine ("Enter 0 to randomly generate numbers based on input size");
            Console.Write("or Enter 1 to manually provide input:");
            int select;
            Random random = new Random();
            while (true)
            {
                CommonClass.CheckInput(out select);
                if (select == 0 || select == 1)
                    break;
                Console.Write("Enter either 0 or 1:");
            }
            switch (select)
            {
                case 1:
                    {
                        int n;
                        Console.Write("Enter the size of the input:");
                        CommonClass.CheckInput(out n);
                        int[] sample = new int[n];
                        Console.WriteLine("Enter the input:");
                        for (int i = 0; i < n; i++)
                        {
                            CommonClass.CheckInput(out sample[i]);
                        }
                        return sample;
                    }

                case 0:
                    {
                        int n;
                        Console.Write("Enter the size of the input:");
                        CommonClass.CheckInput(out n);
                        int[] sample = new int[n];
                        for (int i = 0; i < sample.Length; i++) sample[i] = random.Next(10000);
                        return sample;
                    }

                default:
                    return null;
            }
        }
    }
}
