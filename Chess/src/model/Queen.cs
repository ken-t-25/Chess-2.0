using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Queen : ChessPiece, IEquatable<Queen?>
    {
        // REQUIRES: colour must be one of "black" and "white"
        // EFFECTS: constructs a queen
        public Queen(String colour) : base(colour)
        {
            this.type = "queen";
        }

        // REQUIRES: colour must be one of "black" and "white", x and y must be in the range [1.8]
        // EFFECTS: constructs a queen that is on the game board
        public Queen(String colour, int x, int y) : base(colour, x, y)
        {
            this.type = "queen";
        }

        // REQUIRES: colour must be one of "black" and "white", x and y must be in the range [1.8]
        // EFFECTS: constructs a queen with given information
        public Queen(String colour, int x, int y, bool onBoard, bool move) : base(colour, x, y, onBoard, move)  
        {
            this.type = "queen";
        }

        // EFFECTS: consumes a game board and returns a list of positions that represents
        //          the possible moves of this queen
        public override HashSet<Position> possibleMoves(Game game)
        {
            HashSet<Position> moves = new HashSet<Position>();
            moves.UnionWith(lineTest(game, -1, -1));
            moves.UnionWith(lineTest(game, -1, 1));
            moves.UnionWith(lineTest(game, 1, -1));
            moves.UnionWith(lineTest(game, 1, 1));
            moves.UnionWith(lineTest(game, 0, -1));
            moves.UnionWith(lineTest(game, 0, 1));
            moves.UnionWith(lineTest(game, -1, 0));
            moves.UnionWith(lineTest(game, 1, 0));
            return moves;
        }

        // REQUIRES: posn must not be the same as the current position of this bishop
        // EFFECTS: returns a boolean that tells whether this queen can move to given position(enemy king's position)
        // in one step, ignoring whether the king on the same team will be checked
        public override bool checkEnemy(Game game, Position posn)
        {
            Board bd = game.getBoard();
            int x = posn.getPosX();
            int y = posn.getPosY();
            int diffX = x - posX;
            int diffY = y - posY;
            if (diffX == 0 && diffY == 0)
            {
                return false;
            }
            else if (Math.Abs(diffX) == Math.Abs(diffY))
            {
                int diff = Math.Abs(diffX);
                int deltaX = Math.Abs(diffX) / diffX;
                int deltaY = Math.Abs(diffY) / diffY;
                return checkEnemyDiagonalPath(deltaX, deltaY, posX, posY, diff, bd);
            }
            else if (x == posX)
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
            return Equals(obj as Queen);
        }

        public bool Equals(Queen? other)
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

        public static bool operator ==(Queen? left, Queen? right)
        {
            return EqualityComparer<Queen>.Default.Equals(left, right);
        }

        public static bool operator !=(Queen? left, Queen? right)
        {
            return !(left == right);
        }
    }
}
