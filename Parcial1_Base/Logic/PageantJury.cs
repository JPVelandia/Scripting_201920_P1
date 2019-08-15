using System.Collections.Generic;
using System.Linq;

namespace Parcial1_Base.Logic
{
    public class PageantJury
    {
        /// <summary>
        /// The max count of contestants accepeted.
        /// </summary>
        private const int MAX_CONTESTANTS = 4;

        /// <summary>
        /// The contestants collection.
        /// </summary>
        private List<Doll> contestants = new List<Doll>(MAX_CONTESTANTS);

        /// <summary>
        /// The sorted contestants collection after the results.
        /// </summary>
        private List<Doll> sortedContestants;

        /// <summary>
        /// Returns the total contestants count for a pageant round.
        /// </summary>
        public int TotalContestants { get => contestants.Count; }

        /// <summary>
        /// Adds a contestant to the pageant.
        /// </summary>
        /// <param name="d"></param>
        /// <returns>True if the contestant could be added, False otherwise</returns>
        public bool AddContestant(Doll d)
        {
            bool result = false;

            if (contestants.Count < MAX_CONTESTANTS && d.CanParticipate && !contestants.Contains(d))
            {
                contestants.Add(d);
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Clears the contestants collection
        /// </summary>
        public void ClearContestants()
        {
            contestants.Clear();
        }

        /// <summary>
        /// Returns the winner of the pageant
        /// </summary>
        /// <returns>The winner Doll</returns>
        public Doll GetWinner()
        {
            Doll winner = null;

            switch (contestants.Count)
            {
                case 0:
                    //No contestant should win in a zero-contestant contest.
                    break;

                case 1:
                    // Single contestant is deemed winner, no matter its score.
                    winner = contestants[0];
                    break;

                default:
                    //Sort descending the contestants by Style score, retrieve the first of them.
                    sortedContestants = contestants.OrderByDescending(doll => doll.Style).ToList();
                    return sortedContestants[0];
            }

            return winner;
        }
    }
}