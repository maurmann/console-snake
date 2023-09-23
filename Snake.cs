namespace Snake
{
    public class Snake
    {
        public int CurrentX { get; private set; }
        public int CurrentY { get; private set; }

        public const char HeadUp = '^';
        public const char HeadDown = 'V';
        public const char HeadLeft = '<';
        public const char HeadRight = '>';

        public const char Tail = '*';

        public char Head { get; set; }

        public List<Coordinate> TailNodes { get; set; }

        public Snake()
        {
            CurrentX = 4;
            CurrentY = 6;
            Head = HeadRight;
            TailNodes = new List<Coordinate>();
        }

        public void Move(ConsoleKey pressedKey)
        {
            MoveTail();

            EraseHead();

            switch (pressedKey)
            {
                case ConsoleKey.LeftArrow:
                    MoveLeft();
                    break;
                case ConsoleKey.RightArrow:
                    MoveRight();
                    break;
                case ConsoleKey.UpArrow:
                    MoveUp();
                    break;
                case ConsoleKey.DownArrow:
                    MoveDown();
                    break;
            }

            DrawHead();
        }

        public void Eat()
        {
            TailNodes.Add(new Coordinate(0, 0));
        }

        private void MoveLeft()
        {
            CurrentX--;
            Head = HeadLeft;
        }

        private void MoveRight()
        {
            CurrentX++;
            Head = HeadRight;
        }

        private void MoveUp()
        {
            CurrentY--;
            Head = HeadUp;
        }

        private void MoveDown()
        {
            CurrentY++;
            Head = HeadDown;
        }

        private void EraseHead()
        {
            if (TailNodes.Any())
                return;

            Console.SetCursorPosition(CurrentX, CurrentY);
            Console.Write(" ");
        }

        private void DrawHead()
        {
            Console.SetCursorPosition(CurrentX, CurrentY);
            Console.Write(Head);
        }

        private void MoveTail()
        {
            if (!TailNodes.Any())
                return;

            EraseLastTailNode();

            for (int i = TailNodes.Count - 1; i > 0; i--)
            {
                TailNodes[i] = TailNodes[i - 1];
                continue;
            }

            TailNodes[0] = new Coordinate(CurrentX, CurrentY);

            DrawTail();
        }

        private void EraseLastTailNode()
        {
            if (!TailNodes.Any())
                return;

            var lastTailNode = TailNodes.Last();
            Console.SetCursorPosition(lastTailNode.X, lastTailNode.Y);
            Console.Write(" ");
        }

        private void DrawTail()
        {
            if (!TailNodes.Any())
                return;

            foreach (var node in TailNodes)
            {
                Console.SetCursorPosition(node.X, node.Y);
                Console.Write(Tail);
            }
        }

        public bool DetectHit(int x, int y)
        {
            foreach (var tailNode in TailNodes)
            {
                if (tailNode.X == x && tailNode.Y == y)
                    return true;
            }
            return false;
        }

        public bool DetectConflict(Coordinate? coordinate)
        {
            if (coordinate == null)
                return false;

            foreach (var node in TailNodes)
                if (node.X == coordinate.X && node.Y == coordinate.Y)
                    return true;

            if (CurrentX == coordinate.X && CurrentY == coordinate.Y)
                return true;

            return false;
        }
    }
}