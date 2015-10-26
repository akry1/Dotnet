using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoProject1
{
    /// <summary>
    /// Class for Heap Sort
    /// </summary>
    class HeapSortProgram
    {
        /// <summary>
        /// Gets the largest i elements from the array
        /// </summary>
        /// <param name="searchSample"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public int[] largestIElements(int[] searchSample, int size)
        {
            int[] result = new int[size];
            BuildHeap(searchSample);
            for (int i = 0; i < size; i++)
            {
                result[i] = Delete(ref searchSample);
            }
            return result;
        }

        /// <summary>
        /// Creates the Max-heap from the array
        /// </summary>
        /// <param name="searchSample"></param>
        public void BuildHeap(int[] searchSample)
        {
            int heapSize = searchSample.Length;
            for (int i = (int)Math.Floor((double)heapSize / 2); i >= 0; i--)
            {
                Heapify(searchSample, i);
            }
        }

        /// <summary>
        /// Creates the heap around the index i
        /// </summary>
        /// <param name="searchSample"></param>
        /// <param name="index"></param>
        public void Heapify(int[] searchSample, int index)
        {
            int heapsize = searchSample.Length; 
            int left;
            if (index == 0) left = 1;
            else left = 2 * index + 1;
            int right = left + 1; 
            int largest = index;

            if (left < heapsize && searchSample[left] > searchSample[largest])
            {
                largest = left;
            }
            CommonClass.IncreaseCount(); CommonClass.IncreaseCount();

            if (right < heapsize && searchSample[right] > searchSample[largest])
            {
                largest = right;
            }

            CommonClass.IncreaseCount();CommonClass.IncreaseCount();

            if (largest != index)
            {
                CommonClass.Swap(ref searchSample[index], ref searchSample[largest]);
                Heapify(searchSample, largest);
            }
            CommonClass.IncreaseCount();
        }

        /// <summary>
        /// Deletes the first element from the heap
        /// </summary>
        /// <param name="searchSample"></param>
        /// <returns></returns>
        public int Delete(ref int[] searchSample)
        {
            int max = searchSample[0];
            int heapSize = searchSample.Length;
            CommonClass.Swap(ref searchSample[0], ref searchSample[heapSize - 1]);
            heapSize--;
            Array.Resize(ref searchSample, heapSize);
            Heapify(searchSample, 0);
            return max;
        }
    }
}
