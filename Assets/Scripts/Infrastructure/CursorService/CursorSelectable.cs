using System.Linq;
using LanguageExt;
using UnityEngine;
using Zenject;

namespace Infrastructure.CursorService
{
    public class CursorSelectable : MonoBehaviour, ICursorSelectable
    {
        [SerializeField] private Collider collider;

        private readonly Vector3[] _boundsScreenPoints = new Vector3[8];
        private ICameraService _cameraService;
        
        [Inject]
        private void Construct(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        public Option<Bounds> MaybeBounds
        {
            get
            {
                var bounds = collider.bounds.size;

                // Calculate and set ScreenPoint for each corner of the bounding box
                SetScreenPoint(index: 0, x: 0,        y: 0,        z: 0       );
                SetScreenPoint(index: 1, x: 0,        y: 0,        z: bounds.z);
                SetScreenPoint(index: 2, x: 0,        y: bounds.y, z: 0       );
                SetScreenPoint(index: 3, x: 0,        y: bounds.y, z: bounds.z);
                SetScreenPoint(index: 4, x: bounds.x, y: 0,        z: 0       );
                SetScreenPoint(index: 5, x: bounds.x, y: 0,        z: bounds.z);
                SetScreenPoint(index: 6, x: bounds.x, y: bounds.y, z: 0       );
                SetScreenPoint(index: 7, x: bounds.x, y: bounds.y, z: bounds.z);
                
                var maxX = _boundsScreenPoints.Select(p => p.x).Max();
                var maxY = _boundsScreenPoints.Select(p => p.y).Max();
                var minX = _boundsScreenPoints.Select(p => p.x).Min();
                var minY = _boundsScreenPoints.Select(p => p.y).Min();

                var min = new Vector3(minX, minY);
                var max = new Vector3(maxX, maxY);
				
                var newBounds = new Bounds((min + max) * 0.5f, (max - min) * 2);
				
                return newBounds;
                
                void SetScreenPoint(int index, float x, float y, float z)
                {
                    var offset = collider.bounds.center - collider.bounds.size * 0.5f;
                    var mainCamera = _cameraService.MainCamera;
                    _boundsScreenPoints[index] = mainCamera.WorldToScreenPoint(offset + new Vector3(x, y, z));
                }
            }
        }
        
        public void OnHover() { }
        public void OnSelect() { }
    }
}