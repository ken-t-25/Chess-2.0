using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Move
    {
        private readonly int beginX;
        private readonly int beginY;
        private readonly int endX;
        private readonly int endY;
        private readonly ChessPiece chess;
        private readonly bool moveStatus;

        // EFFECTS: constructs a move
        public Move(int beginX, int beginY, int endX, int endY, ChessPiece chess, bool moveStatus)
        {
            this.beginX = beginX;
            this.beginY = beginY;
            this.endX = endX;
            this.endY = endY;
            this.chess = chess;
            this.moveStatus = moveStatus;
        }

        // EFFECTS: returns the beginning x position of the move
        public int getBeginX()
        {
            return beginX;
        }

        // EFFECTS: returns the beginning y position of the move
        public int getBeginY()
        {
            return beginY;
        }

        // EFFECTS: returns the ending x position of the move
        public int getEndX()
        {
            return endX;
        }

        // EFFECTS: returns the ending y position of the move
        public int getEndY()
        {
            return endY;
        }

        // EFFECTS: returns the chess that is being moved
        public ChessPiece getChessPiece()
        {
            return chess;
        }

        // EFFECTS: returns the move status of the moved chess before this move
        public bool getMoveStatus()
        {
            return moveStatus;
        }
    }
}
