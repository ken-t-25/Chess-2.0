using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Knight : ChessPiece, IEquatable<Knight?>
    {
        // REQUIRES: colour must be one of "black" and "white"
        // EFFECTS: constructs a knight
        public Knight(String colour) : base(colour)
        {
            this.type = "knight";
        }

        // REQUIRES: colour must be one of "black" and "white", x and y must be in the range [1.8]
        // EFFECTS: constructs a knight that is on the game board
        public Knight(String colour, int x, int y) : base(colour, x, y)
        {
            this.type = "knight";
        }

        // REQUIRES: colour must be one of "black" and "white", x and y must be in the range [1.8]
        // EFFECTS: constructs a knight with given information
        public Knight(String colour, int x, int y, bool onBoard, bool move) : base(colour, x, y, onBoard, move) 
        {
            this.type = "knight";
        }

        // EFFECTS: consumes a game board and returns a list of positions that represents
        //          the possible moves of this knight
        public override HashSet<Position> possibleMoves(Game game)
        {
            HashSet<Position> moves = new HashSet<Position>();
            moves.UnionWith(knightPositionTest(game, posX - 1, posY - 2));
            moves.UnionWith(knightPositionTest(game, posX - 2, posY - 1));
            moves.UnionWith(knightPositionTest(game, posX - 1, posY + 2));
            moves.UnionWith(knightPositionTest(game, posX - 2, posY + 1));
            moves.UnionWith(knightPositionTest(game, posX + 1, posY - 2));
            moves.UnionWith(knightPositionTest(game, posX + 2, posY - 1));
            moves.UnionWith(knightPositionTest(game, posX + 1, posY + 2));
            moves.UnionWith(knightPositionTest(game, posX + 2, posY + 1));
            return moves;
        }

        // EFFECTS: find possible moves of a knight by positions
        private List<Position> knightPositionTest(Game game, int x, int y)
        {
            List<Position> moves = new List<Position>();
            Position testPosition = new Position(x, y);
            if (x >= 1 && x <= 8 && y >= 1 && y <= 8)
            {
                moves = positionTest(game, testPosition);
            }
            return moves;
        }

        // REQUIRES: posn must not be the same as the current position of this king
        // EFFECTS: returns a boolean that tells whether this knight can move to given position(enemy king's position)
        //          in one step, ignoring whether the king on the same team will be checked
        public override bool checkEnemy(Game game, Position posn)
        {
            int x = posn.getPosX();
            int y = posn.getPosY();
            int diffX = x - posX;
            int diffY = y - posY;
            return Math.Abs(diffX) + Math.Abs(diffY) == 3 && diffX != 0 && diffY != 0;
        }

        /**
         * overriden equals, hashcode, and operator functions
         */
        public override bool Equals(object? obj)
        {
            return Equals(obj as Knight);
        }

        public bool Equals(Knight? other)
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

        public static bool operator ==(Knight? left, Knight? right)
        {
            return EqualityComparer<Knight>.Default.Equals(left, right);
        }

        public static bool operator !=(Knight? left, Knight? right)
        {
            return !(left == right);
        }
    }
}
