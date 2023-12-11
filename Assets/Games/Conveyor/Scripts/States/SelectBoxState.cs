using Cysharp.Threading.Tasks;
using Infrastructure;

namespace Game.Conveyor
{
    public class SelectBoxState : IConveyorGameState
    {
        private readonly ILog _log;
        public SelectBoxState(ILog log)
        {
            _log = log;
        }

        public UniTask Enter()
        {
            _log.Log("Select Box State Enter()");
            return default;
        }

        public UniTask Exit() => default;
    }
}