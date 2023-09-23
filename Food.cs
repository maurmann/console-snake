namespace Snake
{
    public class Food
    {
        private const char food = '$';

        public Food()
        {
            FoodEaten = true;
            FoodCoordinate = new Coordinate(0, 0);
        }

        public bool FoodEaten { get; set; }
        public Coordinate FoodCoordinate { get; set; }


        public Coordinate? Harvest()
        {
            if (!FoodEaten)
                return null;

            int startX = Board.LeftMargin + 1;
            int endX = Board.LeftMargin + Board.Width - 1;

            int startY = Board.TopMargin + 1;
            int endY = Board.TopMargin + Board.Height - 1;

            Random random = new Random();
            var x = random.Next(startX, endX);
            var y = random.Next(startY, endY);

            SaveFoodCoordinate(x, y);

            FoodEaten = false;

            Draw();

            return new Coordinate(x, y);
        }

        public void Draw()
        {
            Console.SetCursorPosition(FoodCoordinate.X, FoodCoordinate.Y);
            Console.Write(food);
        }

        public bool Find(int x, int y)
        {
            if (FoodCoordinate.X == x && FoodCoordinate.Y == y)
            {
                FoodEaten = true;
            }
            return FoodEaten;
        }

        private void SaveFoodCoordinate(int x, int y)
        {
            FoodCoordinate = new Coordinate(x, y);
        }

    }
}
