using UnityEngine;

namespace Infrastructure
{
    public class CameraService : ICameraService
    {
        // TODO: cache
        public Camera MainCamera => Camera.main;
    }
}