using System;
using System.Text;
using cs2;

namespace FamersProblem
{
    /// <summary>
    /// Class representing states of the Farmer's Problem.
    /// Author: George Wells
    /// Version 1.0 (10 May 2015)
    /// </summary>
    class ProblemState
    {
        private const int FARMER = 0;
        private const int GOAT = 1;
        private const int LION = 2;
        private const int CABBAGE = 3;
        private string[] STRING = { "F:", "G", "L", "C" };

        private bool[] leftBank = new bool[4];
        private bool[] rightBank = new bool[4];

        private ProblemState (bool fl, bool gl, bool ll, bool cl, bool fr, bool gr, bool lr, bool cr)
        // Constructor: internal use only
        {
            leftBank[FARMER] = fl;
            leftBank[GOAT] = gl;
            leftBank[LION] = ll;
            leftBank[CABBAGE] = cl;

            rightBank[FARMER] = fr;
            rightBank[GOAT] = gr;
            rightBank[LION] = lr;
            rightBank[CABBAGE] = cr;
        }  // constructor

        /// <summary>
        /// Create initial state, with the farmer, canoe, goat, lion and cabbage all on
        /// the left bank (represented as <TT>[F:GLC] []</TT>).
        /// </summary>
        public ProblemState ( )
            : this(true, true, true, true, false, false, false, false)
        {
        }  // constructor

        /// <summary>
        /// Return list of all possible new states.
        /// This method does <EM>not</EM> take the safety of the states into account
        /// (i.e. all possible states are returned, including unsafe states).
        /// </summary>
        /// <returns>A <TT>GenericList</TT> containing all states that can be reached
        ///   from the current state in one step.</returns>
        public GenericList<ProblemState> GetMoves ( )
        {
            GenericList<ProblemState> lst = new GenericList<ProblemState>();
            // First move only the farmer
            lst.Add(new ProblemState(!leftBank[FARMER], leftBank[GOAT], leftBank[LION], leftBank[CABBAGE],
                                     !rightBank[FARMER], rightBank[GOAT], rightBank[LION], rightBank[CABBAGE]));
            // Try to move farmer and goat
            if ((leftBank[FARMER] && leftBank[GOAT]) || (rightBank[FARMER] && rightBank[GOAT]))
                lst.Add(new ProblemState(!leftBank[FARMER], !leftBank[GOAT], leftBank[LION], leftBank[CABBAGE],
                                         !rightBank[FARMER], !rightBank[GOAT], rightBank[LION], rightBank[CABBAGE]));
            // Try to move farmer and lion
            if ((leftBank[FARMER] && leftBank[LION]) || (rightBank[FARMER] && rightBank[LION]))
                lst.Add(new ProblemState(!leftBank[FARMER], leftBank[GOAT], !leftBank[LION], leftBank[CABBAGE],
                                       !rightBank[FARMER], rightBank[GOAT], !rightBank[LION], rightBank[CABBAGE]));
            // Try to move farmer and cabbage
            if ((leftBank[FARMER] && leftBank[CABBAGE]) || (rightBank[FARMER] && rightBank[CABBAGE]))
                lst.Add(new ProblemState(!leftBank[FARMER], leftBank[GOAT], leftBank[LION], !leftBank[CABBAGE],
                                       !rightBank[FARMER], rightBank[GOAT], rightBank[LION], !rightBank[CABBAGE]));

            return lst;
        } // GetMoves

        private bool isSafe (bool[] bank)
        // Check whether given bank is safe
        {
            bool safe = true; // Initial assumption
            if (bank[GOAT])
                safe = safe && !bank[LION];
            if (bank[CABBAGE])
                safe = safe && !bank[GOAT];
            return safe;
        } // isSafe

        /// <summary>
        /// Check whether the current state is safe.
        /// Only needs to check opposite side to the farmer to ensure that there
        /// is no dangerous configuration: lion and goat, or goat and cabbage.
        /// </summary>
        /// <returns>true if this state is safe.</returns>
        public bool IsSafe ( )
        {
            if (leftBank[FARMER])
                return isSafe(rightBank);
            else
                return isSafe(leftBank);
        } // IsSafe

        /// <summary>
        /// Tell if this is the final state (represented as <TT>[] [F:GLC]</TT>).
        /// </summary>
        /// <returns>true if this is the final state.</returns>
        public bool IsFinal ( )
        {
            return (!leftBank[FARMER] && !leftBank[GOAT] && !leftBank[LION] && !leftBank[CABBAGE]
                    && rightBank[FARMER] && rightBank[GOAT] && rightBank[LION] && rightBank[CABBAGE]);
        } // IsFinal

        private void bankToString (StringBuilder sb, bool[] bank)
        // Convert a representation of one bank of the river to a string
        {
            int k;
            for (k = 0; k < bank.Length; k++)
                if (bank[k])
                    sb.Append(STRING[k]);
        } // bankToString

        /// <summary>
        /// Return a string representing the current state.
        /// The two banks are represented by lists enclosed in square brackets
        /// The items are represented by letters: F = Farmer (and canoe);
        /// G = Goat; L = Lion; C = Cabbage
        /// e.g. <TT>"[F:C] [GL]"</TT>, meaning the Farmer and the Cabbage are on the
        /// left bank and the Goat and the Lion are on the right bank (for now anyway!).
        /// </summary>
        /// <returns>String representation of state.</returns>
        public override string ToString ( )
        {
            StringBuilder sb = new StringBuilder("[");
            bankToString(sb, leftBank);
            sb.Append("] [");
            bankToString(sb, rightBank);
            sb.Append("]");
            return sb.ToString();
        } // ToString

        /// <summary>
        /// Tell whether two states are equal.  This is the case if the two states have exactly the same
        /// configuration of farmer, goat, lion and cabbage on the same banks.
        /// </summary>
        /// <returns>true if <TT>o</TT> is an instance of <TT>ProblemState</TT>
        ///   and describes exactly the same configuration as this state.</returns>
        public override bool Equals (Object o)
        {
            if (o is ProblemState)
            {
                ProblemState other = (ProblemState)o;
                for (int k = 0; k < 4; k++)
                    if (leftBank[k] != other.leftBank[k] || rightBank[k] != other.rightBank[k])
                        return false;
                return true;
            }
            else // Not even right class!
                return false;
        } // Equals

        /// <summary>
        /// Obtain hashcode for this problem state.  All hashcodes are unique.
        /// </summary>
        /// <returns>hash code for this <TT>ProblemState</TT>.</returns>
        public override int GetHashCode ( )
        {
            int hashcode = 0;
            for (int k = 0; k < 4; k++)
            {
                if (leftBank[k])
                    hashcode += 1 << k;
                if (rightBank[k])
                    hashcode += 1 << (k + 4);
            }
            return hashcode;
        } // GetHashCode

    } // class ProblemState
}
