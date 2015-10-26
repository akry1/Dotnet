using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoProject1
{
    /// <summary>
    /// Class for Quick Sort
    /// </summary>
    class QuickSortProgram
    {
        /// <summary>
        /// Calls the Partition method to partition the input array into two arrays and recursively calls those arrays
        /// </summary>
        /// <param name="searchSample"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void QuickSort(int[] searchSample, int min, int max)
        {
            if (min < max)
            {
                CommonClass.IncreaseCount();
                int q = Partition(searchSample, min, max); 
                if(max<=CommonClass .resultSize )
                    QuickSort(searchSample, min, q - 1);
                QuickSort(searchSample, q+1, max);
            }
            else return;
        }


        /// <summary>
        /// Selects a pivot using the SelectPivot method and arranges the input such that left side elements are lesser than pivot 
        /// and right side elements are greater than pivot
        /// </summary>
        /// <param name="searchSample"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int Partition(int[] searchSample, int min, int max)
        {
            int pos = SelectPivot(searchSample, min, max);
            int x = searchSample[pos];
            int i = min - 1;
            CommonClass.Swap(ref searchSample[pos], ref searchSample[max]);
            for (int j = min; j < max ; j++)
            {
                if (searchSample[j] <= x)
                {
                    i++;
                    if (i != j)
                        CommonClass.Swap(ref searchSample[i], ref  searchSample[j]);

                }
                CommonClass.IncreaseCount();
            }
            i++;
            if (i != max)
                CommonClass.Swap(ref searchSample[i], ref searchSample[max]);
            return i;
        }

        public int  CommonPartition(int[] searchSample,int min,int max,int pos)
        {
            int x = searchSample[pos];
            int i = min; 
            CommonClass.Swap(ref searchSample[pos], ref searchSample[max]);
            for (int j = min; j < max; j++)
            {
                if (searchSample[j] < x)
                {
                     CommonClass.Swap(ref searchSample[i], ref  searchSample[j]);
                     i++;
                    
                }
            }
            CommonClass.Swap(ref searchSample[i], ref searchSample[max]);
            return i;
        }


        
        /// <summary>
        /// Selects a pivot
        /// </summary>
        /// <param name="searchSample"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int SelectPivot(int[] searchSample, int min, int max)
        {            
            //return (min+max)/2;
            
            if (min == max)
                return min;
            int pos = (min + max) / 2;
            pos = CommonPartition(searchSample, min, max, pos);
            if (CommonClass.resultSize == pos)
                return pos;
            else if (CommonClass.resultSize < pos)
                return SelectPivot (searchSample ,min,pos-1);
            else
                return SelectPivot (searchSample ,pos+1,max);   
        }
    }
}
