using Snake;

var menu = new Menu();
menu.Display();

while (!menu.WasQuitKeyPressed)
{
    if (menu.WasNewGameKeyPressed)
    {
        var game = new Game();
        game.Run();
    }
    menu.Display();
}