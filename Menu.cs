namespace Snake
{
    public class Menu
    {
        public char Display()
        {
            Title();
            SubTitle();
            var option = ReadOption();

            return option;
        }

        private void Title()
        {
            string[] title = new string[6];

            title[0] = @"   _____             _         ";
            title[1] = @"  / ____|           | |        ";
            title[2] = @" | (___  _ __   __ _| | _____  ";
            title[3] = @"  \___ \| '_ \ / _` | |/ / _ \ ";
            title[4] = @"  ____) | | | | (_| |   <  __/ ";
            title[5] = @" |_____/|_| |_|\__,_|_|\_\___| ";

            Console.Clear();
            for (int i = 0; i < title.Length; i++)
            {
                Console.WriteLine(title[i]);
            }
        }

        private void SubTitle()
        {
            Console.SetCursorPosition(2, 8);
            Console.Write("N = New Game");
            Console.SetCursorPosition(2, 9);
            Console.WriteLine("Q = Quit");
        }

        private char ReadOption()
        {
            var pressedKey = ReadKey();
            while (pressedKey.Key != ConsoleKey.N && pressedKey.Key != ConsoleKey.Q)
            {
                Console.Beep();
                pressedKey = ReadKey();
            }

            return pressedKey.KeyChar;            
        }

        private ConsoleKeyInfo ReadKey()
        {
            Console.SetCursorPosition(2, 10);
            Console.Write("? ");
            return Console.ReadKey(false);
        }
    }
}
