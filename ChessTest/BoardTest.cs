using Chess;

namespace ChessTest
{
    public class BoardTest
    {
        private Board board;

        [SetUp]
        public void Setup()
        {
            board = new Board();
        }

        [Test]
        public void testPlacePiecePosition()
        {
            Setup();
            Bishop bishop = new Bishop("black");
            board.place(bishop, 5, 6);
            Position posn = new Position(5, 6);
            Assert.AreEqual(bishop, board.getOnBoard()[posn.getIdx()]);
        }

        [Test]
        public void testMovePiece()
        {
            Setup();
            Rook rook = new Rook("white");
            board.place(rook, 3, 8);
            rook.setPosX(3);
            rook.setPosY(8);
            board.move(rook, 4, 8);
            Position posn = new Position(4, 8);
            Assert.AreEqual(rook, board.getOnBoard()[posn.getIdx()]);
            Position oldPosn = new Position(3, 8);
            Assert.IsNull(board.getOnBoard()[oldPosn.getIdx()]);
        }

        [Test]
        public void testRemovePiece()
        {
            Setup();
            Rook rook = new Rook("white");
            board.place(rook, 3, 8);
            rook.setPosX(3);
            rook.setPosY(8);
            board.remove(rook);
            Position posn = new Position(3, 8);
            Assert.IsNull(board.getOnBoard()[posn.getIdx()]);
        }
    }
}
