using Chess;

namespace ChessTest
{
    public class QueenTest
    {

        Queen queen;
        King king;
        Game game;
        Board bd;
        HashSet<ChessPiece> white;
        HashSet<ChessPiece> black;

        [SetUp]
        public void Setup()
        {
            queen = new Queen("white");
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
        public void testPossibleMovesNoBlockMiddle()
        {
            placeOnBoard(queen, 5, 4);
            placeOnBoard(king, 3, 3);
            white.Add(queen);
            white.Add(king);
            setGame();
            HashSet<Position> pm = queen.possibleMoves(game);
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
            expected.Add(new Position(5, 3));
            expected.Add(new Position(5, 2));
            expected.Add(new Position(5, 1));
            expected.Add(new Position(5, 5));
            expected.Add(new Position(5, 6));
            expected.Add(new Position(5, 7));
            expected.Add(new Position(5, 8));
            expected.Add(new Position(4, 4));
            expected.Add(new Position(3, 4));
            expected.Add(new Position(2, 4));
            expected.Add(new Position(1, 4));
            expected.Add(new Position(6, 4));
            expected.Add(new Position(7, 4));
            expected.Add(new Position(8, 4));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesNoBlockSide()
        {
            placeOnBoard(queen, 1, 6);
            placeOnBoard(king, 2, 4);
            white.Add(queen);
            white.Add(king);
            setGame();
            HashSet<Position> pm = queen.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(2, 5));
            expected.Add(new Position(3, 4));
            expected.Add(new Position(4, 3));
            expected.Add(new Position(5, 2));
            expected.Add(new Position(6, 1));
            expected.Add(new Position(2, 7));
            expected.Add(new Position(3, 8));
            expected.Add(new Position(1, 5));
            expected.Add(new Position(1, 4));
            expected.Add(new Position(1, 3));
            expected.Add(new Position(1, 2));
            expected.Add(new Position(1, 1));
            expected.Add(new Position(1, 7));
            expected.Add(new Position(1, 8));
            expected.Add(new Position(2, 6));
            expected.Add(new Position(3, 6));
            expected.Add(new Position(4, 6));
            expected.Add(new Position(5, 6));
            expected.Add(new Position(6, 6));
            expected.Add(new Position(7, 6));
            expected.Add(new Position(8, 6));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesNoBlockCorner()
        {
            placeOnBoard(queen, 1, 1);
            placeOnBoard(king, 2, 3);
            white.Add(queen);
            white.Add(king);
            setGame();
            HashSet<Position> pm = queen.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(2, 2));
            expected.Add(new Position(3, 3));
            expected.Add(new Position(4, 4));
            expected.Add(new Position(5, 5));
            expected.Add(new Position(6, 6));
            expected.Add(new Position(7, 7));
            expected.Add(new Position(8, 8));
            expected.Add(new Position(1, 2));
            expected.Add(new Position(1, 3));
            expected.Add(new Position(1, 4));
            expected.Add(new Position(1, 5));
            expected.Add(new Position(1, 6));
            expected.Add(new Position(1, 7));
            expected.Add(new Position(1, 8));
            expected.Add(new Position(2, 1));
            expected.Add(new Position(3, 1));
            expected.Add(new Position(4, 1));
            expected.Add(new Position(5, 1));
            expected.Add(new Position(6, 1));
            expected.Add(new Position(7, 1));
            expected.Add(new Position(8, 1));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesBlockByTeammate()
        {
            placeOnBoard(queen, 5, 4);
            placeOnBoard(king, 4, 2);
            Rook rook1 = new Rook("white");
            Rook rook2 = new Rook("white");
            Bishop bishop = new Bishop("white");
            placeOnBoard(rook1, 5, 2);
            placeOnBoard(rook2, 8, 4);
            placeOnBoard(bishop, 3, 6);
            white.Add(queen);
            white.Add(king);
            white.Add(rook1);
            white.Add(rook2);
            white.Add(bishop);
            setGame();
            HashSet<Position> pm = queen.possibleMoves(game);
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
            expected.Add(new Position(5, 3));
            expected.Add(new Position(5, 5));
            expected.Add(new Position(5, 6));
            expected.Add(new Position(5, 7));
            expected.Add(new Position(5, 8));
            expected.Add(new Position(4, 4));
            expected.Add(new Position(3, 4));
            expected.Add(new Position(2, 4));
            expected.Add(new Position(1, 4));
            expected.Add(new Position(6, 4));
            expected.Add(new Position(7, 4));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesBlockByEnemy()
        {
            placeOnBoard(queen, 5, 4);
            placeOnBoard(king, 1, 1);
            Rook rook1 = new Rook("black");
            Rook rook2 = new Rook("black");
            Bishop bishop = new Bishop("black");
            placeOnBoard(rook1, 5, 2);
            placeOnBoard(rook2, 8, 4);
            placeOnBoard(bishop, 3, 6);
            white.Add(queen);
            white.Add(king);
            black.Add(rook1);
            black.Add(rook2);
            black.Add(bishop);
            setGame();
            HashSet<Position> pm = queen.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(4, 3));
            expected.Add(new Position(3, 2));
            expected.Add(new Position(2, 1));
            expected.Add(new Position(4, 5));
            expected.Add(new Position(3, 6));
            expected.Add(new Position(6, 3));
            expected.Add(new Position(7, 2));
            expected.Add(new Position(8, 1));
            expected.Add(new Position(6, 5));
            expected.Add(new Position(7, 6));
            expected.Add(new Position(8, 7));
            expected.Add(new Position(5, 3));
            expected.Add(new Position(5, 2));
            expected.Add(new Position(5, 5));
            expected.Add(new Position(5, 6));
            expected.Add(new Position(5, 7));
            expected.Add(new Position(5, 8));
            expected.Add(new Position(4, 4));
            expected.Add(new Position(3, 4));
            expected.Add(new Position(2, 4));
            expected.Add(new Position(1, 4));
            expected.Add(new Position(6, 4));
            expected.Add(new Position(7, 4));
            expected.Add(new Position(8, 4));
            setEquals(pm, expected);
        }

        [Test]
        public void testCheckingEnemy()
        {
            placeOnBoard(queen, 6, 6);
            placeOnBoard(king, 1, 2);
            white.Add(queen);
            white.Add(king);
            setGame();
            Position pos1 = new Position(4, 4);
            Position pos2 = new Position(5, 6);
            Position pos3 = new Position(6, 6);
            Position pos4 = new Position(6, 5);
            Assert.True(queen.checkEnemy(game, pos1));
            Assert.True(queen.checkEnemy(game, pos2));
            Assert.False(queen.checkEnemy(game, pos3));
            Assert.True(queen.checkEnemy(game, pos4));
        }

        [Test]
        public void testPossibleMovesKingUnderAttack()
        {
            placeOnBoard(queen, 5, 4);
            Rook rook = new Rook("black");
            placeOnBoard(king, 3, 6);
            placeOnBoard(rook, 3, 2);
            white.Add(queen);
            white.Add(king);
            black.Add(rook);
            setGame();
            HashSet<Position> pm = queen.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(3, 2));
            expected.Add(new Position(3, 4));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesKingWillBeUnderAttack()
        {
            placeOnBoard(queen, 3, 5);
            Rook rook = new Rook("black");
            placeOnBoard(king, 3, 6);
            placeOnBoard(rook, 3, 2);
            white.Add(queen);
            white.Add(king);
            black.Add(rook);
            setGame();
            HashSet<Position> pm = queen.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(3, 4));
            expected.Add(new Position(3, 3));
            expected.Add(new Position(3, 2));
            setEquals(pm, expected);
        }
    }

}
