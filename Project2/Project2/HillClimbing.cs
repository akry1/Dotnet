using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    /// <summary>
    /// Implementation of Hill Climbing approach to solve n-queens problem
    /// </summary>
    class HillClimbingProblem
    {
        /// <summary>
        /// Takes the initialState as the parameter
        /// generates its successors and select the best successor based on heuristic
        /// </summary>
        /// <param name="initialState"></param>
        /// <returns></returns>
        public static NQueensProblem HillClimbing(NQueensProblem initialState)
        {
            NQueensProblem current = initialState ;
            while(true)
            {
                if (Program.numberOfSteps < 100)
                {
                    //Generate the successors
                    List<NQueensProblem> successors = current.Successors();
                    List<NQueensProblem> bestSuccessor= new List<NQueensProblem>() ;
                    bestSuccessor.Add(successors[0]);
                    //Select the best successor
                    foreach (NQueensProblem successor in successors)
                    {
                        if (successor.Conflicts == bestSuccessor[0].Conflicts)
                            bestSuccessor.Add(successor);
                        else if (successor.Conflicts < bestSuccessor[0].Conflicts)
                        {
                            bestSuccessor.Clear();
                            bestSuccessor.Add ( successor);
                        }
                    }

                    Program.numberOfSteps++;
                    Random random = new Random();
                    int chooseOneSuccessor = random.Next (bestSuccessor .Count );
                    //if current better than best successor return current
                    if (bestSuccessor[chooseOneSuccessor].Conflicts > current.Conflicts) return current;
                    //else set current to best successor
                    current = bestSuccessor[chooseOneSuccessor];                    
                }
                else
                {
                    //enters here if the Hill-Climbing is stuck due to local maximum or plateau or ridge
                    Program.numberOfInits++;
                    Program.numberOfSteps = 0;
                    initialState.InitializeBoard();
                    return HillClimbing(initialState);
                }
            }
        }
    }
}
