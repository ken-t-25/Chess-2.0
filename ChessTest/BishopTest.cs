using Chess;

namespace ChessTest
{
    public class BishopTest
    {
        Bishop bishop;
        King king;
        Game game;
        Board bd;
        HashSet<ChessPiece> white;
        HashSet<ChessPiece> black;

        [SetUp]
        public void Setup()
        {
            bishop = new Bishop("white");
            king = new King("white");
            game = new Game();
            bd = new Board();
            white = new HashSet<ChessPiece>();
            black = new HashSet<ChessPiece>();
        }

        private void setEquals(HashSet<Position> l1, HashSet<Position> l2)
        {
            Assert.AreEqual(l1.Count, l2.Count);
            foreach (Position p in l1) 
            {
                Assert.True(l2.Contains(p));
            }
        }

        private void setGame()
        {
            game.setGameBoard(bd);
            game.setWhiteChessPiecesOnBoard(white);
            game.setBlackChessPiecesOnBoard(black);
            game.setWhiteKing(king);
        }

        private void placeOnBoard(ChessPiece cp, int x, int y)
        {
            bd.place(cp, x, y);
            cp.setPosX(x);
            cp.setPosY(y);
        }

        [Test]
        public void testBishopPossibleMovesNoBlockingMiddle()
        {
            placeOnBoard(bishop, 5, 4);
            placeOnBoard(king, 5, 5);
            white.Add(bishop);
            white.Add(king);
            setGame();
            HashSet<Position> pm = bishop.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(4, 3));
            expected.Add(new Position(3, 2));
            expected.Add(new Position(2, 1));
            expected.Add(new Position(4, 5));
            expected.Add(new Position(3, 6));
            expected.Add(new Position(2, 7));
            expected.Add(new Position(1, 8));
            expected.Add(new Position(6, 3));
            expected.Add(new Position(7, 2));
            expected.Add(new Position(8, 1));
            expected.Add(new Position(6, 5));
            expected.Add(new Position(7, 6));
            expected.Add(new Position(8, 7));
            setEquals(pm, expected);
        }

        [Test]
        public void testBishopPossibleMovesNoBlockingSide()
        {
            placeOnBoard(bishop, 1, 4);
            placeOnBoard(king, 1, 3);
            white.Add(bishop);
            white.Add(king);
            setGame();
            HashSet<Position> pm = bishop.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(2, 3));
            expected.Add(new Position(3, 2));
            expected.Add(new Position(4, 1));
            expected.Add(new Position(2, 5));
            expected.Add(new Position(3, 6));
            expected.Add(new Position(4, 7));
            expected.Add(new Position(5, 8));
            setEquals(pm, expected);
        }

        [Test]
        public void testBishopPossibleMovesNoBlockingCorner()
        {
            placeOnBoard(bishop, 1, 1);
            placeOnBoard(king, 1, 2);
            white.Add(bishop);
            white.Add(king);
            setGame();
            HashSet<Position> pm = bishop.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(2, 2));
            expected.Add(new Position(3, 3));
            expected.Add(new Position(4, 4));
            expected.Add(new Position(5, 5));
            expected.Add(new Position(6, 6));
            expected.Add(new Position(7, 7));
            expected.Add(new Position(8, 8));
            setEquals(pm, expected);
        }

        [Test]
        public void testBishopPossibleMovesBlockBySameTeam()
        {
            Rook rook = new Rook("white");
            placeOnBoard(bishop, 5, 4);
            placeOnBoard(king, 5, 5);
            placeOnBoard(rook, 3, 6);
            white.Add(bishop);
            white.Add(king);
            white.Add(rook);
            setGame();
            HashSet<Position> pm = bishop.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(4, 3));
            expected.Add(new Position(3, 2));
            expected.Add(new Position(2, 1));
            expected.Add(new Position(4, 5));
            expected.Add(new Position(6, 3));
            expected.Add(new Position(7, 2));
            expected.Add(new Position(8, 1));
            expected.Add(new Position(6, 5));
            expected.Add(new Position(7, 6));
            expected.Add(new Position(8, 7));
            setEquals(pm, expected);
        }

        [Test]
        public void testBishopPossibleMovesBlockByOpponents()
        {
            Pawn pawn = new Pawn("black");
            placeOnBoard(bishop, 5, 4);
            placeOnBoard(king, 5, 5);
            placeOnBoard(pawn, 4, 3);
            white.Add(bishop);
            white.Add(king);
            black.Add(pawn);
            setGame();
            HashSet<Position> pm = bishop.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(4, 3));
            expected.Add(new Position(4, 5));
            expected.Add(new Position(3, 6));
            expected.Add(new Position(2, 7));
            expected.Add(new Position(1, 8));
            expected.Add(new Position(6, 3));
            expected.Add(new Position(7, 2));
            expected.Add(new Position(8, 1));
            expected.Add(new Position(6, 5));
            expected.Add(new Position(7, 6));
            expected.Add(new Position(8, 7));
            setEquals(pm, expected);
        }

        [Test]
        public void testCheckingEnemy()
        {
            placeOnBoard(bishop, 6, 6);
            placeOnBoard(king, 1, 2);
            white.Add(bishop);
            white.Add(king);
            setGame();
            Position pos1 = new Position(4, 4);
            Position pos2 = new Position(5, 6);
            Position pos3 = new Position(6, 6);
            Position pos4 = new Position(6, 5);
            Assert.True(bishop.checkEnemy(game, pos1));
            Assert.False(bishop.checkEnemy(game, pos2));
            Assert.False(bishop.checkEnemy(game, pos3));
            Assert.False(bishop.checkEnemy(game, pos4));
        }

        [Test]
        public void testBishopPossibleMovesKingWillBeChecked()
        {
            Rook rook = new Rook("black");
            placeOnBoard(bishop, 5, 4);
            placeOnBoard(rook, 5, 3);
            placeOnBoard(king, 5, 5);
            white.Add(bishop);
            white.Add(king);
            black.Add(rook);
            setGame();
            HashSet<Position> pm = bishop.possibleMoves(game);
            Assert.AreEqual(0, pm.Count);
        }

        [Test]
        public void testBishopPossibleMovesKingIsChecked()
        {
            Knight knight = new Knight("black");
            placeOnBoard(bishop, 5, 4);
            placeOnBoard(knight, 6, 5);
            placeOnBoard(king, 4, 6);
            white.Add(bishop);
            white.Add(king);
            black.Add(knight);
            setGame();
            HashSet<Position> pm = bishop.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(6, 5));
            setEquals(pm, expected);
        }
    }
}
