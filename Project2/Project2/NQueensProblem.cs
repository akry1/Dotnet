using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    /// <summary>
    /// Class to represent n-queens state
    /// </summary>
    class NQueensProblem
    {
        private static int size;
        private int[,] boardConfig;
        private int[] queenPositions;
        private int conflicts;

        public int Conflicts
        {
            get { return this.HCost(); }
            set { conflicts = this.HCost (); }
        }

        public int Size
        {
            get { return size; }
        }

        public int[,] BoardConfig
        {
            get { return boardConfig; }
            set { boardConfig = value; }
        }        

        public int[] QueenPositions
        {
            get { return queenPositions; }
            set { queenPositions = value; }
        }

        /// <summary>
        /// Constructor to initialize the size of the problem
        /// Create boardConfig and queenPositions
        /// </summary>
        /// <param name="value"></param>
        public NQueensProblem(int value)
        {
            size = value;
            boardConfig = new int[value, value];
            queenPositions = new int[value];
        }

        /// <summary>
        /// Initializes the board randomly
        /// </summary>
        public void InitializeBoard()
        {
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    BoardConfig[i,j] = 0;


            Random random = new Random();
            for (int i = 0; i < Size; i++)
            {
                int next;
                //Assigns a queen to every row
                while (true)
                {
                    next = random.Next(Size);
                    int j;
                    for (j = 0; j < i; j++)
                        if (next == QueenPositions[j])
                            break;
                    if (i == j) break;
                }

                BoardConfig[i,next] = 1;
                QueenPositions[i] = next;
            }
        }

        /// <summary>
        /// Calculates and returns the heuristic value which is the number of conflicts between the queens
        /// </summary>
        /// <returns></returns>
        public int HCost()
        {
            int numberOfConflicts = 0;
            for (int i = 0; i < Size; i++)
            {
                for(int j=0;j<Size;j++)
                {
                    if(j == QueenPositions [i])
                    {
                        for (int k = 0; k < i;k++ )
                            if (j == QueenPositions[k] || (j > QueenPositions[k] && (k+j) == (i + QueenPositions[k])) ||
                                ( j< QueenPositions[k] && (k+QueenPositions [k])== (i+j)))
                                numberOfConflicts++;
                    }
                }
            }
            return numberOfConflicts;
        }


        /// <summary>
        /// Prints a state on the console
        /// </summary>
        public void PrintState()
        {
            Console.WriteLine();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (this.QueenPositions[i] == j) Console.Write(1 + " ");
                    else Console.Write(0 + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Generates the successors for the current node
        /// Returns a list of NQueenProblem objects
        /// </summary>
        /// <returns></returns>
        public List<NQueensProblem> Successors()
        {
            List<NQueensProblem> succ = new List<NQueensProblem>();
            for(int i=0;i<Size;i++)
            {
                for(int j=0; j<Size;j++)
                {
                    if (j != QueenPositions[i])
                        succ.Add( Operation(i, j, QueenPositions[i]));
                }
            }
            return succ;
        }

        /// <summary>
        /// Generate a new NQueensProblem based on parameters row, column and pos
        /// and returns it
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public NQueensProblem Operation(int row, int column, int pos)
        {
            NQueensProblem tempState = new NQueensProblem(Size);
            for (int i = 0; i < Size; i++)
            {
                tempState.QueenPositions[i] = this.QueenPositions[i];
                for (int j = 0; j < Size; j++)
                    tempState.BoardConfig[i, j] = this.BoardConfig[i, j];
            }

            tempState.BoardConfig[row, column] = 1;
            tempState.BoardConfig[row, pos] = 0;
            tempState.QueenPositions[row] = column;

            return tempState;
            
        }
    }
}
