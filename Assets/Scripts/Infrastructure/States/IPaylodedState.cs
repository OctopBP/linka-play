using Cysharp.Threading.Tasks;

namespace Infrastructure.States
{
	public interface IPaylodedState<in TPayload> : IExitableState
	{
		UniTask Enter(TPayload payload);
	}
}