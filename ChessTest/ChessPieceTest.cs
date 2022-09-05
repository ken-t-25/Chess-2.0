using Chess;
using System.Net.NetworkInformation;

namespace ChessTest
{
    public class ChessPieceTest
    {
        private Rook rook;
        private Game game;
        private Board bd;
        private HashSet<ChessPiece> white;
        private HashSet<ChessPiece> black;

        [SetUp]
        public void Setup()
        {
            rook = new Rook("white");
            game = new Game();
            bd = new Board();
            white = new HashSet<ChessPiece>();
            black = new HashSet<ChessPiece>();
        }

        private void setGame()
        {
            game.setGameBoard(bd);
            game.setWhiteChessPiecesOnBoard(white);
            game.setBlackChessPiecesOnBoard(black);
        }

        private void placeOnBoard(ChessPiece cp, int x, int y)
        {
            bd.place(cp, x, y);
            cp.setPosX(x);
            cp.setPosY(y);
        }

        [Test]
        public void testHasMovedMethod()
        {
            placeOnBoard(rook, 1, 1);
            white.Add(rook);
            setGame();
            Assert.False(rook.hasMoved());
            game.move(rook, 1, 5);
            Assert.True(rook.hasMoved());
        }

        [Test]
        public void testChessEquals()
        {
            Assert.False(rook.Equals(null));
            Assert.True(rook.Equals(rook));
            Bishop bishop = new Bishop("white");
            Assert.False(rook.Equals(bishop));
            Rook rook1 = new Rook("white");
            Assert.True(rook.Equals(rook1));
            alterOnBoardMoveXYCombinations(rook1);
            Rook rook2 = new Rook("black");
            Assert.False(rook.Equals(rook2));
            alterOnBoardMoveXYCombinations(rook2);
        }

        private void alterOnBoardAndMoveCombinations(ChessPiece cp)
        {
            cp.setMove(true);
            Assert.False(rook.Equals(cp));
            cp.setMove(false);
            cp.setOnBoard(true);
            Assert.False(rook.Equals(cp));
            cp.setMove(true);
            Assert.False(rook.Equals(cp));
        }

        private void alterOnBoardMoveXYCombinations(ChessPiece cp)
        {
            alterOnBoardAndMoveCombinations(cp);
            cp.setPosY(1);
            cp.setMove(false);
            cp.setOnBoard(false);
            Assert.False(rook.Equals(cp));
            alterOnBoardAndMoveCombinations(cp);
            cp.setPosX(1);
            cp.setPosY(0);
            cp.setMove(false);
            cp.setOnBoard(false);
            Assert.False(rook.Equals(cp));
            alterOnBoardAndMoveCombinations(cp);
            cp.setPosY(1);
            cp.setMove(false);
            cp.setOnBoard(false);
            Assert.False(rook.Equals(cp));
            alterOnBoardAndMoveCombinations(cp);
        }
    }
}
