using Cysharp.Threading.Tasks;
using Infrastructure;

namespace Game.Conveyor
{
    public class LoseState : IConveyorGameState
    {
        private readonly ILog _log;
        public LoseState(ILog log)
        {
            _log = log;
        }

        public UniTask Enter()
        {
            _log.Log("Lose State Enter()");
            return default;
        }

        public UniTask Exit() => default;
    }
}