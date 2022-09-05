using Chess;

namespace ChessTest
{
    public class KingTest
    {

        King king;
        Game game;
        Board bd;
        HashSet<ChessPiece> white;
        HashSet<ChessPiece> black;

        [SetUp]
        public void Setup()
        {
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
        public void testPossibleMovesMiddle()
        {
            placeOnBoard(king, 5, 4);
            white.Add(king);
            setGame();
            HashSet<Position> pm = king.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(4, 3));
            expected.Add(new Position(4, 4));
            expected.Add(new Position(4, 5));
            expected.Add(new Position(5, 3));
            expected.Add(new Position(5, 5));
            expected.Add(new Position(6, 3));
            expected.Add(new Position(6, 4));
            expected.Add(new Position(6, 5));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesSide()
        {
            placeOnBoard(king, 8, 3);
            white.Add(king);
            setGame();
            HashSet<Position> pm = king.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(7, 2));
            expected.Add(new Position(7, 3));
            expected.Add(new Position(7, 4));
            expected.Add(new Position(8, 2));
            expected.Add(new Position(8, 4));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesCorner()
        {
            placeOnBoard(king, 1, 8);
            white.Add(king);
            setGame();
            HashSet<Position> pm = king.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(1, 7));
            expected.Add(new Position(2, 7));
            expected.Add(new Position(2, 8));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesBlockedByTeamMate()
        {
            placeOnBoard(king, 5, 4);
            Bishop bishop = new Bishop("white");
            placeOnBoard(bishop, 5, 5);
            white.Add(king);
            white.Add(bishop);
            setGame();
            HashSet<Position> pm = king.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(4, 3));
            expected.Add(new Position(4, 4));
            expected.Add(new Position(4, 5));
            expected.Add(new Position(5, 3));
            expected.Add(new Position(6, 3));
            expected.Add(new Position(6, 4));
            expected.Add(new Position(6, 5));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesBlockedByOpponentCanAttack()
        {
            placeOnBoard(king, 5, 4);
            Rook rook = new Rook("black");
            placeOnBoard(rook, 5, 5);
            white.Add(king);
            black.Add(rook);
            setGame();
            HashSet<Position> pm = king.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(4, 3));
            expected.Add(new Position(4, 4));
            expected.Add(new Position(5, 5));
            expected.Add(new Position(6, 3));
            expected.Add(new Position(6, 4));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesBlockedByOpponentCannotAttack()
        {
            placeOnBoard(king, 5, 4);
            Rook rook = new Rook("black");
            Queen queen = new Queen("black");
            placeOnBoard(rook, 5, 5);
            placeOnBoard(queen, 6, 6);
            white.Add(king);
            black.Add(rook);
            black.Add(queen);
            setGame();
            HashSet<Position> pm = king.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(4, 3));
            expected.Add(new Position(4, 4));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesCastlingNoEnemiesRookKingHasNotMoved()
        {
            placeOnBoard(king, 4, 1);
            Rook rook1 = new Rook("white");
            Rook rook2 = new Rook("white");
            placeOnBoard(rook1, 1, 1);
            placeOnBoard(rook2, 8, 1);
            white.Add(king);
            white.Add(rook1);
            white.Add(rook2);
            setGame();
            HashSet<Position> pm = king.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(3, 1));
            expected.Add(new Position(3, 2));
            expected.Add(new Position(4, 2));
            expected.Add(new Position(5, 1));
            expected.Add(new Position(5, 2));
            expected.Add(new Position(2, 1));
            expected.Add(new Position(6, 1));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesCastlingNoEnemiesRookMoved()
        {
            placeOnBoard(king, 4, 1);
            Rook rook1 = new Rook("white");
            Rook rook2 = new Rook("white");
            Rook rook3 = new Rook("white");
            Rook rook4 = new Rook("white");
            placeOnBoard(rook1, 1, 1);
            placeOnBoard(rook2, 6, 1);
            placeOnBoard(rook3, 4, 3);
            placeOnBoard(rook4, 8, 2);
            white.Add(king);
            white.Add(rook1);
            white.Add(rook2);
            white.Add(rook3);
            white.Add(rook4);
            setGame();
            game.move(rook1, 1, 2);
            HashSet<Position> pm = king.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(3, 1));
            expected.Add(new Position(3, 2));
            expected.Add(new Position(4, 2));
            expected.Add(new Position(5, 1));
            expected.Add(new Position(5, 2));
            setEquals(pm, expected);
            game.move(rook1, 1, 1);
            pm = king.possibleMoves(game);
            setEquals(pm, expected);
            game.move(rook1, 2, 1);
            pm = king.possibleMoves(game);
            setEquals(pm, expected);
            game.move(rook1, 2, 2);
            pm = king.possibleMoves(game);
            setEquals(pm, expected);

        }

