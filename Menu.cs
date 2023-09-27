namespace Snake
{
    public class Menu
    {
        public bool WasQuitKeyPressed 
            => PressedKey.Key == ConsoleKey.Q;

        public bool WasNewGameKeyPressed 
            => PressedKey.Key == ConsoleKey.N;

        private ConsoleKeyInfo PressedKey { get; set; }

        public void Display()
        {
            Title();
            SubTitle();
            ReadOption();
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
            Console.Write("[N]ew Game");
            Console.SetCursorPosition(2, 9);
            Console.WriteLine("[Q]uit");
        }

        private void ReadOption()
        {
            PressedKey = ReadKey();
            while (PressedKey.Key != ConsoleKey.N 
                && PressedKey.Key != ConsoleKey.Q)
            {
                Console.Beep();
                PressedKey = ReadKey();
            }
        }

        private ConsoleKeyInfo ReadKey()
        {
            Console.SetCursorPosition(2, 10);
            Console.Write("? ");
            return Console.ReadKey(false);
        }
    }
}
