using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Bishop : ChessPiece, IEquatable<Bishop?>
    {
        // REQUIRES: colour must be one of "black" and "white"
        // EFFECTS: constructs a bishop
        public Bishop(String colour) : base(colour) 
        {
            this.type = "bishop";
        }

        // REQUIRES: colour must be one of "black" and "white", x and y must be in the range [1.8]
        // EFFECTS: constructs a bishop that is on the game board
        public Bishop(String colour, int x, int y) : base(colour, x, y)
        {
            this.type = "bishop";
        }

        // REQUIRES: colour must be one of "black" and "white", x and y must be in the range [1.8]
        // EFFECTS: constructs a bishop with given information
        public Bishop(String colour, int x, int y, bool onBoard, bool move) : base(colour, x, y, onBoard, move)
        {
            this.type = "bishop";
        }

        // EFFECTS: consumes a game board and returns a list of positions that represents
        //          the possible moves of this bishop
        public override HashSet<Position> possibleMoves(Game game)
        {
            HashSet<Position> moves = new HashSet<Position>();
            moves.UnionWith(lineTest(game, -1, -1));
            moves.UnionWith(lineTest(game, -1, 1));
            moves.UnionWith(lineTest(game, 1, -1));
            moves.UnionWith(lineTest(game, 1, 1));
            return moves;
        }

        // REQUIRES: posn must not be the same as the current position of this bishop
        // EFFECTS: returns a boolean that tells whether this bishop can move to given position(enemy king's position)
        // in one step, ignoring whether the king on the same team will be checked
        public override bool checkEnemy(Game game, Position posn)
        {
            Board bd = game.getBoard();
            int x = posn.getPosX();
            int y = posn.getPosY();
            int diffX = x - posX;
            int diffY = y - posY;
            if (Math.Abs(diffX) == Math.Abs(diffY) && diffX != 0)
            {
                int deltaX = Math.Abs(diffX) / diffX;
                int deltaY = Math.Abs(diffY) / diffY;
                int diff = Math.Abs(diffX);
                return checkEnemyDiagonalPath(deltaX, deltaY, posX, posY, diff, bd);
            }
            else
            {
                return false;
            }
        }

        /**
         * overriden equals, hashcode, and operator functions
         */
        public override bool Equals(object? obj)
        {
            return Equals(obj as Bishop);
        }

        public bool Equals(Bishop? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   posX == other.posX &&
                   posY == other.posY &&
                   onBoard == other.onBoard &&
                   colour == other.colour &&
                   move == other.move &&
                   type == other.type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), posX, posY, onBoard, colour, move, type);
        }

        public static bool operator ==(Bishop? left, Bishop? right)
        {
            return EqualityComparer<Bishop>.Default.Equals(left, right);
        }

        public static bool operator !=(Bishop? left, Bishop? right)
        {
            return !(left == right);
        }
    }
}
