using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Board
    {
        private ChessPiece[] onBoard;

        // EFFECTS: constructs a chess board (8 times 8) with no chess pieces on it
        public Board()
        {
            onBoard = new ChessPiece[64];
            for (int i = 0; i < 64; i++)
            {
                onBoard[i] = null;
            }
        }

        // REQUIRES: onBoard must have a size of 64
        // EFFECTS: constructs a chess board (8 times 8) with given onBoard list
        public Board(ChessPiece[] onBoard)
        {
            this.onBoard = onBoard;
        }

        // EFFECTS: return onBoard
        public ChessPiece[] getOnBoard()
        {
            return onBoard;
        }

        // REQUIRES: cp must be on the board,
        //           x and y must be within the range [1, 8] and must be different from the current x, y
        //           x and y must not be previously occupied
        // MODIFIES: this, cp
        // EFFECTS: change the position of cp on this board to (x, y)
        public void move(ChessPiece cp, int x, int y)
        {
            remove(cp);
            place(cp, x, y);
        }

        // REQUIRES: cp must be on the board
        // MODIFIES: this, cp
        // EFFECTS: remove cp from this board
        public void remove(ChessPiece cp)
        {
            int x = cp.getPosX();
            int y = cp.getPosY();
            Position posn = new Position(x, y);
            onBoard[posn.getIdx()] = null;
        }

        // REQUIRES: cp must not be on the board, x and y must be within the range [1, 8]
        //           x and y must not be previously occupied
        // MODIFIES: this
        // EFFECTS: place cp on this board at given x, y position
        public void place(ChessPiece cp, int x, int y)
        {
            Position posn = new Position(x, y);
            onBoard[posn.getIdx()] = cp;
        }
    }
}
