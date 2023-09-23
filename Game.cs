namespace Snake
{
    public class Game
    {
        private const int HorizontalDelayMs = 180;
        private const int VerticalDelayMs = 240;
        private const ConsoleKey StartKey = ConsoleKey.RightArrow;

        private int Points { get; set; }

        public Game()
        {
            Points = 0;
        }

        public void Run()
        {
            InitializeGame();

            Board board = new Board();
            board.Draw();

            Snake snake = new Snake();

            Food food = new Food();

            var colisionDetected = false;

            var pressedKey = StartKey;
            while (pressedKey != ConsoleKey.Escape && !colisionDetected)
            {
                while (!Console.KeyAvailable)
                {
                    Thread.Sleep(GetDelay(snake));

                    var foodCoordinate = food.Harvest();
                    while (snake.DetectConflict(foodCoordinate))
                        foodCoordinate = food.Harvest();

                    food.Draw();
                    snake.Move(pressedKey);

                    if (food.Find(snake.CurrentX, snake.CurrentY))
                    {
                        snake.Eat();
                        RefreshScore(1);
                    }

                    if (board.DetectHit(snake.CurrentX, snake.CurrentY)
                        || snake.DetectHit(snake.CurrentX, snake.CurrentY))
                    {
                        colisionDetected = true;
                        break;
                    }
                }

                if (colisionDetected)
                    break;

                pressedKey = Console.ReadKey(true).Key;
            }
            GameOver();
        }

        private int GetDelay(Snake snake)
        {
            if (snake.Head == Snake.HeadUp || snake.Head == Snake.HeadDown)
                return VerticalDelayMs;

            return HorizontalDelayMs;
        }

        private void GameOver()
        {
            Message message = new Message();
            message.Write("Game Over!", MessageLocation.Bottom);
            Console.CursorVisible = true;
        }

        private void InitializeGame()
        {
            Console.CursorVisible = false;
            RefreshScore(0);
        }

        private void RefreshScore(int increaseValue)
        {
            Points += increaseValue;
            Message message = new Message();
            message.Write($"[ESC=Exit] - Points: {Points}");
        }
    }
}