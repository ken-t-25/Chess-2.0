using Chess;
namespace ChessTest
{
    public class MovesTest
    {

        Moves moves;

        [SetUp]
        public void Setup()
        {
            moves = new Moves();
        }

        [Test]
        public void testAddMoveThenGetMoves()
        {
            List<Move> initial = moves.getMoves();
            Assert.AreEqual(0, initial.Count);
            Bishop bishop = new Bishop("white");
            King king = new King("black");
            Move m1 = new Move(1, 1, 5, 5, bishop, true);
            Move m2 = new Move(4, 1, 4, 2, king, false);
            moves.addMove(m1);
            moves.addMove(m2);
            List<Move> after = moves.getMoves();
            Assert.AreEqual(2, after.Count());
            Assert.AreEqual(m1, after[0]);
            Assert.AreEqual(m2, after[1]);
        }
    }
}
