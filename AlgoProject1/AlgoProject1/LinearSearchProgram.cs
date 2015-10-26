using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoProject1
{
    /// <summary>
    /// Class for Linear Search
    /// </summary>
    class LinearSearchProgram
    {
        /// <summary>
        /// Finds the i largest elements in an array using linear search
        /// </summary>
        /// <param name="searchSample"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public int[] LinearSearch(int[] searchSample, int size)
        {
            int[] result = new int[size];         
            
            for (int i = 0; i < size; i++)
            {
                int max = searchSample[i]; 
                int pos = i; 
                for (int j = i+1; j < searchSample.Length; j++)
                {
                    CommonClass.IncreaseCount();
                    if (searchSample[j] > max)
                    {
                        max = searchSample[j];
                        pos = j; 
                    }
                }
                CommonClass.Swap(ref searchSample[i], ref searchSample[pos]);
                result[i] = max;
            }
            return result;
        }
    }
}
