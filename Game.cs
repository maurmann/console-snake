namespace Snake
{
    public class Game
    {
        private const int HorizontalDelayMs = 170;
        private const int VerticalDelayMs = 230;
        private const int HorizontalRunningDelayMs = 10;
        private const int VerticalRunningDelayMs = 20;

        private const ConsoleKey StartKey = ConsoleKey.RightArrow;

        private int Points { get; set; }

        private readonly Command command;
        private readonly Board board;
        private readonly Snake snake;
        private readonly Food food;

        public Game()
        {
            command = new Command();
            board = new Board();
            snake = new Snake();
            food = new Food();

            Points = 0;

            Console.Clear();
        }

        public void Run()
        {
            InitializeGame();

            board.Draw();

            var colisionDetected = false;

            var pressedKey = StartKey;
            while (pressedKey != ConsoleKey.Escape && !colisionDetected)
            {
                while (!Console.KeyAvailable)
                {
                    var canRun = CanRunToFood(snake, food);

                    command.DisplayCommands(canRun);

                    var delay = SetDelay(snake, canRun, pressedKey);

                    Thread.Sleep(delay);

                    var foodCoordinate = food.Create();
                    while (snake.DetectConflict(foodCoordinate))
                        foodCoordinate = food.Create();

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

        private int SetDelay(Snake snake, bool canRun, ConsoleKey pressedKey)
        {
            if (canRun && pressedKey == ConsoleKey.R)
                return IsMovingVertical(snake) ? VerticalRunningDelayMs : HorizontalRunningDelayMs;

            return IsMovingVertical(snake) ? VerticalDelayMs : HorizontalDelayMs;
        }

        private bool IsMovingVertical(Snake snake)
        {
            if (snake.Head == Snake.HeadUp || snake.Head == Snake.HeadDown)
                return true;

            return false;
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
            message.Write($"~~~ Console Snake ~~~  Points: {Points}");
        }

        private bool CanRunToFood(Snake snake, Food food)
        {
            if (snake.MovingRight && AreSnakeAndFoodSameLine(snake, food) && snake.CurrentX < food.FoodCoordinate.X)
                return true;
            else if (snake.MovingLeft && AreSnakeAndFoodSameLine(snake, food) && snake.CurrentX > food.FoodCoordinate.X)
                return true;
            else if (snake.MovingDown && AreSnakeAndFoodSameColumn(snake, food) && snake.CurrentY < food.FoodCoordinate.Y)
                return true;
            else if (snake.MovingUp && AreSnakeAndFoodSameColumn(snake, food) && snake.CurrentY > food.FoodCoordinate.Y)
                return true;

            return false;
        }

        private bool AreSnakeAndFoodSameLine(Snake snake, Food food)
        {
            return snake.CurrentY == food.FoodCoordinate.Y;
        }

        private bool AreSnakeAndFoodSameColumn(Snake snake, Food food)
        {
            return snake.CurrentX == food.FoodCoordinate.X;
        }
    }
}