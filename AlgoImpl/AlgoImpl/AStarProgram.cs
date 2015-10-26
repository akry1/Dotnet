using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoImpl
{
    /// <summary>
    /// This class implements the A* algorithm
    /// </summary>
    class AStarProgram
    {
        //Global variables to store the number of nodes generated and nodes expanded
        public static int noOfNodesGenerated = 0;
        public static int noOfNodesExpanded = 0;

        /// <summary>
        /// Checks if the Node child is present in the list of nodes
        /// Returns -1 if not present or returns the index if present
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        public static int FindNode(List<Node> nodes, Node child)
        {
            int flag = -1;
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].Equals(child))
                {
                    flag = i;
                    break;
                }
            }
            return flag;

        }

        /// <summary>
        /// Implementation of A* Algorithm
        /// Takes the start node and goal node as input and returns the node which matches the goal node
        /// </summary>
        /// <param name="startNode"></param>
        /// <param name="goalNode"></param>
        /// <returns></returns>
        public Node AStar(Node startNode, Node goalNode)
        {
            Node INIT = startNode;

            //Create a list to track the visited nodes
            List<Node> visitedNodes = new List<Node>();

            //Create a Queue for the open nodes
            AlgoImpl.PriorityQueue queue = new AlgoImpl.PriorityQueue();

            //Add the start node to the queue
            INIT.Priority = INIT.HCost(goalNode) + INIT.PathCost;
            queue.Enqueue(INIT);

            //Check the queue for nodes, if empty then return null
            while (queue.nodes.Count != 0)
            {
                //Get the best node with minimum f value from the queue
                Node current = queue.Dequeue();

                //Add the node to visited nodes
                visitedNodes.Add(current);

                //Check if the node is goal node and return the node if true else expand its child nodes
                if (current.Equals(goalNode))
                {
                    return current;
                }
                else
                {
                    //Increment the expanded nodes global variable
                    ++noOfNodesExpanded;

                    //Generated the successor nodes 
                    List<Node> childNodes = current.ChildrenNodes();

                    //Traverse the nodes
                    foreach (Node successor in childNodes)
                    {
                        int distanceFromNewNode = successor.HCost(goalNode);

                        //Compute the estimated g-cost of the successor
                        int estGCost = current.GCost() + Math.Abs(distanceFromNewNode - current.HCost(goalNode));
                        int findVisited = FindNode(visitedNodes, successor);

                        //check if the successor is already visited or not, if visted then discard it
                        if (findVisited == -1)
                        {
                            //check if the successor is already in the queue
                            int found = FindNode(queue.nodes, successor);

                            //if true
                            if (found != -1)
                            {
                                //if f cost of old node is greater than f cost of successor node, delete it from the queue
                                //else discard the successor node
                                if ((queue.nodes.ElementAt(found).PathCost > estGCost + distanceFromNewNode))
                                    queue.Delete(found);
                                else
                                    continue;
                            }
                            //Add the node to the queue and set its parent to curren
                            successor.Parent = current;
                            successor.PathCost = estGCost;
                            successor.Priority = successor.PathCost + distanceFromNewNode;
                            queue.Enqueue(successor);
                            ++noOfNodesGenerated;
                        }
                        else
                        {
                            if (estGCost < successor.GCost())
                            {
                                visitedNodes.RemoveAt(findVisited);
                                continue;
                            }
                        }
                    }
                }
                if (queue.nodes.Count == 100000)
                    break;
            }
            return null;
        }
    }//End of class AStarProgram
}
