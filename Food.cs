namespace Snake
{
    public class Food
    {
        private char[] FoodTypes => new char[3] { '$', '#', '@' };

        private int[] FoodPoints => new int[3] { 10, 50, 100 };

        private char FoodType { get; set; }

        public Food()
        {
            FoodEaten = true;
            FoodCoordinate = new Coordinate(0, 0);
        }

        public bool FoodEaten { get; set; }
        public Coordinate FoodCoordinate { get; set; }

        public Coordinate? Create()
        {
            if (!FoodEaten)
                return null;

            int startX = Board.LeftMargin + 1;
            int endX = Board.LeftMargin + Board.Width;

            int startY = Board.TopMargin + 1;
            int endY = Board.TopMargin + Board.Height;

            Random random = new Random();
            var x = random.Next(startX, endX);
            var y = random.Next(startY, endY);
            var foodTypeIndex = random.Next(0, FoodTypes.Length);

            FoodType = FoodTypes[foodTypeIndex];

            SaveFoodCoordinate(x, y);

            FoodEaten = false;

            Draw();

            return new Coordinate(x, y);
        }

        public void Draw()
        {
            Console.SetCursorPosition(FoodCoordinate.X, FoodCoordinate.Y);
            Console.Write(FoodType);
        }

        public bool Find(int x, int y)
        {
            if (FoodCoordinate.X == x 
                && FoodCoordinate.Y == y)
            {
                FoodEaten = true;
            }
            return FoodEaten;
        }

        public int GetFoodPoints()
        {
            for (int i = 0; i < FoodTypes.Length; i++)
                if (FoodTypes[i] == FoodType)
                    return FoodPoints[i];

            return 0;
        }

        private void SaveFoodCoordinate(int x, int y)
        {
            FoodCoordinate = new Coordinate(x, y);
        }
    }
}
