using Cysharp.Threading.Tasks;
using Infrastructure;

namespace Game.Conveyor
{
    public class BootstrapState : IConveyorGameState
    {
        private readonly ConveyorGameStateMachine _stateMachine;
        private readonly ILog _log;

        protected BootstrapState(ConveyorGameStateMachine stateMachine, ILog log)
        {
            _stateMachine = stateMachine;
            _log = log;
        }
        
        public UniTask Enter()
        {
            _log.Log("Bootstrap State Enter()");
            return _stateMachine.Enter<NextItemDeliveryState>();
        }

        public UniTask Exit() => default;
    }
}