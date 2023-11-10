using UnityEngine;

namespace Game.Conveyor
{
    public partial class ConveyorPath : MonoBehaviour
    {
        [SerializeField, PublicAccessor] Vector3 spawnPoint, stopPoint;
        [SerializeField, PublicAccessor] Vector3 leftPointUp, leftPointDown;
        [SerializeField, PublicAccessor] Vector3 rightPointUp, rightPointDown;

        void OnDrawGizmos()
        {
            Gizmos.DrawLine(spawnPoint, stopPoint);
            Gizmos.DrawLine(stopPoint, leftPointUp);
            Gizmos.DrawLine(stopPoint, rightPointUp);
            Gizmos.DrawLine(leftPointUp, leftPointDown);
            Gizmos.DrawLine(rightPointUp, rightPointDown);
            
            Gizmos.DrawSphere(spawnPoint, 0.1f);
            Gizmos.DrawSphere(stopPoint, 0.1f);
            Gizmos.DrawSphere(leftPointUp, 0.1f);
            Gizmos.DrawSphere(leftPointDown, 0.1f);
            Gizmos.DrawSphere(rightPointUp, 0.1f);
            Gizmos.DrawSphere(rightPointDown, 0.1f);
        }
    }
}
