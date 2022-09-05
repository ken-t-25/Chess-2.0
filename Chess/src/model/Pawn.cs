using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Pawn : ChessPiece, IEquatable<Pawn?>
    {
        private int direction;

        // REQUIRES: colour must be one of "black" and "white"
        // EFFECTS: constructs a pawn
        public Pawn(String colour) : base(colour)
        {
            if (colour.Equals("white"))
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }
            this.type = "pawn";
        }

        // REQUIRES: colour must be one of "black" and "white", x and y must be in the range [1.8]
        // EFFECTS: constructs a pawn that is on the game board
        public Pawn(String colour, int x, int y) : base(colour, x, y)
        {
            if (colour.Equals("white"))
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }
            this.type = "pawn";
        }

        // REQUIRES: colour must be one of "black" and "white", x and y must be in the range [1.8]
        // EFFECTS: constructs a pawn with given information
        public Pawn(String colour, int x, int y, bool onBoard, bool move) : base(colour, x, y, onBoard, move)
        {
            if (colour.Equals("white"))
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }
            this.type = "pawn";
        }

        // EFFECTS: consumes a game board and returns a list of positions that represents
        //          the possible moves of this pawn
        public override HashSet<Position> possibleMoves(Game game)
        {
            HashSet<Position> moves = new HashSet<Position>();
            if (!move)
            {
                Board bd = game.getBoard();
                Position skip = new Position(posX, posY + direction);
                if (posY + direction >= 1 && posY + direction <= 8 && bd.getOnBoard()[skip.getIdx()] == null)
                {
                    moves.UnionWith(pawnPositionTest(game, posX, posY + (2 * direction)));
                }
            }
            moves.UnionWith(pawnPositionTest(game, posX, posY + direction));
            moves.UnionWith(attackTest(game, posX - 1, posY + direction));
            moves.UnionWith(attackTest(game, posX + 1, posY + direction));
            return moves;
        }

        // REQUIRES: x is within the range [1, 8]
        // EFFECTS: find possible moves of a pawn by positions (excluding attack moves)
        private List<Position> pawnPositionTest(Game game, int x, int y)
        {
            List<Position> moves = new List<Position>();
            Position testPosition = new Position(x, y);
            if (y >= 1 && y <= 8)
            {
                Board bd = game.getBoard();
                int initialX = this.posX;
                int initialY = this.posY;
                bool initialMove = move;
                if (bd.getOnBoard()[testPosition.getIdx()] == null)
                {
                    game.move(this, x, y);
                    if (!game.check(colour))
                    {
                        moves.Add(testPosition);
                    }
                    game.move(this, initialX, initialY);
                    move = initialMove;
                }
            }
            return moves;
        }

        // EFFECTS: find possible attack moves of a pawn by positions
        private List<Position> attackTest(Game game, int x, int y)
        {
            List<Position> moves = new List<Position>();
            Position testPosition = new Position(x, y);
            if (x >= 1 && x <= 8 && y >= 1 && y <= 8)
            {
                Board bd = game.getBoard();
                int posnIndex = testPosition.getIdx();
                int initialX = this.posX;
                int initialY = this.posY;
                bool initialMove = move;
                if (bd.getOnBoard()[posnIndex] != null)
                {
                    if (!bd.getOnBoard()[posnIndex].getColour().Equals(colour))
                    {
                        hasEnemy(game, bd, posnIndex, moves, testPosition, initialX, initialY, initialMove);
                    }
                }
            }
            return moves;
        }

        // MODIFIES: ms
        // EFFECTS: do a pawn attack test on a position that is occupied by an enemy
        //          g = game, bd = board, pi = positionIndex, ms = moves, tp = testPosition, ix = initialX
        //          iy = initialY, im = initialMove
        private void hasEnemy(Game g, Board bd, int pi, List<Position> ms, Position tp, int ix, int iy, bool im)
        {
            ChessPiece enemyAttacked = bd.getOnBoard()[pi];
            int enemyX = enemyAttacked.getPosX();
            int enemyY = enemyAttacked.getPosY();
            g.remove(enemyAttacked);
            g.move(this, tp.getPosX(), tp.getPosY());
            if (!g.check(colour))
            {
                ms.Add(tp);
            }
            g.move(this, ix, iy);
            g.placeFromOffBoard(enemyAttacked, enemyX, enemyY);
            move = im;
        }

        // REQUIRES: posn must not be the same as the current position
        // EFFECTS: returns a boolean that tells whether this pawn can move to given position(enemy king's position)
        //          in one step, ignoring whether the king on the same team will be checked
        public override bool checkEnemy(Game game, Position posn)
        {
            int x = posn.getPosX();
            int y = posn.getPosY();
            int diffX = x - posX;
            int diffY = y - posY;
            return diffY == direction && Math.Abs(diffX) == 1;
        }

        /**
         * overriden equals, hashcode, and operator functions
         */
        public override bool Equals(object? obj)
        {
            return Equals(obj as Pawn);
        }

        public bool Equals(Pawn? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   posX == other.posX &&
                   posY == other.posY &&
                   onBoard == other.onBoard &&
                   colour == other.colour &&
                   move == other.move &&
                   type == other.type &&
                   direction == other.direction;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), posX, posY, onBoard, colour, move, type, direction);
        }

        public static bool operator ==(Pawn? left, Pawn? right)
        {
            return EqualityComparer<Pawn>.Default.Equals(left, right);
        }

        public static bool operator !=(Pawn? left, Pawn? right)
        {
            return !(left == right);
        }
    }
}
