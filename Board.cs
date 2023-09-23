namespace Snake
{
    public class Board
    {
        public const int LeftMargin = 2;
        public const int TopMargin = 2;

        public const int Width = 60;
        public const int Height = 12;

        public void Draw()
        {
            DrawHorizontalLine(LeftMargin, TopMargin);
            DrawHorizontalLine(LeftMargin, TopMargin + Height);
            DrawVerticalLine(LeftMargin, TopMargin + 1);
            DrawVerticalLine(LeftMargin + Width + 1, TopMargin + 1);
        }

        private void DrawHorizontalLine(int startX, int startY)
        {
            Console.SetCursorPosition(startX, startY);
            Console.Write("+");
            for (int i = 0; i < Board.Width; i++)
            {
                Console.Write("-");
            }
            Console.Write("+");
            Console.Write("\n");
        }

        private void DrawVerticalLine(int startX, int startY)
        {
            var yPoint = startY;
            for (int i = 0; i < Height - 1; i++)
            {
                Console.SetCursorPosition(startX, yPoint);
                Console.Write("|");
                yPoint++;
            }
        }

        public bool DetectHit(int x, int y)
        {
            if (x <= LeftMargin)
                return true;

            if (x >= LeftMargin + Width + 1)
                return true;

            if (y <= TopMargin)
                return true;

            if (y >= TopMargin + Height)
                return true;

            return false;
        }
    }
}
