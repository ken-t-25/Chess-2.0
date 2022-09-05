using Chess;

namespace ChessTest
{
    public class KnightTest
    {

        Knight knight;
        King king;
        Game game;
        Board bd;
        HashSet<ChessPiece> white;
        HashSet<ChessPiece> black;

        [SetUp]
        public void Setup()
        {
            knight = new Knight("white");
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
            placeOnBoard(knight, 4, 6);
            placeOnBoard(king, 4, 5);
            white.Add(knight);
            white.Add(king);
            setGame();
            HashSet<Position> pm = knight.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(3, 4));
            expected.Add(new Position(2, 5));
            expected.Add(new Position(3, 8));
            expected.Add(new Position(2, 7));
            expected.Add(new Position(5, 4));
            expected.Add(new Position(6, 5));
            expected.Add(new Position(5, 8));
            expected.Add(new Position(6, 7));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesTopRightCorner()
        {
            placeOnBoard(knight, 8, 1);
            placeOnBoard(king, 5, 7);
            white.Add(knight);
            white.Add(king);
            setGame();
            HashSet<Position> pm = knight.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(7, 3));
            expected.Add(new Position(6, 2));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesBottomLeftCorner()
        {
            placeOnBoard(knight, 1, 8);
            placeOnBoard(king, 5, 7);
            white.Add(knight);
            white.Add(king);
            setGame();
            HashSet<Position> pm = knight.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(2, 6));
            expected.Add(new Position(3, 7));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesCorner()
        {
            placeOnBoard(knight, 8, 8);
            placeOnBoard(king, 8, 7);
            white.Add(knight);
            white.Add(king);
            setGame();
            HashSet<Position> pm = knight.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(7, 6));
            expected.Add(new Position(6, 7));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleBlockByTeammate()
        {
            placeOnBoard(knight, 4, 6);
            placeOnBoard(king, 4, 5);
            Bishop bishop = new Bishop("white");
            placeOnBoard(bishop, 2, 5);
            white.Add(knight);
            white.Add(bishop);
            white.Add(king);
            setGame();
            HashSet<Position> pm = knight.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(3, 4));
            expected.Add(new Position(3, 8));
            expected.Add(new Position(2, 7));
            expected.Add(new Position(5, 4));
            expected.Add(new Position(6, 5));
            expected.Add(new Position(5, 8));
            expected.Add(new Position(6, 7));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesBlockByEnemy()
        {
            placeOnBoard(knight, 4, 6);
            placeOnBoard(king, 4, 5);
            Bishop bishop = new Bishop("black");
            placeOnBoard(bishop, 6, 5);
            white.Add(knight);
            white.Add(king);
            black.Add(bishop);
            setGame();
            HashSet<Position> pm = knight.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(3, 4));
            expected.Add(new Position(2, 5));
            expected.Add(new Position(3, 8));
            expected.Add(new Position(2, 7));
            expected.Add(new Position(5, 4));
            expected.Add(new Position(6, 5));
            expected.Add(new Position(5, 8));
            expected.Add(new Position(6, 7));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesKingUnderAttack()
        {
            Rook rook = new Rook("black");
            placeOnBoard(knight, 4, 6);
            placeOnBoard(king, 6, 3);
            placeOnBoard(rook, 6, 8);
            white.Add(knight);
            white.Add(king);
            black.Add(rook);
            setGame();
            HashSet<Position> pm = knight.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(6, 5));
            expected.Add(new Position(6, 7));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesKingWillBeUnderAttack()
        {
            Rook rook = new Rook("black");
            placeOnBoard(knight, 4, 6);
            placeOnBoard(king, 4, 3);
            placeOnBoard(rook, 4, 8);
            white.Add(knight);
            white.Add(king);
            black.Add(rook);
            setGame();
            HashSet<Position> pm = knight.possibleMoves(game);
            Assert.AreEqual(0, pm.Count);
        }

        [Test]
        public void testCheckingEnemy()
        {
            placeOnBoard(knight, 5, 5);
            placeOnBoard(king, 6, 8);
            white.Add(knight);
            white.Add(king);
            setGame();
            Position pos1 = new Position(4, 3);
            Position pos2 = new Position(8, 5);
            Position pos3 = new Position(5, 2);
            Position pos4 = new Position(1, 8);
            Position pos5 = new Position(1, 5);
            Position pos6 = new Position(5, 1);
            Position pos7 = new Position(5, 5);
            Assert.True(knight.checkEnemy(game, pos1));
            Assert.False(knight.checkEnemy(game, pos2));
            Assert.False(knight.checkEnemy(game, pos3));
            Assert.False(knight.checkEnemy(game, pos4));
            Assert.False(knight.checkEnemy(game, pos5));
            Assert.False(knight.checkEnemy(game, pos6));
            Assert.False(knight.checkEnemy(game, pos7));
        }
    }

}
