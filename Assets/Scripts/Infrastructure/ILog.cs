namespace Infrastructure
{
    public interface ILog
    {
        void Log(object message);
        void LogWarning(object message);
        void LogError(object message);
        void Log(object message, LogTag tag);
        void LogWarning(object message, LogTag tag);
        void LogError(object message, LogTag tag);
    }

    public class LogTag
    {
        public static readonly LogTag Default = new LogTag("Default");
        private readonly string _tagName;

        public LogTag(string tagName)
        {
            _tagName = tagName;
        }

        public override string ToString() => $"[${_tagName}]";
    }
}