        [Test]
        public void testPossibleMovesCastlingNoEnemiesKingMoved()
        {
            placeOnBoard(king, 4, 1);
            Rook rook1 = new Rook("white");
            Rook rook2 = new Rook("white");
            placeOnBoard(rook1, 1, 1);
            placeOnBoard(rook2, 8, 1);
            white.Add(king);
            white.Add(rook1);
            white.Add(rook2);
            setGame();
            game.move(king, 3, 1);
            game.move(king, 4, 1);
            HashSet<Position> pm = king.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(3, 1));
            expected.Add(new Position(3, 2));
            expected.Add(new Position(4, 2));
            expected.Add(new Position(5, 1));
            expected.Add(new Position(5, 2));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesCastlingNoEnemiesPathBlocked()
        {
            placeOnBoard(king, 4, 1);
            Rook rook1 = new Rook("white");
            Rook rook2 = new Rook("white");
            Bishop bishop = new Bishop("white");
            Bishop bishop1 = new Bishop("white");
            placeOnBoard(rook1, 1, 1);
            placeOnBoard(rook2, 8, 1);
            placeOnBoard(bishop, 7, 1);
            placeOnBoard(bishop1, 2, 1);
            white.Add(king);
            white.Add(rook1);
            white.Add(rook2);
            white.Add(bishop);
            white.Add(bishop1);
            setGame();
            HashSet<Position> pm = king.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(3, 1));
            expected.Add(new Position(3, 2));
            expected.Add(new Position(4, 2));
            expected.Add(new Position(5, 1));
            expected.Add(new Position(5, 2));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesCastlingEnemiesAttackingKing()
        {
            placeOnBoard(king, 4, 1);
            Rook rook1 = new Rook("white");
            Rook rook2 = new Rook("white");
            Rook rook3 = new Rook("black");
            placeOnBoard(rook1, 1, 1);
            placeOnBoard(rook2, 8, 1);
            placeOnBoard(rook3, 4, 6);
            white.Add(king);
            white.Add(rook1);
            white.Add(rook2);
            black.Add(rook3);
            setGame();
            HashSet<Position> pm = king.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(3, 1));
            expected.Add(new Position(3, 2));
            expected.Add(new Position(5, 1));
            expected.Add(new Position(5, 2));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesCastlingEnemiesAttackingPath()
        {
            placeOnBoard(king, 4, 1);
            Rook rook1 = new Rook("white");
            Rook rook2 = new Rook("white");
            Rook rook3 = new Rook("black");
            placeOnBoard(rook1, 1, 1);
            placeOnBoard(rook2, 8, 1);
            placeOnBoard(rook3, 5, 6);
            white.Add(king);
            white.Add(rook1);
            white.Add(rook2);
            black.Add(rook3);
            setGame();
            HashSet<Position> pm = king.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(3, 1));
            expected.Add(new Position(3, 2));
            expected.Add(new Position(4, 2));
            expected.Add(new Position(2, 1));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesCastlingEnemiesWillAttackEndKing()
        {
            placeOnBoard(king, 4, 1);
            Rook rook1 = new Rook("white");
            Rook rook2 = new Rook("white");
            Rook rook3 = new Rook("black");
            placeOnBoard(rook1, 1, 1);
            placeOnBoard(rook2, 8, 1);
            placeOnBoard(rook3, 6, 6);
            white.Add(king);
            white.Add(rook1);
            white.Add(rook2);
            black.Add(rook3);
            setGame();
            HashSet<Position> pm = king.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(3, 1));
            expected.Add(new Position(3, 2));
            expected.Add(new Position(4, 2));
            expected.Add(new Position(5, 1));
            expected.Add(new Position(5, 2));
            expected.Add(new Position(2, 1));
            setEquals(pm, expected);
        }
    }
}
