using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Game
    {
        private HashSet<ChessPiece> whiteChessPiecesOnBoard;
        private HashSet<ChessPiece> blackChessPiecesOnBoard;
        private King whiteKing;
        private King blackKing;
        private Board gameBoard;
        private List<Moves> history;
        private String turn;
        private bool drawn;

        // MODIFIES: EventLog
        // EFFECTS: constructs and sets up a chess game
        public Game()
        {
            whiteChessPiecesOnBoard = buildDefaultChessOnBoard("white");
            blackChessPiecesOnBoard = buildDefaultChessOnBoard("black");
            gameBoard = new Board();
            updateBoard(whiteChessPiecesOnBoard);
            updateBoard(blackChessPiecesOnBoard);
            history = new List<Moves>();
            turn = "white";
            drawn = false;
        }

        // REQUIRES: t must be "black" or "white"
        // MODIFIES: EventLog
        // EFFECTS: constructs and sets up a chess game using given chess lists, game board, and history
        public Game(HashSet<ChessPiece> onW, HashSet<ChessPiece> onB, King wk, King bk, Board gb, List<Moves> his, String t, bool d)
        {
            whiteChessPiecesOnBoard = onW;
            blackChessPiecesOnBoard = onB;
            whiteKing = wk;
            blackKing = bk;
            gameBoard = gb;
            history = his;
            turn = t;
            drawn = d;
        }

        // REQUIRES: colour must be "black" or "white"
        // EFFECTS: constructs the default chess pieces on board
        private HashSet<ChessPiece> buildDefaultChessOnBoard(String colour)
        {
            HashSet<ChessPiece> cp = new HashSet<ChessPiece>();
            int firstRow;
            int secondRow;
            King k;
            if (colour.Equals("white"))
            {
                firstRow = 8;
                secondRow = 7;
                k = new King(colour, 5, firstRow);
                whiteKing = k;
            }
            else
            {
                firstRow = 1;
                secondRow = 2;
                k = new King(colour, 5, firstRow);
                blackKing = k;
            }
            cp.Add(new Rook(colour, 1, firstRow));
            cp.Add(new Knight(colour, 2, firstRow));
            cp.Add(new Bishop(colour, 3, firstRow));
            cp.Add(new Queen(colour, 4, firstRow));
            cp.Add(k);
            cp.Add(new Bishop(colour, 6, firstRow));
            cp.Add(new Knight(colour, 7, firstRow));
            cp.Add(new Rook(colour, 8, firstRow));
            for (int i = 1; i <= 8; i++)
            {
                cp.Add(new Pawn(colour, i, secondRow));
            }
            return cp;
        }

        // EFFECTS: update the chess info in given chess list to the board in this game
        private void updateBoard(HashSet<ChessPiece> cpl)
        {
            foreach (ChessPiece cp in cpl)
            {
                gameBoard.place(cp, cp.getPosX(), cp.getPosY());
            }
        }

        // MODIFIES: this
        // EFFECTS: replaces current gameBoard with given board
        public void setGameBoard(Board bd)
        {
            gameBoard = bd;
        }

        // MODIFIES: this
        // EFFECTS: replaces current gameBoard with given board
        public void setHistory(List<Moves> movesList)
        {
            history = movesList;
        }

        // MODIFIES: this
        // EFFECTS: replaces current whiteChessPieceOnBoard list with given list
        public void setWhiteChessPiecesOnBoard(HashSet<ChessPiece> cp)
        {
            whiteChessPiecesOnBoard = cp;
        }

        // MODIFIES: this
        // EFFECTS: replaces current blackChessPieceOnBoard list with given list
        public void setBlackChessPiecesOnBoard(HashSet<ChessPiece> cp)
        {
            blackChessPiecesOnBoard = cp;
        }

        // MODIFIES: this
        // EFFECTS: replaces current whiteKing with given whiteKing
        public void setWhiteKing(King cp)
        {
            whiteKing = cp;
        }

        // MODIFIES: this
        // EFFECTS: replaces current blackKing with given blackKing
        public void setBlackKing(King cp)
        {
            blackKing = cp;
        }

        // EFFECTS: returns history of moves of this game
        public List<Moves> getHistory()
        {
            return history;
        }

        // EFFECTS: returns the game board of this game
        public Board getBoard()
        {
            return gameBoard;
        }

        // EFFECTS: returns the list of white chess pieces on board in this game
        public HashSet<ChessPiece> getWhiteChessPiecesOnBoard()
        {
            return whiteChessPiecesOnBoard;
        }

        // EFFECTS: returns the list of black chess pieces on board in this game
        public HashSet<ChessPiece> getBlackChessPiecesOnBoard()
        {
            return blackChessPiecesOnBoard;
        }

        // EFFECTS: returns whiteKing
        public King getWhiteKing(King cp)
        {
            return whiteKing;
        }

        // EFFECTS: returns blackKing
        public King getBlackKing(King cp)
        {
            return blackKing;
        }

        // REQUIRES: side must be one of "black" and "white"
        // EFFECTS: tells whether a stalemate has occurred on the given side (i.e. no more legal moves)
        public bool stalemate(String side)
        {
            HashSet<ChessPiece> examinedList;
            if (side.Equals("white"))
            {
                examinedList = whiteChessPiecesOnBoard;
            }
            else
            {
                examinedList = blackChessPiecesOnBoard;
            }
            return !haveLegalMove(examinedList) && !check(side);
        }

        // EFFECTS: tells whether given team has legal move
        private bool haveLegalMove(HashSet<ChessPiece> chessList)
        {
            foreach (ChessPiece cp in chessList)
            {
                if (cp.possibleMoves(this).Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        // REQUIRES: side must be one of "black" and "white"
        // EFFECTS: tells whether the given side's king is checked
        public bool check(String side)
        {
            HashSet<ChessPiece> enemySide;
            King examinedKing;
            bool check;
            if (side.Equals("white"))
            {
                enemySide = blackChessPiecesOnBoard;
                examinedKing = whiteKing;
            }
            else
            {
                enemySide = whiteChessPiecesOnBoard;
                examinedKing = blackKing;
            }
            Position kingPosn = new Position(examinedKing.getPosX(), examinedKing.getPosY());
            check = kingPosn.attacked(enemySide, this);
            return check;
        }

        // REQUIRES: cp must be on the board,
        //           x and y must be within the range [1, 8] and must be different from the current x, y
        //           x and y must not be occupied
        // MODIFIES: this, cp
        // EFFECTS: change the position of cp in this game to (x, y)
        public void move(ChessPiece cp, int x, int y)
        {
            gameBoard.move(cp, x, y);
            cp.setPosX(x);
            cp.setPosY(y);
            if (!cp.hasMoved())
            {
                cp.setMove(true);
            }
        }

        // REQUIRES: cp must be on the board
        // MODIFIES: this, cp
        // EFFECTS: remove cp from this game
        public void remove(ChessPiece cp)
        {
            gameBoard.remove(cp);
            String colour = cp.getColour();
            HashSet<ChessPiece> listRemoveFrom;
            if (colour.Equals("white"))
            {
                listRemoveFrom = whiteChessPiecesOnBoard;
            }
            else
            {
                listRemoveFrom = blackChessPiecesOnBoard;
            }
            listRemoveFrom.Remove(cp);
            cp.setPosX(0);
            cp.setPosY(0);
            cp.setOnBoard(false);
        }

        // REQUIRES: cp must be off board, x and y must be within the range [1, 8]
        //           x and y must not be occupied
        // MODIFIES: this
        // EFFECTS: take cp from off board list and place cp on this board at given x, y position
        public void placeFromOffBoard(ChessPiece cp, int x, int y)
        {
            gameBoard.place(cp, x, y);
            String colour = cp.getColour();
            HashSet<ChessPiece> listAddTo;
            if (colour.Equals("white"))
            {
                listAddTo = whiteChessPiecesOnBoard;
            }
            else
            {
                listAddTo = blackChessPiecesOnBoard;
            }
            cp.setPosX(x);
            cp.setPosY(y);
            cp.setOnBoard(true);
            listAddTo.Add(cp);
        }

        // REQUIRES: cp must not be on the board, cp must have valid x, y position (within the range
        //           [1.8], x and y must not be occupied
        // MODIFIES: this
        // EFFECTS: place newly created cp on this board at given x, y position
        public void placeNew(ChessPiece cp)
        {
            int x = cp.getPosX();
            int y = cp.getPosY();
            placeFromOffBoard(cp, x, y);
        }

        // REQUIRES: side must be one of "black" and "white"
        // EFFECTS: tells whether the given side's king is checkmated
        public bool checkmate(String side)
        {
            HashSet<ChessPiece> examinedList;
            if (side.Equals("white"))
            {
                examinedList = whiteChessPiecesOnBoard;
            }
            else
            {
                examinedList = blackChessPiecesOnBoard;
            }
            return !haveLegalMove(examinedList) && check(side);
        }

        // EFFECTS: tells whether the game has ended by checkmate or stalemate
        public bool hasEnded()
        {
            return checkmate("white") || checkmate("black") || stalemate(turn) || drawn;
        }

        // REQUIRES: history must not be empty
        // EFFECTS: returns the most recent moves in history
        public Moves getMostRecentMoves()
        {
            int totalMoves = history.Count;
            return history[totalMoves - 1];
        }

        // REQUIRES: moves must consist of 1 to 3 move variables
        // MODIFIES: this, EventLog
        // EFFECTS: update the given move on history, update EventLog
        public void updateHistory(Moves moves)
        {
            history.Add(moves);
        }

        // REQUIRES: history must not be empty
        // MODIFIES: this, EventLog
        // EFFECTS: remove the most recent moves from history, do nothing if history is empty, update EventLog
        public void removeMostRecentMoves()
        {
            int totalMoves = history.Count;
            Moves recent = getMostRecentMoves();
            List<Move> moveList = recent.getMoves();
            for (int i = moveList.Count - 1; i >= 0; i--)
            {
                Move move = moveList[i];
                ChessPiece movedChess = move.getChessPiece();
            }
            history.RemoveAt(totalMoves - 1);
        }

        // MODIFIES: this
        // EFFECTS: set draw to given boolean value
        public void setDrawn(bool d)
        {
            drawn = d;
        }

        // MODIFIES: this
        // EFFECTS: reverse the field "turn" and make it the opposite team's turn
        public void reverseTurn()
        {
            if (turn.Equals("white"))
            {
                turn = "black";
            }
            else
            {
                turn = "white";
            }
        }

        // EFFECTS: return turn
        public String getTurn()
        {
            return turn;
        }

        // EFFECTS: return drawn
        public bool getDrawn()
        {
            return drawn;
        }

        // REQUIRES: column must be in the range [1, 8]
        // EFFECTS: returns the formal name for a column in chess (a - g)
        public String columnName(int column)
        {
            if (column == 1)
            {
                return "a";
            }
            else if (column == 2)
            {
                return "b";
            }
            else if (column == 3)
            {
                return "c";
            }
            else if (column == 4)
            {
                return "d";
            }
            else if (column == 5)
            {
                return "e";
            }
            else if (column == 6)
            {
                return "f";
            }
            else if (column == 7)
            {
                return "g";
            }
            else
            {
                return "h";
            }
        }

        // REQUIRES: row must be in the range [1, 8]
        // EFFECTS: returns the formal name for a row in chess (1 - 8)
        public String rowName(int row)
        {
            int rowName = 8 - row + 1;
            return rowName.ToString();
        }

        // REQUIRES: row must be in the range [1, 8], column must be in the range [1, 8]
        // EFFECTS: returns the formal name for a position on a chess board
        public String posName(int column, int row)
        {
            return columnName(column) + rowName(row);
        }
    }
}
