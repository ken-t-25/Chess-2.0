using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Rook : ChessPiece, IEquatable<Rook?>
    {
        // REQUIRES: colour must be one of "black" and "white"
        // EFFECTS: constructs a rook
        public Rook(String colour) : base(colour)
        {
            this.type = "rook";
        }

        // REQUIRES: colour must be one of "black" and "white", x and y must be in the range [1.8]
        // EFFECTS: constructs a rook that is on the game board
        public Rook(String colour, int x, int y) : base(colour, x, y)
        {
            this.type = "rook";
        }

        // REQUIRES: colour must be one of "black" and "white", x and y must be in the range [1.8]
        // EFFECTS: constructs a rook with given information
        public Rook(String colour, int x, int y, bool onBoard, bool move) : base(colour, x, y, onBoard, move)
        {
            this.type = "rook";
        }

        // EFFECTS: consumes a game board and returns a list of positions that represents
        //          the possible moves of this rook
        public override HashSet<Position> possibleMoves(Game game)
        {
            HashSet<Position> moves = new HashSet<Position>();
            moves.UnionWith(lineTest(game, 0, -1));
            moves.UnionWith(lineTest(game, 0, 1));
            moves.UnionWith(lineTest(game, -1, 0));
            moves.UnionWith(lineTest(game, 1, 0));
            return moves;
        }

        // REQUIRES: posn must not be the same as the current position of this rook
        // EFFECTS: returns a boolean that tells whether this rook can move to given position(enemy king's position)
        // in one step, ignoring whether the king on the same team will be checked
        public override bool checkEnemy(Game game, Position posn)
        {
            Board bd = game.getBoard();
            int x = posn.getPosX();
            int y = posn.getPosY();
            if (x == posX)
            {
                return checkEnemyStraightPath(posX, posY, y, "y", bd);
            }
            else if (y == posY)
            {
                return checkEnemyStraightPath(posY, posX, x, "x", bd);
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
            return Equals(obj as Rook);
        }

        public bool Equals(Rook? other)
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

        public static bool operator ==(Rook? left, Rook? right)
        {
            return EqualityComparer<Rook>.Default.Equals(left, right);
        }

        public static bool operator !=(Rook? left, Rook? right)
        {
            return !(left == right);
        }
    }
}
