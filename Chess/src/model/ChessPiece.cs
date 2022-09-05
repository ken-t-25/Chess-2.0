using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public abstract class ChessPiece : IEquatable<ChessPiece?>
    {
        protected int posX;
        protected int posY;
        protected bool onBoard;
        protected String colour;
        protected bool move;
        protected String type;

        // REQUIRES: colour must be one of "black", "white", or an empty string (stands for a null chess);
        // EFFECTS: an abstract constructor for chess piece
        protected ChessPiece(String colour)
        {
            posX = 0;
            posY = 0;
            this.colour = colour;
            onBoard = false;
            move = false;
        }

        // REQUIRES: colour must be one of "black" and "white", x and y must be in the range [1.8]
        // EFFECTS: an abstract constructor for chess piece that is on the game board
        protected ChessPiece(String colour, int x, int y)
        {
            posX = x;
            posY = y;
            this.colour = colour;
            onBoard = true;
            move = false;
        }

        // REQUIRES: colour must be one of "black" and "white", x and y must be in the range [1.8]
        // EFFECTS: an abstract constructor for chess piece with given information
        protected ChessPiece(String colour, int x, int y, bool onBoard, bool move)
        {
            posX = x;
            posY = y;
            this.colour = colour;
            this.onBoard = onBoard;
            this.move = move;
        }

        // EFFECTS: return the possible next moves that this chess piece can take on given board
        public abstract HashSet<Position> possibleMoves(Game game);

        // REQUIRES: posn must not be the same as the current position of this chess piece
        // EFFECTS: returns a boolean that tells whether this chess piece can move to this position(enemy king's position)
        // in one step, ignoring whether the king on the same team will be checked
        public abstract bool checkEnemy(Game game, Position posn);

        // EFFECTS: return move, which tells whether this chess piece has been moved during the game
        public bool hasMoved()
        {
            return move;
        }

        // EFFECTS: return the colour of this chess piece
        public String getColour()
        {
            return colour;
        }

        // EFFECTS: return a boolean telling whether this chess piece is on board
        public Boolean getOnBoard()
        {
            return onBoard;
        }

        // EFFECTS: return the chess piece's x position (horizontal) on the chess board
        public int getPosX()
        {
            return posX;
        }

        // EFFECTS: return the chess piece's y position (vertical) on the chess board
        public int getPosY()
        {
            return posY;
        }

        // REQUIRES: x must in the interval [1,8]
        // MODIFIES: this
        // EFFECTS: change the x position of this chess piece to the given value
        public void setPosX(int x)
        {
            posX = x;
        }

        // REQUIRES: y must in the interval [1,8]
        // MODIFIES: this
        // EFFECTS: change the y position of this chess piece to the given value
        public void setPosY(int y)
        {
            posY = y;
        }

        // MODIFIES: this
        // EFFECTS: set assign onBoard with the given boolean value
        public void setOnBoard(Boolean b)
        {
            onBoard = b;
        }

        // MODIFIES: this
        // EFFECTS: set assign move with the given boolean value
        public void setMove(Boolean m)
        {
            move = m;
        }

        // REQUIRES: x and y of posn must be with in the range [1, 8]
        // EFFECTS: if given position can be a next move of this chess, return a list containing only this position
        //          otherwise, return an empty list
        protected List<Position> positionTest(Game game, Position posn)
        {
            List<Position> moves = new List<Position>();
            Board bd = game.getBoard();
            int posnIndex = posn.getIdx(); ;
            int initialX = this.posX;
            int initialY = this.posY;
            bool initialMove = move;
            ChessPiece cp = (ChessPiece)bd.getOnBoard()[posnIndex];
            if (cp == null)
            {
                positionEmpty(game, posn, moves, initialX, initialY, initialMove);
            }
            else
            {
                if (!cp.getColour().Equals(colour))
                {
                    hasEnemy(game, posn, moves, initialX, initialY, initialMove, bd, posnIndex);
                }
            }
            return moves;
        }

        // MODIFIES: moves
        // EFFECTS: do position test on a position that is empty
        //          ix = initialX, iy = initialY, im = initialMove
        protected void positionEmpty(Game game, Position posn, List<Position> moves, int ix, int iy, bool im)
        {
            game.move(this, posn.getPosX(), posn.getPosY());
            if (!game.check(colour))
            {
                moves.Add(posn);
            }
            game.move(this, ix, iy);
            this.move = im;
        }

        // MODIFIES: ms (moves)
        // EFFECTS: do position test on a position that is occupied by an enemy
        //          g = game, p = position, ms = moves, tp = testPosition, ix = initialX, iy = initialY,
        //          im = initialMove, b = board, pi = positionIndex
        protected void hasEnemy(Game g, Position p, List<Position> ms, int ix, int iy, bool im, Board b, int pi)
        {
            ChessPiece enemyAttacked = b.getOnBoard()[pi];
            int enemyInitialX = enemyAttacked.getPosX();
            int enemyInitialY = enemyAttacked.getPosY();
            g.remove(enemyAttacked);
            g.move(this, p.getPosX(), p.getPosY());
            if (!g.check(colour))
            {
                ms.Add(p);
            }
            g.move(this, ix, iy);
            g.placeFromOffBoard(enemyAttacked, enemyInitialX, enemyInitialY);
            move = im;
        }

        // REQUIRES: this chess must be one of queen, rook, and bishop
        // EFFECTS: find possible moves of this chess by lines
        protected List<Position> lineTest(Game game, int deltaX, int deltaY)
        {
            List<Position> moves = new List<Position>();
            int x = posX + deltaX;
            int y = posY + deltaY;
            bool blocked = false;
            Board bd = game.getBoard();
            while (x >= 1 && x <= 8 && y >= 1 && y <= 8 && !blocked)
            {
                Position testPosn = new Position(x, y);
                List<Position> testedMove = positionTest(game, testPosn);
                moves.AddRange(testedMove);
                if (bd.getOnBoard()[testPosn.getIdx()] != null)
                {
                    blocked = true;
                }
                x += deltaX;
                y += deltaY;
            }
            return moves;
        }

        // EFFECTS: returns true if there are no blocks on the given diagonal path
        protected bool checkEnemyDiagonalPath(int deltaX, int deltaY, int x, int y, int diff, Board board)
        {
            bool notBlock = true;
            for (int i = 1; i < diff; i++)
            {
                int testX = x + (deltaX * i);
                int testY = y + (deltaY * i);
                Position testPosn = new Position(testX, testY);
                if (board.getOnBoard()[testPosn.getIdx()] != null)
                {
                    notBlock = false;
                    break;
                }
            }
            return notBlock;
        }

        // EFFECTS: returns true if there are no blocks on the given horizontal or vertical path
        protected bool checkEnemyStraightPath(int constance, int change, int changeTo, String direction, Board board)
        {
            bool notBlock = true;
            int diff = changeTo - change;
            int absDiff = Math.Abs(diff);
            int delta = diff / absDiff;
            for (int i = 1; i < absDiff; i++)
            {
                Position testPosn;
                if (direction.Equals("x"))
                {
                    testPosn = new Position(change + (delta * i), constance);
                }
                else
                {
                    testPosn = new Position(constance, change + (delta * i));
                }
                int testIndex = testPosn.getIdx();
                if (board.getOnBoard()[testIndex] != null)
                {
                    notBlock = false;
                }
            }
            return notBlock;
        }

        // EFFECTS: returns chess type
        public String getType()
        {
            return type;
        }

        /**
         * overriden equals, hashcode, and operator functions
         */
        public override bool Equals(object? obj)
        {
            return Equals(obj as ChessPiece);
        }

        public bool Equals(ChessPiece? other)
        {
            return other is not null &&
                   posX == other.posX &&
                   posY == other.posY &&
                   onBoard == other.onBoard &&
                   colour == other.colour &&
                   move == other.move &&
                   type == other.type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(posX, posY, onBoard, colour, move, type);
        }

        public static bool operator ==(ChessPiece? left, ChessPiece? right)
        {
            return EqualityComparer<ChessPiece>.Default.Equals(left, right);
        }

        public static bool operator !=(ChessPiece? left, ChessPiece? right)
        {
            return !(left == right);
        }
    }


}
