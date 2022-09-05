using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Position : IEquatable<Position?>
    {
        private readonly int posX;
        private readonly int posY;
        private readonly int index;

        // REQUIRES: x and y must be in the range [1, 8]
        // EFFECTS: constructs a position on a chess board
        public Position(int x, int y)
        {
            this.posX = x;
            this.posY = y;
            this.index = 8 * (y - 1) + x - 1;
        }

        // EFFECTS: returns posX of this position
        public int getPosX()
        {
            return posX;
        }

        // EFFECTS: returns posY of this position
        public int getPosY()
        {
            return posY;
        }

        // EFFECTS: return corresponding index of this position
        public int getIdx()
        {
            return index;
        }

        // EFFECTS: returns a boolean representing whether this position is attacked by the given list of chess pieces
        public Boolean attacked(HashSet<ChessPiece> chessPieces, Game game)
        {
            bool attacked = false;
            foreach (ChessPiece cp in chessPieces)
            {
                if (cp.checkEnemy(game, this))
                {
                    attacked = true;
                    break;
                }
            }
            return attacked;
        }



        /**
         * overriden equals and hashcode functions
         */
        public override bool Equals(object? obj)
        {
            return Equals(obj as Position);
        }

        public bool Equals(Position? other)
        {
            return other is not null &&
                   posX == other.posX &&
                   posY == other.posY &&
                   index == other.index;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(posX, posY, index);
        }
    }
}
