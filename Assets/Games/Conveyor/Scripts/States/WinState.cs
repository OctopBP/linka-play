using Cysharp.Threading.Tasks;
using Infrastructure;

namespace Game.Conveyor
{
    public class WinState : IConveyorGameState
    {
        private readonly ILog _log;
        public WinState(ILog log)
        {
            _log = log;
        }
        
        public UniTask Enter()
        {
            _log.Log("Win State Enter()");
            return default;
        }

        public UniTask Exit() => default;
    }
}