using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class King : ChessPiece, IEquatable<King?>
    {
        // REQUIRES: colour must be one of "black" and "white"
        // EFFECTS: constructs a king
        public King(String colour) : base(colour)
        {
            this.type = "king";
        }

        // REQUIRES: colour must be one of "black" and "white", x and y must be in the range [1.8]
        // EFFECTS: constructs a ing that is on the game board
        public King(String colour, int x, int y) : base(colour, x, y)
        {
            this.type = "king";
        }

        // REQUIRES: colour must be one of "black" and "white", x and y must be in the range [1.8]
        // EFFECTS: constructs a king with given information
        public King(String colour, int x, int y, bool onBoard, bool move) : base(colour, x, y, onBoard, move)
        {
            this.type = "king";
        }

        // EFFECTS: consumes a game board and returns a list of positions that represents
        //          the possible moves of this king
        public override HashSet<Position> possibleMoves(Game game)
        {
            HashSet<Position> moves = new HashSet<Position>();
            int x = posX;
            int y = posY;
            moves.UnionWith(kingPositionTest(game, x - 1, y - 1));
            moves.UnionWith(kingPositionTest(game, posX - 1, posY));
            moves.UnionWith(kingPositionTest(game, posX - 1, posY + 1));
            moves.UnionWith(kingPositionTest(game, posX, posY - 1));
            moves.UnionWith(kingPositionTest(game, posX, posY + 1));
            moves.UnionWith(kingPositionTest(game, posX + 1, posY - 1));
            moves.UnionWith(kingPositionTest(game, posX + 1, posY));
            moves.UnionWith(kingPositionTest(game, posX + 1, posY + 1));
            moves.UnionWith(castling(game));
            return moves;
        }

        // EFFECTS: find possible moves of a king by positions (excluding castling)
        private List<Position> kingPositionTest(Game game, int x, int y)
        {
            List<Position> moves = new List<Position>();
            Position testPosition = new Position(x, y);
            if (x >= 1 && x <= 8 && y >= 1 && y <= 8)
            {
                moves = positionTest(game, testPosition);
            }
            return moves;
        }

        // EFFECTS: find possible castling moves
        private List<Position> castling(Game game)
        {
            List<Position> moves = new List<Position>();
            if (!move && !game.check(colour))
            {
                HashSet<ChessPiece> rooks = new HashSet<ChessPiece>();
                HashSet<ChessPiece> examinedList;
                if (colour.Equals("white"))
                {
                    examinedList = game.getWhiteChessPiecesOnBoard();
                }
                else
                {
                    examinedList = game.getBlackChessPiecesOnBoard();
                }
                foreach (ChessPiece cp in examinedList)
                {
                    if (cp is Rook) 
                    {
                        rooks.Add(cp);
                    }
                }
                foreach (ChessPiece rook in rooks)
                {
                    moves.AddRange(testRook(rook, game));
                }
            }
            return moves;
        }

        // EFFECTS: test whether given rook can perform castling move with this king,
        //          if can, return a list that contains the corresponding move of this king
        //          otherwise, return an empty list
        private List<Position> testRook(ChessPiece rook, Game game)
        {
            List<Position> moves = new List<Position>();
            int rookX = rook.getPosX();
            int difference = rookX - posX;
            int absDiff = Math.Abs(difference);
            if (!rook.hasMoved() && rook.getPosY() == posY && (absDiff == 3 || absDiff == 4))
            {
                int direction = difference / absDiff;
                Position testPosn1 = new Position(posX + direction, posY);
                Position testPosn2 = new Position(posX + (2 * direction), posY);
                bool test3 = true;
                if (absDiff == 4)
                {
                    Position testPosn3 = new Position(posX + (3 * direction), posY);
                    test3 = (game.getBoard().getOnBoard()[testPosn3.getIdx()] == null);
                }
                List<Position> test1 = castlingPositionTest(game, testPosn1);
                List<Position> test2 = castlingPositionTest(game, testPosn2);
                if (test1.Count > 0  && test2.Count > 0 && test3)
                {
                    moves.Add(testPosn2);
                }
            }
            return moves;
        }

        // EFFECTS: test whether given posn that is in the path of castling meets the castling criteria
        //          (i.e. posn must be empty and king must not be attacked if move to posn),
        //          if test passed, return a list that contains the passed position, otherwise return an empty list
        private List<Position> castlingPositionTest(Game game, Position posn)
        {
            List<Position> moves = new List<Position>();
            Board bd = game.getBoard();
            int initialX = this.posX;
            int initialY = this.posY;
            bool initialMove = move;
            if (bd.getOnBoard()[posn.getIdx()] == null)
            {
                game.move(this, posn.getPosX(), posn.getPosY());
                if (!game.check(colour))
                {
                    moves.Add(posn);
                }
                game.move(this, initialX, initialY);
                move = initialMove;
            }
            return moves;
        }

        // REQUIRES: posn must not be the same as the current position of this king
        // EFFECTS: returns a boolean that tells whether this king can move to given position(enemy king's position)
        // in one step, ignoring whether this king will be checked
        public override bool checkEnemy(Game game, Position posn)
        {
            int x = posn.getPosX();
            int y = posn.getPosY();
            int diffX = x - posX;
            int diffY = y - posY;
            return (Math.Abs(diffX) + Math.Abs(diffY) <= 2) && (Math.Abs(diffX) == 1 || Math.Abs(diffY) == 1);
        }

        /**
         * overriden equals, hashcode, and operator functions
         */
        public override bool Equals(object? obj)
        {
            return Equals(obj as King);
        }

        public bool Equals(King? other)
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

        public static bool operator ==(King? left, King? right)
        {
            return EqualityComparer<King>.Default.Equals(left, right);
        }

        public static bool operator !=(King? left, King? right)
        {
            return !(left == right);
        }
    }
}
