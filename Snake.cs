namespace Snake
{
    public class Snake
    {
        public Coordinate Coordinate { get; set; }

        public bool MovingRight => Head == Snake.HeadRight;
        public bool MovingLeft => Head == Snake.HeadLeft;
        public bool MovingUp => Head == Snake.HeadUp;
        public bool MovingDown => Head == Snake.HeadDown;

        public const char HeadUp = '^';
        public const char HeadDown = 'V';
        public const char HeadLeft = '<';
        public const char HeadRight = '>';

        public const char Tail = '0';

        public char Head { get; set; }

        public List<Coordinate> TailNodes { get; set; }

        public ConsoleKey LastValidArrowKey { get; set; }

        public Snake()
        {
            Coordinate = new Coordinate(4, 6);
            Head = HeadRight;
            TailNodes = new List<Coordinate>();
            LastValidArrowKey = ConsoleKey.RightArrow;
        }

        public void Move(ConsoleKey pressedKey)
        {
            pressedKey = Run(pressedKey);

            MoveTail();

            EraseHead();

            switch (pressedKey)
            {
                case ConsoleKey.LeftArrow:
                    MoveLeft();
                    LastValidArrowKey = ConsoleKey.LeftArrow;
                    break;
                case ConsoleKey.RightArrow:
                    MoveRight();
                    LastValidArrowKey = ConsoleKey.RightArrow;
                    break;
                case ConsoleKey.UpArrow:
                    MoveUp();
                    LastValidArrowKey = ConsoleKey.UpArrow;
                    break;
                case ConsoleKey.DownArrow:
                    MoveDown();
                    LastValidArrowKey = ConsoleKey.DownArrow;
                    break;
            }

            DrawHead();
        }

        public void Eat()
        {
            TailNodes.Add(new Coordinate(0, 0));
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

            if (Coordinate.X == coordinate.X && Coordinate.Y == coordinate.Y)
                return true;

            return false;
        }

        private void MoveLeft()
        {
            Coordinate.X--;
            Head = HeadLeft;
        }

        private void MoveRight()
        {
            Coordinate.X++;
            Head = HeadRight;
        }

        private void MoveUp()
        {
            Coordinate.Y--;
            Head = HeadUp;
        }

        private void MoveDown()
        {
            Coordinate.Y++;
            Head = HeadDown;
        }

        private ConsoleKey Run(ConsoleKey pressedKey)
        {
            if (pressedKey == ConsoleKey.R)
                return LastValidArrowKey;

            return pressedKey;
        }

        private void EraseHead()
        {
            if (TailNodes.Any())
                return;

            Console.SetCursorPosition(Coordinate.X, Coordinate.Y);
            Console.Write(" ");
        }

        private void DrawHead()
        {
            Console.SetCursorPosition(Coordinate.X, Coordinate.Y);
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

            TailNodes[0] = new Coordinate(Coordinate.X, Coordinate.Y);

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
    }
}