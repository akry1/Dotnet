using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoImpl
{

    /// <summary>
    /// This class represents the state of a 8-puzzle problem
    /// Also contains the methods to move from one state to next state, print a state,
    /// check if two states are equal, calculate g-cost and h-cost 
    /// </summary>
    public class Node
    {
        //Global variable to define the problem size
        public static int puzzleSize = 3;

        private int[] state = new int[puzzleSize * puzzleSize];
        private int pathCost;
        private int stepCost;
        private int priority;
        private Node parent;

        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public int[] State
        {
            get { return state; }
            set { state = value; }
        }

        internal Node Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public int PathCost
        {
            get { return pathCost; }
            set { pathCost = value; }
        }

        public int StepCost
        {
            get { return stepCost; }
            set { stepCost = value; }
        }

        /// <summary>
        /// Constructor to initialize the stepCost, this is always a constant in this problem
        /// </summary>
        public Node()
        {
            stepCost = 1;
        }

        /// <summary>
        /// Generates the successor nodes
        /// </summary>
        /// <returns></returns>
        public List<Node> ChildrenNodes()
        {

            int currentRowPosition = puzzleSize;
            int currentColPosition = puzzleSize;
            List<Node> children = new List<Node>();

            //find the empty block in the state
            for (int i = 0; i < this.State.Length; i++)
            {
                if (this.State[i] == 0)
                {
                    currentRowPosition = i / puzzleSize;
                    currentColPosition = i % puzzleSize;
                    break;
                }
            }

            //Move down
            int position = currentRowPosition + 1;
            if (position < puzzleSize)
            {
                children.Add(MoveOperation(position, currentColPosition, true, true));
            }

            //Move up
            position = currentRowPosition - 1;
            if (position >= 0)
            {
                children.Add(MoveOperation(position, currentColPosition, true, false));
            }

            //Move right
            position = currentColPosition + 1;
            if (position < puzzleSize)
            {
                children.Add(MoveOperation(currentRowPosition, position, false, true));
            }

            //Move left
            position = currentColPosition - 1;
            if (position >= 0)
            {
                children.Add(MoveOperation(currentRowPosition, position, false, false));
            }

            return children;
        }

        /// <summary>
        /// Moves the blank space to a desired position
        /// Returns a child node
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="flag1"></param>
        /// <param name="flag2"></param>
        /// <returns></returns>
        public Node MoveOperation(int row, int col, bool flag1, bool flag2)
        {
            Node successor = new Node();

            //Copy the current state into a temporary storage
            int[] value = new int[State.Length];
            for (int i = 0; i < State.Length; i++)
            {
                value[i] = this.State[i];
            }

            //Find the required block to move based the bool values flag1 and flag2
            int a;
            if (flag2) a = -1; else a = 1;
            int row1 = row, col1 = col;
            if (flag1) row1 = row + a; else col1 = col + a;

            //set the new Node and return it
            successor.state = value;
            successor.State[puzzleSize * row1 + col1] = successor.State[puzzleSize * row + col];
            successor.State[puzzleSize * row + col] = 0;
            return successor;

        }

        /// <summary>
        /// Finds the h-cost which is the Manhattan distance between the calling Node and parameter Node
        /// </summary>
        /// <param name="goalNode"></param>
        /// <returns></returns>
        public int HCost(Node goalNode)
        {
            int distance = 0;
            for (int i = 0; i < this.State.Length; i++)
            {
                for (int j = 0; j < this.State.Length; j++)
                {
                    if (this.State[i] != 0 && this.State[i] == goalNode.State[j])
                    {
                        int x1 = (i) / puzzleSize; int y1 = (i) % puzzleSize;
                        int x2 = j / puzzleSize; int y2 = j % puzzleSize;
                        distance += Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
                    }
                }
            }

            return distance;
        }

        /// <summary>
        /// Displays the state of the calling Node
        /// </summary>
        public void PrintState()
        {
            for (int i = 0; i < this.State.Length; i++)
            {
                if (i % puzzleSize == 0) Console.WriteLine();
                if (this.State[i] == 0)
                    Console.Write(' ');
                else
                    Console.Write(this.State[i]);

            }
        }

        /// <summary>
        /// Compares the states of two Nodes and returns true if they are same
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public bool Equals(Node current)
        {
            for (int i = 0; i < this.State.Length; i++)
            {
                if (this.State[i] != current.State[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Calculates the g-cost of a node
        /// </summary>
        /// <returns></returns>
        public int GCost()
        {
            if (this.parent != null)
                return this.Parent.PathCost + stepCost;
            else return 0;
        }
    }//End of Class Node

    /// <summary>
    /// Main program that takes the input from the user and calls the AStar algorithm
    /// </summary>
    class EightPuzzzleProgram
    {
        public static int puzzleSize = 3;

        /// <summary>
        /// Main method where the program execution starts
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Node startNode = new Node();
            Node goalNode = new Node();

            //Prompts the user to enter start state and goal state
            Console.WriteLine("Enter the start state:");
            startNode.State = CheckInput();
            Console.WriteLine("Enter the goal state:");
            goalNode.State = CheckInput();

            startNode.PathCost = 0;
            startNode.Parent = null;

            Node bestNode = new Node();

            //Create an object of AStarProgram and call the AStar algorithm
            AStarProgram astar = new AStarProgram();
            bestNode = astar.AStar(startNode, goalNode);

            //Used a stack to display the path of solution
            Stack<Node> result = new Stack<Node>();
            if (bestNode != null)
            {
                while (bestNode.Parent != null)
                {
                    result.Push(bestNode);
                    bestNode = bestNode.Parent;
                }
            }
            else
            {
                //if bestnode is null then display
                Console.WriteLine("Solution does not exist");
            }

            //Print the solution, Number of nodes generated and nodes expanded
            Console.WriteLine("Number of Nodes in solution path : " + result.Count());
            Node tempNode = new Node();
            startNode.PrintState();
            Console.WriteLine();
            while (result.Count != 0)
            {
                tempNode = result.Pop();
                tempNode.PrintState();
                Console.WriteLine();
            }
            Console.WriteLine(); Console.WriteLine();
            Console.WriteLine("Number of Nodes generated : " + AStarProgram.noOfNodesGenerated);
            Console.WriteLine("Number of Nodes Expanded : " + AStarProgram.noOfNodesExpanded);
            Console.Read();
        }

        /// <summary>
        /// Validates the input given by the user
        /// </summary>
        /// <returns></returns>
        public static int[] CheckInput()
        {
            int[] input = new int[puzzleSize * puzzleSize];
            int count = 0;
            while (true)
            {
                if (count == 9)
                    break;
                int temp;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                ConsoleKeyInfo keyPressed = Console.ReadKey();
                if (Int32.TryParse((keyPressed.KeyChar).ToString(), out temp))
                {
                    if (temp < 0 || temp >= input.Length)
                    {
                        continue;
                    }
                    bool flag = false;
                    for (int i = 0; i < count; i++)
                    {
                        if (input[i] == temp)
                        {
                            flag = true;
                        }
                    }

                    if (!flag)
                    {
                        input[count++] = temp;
                        Console.WriteLine();
                    }
                    else
                        continue;
                }
                else
                    continue;

            }
            return input;
        }
    }//End of class EightPuzzzleProgram
}
