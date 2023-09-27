namespace Snake
{
    public class Command
    {
        private readonly Message message;
        private const string commandTemplate = "[ESC]=Exit {0}";
        private const string runTemplate = "[R]=Run";

        public Command()
        {
            message = new Message();
        }

        public void DisplayCommands(bool isRunEnabled)
        {
            var text = isRunEnabled ? 
                string.Format(commandTemplate, runTemplate) : 
                string.Format(commandTemplate, new string(' ', 10));

            message.Write(text, MessageLocation.Bottom);
        }
    }
}
