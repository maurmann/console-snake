using Snake ;

Menu menu = new Menu();

var option = menu.Display();

while (!option.ToString().Equals("Q", StringComparison.InvariantCultureIgnoreCase))
{
    if (option.ToString().Equals("N", StringComparison.InvariantCultureIgnoreCase))
    {
        Game game = new Game();
        game.Run();
    }

    option = menu.Display();
}