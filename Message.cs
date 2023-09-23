namespace Snake
{
    public class Message
    {
        public const int topX = 2;
        public const int topY = 1;

        public const int bottomX = 2;
        public const int bottomY = 15;

        public void Write(string message, MessageLocation messageLocation = MessageLocation.Top)
        {
            SetCursor(messageLocation);
            Console.Write(message);
        }

        private void SetCursor(MessageLocation messageLocation)
        {
            if (messageLocation == MessageLocation.Top)
                SetCursorToTop();
            else if (messageLocation == MessageLocation.Bottom)
                SetCursorToBottom();
        }

        private void SetCursorToTop()
        {
            Console.SetCursorPosition(topX, topY);
        }

        private void SetCursorToBottom()
        {
            Console.SetCursorPosition(bottomX, bottomY);
        }
    }

    public enum MessageLocation
    {
        Top = 0,
        Bottom = 1
    }
}
