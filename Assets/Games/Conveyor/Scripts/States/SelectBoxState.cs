using Cysharp.Threading.Tasks;
using Infrastructure;
using Infrastructure.Input;

namespace Game.Conveyor
{
    public class SelectBoxState : IConveyorGameState
    {
        private readonly ILog _log;
        private readonly ConveyorPath _conveyorPath;
        private IInput _input;

        public SelectBoxState(ILog log, ConveyorPath conveyorPath, IInput input)
        {
            _log = log;
            _conveyorPath = conveyorPath;
            _input = input;
        }

        public UniTask Enter()
        {
            _log.Log("Select Box State Enter()");
            
            _input.Enabled.Value = true;
            
            return default;
        }

        public UniTask Exit() => default;
    }
}