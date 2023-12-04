using UnityEngine;

namespace Infrastructure
{
    public interface ICameraService
    {
        Camera MainCamera { get; }
    }
}