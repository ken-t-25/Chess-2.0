using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Moves
    {
        List<Move> moves;

        // EFFECTS: constructs a moves object with given list of moves
        public Moves(List<Move> moves)
        {
            this.moves = moves;
        }

        // EFFECTS: constructs a moves object that represents the moves that took place in a hand
        public Moves()
        {
            moves = new List<Move>();
        }

        // MODIFIES: this
        // EFFECTS: add given move to this moves
        public void addMove(Move m)
        {
            moves.Add(m);
        }

        // EFFECTS: this turn this moves as a list of moves
        public List<Move> getMoves()
        {
            return moves;
        }
    }
}
