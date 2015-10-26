using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoProject1
{
    /// <summary>
    /// Class for Merge Sort
    /// </summary>
    class MergeSortProgram
    {
        /// <summary>
        /// Partitions the array into two halves and calls the sub arrays recursively
        /// </summary>
        /// <param name="searchSample"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void MergeSort(int[] searchSample, int min, int max)
        {
            int mid;
            if (max - min > 1)
            {
                CommonClass.IncreaseCount();
                mid = (int)Math.Floor((double)((min + max) / 2)); 

                int[] leftArray = new int[mid - min];
                int[] rightArray = new int[max - mid];

                for (int i = 0; i < mid - min; i++)
                {
                    leftArray[i] = searchSample[i]; 
                }
                for (int j = 0; j < max - mid; j++)
                {
                    rightArray[j] = searchSample[mid - min + j]; 
                }

                MergeSort(leftArray, min, mid);
                MergeSort(rightArray, mid, max);
                Merge(searchSample, leftArray, rightArray);
            }
            else return;

        }

        /// <summary>
        /// Takes two sub arrays as input and merges them to a sorted array
        /// </summary>
        /// <param name="sortedArray"></param>
        /// <param name="leftArray"></param>
        /// <param name="rightArray"></param>
        public void Merge(int[] sortedArray, int[] leftArray, int[] rightArray)
        {
            int end = leftArray.Length + rightArray.Length; 
            int left = 0, right = 0;
            for (int i = 0; i < end; i++)
            {
                CommonClass.IncreaseCount();CommonClass.IncreaseCount();
                if (left < leftArray.Length && (right >= rightArray.Length || leftArray[left] <= rightArray[right]))
                {
                    sortedArray[i] = leftArray[left];
                    left++; 
                }
                else
                {
                    sortedArray[i] = rightArray[right]; 
                    right++; 
                }

            }
        }
    }
}
