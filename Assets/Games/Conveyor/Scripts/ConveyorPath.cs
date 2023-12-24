using System;
using UnityEngine;

namespace Game.Conveyor
{
    public partial class ConveyorPath : MonoBehaviour
    {
        [SerializeField, PublicAccessor] private Vector3 spawnPoint, stopPoint;
        [SerializeField, PublicAccessor] private Vector3 leftPointUp, leftPointDown;
        [SerializeField, PublicAccessor] private Vector3 rightPointUp, rightPointDown;

        private Vector3[] _leftPoints;
        private Vector3[] _rightPoints;

        private void Awake()
        {
            _leftPoints = new [] { leftPointUp, leftPointDown };
            _rightPoints = new [] { rightPointUp, rightPointDown };
        }

        public Vector3[] GetSidePoints(BoxPlace at) => at switch
        {
            BoxPlace.Left => _leftPoints,
            BoxPlace.Right => _rightPoints,
            _ => throw new ArgumentOutOfRangeException(nameof(at), at, null)
        };

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
