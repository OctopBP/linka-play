using UnityEngine;

namespace Infrastructure
{
    public class UnityLogger : ILog
    {
        public void Log(object message) => Log(message, LogTag.Default);
        public void LogWarning(object message) => LogWarning(message, LogTag.Default);
        public void LogError(object message) => LogError(message, LogTag.Default);
        public void Log(object message, LogTag tag) => Debug.unityLogger.Log(LogType.Log, tag.ToString(), message);
        public void LogWarning(object message, LogTag tag) => Debug.unityLogger.Log(LogType.Warning, tag.ToString(), message);
        public void LogError(object message, LogTag tag) => Debug.unityLogger.Log(LogType.Error, tag.ToString(), message);
    }
}