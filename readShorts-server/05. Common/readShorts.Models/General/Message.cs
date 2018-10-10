using System.Collections.Generic;

namespace readShorts.Models
{
    public class Message
    {
        public LogLevel LogLevel { get; set; }

        public string Text { get; set; }

        public Message(LogLevel logLevel, string textMsg)
        {
            LogLevel = logLevel;
            Text = textMsg;
        }

        public static bool IsErrorOccured(IEnumerable<Message> msgs)
        {
            foreach (Models.Message item in msgs)
            {
                if (item.LogLevel == Models.LogLevel.Error)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public enum LogLevel
    {
        Info = 0,
        Warning = 1,
        Error = 2,
        Fatal = 3
    }
}