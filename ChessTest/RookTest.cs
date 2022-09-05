using Chess;

namespace ChessTest
{
    public class RookTest
    {

        Rook rook;
        King king;
        Game game;
        Board bd;
        HashSet<ChessPiece> white;
        HashSet<ChessPiece> black;

        [SetUp]
        public void Setup()
        {
            rook = new Rook("white");
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
            placeOnBoard(rook, 5, 4);
            placeOnBoard(king, 4, 3);
            white.Add(rook);
            white.Add(king);
            setGame();
            HashSet<Position> pm = rook.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
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
            placeOnBoard(rook, 1, 6);
            placeOnBoard(king, 2, 5);
            white.Add(rook);
            white.Add(king);
            setGame();
            HashSet<Position> pm = rook.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
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
            placeOnBoard(rook, 1, 1);
            placeOnBoard(king, 2, 2);
            white.Add(rook);
            white.Add(king);
            setGame();
            HashSet<Position> pm = rook.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
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
            placeOnBoard(rook, 5, 4);
            placeOnBoard(king, 4, 3);
            Bishop bishop1 = new Bishop("white");
            Bishop bishop2 = new Bishop("white");
            placeOnBoard(bishop1, 5, 2);
            placeOnBoard(bishop2, 8, 4);
            white.Add(rook);
            white.Add(king);
            white.Add(bishop1);
            white.Add(bishop2);
            setGame();
            HashSet<Position> pm = rook.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
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
            placeOnBoard(rook, 5, 4);
            placeOnBoard(king, 4, 5);
            Bishop bishop1 = new Bishop("black");
            Bishop bishop2 = new Bishop("black");
            placeOnBoard(bishop1, 5, 2);
            placeOnBoard(bishop2, 8, 4);
            white.Add(rook);
            white.Add(king);
            black.Add(bishop1);
            black.Add(bishop2);
            setGame();
            HashSet<Position> pm = rook.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
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
        public void testPossibleMovesKingUnderAttack()
        {
            placeOnBoard(rook, 5, 4);
            Queen queen = new Queen("black");
            placeOnBoard(king, 3, 6);
            placeOnBoard(queen, 3, 2);
            white.Add(rook);
            white.Add(king);
            black.Add(queen);
            setGame();
            HashSet<Position> pm = rook.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(3, 4));
            setEquals(pm, expected);
        }

        [Test]
        public void testPossibleMovesKingWillBeUnderAttack()
        {
            placeOnBoard(rook, 3, 5);
            Queen queen = new Queen("black");
            placeOnBoard(king, 3, 6);
            placeOnBoard(queen, 3, 2);
            white.Add(rook);
            white.Add(king);
            black.Add(queen);
            setGame();
            HashSet<Position> pm = rook.possibleMoves(game);
            HashSet<Position> expected = new HashSet<Position>();
            expected.Add(new Position(3, 4));
            expected.Add(new Position(3, 3));
            expected.Add(new Position(3, 2));
            setEquals(pm, expected);
        }
    }
}
