using UnityEngine;

namespace Game.Conveyor
{
    public partial class ConveyorPath : MonoBehaviour
    {
        [SerializeField, PublicAccessor] private Vector3 spawnPoint, stopPoint;
        [SerializeField, PublicAccessor] private Vector3 leftPointUp, leftPointDown;
        [SerializeField, PublicAccessor] private Vector3 rightPointUp, rightPointDown;

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(spawnPoint, stopPoint);
            Gizmos.DrawLine(stopPoint, leftPointUp);
            Gizmos.DrawLine(stopPoint, rightPointUp);
            Gizmos.DrawLine(leftPointUp, leftPointDown);
            Gizmos.DrawLine(rightPointUp, rightPointDown);
            
            const float sphereSize = 0.075f;
            Gizmos.DrawSphere(spawnPoint, sphereSize);
            Gizmos.DrawSphere(stopPoint, sphereSize);
            Gizmos.DrawSphere(leftPointUp, sphereSize);
            Gizmos.DrawSphere(leftPointDown, sphereSize);
            Gizmos.DrawSphere(rightPointUp, sphereSize);
            Gizmos.DrawSphere(rightPointDown, sphereSize);
        }
    }
}
