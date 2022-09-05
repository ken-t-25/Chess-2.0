using Chess;

namespace ChessTest
{
    public class PositionTest
    {
        private Position posn;

        private void placeOnBoard(ChessPiece cp, int x, int y, Board bd)
        {
            bd.place(cp, x, y);
            cp.setPosX(x);
            cp.setPosY(y);
        }

        [Test]
        public void testPositionAttacked()
        {
            Game game = new Game();
            Board bd = new Board();
            ChessPiece rook = new Rook("white");
            ChessPiece bishop = new Bishop("white");
            placeOnBoard(rook, 4, 5, bd);
            placeOnBoard(bishop, 5, 5, bd);
            HashSet<ChessPiece> white = new HashSet<ChessPiece>();
            HashSet<ChessPiece> black = new HashSet<ChessPiece>();
            white.Add(rook);
            white.Add(bishop);
            game.setBlackChessPiecesOnBoard(black);
            game.setWhiteChessPiecesOnBoard(white);
            game.setGameBoard(bd);
            posn = new Position(4, 1);
            Assert.True(posn.attacked(white, game));
            posn = new Position(8, 8);
            Assert.True(posn.attacked(white, game));
            posn = new Position(5, 1);
            Assert.False(posn.attacked(white, game));
        }

        [Test]
        public void testPositionEqualsMethod()
        {
            posn = new Position(4, 1);
            Position posn1 = new Position(4, 1);
            Position posn2 = new Position(1, 4);
            Position posn3 = new Position(4, 2);
            Position posn4 = new Position(6, 1);
            Position posn5 = new Position(5, 6);
            Assert.True(posn.Equals(posn1));
            Assert.False(posn.Equals(posn2));
            Assert.False(posn.Equals(posn3));
            Assert.False(posn.Equals(posn4));
            Assert.False(posn.Equals(posn5));
        }
    }
}