using Chess;

namespace ChessTest
{
    public class PawnTest
    {

        Pawn pawn;
        King king;
        Game game;
        Board bd;
        HashSet<ChessPiece> white;
        HashSet<ChessPiece> black;

        [SetUp]
        public void Setup()
        {
            pawn = new Pawn("white");
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
        public void testPossibleMovesNoBlockHasNotMoved()
        {
            placeOnBoard(pawn, 6, 7);
            placeOnBoard(king, 6, 8);
            white.Add(pawn);
            white.Add(king);
            setGame();
            HashSet<Position> pm = pawn.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(6, 5));
            expected.Add(new Position(6, 6));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesNoBlockHasMoved()
        {
            placeOnBoard(pawn, 8, 7);
            placeOnBoard(king, 6, 8);
            white.Add(pawn);
            white.Add(king);
            setGame();
            game.move(pawn, 8, 5);
            HashSet<Position> pm = pawn.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(8, 4));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesBlockByTeammate()
        {
            placeOnBoard(pawn, 1, 7);
            placeOnBoard(king, 6, 8);
            Bishop bishop = new Bishop("white");
            placeOnBoard(bishop, 1, 6);
            white.Add(pawn);
            white.Add(king);
            white.Add(bishop);
            setGame();
            HashSet<Position> pm = pawn.possibleMoves(game);
            Assert.AreEqual(0, pm.Count);
        }

        [Test]
        public void testPossibleMovesDiagonalToTeammateCannotAttack()
        {
            placeOnBoard(pawn, 6, 7);
            placeOnBoard(king, 6, 8);
            Bishop bishop = new Bishop("white");
            placeOnBoard(bishop, 5, 6);
            white.Add(pawn);
            white.Add(king);
            white.Add(bishop);
            setGame();
            HashSet<Position> pm = pawn.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(6, 5));
            expected.Add(new Position(6, 6));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesNoSpaceInFront()
        {
            Pawn pawn1 = new Pawn("white");
            Pawn pawn2 = new Pawn("white");
            Pawn pawn3 = new Pawn("black");
            Pawn pawn4 = new Pawn("black");
            Pawn pawn5 = new Pawn("black");
            King blackKing = new King("black");
            placeOnBoard(pawn, 6, 1);
            placeOnBoard(pawn1, 1, 1);
            placeOnBoard(pawn2, 8, 1);
            placeOnBoard(pawn3, 6, 8);
            placeOnBoard(pawn4, 1, 8);
            placeOnBoard(pawn5, 8, 8);
            placeOnBoard(king, 8, 5);
            placeOnBoard(blackKing, 1, 5);
            white.Add(pawn);
            white.Add(pawn1);
            white.Add(pawn2);
            white.Add(king);
            black.Add(pawn3);
            black.Add(pawn4);
            black.Add(pawn5);
            black.Add(blackKing);
            setGame();
            Assert.AreEqual(0, pawn.possibleMoves(game).Count);
            Assert.AreEqual(0, pawn1.possibleMoves(game).Count);
            Assert.AreEqual(0, pawn2.possibleMoves(game).Count);
            Assert.AreEqual(0, pawn3.possibleMoves(game).Count);
            Assert.AreEqual(0, pawn4.possibleMoves(game).Count);
            Assert.AreEqual(0, pawn5.possibleMoves(game).Count);
        }

        [Test]
        public void testPossibleMovesBlockByEnemy()
        {
            placeOnBoard(pawn, 6, 7);
            placeOnBoard(king, 6, 8);
            Bishop bishop = new Bishop("black");
            placeOnBoard(bishop, 6, 6);
            white.Add(pawn);
            white.Add(king);
            black.Add(bishop);
            setGame();
            HashSet<Position> pm = pawn.possibleMoves(game);
            Assert.AreEqual(0, pm.Count);
        }

        [Test]
        public void testPossibleMovesCanAttack()
        {
            placeOnBoard(pawn, 4, 5);
            placeOnBoard(king, 4, 6);
            Bishop bishop = new Bishop("black");
            placeOnBoard(bishop, 3, 4);
            Rook rook = new Rook("black");
            placeOnBoard(rook, 5, 4);
            white.Add(pawn);
            white.Add(king);
            black.Add(bishop);
            black.Add(rook);
            setGame();
            HashSet<Position> pm = pawn.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(4, 3));
            expected.Add(new Position(4, 4));
            expected.Add(new Position(3, 4));
            expected.Add(new Position(5, 4));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesKingUnderAttack()
        {
            placeOnBoard(pawn, 4, 5);
            placeOnBoard(king, 5, 5);
            Bishop bishop = new Bishop("black");
            placeOnBoard(bishop, 7, 3);
            white.Add(pawn);
            white.Add(king);
            black.Add(bishop);
            setGame();
            HashSet<Position> pm = pawn.possibleMoves(game);
            Assert.AreEqual(0, pm.Count);
        }

        [Test]
        public void testPossibleMovesKingWillBeUnderAttack()
        {
            placeOnBoard(pawn, 5, 4);
            placeOnBoard(king, 5, 5);
            Bishop bishop = new Bishop("black");
            placeOnBoard(bishop, 6, 3);
            Rook rook = new Rook("black");
            placeOnBoard(rook, 5, 3);
            white.Add(pawn);
            white.Add(king);
            black.Add(bishop);
            black.Add(rook);
            setGame();
            HashSet<Position> pm = pawn.possibleMoves(game);
            Assert.AreEqual(0, pm.Count);
        }

        [Test]
        public void testCheckingEnemy()
        {
            placeOnBoard(pawn, 6, 7);
            placeOnBoard(king, 6, 8);
            white.Add(pawn);
            white.Add(king);
            setGame();
            Position pos1 = new Position(4, 6);
            Position pos2 = new Position(5, 6);
            Position pos3 = new Position(6, 6);
            Position pos4 = new Position(6, 5);
            Assert.False(pawn.checkEnemy(game, pos1));
            Assert.True(pawn.checkEnemy(game, pos2));
            Assert.False(pawn.checkEnemy(game, pos3));
            Assert.False(pawn.checkEnemy(game, pos4));
        }
    }
}
