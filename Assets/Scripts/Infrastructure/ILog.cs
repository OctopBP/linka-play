using Extensions;
using UnityEngine;

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
        public static readonly LogTag Item = new LogTag("Item", Color.cyan);
        
        private readonly string _tagName;
        private readonly Color _color;

        public LogTag(string tagName)
        {
            _tagName = tagName;
            _color = Color.gray;
        }
        
        public LogTag(string tagName, Color color)
        {
            _tagName = tagName;
            _color = color;
        }

        public override string ToString() => $"[{_tagName}]".WrapInColorTag(_color).WrapInBoldTag();
    }
}