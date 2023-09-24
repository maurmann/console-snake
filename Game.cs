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
            Console.Clear();
        }

        public void Run()
        {
            InitializeGame();

            var board = new Board();
            board.Draw();

            var snake = new Snake();

            var food = new Food();

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
                        IncrementScore(food.GetFoodPoints());
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
            Console.Beep(440, 400);
            Console.Beep(540, 400);
            Console.Beep(640, 400);

            Console.CursorVisible = true;
        }

        private void InitializeGame()
        {
            Console.CursorVisible = false;
            Points = 0;
            DisplayScore();
        }

        private void IncrementScore(int increment)
        {
            Points += increment;
            DisplayScore();
        }

        private void DisplayScore()
        {
            Message message = new Message();
            message.Write($"[ESC=Exit] - Points: {Points}");
        }
    }
}