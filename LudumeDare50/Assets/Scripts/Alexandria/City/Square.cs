using UnityEngine;
using System.Collections.Generic;
using Protesters;
using Police;

namespace City
{
    public delegate void OnSquare(Collider collider, Square square);

    public class Square : MonoBehaviour
    {
        public event OnSquare EnterSquare;
        public event OnSquare LeaveSquare;
        [SerializeField]
        private Transform _transform;
        private ProtestWarning _miting;
        private List<AvtozakBehavior> _avtozaksOnSquare = new List<AvtozakBehavior>();

        public Vector3 Center => _transform.position;
        public ProtestWarning Miting => _miting;

        private void OnTriggerEnter(Collider other)
        {
            EnterSquare?.Invoke(other, this);
            var avtozak = other.GetComponent<AvtozakBehavior>();
            if(avtozak == null) return;
            _avtozaksOnSquare.Add(avtozak);
        }

        private void OnTriggerExit(Collider other)
        {
            LeaveSquare?.Invoke(other, this);
            var avtozak = other.GetComponent<AvtozakBehavior>();
            if(avtozak == null) return;
            _avtozaksOnSquare.Remove(avtozak);
        }

        public void EndMiting()
        {
            _miting = null;
        }

        public void StartMiting(ProtestWarning miting)
        {
            _miting = miting;
        }
    }
}