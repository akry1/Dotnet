using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    /// <summary>
    /// Implementation of Constraint Satisfaction Problem approach to solve n-queens problem
    /// The QueenPositions array in the NQueensProblem class is used as the Variables of the problem
    /// </summary>
    class CSP
    {
        /// <summary>
        /// Checks if the 'state' satisfies the constraints
        /// returns 0 if all the constraints are satisfied
        /// returns a random conflicted Variable if one or more constraints are violated
        /// count parameter is used to track the number of constraint violations
        /// </summary>
        /// <param name="state"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int CheckConstraints(NQueensProblem state,out int count)
        {
            List<int> index = new List<int>();
            Random rand = new Random();

            //Constraing 1: Every row must have a queen          
            for (int i = 0; i < state.Size; i++)
            {
                for (int j = i+1; j < state.Size;j++ )
                  if( state .QueenPositions [i] == state.QueenPositions [j])
                  {
                      index .Add (j);
                  }
            }
            
            for (int i = 0; i < state.Size; i++)
            {
                for (int j = 0; j < state.Size; j++)
                {
                    if (j == state.QueenPositions[i])
                    {
                        for (int k = 0; k < i; k++)
                            if (j == state.QueenPositions[k] || (j > state.QueenPositions[k] && k + j == i + state.QueenPositions[k]) ||
                                (j < state.QueenPositions[k] && (k + state.QueenPositions[k]) == (i + j)))
                            {
                                index.Add(i);
                            }
                    }
                }
            }

            //Constraint 2: No conflicts between the queens
            if (index.Count != 0)
            {
                count = index.Count;
                return index[rand.Next(index.Count)];
            }
            else count = 0;
            return 0;
        }

        /// <summary>
        /// Implements the Min_Conflicts algorithm for CSP by local search
        /// InitialState is generated randomly and it is assigned to current
        /// If all the constraints are satisfied in current then it is returned
        /// else a random conflicted variable is taken and
        /// its value is chosen such that it minimizes the number of conflicts 
        /// Variable in current is set to that value
        /// maxsteps parameter determines the number of steps allowed before giving up
        /// returns the goal state if successful or null if failure
        /// </summary>
        /// <param name="intitialState"></param>
        /// <param name="maxSteps"></param>
        /// <returns></returns>
        public static NQueensProblem MinConlicts(NQueensProblem intitialState, int maxSteps)
        {
            NQueensProblem current = intitialState;            
            for (int i = 1; i < maxSteps; i++)
            {
                int conflicts;
                int var = CheckConstraints(current,out conflicts);
                if (var == 0)
                    return current;
                else
                {
                    NQueensProblem tempState = new NQueensProblem(current.Size);
                    for (int j = 0; j < current.Size; j++)
                    {
                        tempState.QueenPositions[j] = current.QueenPositions[j];
                        for (int k = 0; k < current.Size; k++)
                            tempState.BoardConfig[j, k] = current.BoardConfig[j, k];
                    }
                    int hcost=0; bool flag=true;
                    List<int> value = new List<int>(); ;
                    for (int j = 0; j < current.Size; j++)
                    {
                        if (j != current.QueenPositions[var])
                        {
                             
                            tempState.QueenPositions[var] = j;
                            if (flag)
                            {
                                CheckConstraints (tempState ,out hcost);
                                value.Add(j); 
                                flag = false;
                                continue;
                            }

                            CheckConstraints(tempState, out conflicts);
                            if (conflicts  == hcost)
                            {
                                value.Add (j);
                            }
                            else if(conflicts < hcost )
                            {
                                value.Clear();
                                hcost = conflicts;
                                value.Add(j);
                            }
                        }
                    }

                    Random rand= new Random ();
                    tempState.QueenPositions[var] = value[rand.Next (value.Count )];
                    current = tempState;
                    Program.numberOfSteps++;
                }
            }
            return null;
        }
    }
}
