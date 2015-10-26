using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceScheduling
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
        /// <param name="initialState">ProblemState instance which is the start state</param>
        /// <returns>ProblemState instance which is the solution state</returns>
        public static ProblemState HillClimbing(ProblemState initialState)
        {
            ProblemState current = initialState ;
            while(true)
            {
                if (Program.numberOfSteps < 100)
                {
                    //Generate the successors
                    List<ProblemState> successors = current.GenerateNextStates();
                    if (successors.Count>0)
                    {
                        List<ProblemState> bestSuccessor = new List<ProblemState>();
                        bestSuccessor.Add(successors[0]);
                        //Select the best successor
                        foreach (ProblemState successor in successors)
                        {
                            if (successor.HCost() == bestSuccessor[0].HCost())
                                bestSuccessor.Add(successor);
                            else if (successor.HCost() < bestSuccessor[0].HCost())
                            {
                                bestSuccessor.Clear();
                                bestSuccessor.Add(successor);
                            }
                        }

                        Program.numberOfSteps++;
                        Random random = new Random();
                        int chooseOneSuccessor = random.Next(bestSuccessor.Count);
                        //if current better than best successor return current
                        if (bestSuccessor[chooseOneSuccessor].HCost() > current.HCost()) return current;
                        //else set current to best successor
                        current = bestSuccessor[chooseOneSuccessor];
                    }
                    else
                    {
                        //enters here if the Hill-Climbing is stuck due to no successors
                        Program.numberOfInits++;
                        Program.numberOfSteps = 0;
                        initialState.Solution = new int[initialState.Length, initialState.NumberOfIntervals];
                        initialState.InitializeState();
                        return HillClimbing(initialState);
                    }
                }
                else
                {
                    //enters here if the Hill-Climbing is stuck due to local maximum or plateau or ridge
                    Program.numberOfInits++;
                    Program.numberOfSteps = 0;
                    initialState.Solution = new int[initialState.Length, initialState.NumberOfIntervals];
                    initialState.InitializeState();
                    return HillClimbing(initialState);
                }
            }
        }
    }
}
