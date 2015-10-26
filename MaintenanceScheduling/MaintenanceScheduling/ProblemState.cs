using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceScheduling
{
    /// <summary>
    /// Class that represents a state of the Maintenance Scheduling problem
    /// </summary>
    public class ProblemState
    {
        private int[] capacity, maxPerInterval, intervalsLeft;
        private int[,] solution;
        private int total;
        private int length;
        private int numberOfIntervals;
        int maxNetReservePerInterval;

        public int[] MaxPerInterval
        {
            get { return maxPerInterval; }
            set { maxPerInterval = value; }
        }

        public int[] Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }

        public int[] IntervalsLeft
        {
            get { return intervalsLeft; }
            set { intervalsLeft = value; }
        }

        public int NumberOfIntervals
        {
            get { return numberOfIntervals; }
        }

        public int Length
        {
            get { return length; }
        }

        public int Total
        {
            get { return total; }
            set { total = value; }
        }

        public int[,] Solution
        {
            get { return solution; }
            set { solution = value; }
        }

        public int MaxScheduled
        {
            get { return maxNetReservePerInterval; }
            set { maxNetReservePerInterval = value; }
        }

        /// <summary>
        /// Constructor to initialize the values of the state
        /// Takes the input provided and copies it to capacity, maxPerInterval, intervalsLeft 
        /// calculates the Total capacity 
        /// calculates the maximum net reserve possible for each interval
        /// </summary>
        /// <param name="capacity">reference to the array containing capacity of the power units</param>
        /// <param name="maxPerInterval">reference to the array containing the maximum power needed in each interval</param>
        /// <param name="intervalsLeft">reference to the array containing the intervals needed for each unit to compplete the maintenance</param>
        public ProblemState(int[] capacity, int[] maxPerInterval, int[] intervalsLeft)
        {
            this.capacity = capacity;
            this.maxPerInterval = maxPerInterval;
            this.intervalsLeft = intervalsLeft;
            length = capacity.Length;
            numberOfIntervals = maxPerInterval.Length;
            this.Solution = new int[Length, NumberOfIntervals];

            for (int j = 0; j < Length; j++)
            {
                MaxScheduled += capacity[j] * intervalsLeft[j];
                Total += capacity[j];
            }

            for (int i = 0; i < NumberOfIntervals; i++)
                MaxScheduled += MaxPerInterval[i];

            MaxScheduled = Total * NumberOfIntervals - MaxScheduled;

            //maximum net reserve possible for each interval
            MaxScheduled /= NumberOfIntervals;
        }

        /// <summary>
        /// Inializes a state based on the intervalsLeft array. 
        /// The state created will satisfy the constraint that each unit should be scheduled per given intervals to complete its maintenance
        /// An active unit is set to 0 and scheduled unit is set to 1
        /// </summary>
        public void InitializeState()
        {
            Random rand = new Random();
            for (int i = 0; i < Length; i++)
            {
                int intervals = IntervalsLeft[i];

                for (int j = 0; j < intervals; j++)
                {
                    while (true)
                    {
                        int randInterval = rand.Next(NumberOfIntervals);
                        //Check if random interval is available
                        if (Solution[i, randInterval] == 0)
                        {
                            Solution[i, randInterval] = 1;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the state is valid or not by validating the constraint that minimum power needed per interval should be satisfied
        /// </summary>
        /// <returns>true if state is not-valid else false</returns>
        public bool CheckIntervalConstraint()
        {
            int totalCost = 0;
            for (int j = 0; j < NumberOfIntervals; j++)
            {
                for (int i = 0; i < Length; i++)
                {
                    totalCost += Solution[i, j] * Capacity[i];
                }

                if (Total - totalCost < MaxPerInterval [j] )
                    return true;
                totalCost = 0;
            }

            return false;
        }
        

        /// <summary>
        /// This method calculate the Heuristic cost
        /// Heuristic cost of the problem is defined as 
        /// the sum of the absolute differences of the deviation of the net reserve of each interval from the maximum net reserve possible per interval
        /// </summary>
        /// <returns>the heuristic cost</returns>
        public int HCost()
        {
            int compCost=0;
            int[] totalScheduled = new int[NumberOfIntervals];
            for (int i = 0; i < NumberOfIntervals; i++)
            {
                for (int j = 0; j < Length; j++)
                    totalScheduled[i] += this.Solution[j, i]* Capacity [j];
                totalScheduled[i] = Total - MaxPerInterval[i] - totalScheduled[i];
                totalScheduled[i] = Math.Abs(MaxScheduled - totalScheduled[i]);
                compCost += totalScheduled[i];
            }

            return compCost;
        }

        /// <summary>
        /// Function that takes a value and inverts it to 0 or 1
        /// </summary>
        /// <param name="value"> 0 or 1</param>
        /// <returns> 0 if value is 1 or 1 if value is 0</returns>
        public int inverseState(int value)
        {
            if (value == 1) return 0;
            else return 1;
        }

        /// <summary>
        /// Generate the successors for the Hill Climbing approach
        /// Takes the initial state and changes a state of one unit at a time to find the successors
        /// </summary>
        /// <returns>List of states</returns>
        public List<ProblemState> GenerateNextStates()
        {
            List<ProblemState> successors = new List<ProblemState>();
            ProblemState newState;
            for (int i = 0; i < Length; i++)
            {
                //find positions of 1
                int[] pos = new int[IntervalsLeft[i]];
                int inc = 0;
                for (int j = 0; j < NumberOfIntervals; j++)
                {
                    if (Solution[i, j] == 1)
                    {
                        pos[inc] = j;
                        inc++;
                    }
                }
                for (int k = 0; k < pos.Length; k++)
                    for (int j = 0; j < NumberOfIntervals; j++)
                        if (j != pos[k] && Solution[i, j] != 1)
                        {
                            newState = Operation(i, j, pos[k]);

                            if (!newState.CheckIntervalConstraint())
                                successors.Add(newState);
                        }
            }

            return successors;
        }

        /// <summary>
        /// Changes a position in a given state to create a new state
        /// the position is given as parameters
        /// </summary>
        /// <param name="x">Unit number</param>
        /// <param name="y">interval in which unit must be scheduled</param>
        /// <param name="z">current interval in which unit is scheduled</param>
        /// <returns></returns>
        public ProblemState Operation(int x, int y, int z)
        {
            ProblemState newState = new ProblemState(this.Capacity, this.MaxPerInterval, this.IntervalsLeft);

            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < NumberOfIntervals; j++)
                    newState.Solution[i, j] = this.Solution[i, j];
            }

            newState.Solution[x, y] = 1;
            newState.Solution[x, z] = 0;

            return newState;
        }
    }
}
