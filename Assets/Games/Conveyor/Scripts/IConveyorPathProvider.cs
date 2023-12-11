using UnityEngine;

namespace Game.Conveyor
{
    public interface IConveyorPathProvider
    {
        Vector3 GetPoint();
    }
}