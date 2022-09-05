using Chess;
using System.Net.NetworkInformation;

namespace ChessTest
{
    public class GameTest
    {
        private Game game;
        private Board bd;
        private HashSet<ChessPiece> white;
        private HashSet<ChessPiece> black;

        [SetUp]
        public void Setup()
        {
            game = new Game();
        }

        private void setupRest()
        {
            bd = new Board();
            white = new HashSet<ChessPiece>();
            black = new HashSet<ChessPiece>();
        }

        private void placeOnBoard(ChessPiece cp, int x, int y)
        {
            bd.place(cp, x, y);
            cp.setPosX(x);
            cp.setPosY(y);
        }

        private void setGame()
        {
            game.setGameBoard(bd);
            game.setWhiteChessPiecesOnBoard(white);
            game.setBlackChessPiecesOnBoard(black);
        }

        [Test]
        public void testConstructor()
        {
            Assert.AreEqual(16, game.getWhiteChessPiecesOnBoard().Count);
            Assert.AreEqual(16, game.getBlackChessPiecesOnBoard().Count);
            Assert.AreEqual(0, game.getHistory().Count);
        }

        [Test]
        public void testStalemateMethodAndHasEnded()
        {
            setupRest();
            King whiteKing = new King("white");
            King blackKing = new King("black");
            game.setWhiteKing(whiteKing);
            game.setBlackKing(blackKing);
            Bishop blackBishop = new Bishop("black");
            Pawn whitePawn = new Pawn("white");
            Queen blackQueen = new Queen("black");
            placeOnBoard(whiteKing, 1, 1);
            placeOnBoard(blackKing, 2, 3);
            placeOnBoard(blackBishop, 4, 3);
            white.Add(whiteKing);
            black.Add(blackKing);
            black.Add(blackBishop);
            setGame();
            Assert.True(game.stalemate("white"));
            Assert.False(game.stalemate("black"));
            Assert.True(game.hasEnded());
            game.reverseTurn();
            Assert.False(game.hasEnded());
            game.reverseTurn();
            game.setDrawn(true);
            Assert.True(game.hasEnded());
            game.setDrawn(false);
            game.placeFromOffBoard(blackQueen, 3, 3);
            Assert.False(game.stalemate("white"));
            Assert.True(game.hasEnded());
            game.placeFromOffBoard(whitePawn, 8, 6);
            Assert.False(game.stalemate("white"));
            Assert.True(game.hasEnded());
            game.setDrawn(true);
            Assert.True(game.hasEnded());
            game.setDrawn(false);
            game.remove(blackQueen);
            Assert.False(game.stalemate("white"));
            Assert.False(game.hasEnded());
            game.setDrawn(true);
            Assert.True(game.hasEnded());
        }

        [Test]
        public void testCheckAndCheckmateAndHasEnded()
        {
            setupRest();
            King blackKing = new King("black");
            King whiteKing = new King("white");
            game.setWhiteKing(whiteKing);
            game.setBlackKing(blackKing);
            Bishop whiteBishop = new Bishop("white");
            Queen whiteQueen = new Queen("white");
            placeOnBoard(blackKing, 1, 1);
            placeOnBoard(whiteBishop, 6, 6);
            placeOnBoard(whiteKing, 8, 8);
            black.Add(blackKing);
            white.Add(whiteBishop);
            white.Add(whiteKing);
            setGame();
            Assert.True(game.check("black"));
            Assert.False(game.checkmate("black"));
            Assert.False(game.hasEnded());
            game.setDrawn(true);
            Assert.True(game.hasEnded());
            game.setDrawn(false);
            game.placeFromOffBoard(whiteQueen, 3, 2);
            Assert.True(game.checkmate("black"));
            Assert.True(game.hasEnded());
        }

        [Test]
        public void testMovesMethodsInGame()
        {
            Bishop bishop = new Bishop("white");
            Rook rook = new Rook("black");
            Pawn pawn = new Pawn("black");
            Move m1 = new Move(5, 5, 1, 1, bishop, true);
            Move m2 = new Move(1, 1, 0, 0, rook, false);
            Move m3 = new Move(1, 5, 1, 6, pawn, true);
            Moves ms1 = new Moves();
            Moves ms2 = new Moves();
            ms1.addMove(m1);
            ms1.addMove(m2);
            ms2.addMove(m3);
            List<Moves> history = new List<Moves>();
            game.setHistory(history);
            game.updateHistory(ms1);
            game.updateHistory(ms2);
            Assert.AreEqual(2, game.getHistory().Count);
            Assert.AreEqual(ms1, game.getHistory()[0]);
            Assert.AreEqual(ms2, game.getHistory()[1]);
            Assert.AreEqual(ms2, game.getMostRecentMoves());
            game.removeMostRecentMoves();
            Assert.AreEqual(1, game.getHistory().Count);
            Assert.AreEqual(ms1, game.getMostRecentMoves());
        }

        [Test]
        public void testMoveRemovePlaceMethods()
        {
            setupRest();
            King whiteKing = new King("white");
            King blackKing = new King("black");
            game.setWhiteKing(whiteKing);
            game.setBlackKing(blackKing);
            Bishop blackBishop = new Bishop("black", 4, 3);
            Pawn whitePawn = new Pawn("white", 8, 6);
            setGame();
            game.placeFromOffBoard(whiteKing, 1, 1);
            game.placeFromOffBoard(blackKing, 2, 3);
            game.placeNew(blackBishop);
            game.placeNew(whitePawn);
            Assert.AreEqual(2, game.getWhiteChessPiecesOnBoard().Count);
            Assert.AreEqual(2, game.getBlackChessPiecesOnBoard().Count);
            Position whiteKingPosn = new Position(1, 1);
            Assert.AreEqual(whiteKing, game.getBoard().getOnBoard()[whiteKingPosn.getIdx()]);
            Position blackBishopPosn = new Position(4, 3);
            Assert.AreEqual(blackBishop, game.getBoard().getOnBoard()[blackBishopPosn.getIdx()]);
            game.remove(whiteKing);
            game.remove(blackBishop);
            Assert.AreEqual(1, game.getWhiteChessPiecesOnBoard().Count);
            Assert.AreEqual(1, game.getBlackChessPiecesOnBoard().Count);
            Assert.IsNull(game.getBoard().getOnBoard()[whiteKingPosn.getIdx()]);
            Assert.IsNull(game.getBoard().getOnBoard()[blackBishopPosn.getIdx()]);
        }

        [Test]
        public void testSetDrawn()
        {
            Assert.False(game.getDrawn());
            game.setDrawn(true);
            Assert.True(game.getDrawn());
            game.setDrawn(false);
            Assert.False(game.getDrawn());
        }

        [Test]
        public void testReverseTurn()
        {
            Assert.AreEqual("white", game.getTurn());
            game.reverseTurn();
            Assert.AreEqual("black", game.getTurn());
            game.reverseTurn();
            Assert.AreEqual("white", game.getTurn());
        }
    }
}
