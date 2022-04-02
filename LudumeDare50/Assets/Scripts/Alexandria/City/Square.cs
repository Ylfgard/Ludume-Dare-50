using UnityEngine;
using Protesters;

namespace City
{
    public delegate void OnSquare(Collider collider, Square square);

    public class Square : MonoBehaviour
    {
        public event OnSquare EnterSquare;
        public event OnSquare LeaveSquare;
        [SerializeField]
        private Transform _transform;
        public ProtestWarning Miting;
        public Vector3 Center => _transform.position;

        private void OnTriggerEnter(Collider other)
        {
            EnterSquare?.Invoke(other, this);
        }

        private void OnTriggerExit(Collider other)
        {
            LeaveSquare?.Invoke(other, this);
        }

        public void EndMiting()
        {
            Miting = null;
        }
    }
}