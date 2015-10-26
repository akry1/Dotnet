using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoImpl
{
    /// <summary>
    /// Priority Queue Implementation
    /// </summary>
    public class PriorityQueue
    {
        public List<Node> nodes = new List<Node>();
        private int heapsize;


        public int Heapsize
        {
            set { heapsize = value; }
            get
            {
                if (nodes == null) return 0;
                else
                    return nodes.Count;
            }
        }

        /// <summary>
        /// Inserts a node into the priority queue based on Priority value of node
        /// </summary>
        /// <param name="value"></param>
        public void Enqueue(Node value)
        {
            if (IsEmpty())
                nodes.Add(value);
            else
            {
                bool insert = false;
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (value.Priority < nodes[i].Priority)
                    {
                        nodes.Insert(i, value);
                        insert = true;
                        break;
                    }
                    if (nodes[i].Priority == value.Priority)
                    {
                        nodes.Insert(i + 1, value);
                        insert = true;
                        break;
                    }
                }
                if (!insert)
                    nodes.Add(value);

            }
        }

        /// <summary>
        /// Checks if the Priority Queue is empty or not
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if (Heapsize == 0)
                return true;
            else return false;
        }

        /// <summary>
        /// Deletes the first element of the priority queue and returns it
        /// </summary>
        /// <returns></returns>
        public Node Dequeue()
        {
            Node min = nodes.ElementAt(0);
            nodes.RemoveAt(0);
            return min;
        }

        /// <summary>
        /// Deletes the ith element of the priority queue
        /// </summary>
        /// <param name="i"></param>
        public void Delete(int i)
        {
            nodes.RemoveAt(i);
        }
    }
    //End of Class Priority Queue
} 
